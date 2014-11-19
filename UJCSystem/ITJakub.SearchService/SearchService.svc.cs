﻿using System.Collections.Generic;
using System.ServiceModel;
using ITJakub.Shared.Contracts;
using ITJakub.Shared.Contracts.Searching;

namespace ITJakub.SearchService
{
    public class SearchService : ISearchServiceLocal
    {
        private readonly SearchServiceManager m_searchServiceManager;

        public SearchService()
        {
            m_searchServiceManager = Container.Current.Resolve<SearchServiceManager>();
        }

        public void Search(List<SearchCriteriumBase> criteria)
        {
            m_searchServiceManager.Search(criteria);
        }

        public List<SearchResultWithKwicContext> GetKwicContextForKeyWord(string keyWord)
        {
            return m_searchServiceManager.GetKwicContextForKeyWord(keyWord);
        }

        public List<SearchResultWithXmlContext> GetXmlContextForKeyWord(string keyWord)
        {
            return m_searchServiceManager.GetXmlContextForKeyWord(keyWord);
        }

        public List<SearchResultWithHtmlContext> GetHtmlContextForKeyWord(string keyWord)
        {
            return m_searchServiceManager.GetHtmlContextForKeyWord(keyWord);
        }

        public List<SearchResultWithHtmlContext> GetHtmlContextForKeyWordWithBooksRestriction(string keyWord, List<string> bookIds)
        {
            return m_searchServiceManager.GetHtmlContextForKeyWord(keyWord, bookIds);
        }

        public IEnumerable<SearchResult> GetAllBooksContainingSearchTerm(string searchTerm)
        {
            return m_searchServiceManager.GetAllBooksContainingSearchTerm(searchTerm);
        }

        public IEnumerable<SearchResult> GetBooksByTitleFirstLetter(string letter)
        {
            return m_searchServiceManager.GetBooksByTitleFirstLetter(letter);
        }

        public IEnumerable<SearchResult> GetBooksByAuthorFirstLetter(string letter)
        {
            return m_searchServiceManager.GetBooksByAuthorFirstLetter(letter);
        }

        public string GetContentByBookId(string id)
        {
            return m_searchServiceManager.GetContentByBookId(id);
        }

        public SearchResult GetBookById(string id)
        {
            return m_searchServiceManager.GetBookById(id);
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

        public string GetTitleById(string id)
        {
            return m_searchServiceManager.GetTitleById(id);
        }

        public SearchTermPossibleResult AllExtendedTermsForKey(string key)
        {
            return m_searchServiceManager.AllExtendedTermsForKey(key);
        }

        public SearchTermPossibleResult AllExtendedTermsForKeyWithBooksRestriction(string key, List<string> booksIds)
        {
            return m_searchServiceManager.AllExtendedTermsForKeyWithBooksRestriction(key, booksIds);
        }

    }

    [ServiceContract]
    public interface ISearchServiceLocal:ISearchService
    {
    }
}
