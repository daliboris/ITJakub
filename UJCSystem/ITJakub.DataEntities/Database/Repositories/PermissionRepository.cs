﻿using System.Collections.Generic;
using System.Transactions;
using Castle.Facilities.NHibernate;
using Castle.Transactions;
using ITJakub.DataEntities.Database.Daos;
using ITJakub.DataEntities.Database.Entities;
using ITJakub.DataEntities.Database.Entities.Enums;
using NHibernate.Criterion;

namespace ITJakub.DataEntities.Database.Repositories
{
    public class PermissionRepository : NHibernateTransactionalDao
    {
        public PermissionRepository(ISessionManager sessManager)
            : base(sessManager)
        {
        }

        [Transaction(TransactionScopeOption.Required)]
        public virtual Group FindGroupById(int groupId)
        {
            using (var session = GetSession())
            {
                var group = session.QueryOver<Group>()
                    .Fetch(g => g.Users).Eager
                    .Fetch(g => g.CreatedBy).Eager
                    .Where(g => g.Id == groupId)
                    .SingleOrDefault();

                return group;
            }
        }

        [Transaction(TransactionScopeOption.Required)]
        public virtual Group FindGroupWithSpecialPermissionsById(int groupId)
        {
            using (var session = GetSession())
            {
                var group = session.QueryOver<Group>()
                    .Fetch(g => g.CreatedBy).Eager
                    .Fetch(g => g.SpecialPermissions).Eager
                    .Where(g => g.Id == groupId)
                    .SingleOrDefault();

                return group;
            }
        }

        [Transaction(TransactionScopeOption.Required)]
        public virtual IList<Group> GetLastGroups(int recordCount)
        {
            using (var session = GetSession())
            {
                return session.QueryOver<Group>()
                    .OrderBy(x => x.CreateTime).Desc
                    .Take(recordCount)
                    .List<Group>();
            }
        }

        [Transaction(TransactionScopeOption.Required)]
        public virtual IList<Group> GetTypeaheadGroups(string query, int recordCount)
        {
            using (var session = GetSession())
            {
                return session.QueryOver<Group>()
                    .Where(Restrictions.On<Group>(x => x.Name).IsInsensitiveLike(query))
                    .Take(recordCount)
                    .List<Group>();
            }
        }

        [Transaction(TransactionScopeOption.Required)]
        public virtual IList<Book> GetAllowedBooksByGroup(int groupId)
        {

            using (var session = GetSession())
            {
                Book bookAlias = null;
                Permission permissionAlias = null;
                Group groupAlias = null;

                var books = session.QueryOver(() => bookAlias)
                    .JoinQueryOver(x => x.Permissions, () => permissionAlias)
                    .JoinQueryOver(x => permissionAlias.Group, () => groupAlias)
                    .Where(x => groupAlias.Id == groupId)
                    .List<Book>();

                return books;
            }
        }

        [Transaction(TransactionScopeOption.Required)]
        public virtual IList<User> GetUsersByGroup(int groupId)
        {
            using (var session = GetSession())
            {
                User userAlias = null;
                Group groupAlias = null;

                var users = session.QueryOver(() => userAlias)
                    .JoinQueryOver(x => x.Groups, () => groupAlias)
                    .Where(x => groupAlias.Id == groupId)
                    .List<User>();

                return users;
            }
        }

        [Transaction(TransactionScopeOption.Required)]
        public virtual IList<Group> GetGroupsByUser(int userId)
        {
            using (var session = GetSession())
            {
                User userAlias = null;
                Group groupAlias = null;

                var groups = session.QueryOver(() => groupAlias)
                    .JoinQueryOver(x => x.Users, () => userAlias)
                    .Where(x => userAlias.Id == userId)
                    .List<Group>();

                return groups;
            }
        }

        [Transaction(TransactionScopeOption.Required)]
        public virtual int CreateGroup(Group group)
        {
            using (var session = GetSession())
            {
                return (int) session.Save(group);
            }
        }

