﻿using Vokabular.DataEntities.Database.Entities;
using Vokabular.DataEntities.Database.Repositories;
using Vokabular.MainService.Core.Works.Users;
using Vokabular.MainService.DataContracts.Contracts;

namespace Vokabular.MainService.Core.Managers
{
    public class UserManager
    {
        private readonly UserRepository m_userRepository;

        public UserManager(UserRepository userRepository)
        {
            m_userRepository = userRepository;
        }

        public User GetCurrentUser()
        {
            // TODO get correct current user

            m_userRepository.UnitOfWork.BeginTransaction();
            return m_userRepository.GetUserByUsername("Admin");
        }

        public int GetCurrentUserId()
        {
            return GetCurrentUser().Id;
        }

        public int CreateNewUser(CreateUserContract data)
        {
            var userId = new CreateNewUserWork(m_userRepository, data).Execute();
            return userId;
        }
    }
}
