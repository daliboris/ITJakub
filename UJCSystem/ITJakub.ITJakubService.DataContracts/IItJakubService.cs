﻿using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using System.Threading.Tasks;
using ITJakub.Shared.Contracts;
using ITJakub.Shared.Contracts.Resources;

namespace ITJakub.ITJakubService.DataContracts
{
    [ServiceContract]
    public interface IItJakubService
    {
        [OperationContract]
        IEnumerable<AuthorDetailContract> GetAllAuthors();

        [OperationContract]
        Task<string> GetBookPageByNameAsync(string bookGuid, string pageName, OutputFormatEnumContract resultFormat);

        [OperationContract]
        Task<string> GetBookPagesByNameAsync(string bookGuid, string startPageName, string endPageName, OutputFormatEnumContract resultFormat);

        [OperationContract]
        Task<string> GetBookPageByPositionAsync(string bookGuid, int position, OutputFormatEnumContract resultFormat);

        [OperationContract]
        Task<IList<BookPageContract>> GetBookPageListAsync(string bookGuid);

        #region Resource Import
        [OperationContract]       
        void AddResource(UploadResourceContract uploadFileInfoSkeleton);

        [OperationContract]
        bool ProcessSession(string resourceSessionId, string uploadMessage);
        #endregion

        [OperationContract]
        List<SearchResultContract> Search(string term);

        [OperationContract]
        BookInfoContract GetBookInfo(string bookGuid);

        [OperationContract]
        BookTypeSearchResultContract GetBooksWithCategoriesByBookType(BookTypeEnumContract bookType);

        [OperationContract]
        Stream GetBookPageImage(BookPageImageContract bookPageImageContract);

        #region CardFile methods

        [OperationContract]
        IEnumerable<CardFileContract> GetCardFiles();

        [OperationContract]
        IEnumerable<BucketContract> GetBuckets(string cardFileId);

        [OperationContract]
        IEnumerable<BucketContract> GetBucketsWithHeadword(string cardFileId, string headword);

        #endregion
    }
}