﻿using System.Collections.Generic;
using System.ServiceModel;
using ITJakub.Shared.Contracts;

namespace ITJakub.SearchService
{
    public class SearchService : ISearchServiceLocal
    {
        private readonly SearchServiceManager m_searchServiceManager;

        public SearchService()
        {
            m_searchServiceManager = Container.Current.Resolve<SearchServiceManager>();
        }

        public string GetBookPageByPosition(string documentId, int pagePosition)
        {
            return m_searchServiceManager.GetBookPageByPosition(documentId, pagePosition);
        }

        public string GetBookPageByName(string documentId, string pageName)
        {
            return m_searchServiceManager.GetBookPageByName(documentId, pageName);
        }

        public string GetBookPagesByName(string documentId, string startPageName, string endPageName)
        {
            return m_searchServiceManager.GetBookPagesByName(documentId, startPageName, endPageName);
        }

        public IList<BookPage> GetBookPageList(string documentId)
        {
            return m_searchServiceManager.GetBookPageList(documentId);
        }
    }

    [ServiceContract]
    public interface ISearchServiceLocal:ISearchService
    {
    }
}
