﻿using System.Collections.Generic;
using System.ServiceModel;
using ITJakub.Shared.Contracts.Searching;

namespace ITJakub.Shared.Contracts
{
    [ServiceContract]
    public interface ISearchService
    {
        [OperationContract]
        void Search(List<SearchCriteriumBase> criteria);

        [OperationContract]
        string GetTitleById(string id);

        [OperationContract]
        SearchTermPossibleResult AllExtendedTermsForKey(string key);

        [OperationContract]
        SearchTermPossibleResult AllExtendedTermsForKeyWithBooksRestriction(string key, List<string> booksIds);

        [OperationContract]
        List<SearchResultWithKwicContext> GetKwicContextForKeyWord(string keyWord);

        [OperationContract]
        List<SearchResultWithXmlContext> GetXmlContextForKeyWord(string keyWord);

        [OperationContract]
        List<SearchResultWithHtmlContext> GetHtmlContextForKeyWord(string keyWord);

        [OperationContract]
        List<SearchResultWithHtmlContext> GetHtmlContextForKeyWordWithBooksRestriction(string keyWord, List<string> bookIds);

        [OperationContract]
        IEnumerable<SearchResult> GetAllBooksContainingSearchTerm(string searchTerm);

        [OperationContract]
        IEnumerable<SearchResult> GetBooksByTitleFirstLetter(string letter);

        [OperationContract]
        IEnumerable<SearchResult> GetBooksByAuthorFirstLetter(string letter);

        [OperationContract]
        string GetContentByBookId(string id);

        [OperationContract]
        SearchResult GetBookById(string id);

        [OperationContract]
        string GetBookPageByPosition(string documentId, int pagePosition);

        [OperationContract]
        string GetBookPageByName(string documentId, string pageName);

        [OperationContract]
        string GetBookPagesByName(string documentId, string startPageName, string endPageName);
    }
}
