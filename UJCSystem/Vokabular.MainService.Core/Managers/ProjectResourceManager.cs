﻿using System.IO;
using ITJakub.Shared.Contracts.Resources;
using Vokabular.MainService.Core.Communication;

namespace Vokabular.MainService.Core.Managers
{
    public class ProjectResourceManager
    {
        private readonly CommunicationProvider m_communicationProvider;

        public ProjectResourceManager(CommunicationProvider communicationProvider)
        {
            m_communicationProvider = communicationProvider;
        }

        public void UploadResource(string sessionId, Stream data, string fileName)
        {
            using (var client = m_communicationProvider.GetFileProcessingClient())
            {
                var resourceInfo = new UploadResourceContract
                {
                    SessionId = sessionId,
                    Data = data,
                    FileName = fileName
                };
                client.AddResource(resourceInfo);
            }
        }
    }
}