        [Transaction(TransactionScopeOption.Required)]
        public virtual IList<string> GetFilteredBookXmlIdListByUserPermissions(int userId, IEnumerable<string> bookXmlIds)
        {
            using (var session = GetSession())
            {
                Book bookAlias = null;
                Permission permissionAlias = null;
                Group groupAlias = null;
                User userAlias = null;

                var filteredBookXmlIds = session.QueryOver(() => bookAlias)
                    .JoinQueryOver(x => x.Permissions, () => permissionAlias)
                    .JoinQueryOver(x => permissionAlias.Group, () => groupAlias)
                    .JoinQueryOver(x => groupAlias.Users, () => userAlias)
                    .Select(Projections.Distinct(Projections.Property(() => bookAlias.Guid)))
                    .Where(() => userAlias.Id == userId)
                    .AndRestrictionOn(() => bookAlias.Guid).IsInG(bookXmlIds)
                    .List<string>();

                return filteredBookXmlIds;
            }
        }

        [Transaction(TransactionScopeOption.Required)]
        public virtual IList<long> GetFilteredBookIdListByUserPermissions(int userId, IEnumerable<long> bookIds)
        {
            using (var session = GetSession())
            {
                Book bookAlias = null;
                Permission permissionAlias = null;
                Group groupAlias = null;
                User userAlias = null;

                var filteredBookIds = session.QueryOver(() => bookAlias)
                    .JoinQueryOver(x => x.Permissions, () => permissionAlias)
                    .JoinQueryOver(x => permissionAlias.Group, () => groupAlias)
                    .JoinQueryOver(x => groupAlias.Users, () => userAlias)
                    .Select(Projections.Distinct(Projections.Property(() => bookAlias.Id)))
                    .Where(() => userAlias.Id == userId)
                    .AndRestrictionOn(() => bookAlias.Id).IsInG(bookIds)
                    .List<long>();

                return filteredBookIds;
            }
        }

        [Transaction(TransactionScopeOption.Required)]
        public virtual IList<long> GetFilteredBookIdListByGroupPermissions(int groupId, IEnumerable<long> bookIds)
        {
            using (var session = GetSession())
            {
                Book bookAlias = null;
                Permission permissionAlias = null;
                Group groupAlias = null;

                var filteredBookIds = session.QueryOver(() => bookAlias)
                    .JoinQueryOver(x => x.Permissions, () => permissionAlias)
                    .JoinQueryOver(x => permissionAlias.Group, () => groupAlias)
                    .Select(Projections.Distinct(Projections.Property(() => bookAlias.Id)))
                    .Where(() => groupAlias.Id == groupId)
                    .AndRestrictionOn(() => bookAlias.Id).IsInG(bookIds)
                    .List<long>();

                return filteredBookIds;
            }
        }

        [Transaction(TransactionScopeOption.Required)]
        public virtual void CreatePermission(Permission permission)
        {
            using (var session = GetSession())
            {
                session.Save(permission);
            }
        }

        [Transaction(TransactionScopeOption.Required)]
        public virtual void CreateSpecialPermission(SpecialPermission permission)
        {
            using (var session = GetSession())
            {
                session.Save(permission);
            }
        }

        [Transaction(TransactionScopeOption.Required)]
        public virtual IList<Permission> FindPermissionsByGroupAndBooks(int groupId, IList<long> bookIds)
        {
            using (var session = GetSession())
            {
                Book bookAlias = null;
                Permission permissionAlias = null;
                Group groupAlias = null;

                var permissions = session.QueryOver(() => permissionAlias)
                    .JoinQueryOver(x => permissionAlias.Book, () => bookAlias)
                    .JoinQueryOver(x => permissionAlias.Group, () => groupAlias)
                    .Where(() => groupAlias.Id == groupId)
                    .AndRestrictionOn(() => bookAlias.Id).IsInG(bookIds)
                    .List<Permission>();

                return permissions;
            }
        }

        [Transaction(TransactionScopeOption.Required)]
        public virtual void DeletePermissions(IList<Permission> permissionsList)
        {
            using (var session = GetSession())
            {
                foreach (var permission in permissionsList)
                {
                    session.Delete(permission);
                }
            }
        }

        [Transaction(TransactionScopeOption.Required)]
        public virtual IList<SpecialPermission> GetSpecialPermissionsByUser(int userId)
        {
            using (var session = GetSession())
            {
                SpecialPermission specPermissionAlias = null;
                Group groupAlias = null;
                User userAlias = null;

                var permissions = session.QueryOver(() => specPermissionAlias)
                    .JoinQueryOver(x => specPermissionAlias.Groups, () => groupAlias)
                    .JoinQueryOver(x => groupAlias.Users, () => userAlias)
                    .Where(() => userAlias.Id == userId)
                    .List<SpecialPermission>();

                return permissions;
            }
        }
        
