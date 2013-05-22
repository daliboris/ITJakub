﻿using System;
using System.Collections.Generic;
using ITJakub.Contracts.Categories;
using ITJakub.Contracts.Searching;
using ITJakub.Core;
using Ujc.Naki.MVCWebLayer.Services.Mocks;

namespace Ujc.Naki.MVCWebLayer.Services
{
    public class ItJakubSearchProvider : ISearchResultProvider
    {

        private readonly ItJakubServiceClient m_serviceClient;
        private SearchResult[] m_searchResult;
        public ItJakubSearchProvider()
        {
            m_serviceClient = Container.Current.Resolve<ItJakubServiceClient>();
        }


        public string[] GetSearchResults(string query)
        {
            List<string> result = m_serviceClient.GetAllExtendedTermsForKey(query);
            return result.ToArray();
        }

        public SearchResult[] GetSearchResultsByType(string book, string searchTerm)
        {
            if (m_searchResult == null)
                m_searchResult = m_serviceClient.GetResultsByBooks(book, searchTerm);

            return m_searchResult;
        }


        public SearchResult[] GetKwicForKeyWord(string searchTerm)
        {
            if(m_searchResult == null)
                m_searchResult = m_serviceClient.GetContextForKeyWord(searchTerm);

            return m_searchResult;
        }

        public SelectionBase[] GetCategoryChildrenById(string categoryId)
        {
            return m_serviceClient.GetCategoryChildrenById(categoryId);
        }
    }

    
}