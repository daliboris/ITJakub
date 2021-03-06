﻿using System;
using System.Collections.Generic;
using System.Transactions;
using Castle.Facilities.NHibernate;
using Castle.Transactions;
using ITJakub.DataEntities.Database.Daos;
using ITJakub.DataEntities.Database.Entities;
using ITJakub.DataEntities.Database.Entities.Enums;
using NHibernate.Criterion;

namespace ITJakub.DataEntities.Database.Repositories
{
    public class UserRepository : NHibernateTransactionalDao
    {
        public UserRepository(ISessionManager sessManager)
            : base(sessManager)
        {
        }

        [Transaction(TransactionScopeOption.Required)]
        public virtual User FindByUserName(string userName)
        {
            using (var session = GetSession())
            {
                return session.QueryOver<User>()
                    .Where(user => user.UserName == userName)
                    .SingleOrDefault<User>();
            }
        }

        [Transaction(TransactionScopeOption.Required)]
        public virtual User FindById(int userId)
        {
            using (var session = GetSession())
            {
                return session.QueryOver<User>()
                    .Where(user => user.Id == userId)
                    .SingleOrDefault<User>();
            }
        }

        [Transaction(TransactionScopeOption.Required)]
        public virtual User FindByIdWithDetails(int userId)
        {
            using (var session = GetSession())
            {
                return session.QueryOver<User>()
                    .Where(user => user.Id == userId)
                    .Fetch(user => user.Groups).Eager
                    .SingleOrDefault<User>();
            }
        }

        [Transaction(TransactionScopeOption.Required)]
        public virtual int Create(User user)
        {
            using (var session = GetSession())
            {
                return (int) base.Create(user);
            }
        }

        [Transaction(TransactionScopeOption.Required)]
        public virtual IList<User> GetLastUsers(int recordCount)
        {
            using (var session = GetSession())
            {
                return session.QueryOver<User>()
                    .OrderBy(x => x.CreateTime).Desc
                    .Take(recordCount)
                    .List<User>();
            }
        }

        [Transaction(TransactionScopeOption.Required)]
        public virtual IList<User> GetTypeaheadUsers(string query, int recordCount)
        {
            using (var session = GetSession())
            {
                return session.QueryOver<User>()
                    .Where(Restrictions.Or(
                        Restrictions.Or(
                            Restrictions.On<User>(u => u.UserName).IsInsensitiveLike(query),
                            Restrictions.On<User>(u => u.LastName).IsInsensitiveLike(query)),
                        Restrictions.On<User>(u => u.Email).IsInsensitiveLike(query)))
                    .Take(recordCount)
                    .List<User>();
            }
        }


        [Transaction(TransactionScopeOption.Required)]
        public virtual User GetByLogin(string username)
        {
            using (var session = GetSession())
            {
                return session.QueryOver<User>().Where(x => x.UserName == username).SingleOrDefault<User>();
            }
        }

        [Transaction(TransactionScopeOption.Required)]
        public virtual User GetByLoginAndCommToken(string userName, string commToken)
        {
            using (var session = GetSession())
            {
                return session.QueryOver<User>().Where(x => x.UserName == userName && x.CommunicationToken == commToken).SingleOrDefault<User>();
            }
        }

        [Transaction(TransactionScopeOption.Required)]
        public virtual User GetVirtualUserForUnregisteredUsersOrCreate(string unregisteredUserName, Group unregisteredUserGroupName)
        {
            using (var session = GetSession())
            {
                var defaultUser = session.QueryOver<User>().Where(x => x.UserName == unregisteredUserName).SingleOrDefault<User>();
                if (defaultUser != null)
                    return defaultUser;


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
                    CommunicationTokenCreateTime = DateTime.UtcNow,           
                    Groups = new List<Group> { unregisteredUserGroupName }
                };

                session.Save(defaultUser);

                return defaultUser;
            }
        }        

        [Transaction(TransactionScopeOption.Required)]
        public virtual Group GetDefaultGroupOrCreate(string defaultRegisteredGroupName)
        {
            using (var session = GetSession())
            {
                var  registeredUsersGroup = session.QueryOver<Group>().Where(x => x.Name == defaultRegisteredGroupName).SingleOrDefault<Group>();
                if (registeredUsersGroup != null)
                    return registeredUsersGroup;

                var now = DateTime.UtcNow;
                registeredUsersGroup = new Group
                {
                    Name = defaultRegisteredGroupName,
                    CreateTime = now,
                    Description = "Default user group",
                };

                session.Save(registeredUsersGroup);


                return registeredUsersGroup;
            }
        }    
    }
}