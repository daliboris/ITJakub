﻿using System.Collections.Generic;
using System.ServiceModel;
using ITJakub.Contracts.Categories;
using ITJakub.Contracts.Searching;

namespace ITJakub.Contracts
{
    [ServiceContract]
    public interface IItJakubService
    {
        [OperationContract]
        KeyWordsResponse GetAllExtendedTermsForKey(string key, List<string> categorieIds, List<string> booksIds);

        [OperationContract]
        List<SearchResultWithHtmlContext> GetHtmlContextForKeyWord(string keyWord, List<string> categorieIds, List<string> booksIds);

        [OperationContract]
        List<SearchResultWithHtmlContext> GetResultsByBooks(string book, string keyWord);

        [OperationContract]
        List<SelectionBase> GetCategoryChildrenById(string categoryId);

        [OperationContract]
        List<SelectionBase> GetRootCategories();

        [OperationContract]
        IEnumerable<SearchResult> GetBooksBySearchTerm(string searchTerm);

        [OperationContract]
        IEnumerable<SearchResult> GetBooksTitleByLetter(string letter);

        [OperationContract]
        IEnumerable<SearchResult> GetSourcesAuthorByLetter(string letter);
    }
}
