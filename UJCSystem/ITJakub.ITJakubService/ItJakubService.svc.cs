﻿using System.Collections.Generic;
using System.ServiceModel;
using Castle.Windsor;
using ITJakub.ITJakubService.DataContracts;
using ITJakub.Shared.Contracts;

namespace ITJakub.ITJakubService
{
    public class ItJakubService : IItJakubServiceLocal
    {
        private readonly WindsorContainer m_container;
        private readonly ItJakubServiceManager m_serviceManager;

        public ItJakubService()
        {
            m_container = Container.Current;
            m_serviceManager = m_container.Resolve<ItJakubServiceManager>();
        }
       
        public CreateUserResultContract CreateUser(CreateUserContract createUserContract)
        {
            return m_serviceManager.CreateUser(createUserContract);
        }

        public LoginUserResultContract LoginUser(LoginUserContract loginUserContract)
        {
            return m_serviceManager.LoginUser(loginUserContract);
        }

        public ProcessedFileInfoContract ProcessUploadedFile(UploadFileContract uploadFileContract)
        {
            return m_serviceManager.ProcessUploadedFile(uploadFileContract);
        }

        public void SaveFrontImageForFile(UploadImageContract uploadImageContract)
        {
            m_serviceManager.SaveFrontImageForFile(uploadImageContract);
        }

        public void SavePageImageForFile(UploadImageContract uploadImageContract)
        {
            m_serviceManager.SavePageImageForFile(uploadImageContract);
        }

        public void SaveFileMetadata(string fileGuid, string name, string author)
        {
            m_serviceManager.SaveFileMetadata(fileGuid, name, author);
        }

        public IEnumerable<AuthorDetailContract> GetAllAuthors()
        {
            return m_serviceManager.GetAllAuthors();
        }

        public int CreateAuthor(string name)
        {
            return m_serviceManager.CreateAuthor(name);
        }

        public void AssignAuthorsToBook(string bookGuid, string bookVersionGuid, IEnumerable<int> authorIds)
        {
            m_serviceManager.AssignAuthorsToBook(bookGuid, bookVersionGuid, authorIds);
        }

        public string GetBookPageByName(string documentId, string pageName, string resultFormat)
        {
            return m_serviceManager.GetBookPageByName(documentId, pageName, resultFormat);
        }

        public string GetBookPagesByName(string documentId, string startPageName, string endPageName, string resultFormat)
        {
            return m_serviceManager.GetBookPagesByName(documentId, startPageName, endPageName, resultFormat);
        }

        public string GetBookPageByPosition(string documentId, int position, string resultFormat)
        {
            return m_serviceManager.GetBookPageByPosition(documentId, position, resultFormat);
        }

        public IList<BookPage> GetBookPageList(string documentId)
        {
            return m_serviceManager.GetBookPageList(documentId);
        }
    }

    [ServiceContract]
    public interface IItJakubServiceLocal : IItJakubService
    {
    }
}