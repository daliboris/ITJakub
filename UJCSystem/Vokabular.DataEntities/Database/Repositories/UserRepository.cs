﻿using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Criterion;
using Vokabular.DataEntities.Database.Daos;
using Vokabular.DataEntities.Database.Entities;
using Vokabular.DataEntities.Database.Entities.Enums;
using Vokabular.DataEntities.Database.Entities.SelectResults;
using Vokabular.DataEntities.Database.UnitOfWork;

namespace Vokabular.DataEntities.Database.Repositories
{
    public class UserRepository : NHibernateDao
    {
        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public User GetUserByUsername(string username)
        {
            return GetSession().QueryOver<User>()
                .Where(x => x.UserName == username)
                .SingleOrDefault();
        }

        public User GetUserByToken(string authorizationToken)
        {
            return GetSession().QueryOver<User>()
                .Where(x => x.CommunicationToken == authorizationToken)
                .SingleOrDefault();
        }

        public virtual ListWithTotalCountResult<User> GetUserList(int start, int count, string filterByName)
        {
            var query = GetSession().QueryOver<User>()
                .WhereRestrictionOn(x => x.UserName).IsLike(filterByName, MatchMode.Start) // TODO determine correct type of filter value
                .OrderBy(x => x.LastName).Asc
                .ThenBy(x => x.FirstName).Asc;

            var list = query.Skip(start)
                .Take(count)
                .Future();

            var totalCount = query.ToRowCountQuery()
                .FutureValue<int>();

            return new ListWithTotalCountResult<User>
            {
                List = list.ToList(),
                Count = totalCount.Value,
            };
        }

        public virtual User GetVirtualUserForUnregisteredUsersOrCreate(string unregisteredUserName, UserGroup unregisteredUserGroup)
        {
            var defaultUser = GetSession().QueryOver<User>()
                .Where(x => x.UserName == unregisteredUserName)
                .SingleOrDefault<User>();

            if (defaultUser != null)
            {
                return defaultUser;
            }

            var now = DateTime.UtcNow;
            defaultUser = new User
            {
                UserName = unregisteredUserName,
                Email = string.Empty,
                FirstName = unregisteredUserName,
                LastName = unregisteredUserName,
                CreateTime = now,
                PasswordHash = string.Empty,
                AuthenticationProvider = AuthenticationProvider.ItJakub,
                CommunicationToken = string.Empty,
                CommunicationTokenCreateTime = now,
                Groups = new List<UserGroup> { unregisteredUserGroup },
                AvatarUrl = null,
            };

            GetSession().Save(defaultUser);

            return defaultUser;
        }

        public virtual UserGroup GetDefaultGroupOrCreate(string defaultRegisteredGroupName)
        {
            var registeredUsersGroup = GetSession().QueryOver<UserGroup>()
                .Where(x => x.Name == defaultRegisteredGroupName)
                .SingleOrDefault<UserGroup>();

            if (registeredUsersGroup != null)
            {
                return registeredUsersGroup;
            }

            var now = DateTime.UtcNow;
            registeredUsersGroup = new UserGroup
            {
                Name = defaultRegisteredGroupName,
                CreateTime = now,
                Description = "Default user group",
            };

            GetSession().Save(registeredUsersGroup);

            return registeredUsersGroup;
        }
    }
}
