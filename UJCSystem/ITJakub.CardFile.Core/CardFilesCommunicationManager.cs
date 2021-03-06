﻿using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security.Principal;
using System.ServiceModel;
using ITJakub.CardFile.Core.DataContractEntities;
using log4net;
using Vokabular.Shared.Options;

namespace ITJakub.CardFile.Core
{
    public class CardFilesCommunicationManager : IDisposable
    {
        private static readonly ILog m_log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private CardFilesServiceClient m_serviceClient;

        public CardFilesCommunicationManager()
        {
            if (m_log.IsDebugEnabled)
                m_log.DebugFormat("Creating CardFiles client");

            m_serviceClient = new CardFilesServiceClient();

            if (m_serviceClient.ClientCredentials != null)
            {
                m_serviceClient.ClientCredentials.HttpDigest.ClientCredential = new NetworkCredential(UserName, UserPassword);

                m_serviceClient.ClientCredentials.HttpDigest.AllowedImpersonationLevel = TokenImpersonationLevel.Impersonation;
            }
            else
            {
                const string message = "Service client credentials not initialized";

                if (m_log.IsFatalEnabled)
                    m_log.FatalFormat(message);

                throw new InvalidDataException(message);
            }
        }

        private string UserName => ConfigurationManager.AppSettings[SettingKeys.CardFilesUser] ??
                                   throw new ArgumentException("Card files username not found");

        private string UserPassword => ConfigurationManager.AppSettings[SettingKeys.CardFilesPassword] ??
                                       throw new ArgumentException("Card files password not found");

        public files GetFiles()
        {
            return m_serviceClient.GetFiles();
        }

        public buckets GetBuckets(string fileId)
        {
            return m_serviceClient.GetBuckets(fileId);
        }

        public buckets GetBucketsByHeadword(string fileId, string headword)
        {
            return m_serviceClient.GetBucketsByHeadword(fileId, headword);
        }

        public buckets GetCardsFromBucket(string fileId, string bucketId)
        {
            return m_serviceClient.GetCardsFromBucket(fileId, bucketId);
        }

        public card GetCardFromBucket(string fileId, string bucketId, string cardId)
        {
            return m_serviceClient.GetCardFromBucket(fileId, bucketId, cardId);
        }

        public Stream GetImageForCard(string fileId, string bucketId, string cardId, string imageId, string imageSize)
        {
            return m_serviceClient.GetImageForCard(fileId, bucketId, cardId, imageId, imageSize);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~CardFilesCommunicationManager() 
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                if (m_serviceClient != null)
                {
                    if (m_serviceClient.State == CommunicationState.Faulted)
                    {
                        m_serviceClient.Abort();
                    }
                    else
                    {
                        m_serviceClient.Close();
                    }
                    m_serviceClient = null;
                }
            }
        }
    }
}