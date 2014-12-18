﻿using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using Castle.Windsor;
using ITJakub.ITJakubService.DataContracts;
using ITJakub.Shared.Contracts;
using ITJakub.Shared.Contracts.Resources;

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

        public ProcessedFileInfoContract SaveUploadedFile(UploadResourceContract uploadResourceContract)
        {
            return m_serviceManager.SaveUploadedFile(uploadResourceContract);
        }

        public IEnumerable<AuthorDetailContract> GetAllAuthors()
        {
            return m_serviceManager.GetAllAuthors();
        }

        public int CreateAuthor(string name)
        {
            return m_serviceManager.CreateAuthor(name);
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

        public void AddResource(UploadResourceContract resourceInfoSkeleton)
        {
            m_serviceManager.AddResource(resourceInfoSkeleton);
        }

        public bool ProcessSession(string resourceSessionId, string uploadMessage)
        {
            return m_serviceManager.ProcessSession(resourceSessionId, uploadMessage);
        }
    }

    [ServiceContract]
    public interface IItJakubServiceLocal : IItJakubService
    {
    }
}