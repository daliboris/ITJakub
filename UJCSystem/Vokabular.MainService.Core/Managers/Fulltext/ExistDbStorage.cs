﻿using System;
using ITJakub.SearchService.DataContracts.Types;
using Vokabular.DataEntities.Database.Entities;
using Vokabular.MainService.Core.Communication;
using Vokabular.MainService.DataContracts.Contracts.Type;

namespace Vokabular.MainService.Core.Managers.Fulltext
{
    public class ExistDbStorage : IFulltextStorage
    {
        private readonly CommunicationProvider m_communicationProvider;

        public ExistDbStorage(CommunicationProvider communicationProvider)
        {
            m_communicationProvider = communicationProvider;
        }

        private OutputFormatEnumContract ConvertOutputTextFormat(TextFormatEnumContract format)
        {
            switch (format)
            {
                case TextFormatEnumContract.Raw:
                    return OutputFormatEnumContract.Xml;
                case TextFormatEnumContract.Html:
                    return OutputFormatEnumContract.Html;
                case TextFormatEnumContract.Rtf:
                    return OutputFormatEnumContract.Rtf;
                default:
                    throw new ArgumentOutOfRangeException(nameof(format), format, null);
            }
        }

        public ProjectType ProjectType => ProjectType.Research;

        public string GetPageText(TextResource textResource, TextFormatEnumContract format)
        {
            var outputFormat = ConvertOutputTextFormat(format);
            using (var ssc = m_communicationProvider.GetSearchServiceClient())
            {
                var project = textResource.Resource.Project;
                var bookVersion = textResource.BookVersion;
                var result = ssc.GetBookPageByXmlId(project.ExternalId, bookVersion.ExternalId, textResource.ExternalId, "pageToHtml.xsl", outputFormat, ResourceLevelEnumContract.Shared); // TODO dynamically resolve transformation type
                return result;
            }
        }
    }
}