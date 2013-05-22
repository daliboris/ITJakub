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
        List<string> GetAllExtendedTermsForKey(string key);

        [OperationContract]
        SearchResult[] GetContextForKeyWord(string keyWord);

        [OperationContract]
        SearchResult[] GetResultsByBooks(string book, string keyWord);

        [OperationContract]
        SelectionBase[] GetCategoryChildrenById(string categoryId);

        [OperationContract]
        SelectionBase[] GetRootCategories();
    }
}