        [Transaction(TransactionScopeOption.Required)]
        public virtual IList<SpecialPermission> GetSpecialPermissionsByUserAndType(int userId, SpecialPermissionCategorization type)
        {
            using (var session = GetSession())
            {
                SpecialPermission specPermissionAlias = null;
                Group groupAlias = null;
                User userAlias = null;

                var permissions = session.QueryOver(() => specPermissionAlias)
                    .JoinQueryOver(x => specPermissionAlias.Groups, () => groupAlias)
                    .JoinQueryOver(x => groupAlias.Users, () => userAlias)
                    .Where(() => userAlias.Id == userId)
                    .And(()=> specPermissionAlias.PermissionCategorization == type)
                    .List<SpecialPermission>();

                return permissions;
            }
        }

        [Transaction(TransactionScopeOption.Required)]
        public virtual IList<SpecialPermission> GetSpecialPermissions()
        {
            using (var session = GetSession())
            {
                SpecialPermission specPermissionAlias = null;

                var permissions = session.QueryOver(() => specPermissionAlias)
                    .List<SpecialPermission>();

                return permissions;
            }
        }

        [Transaction(TransactionScopeOption.Required)]
        public virtual IList<SpecialPermission> GetSpecialPermissionsByGroup(int groupId)
        {
            using (var session = GetSession())
            {
                SpecialPermission specPermissionAlias = null;
                Group groupAlias = null;

                var permissions = session.QueryOver(() => specPermissionAlias)
                    .JoinQueryOver(x => specPermissionAlias.Groups, () => groupAlias)
                    .Where(() => groupAlias.Id == groupId)
                    .List<SpecialPermission>();

                return permissions;
            }
        }

        [Transaction(TransactionScopeOption.Required)]
        public virtual IList<SpecialPermission> GetSpecialPermissionsByIds(IEnumerable<int> specialPermissionIds)
        {
            using (var session = GetSession())
            {
                SpecialPermission specPermissionAlias = null;

                var permissions = session.QueryOver(() => specPermissionAlias)
                    .AndRestrictionOn(() => specPermissionAlias.Id).IsInG(specialPermissionIds)
                    .List<SpecialPermission>();

                return permissions;
            }
        }

        [Transaction(TransactionScopeOption.Required)]
        public virtual IList<AutoImportCategoryPermission> GetAutoimportPermissionsByCategoryIdList(IEnumerable<int> categoryIds)
        {
            using (var session = GetSession())
            {
                AutoImportCategoryPermission autoimportPermissionAlias = null;
                Category categoryAlias = null;

                var permissions = session.QueryOver(() => autoimportPermissionAlias)
                    .JoinQueryOver(x => autoimportPermissionAlias.Category, () => categoryAlias)
                    .AndRestrictionOn(() => categoryAlias.Id).IsInG(categoryIds)
                    .List<AutoImportCategoryPermission>();

                return permissions;
            }
        }

        [Transaction(TransactionScopeOption.Required)]
        public virtual IList<Group> GetGroupsBySpecialPermissionIds(IEnumerable<int> specialPermissionIds)
        {
            using (var session = GetSession())
            {
                Group groupAlias = null;
                SpecialPermission specialPermissionAlias = null;

                var groups = session.QueryOver(() => groupAlias)
                    .JoinQueryOver(x => groupAlias.SpecialPermissions, () => specialPermissionAlias)
                    .AndRestrictionOn(() => specialPermissionAlias.Id).IsInG(specialPermissionIds)
                    .List<Group>();

                return groups;
            }
        }

        [Transaction(TransactionScopeOption.Required)]
        public virtual void CreatePermissionIfNotExist(Permission permission)
        {
            using (var session = GetSession())
            {
                var tmpPermission = FindPermissionByBookAndGroup(permission.Book.Id, permission.Group.Id);
                if (tmpPermission == null)
                {
                    session.Save(permission);
                }
            }
        }

        [Transaction(TransactionScopeOption.Required)]
        public virtual Permission FindPermissionByBookAndGroup(long bookId, int groupId)
        {
            using (var session = GetSession())
            {
                return
                    session.QueryOver<Permission>()
                        .Where(
                            permission =>
                                permission.Book.Id == bookId &&
                                permission.Group.Id == groupId)
                        .SingleOrDefault<Permission>();
            }
        }
    }
}