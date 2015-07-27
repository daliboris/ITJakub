﻿using System.Collections.Generic;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Castle.Services.Transaction;
using ITJakub.DataEntities.Database.Daos;
using ITJakub.DataEntities.Database.Entities;
using ITJakub.DataEntities.Database.Entities.Enums;
using ITJakub.DataEntities.Database.Exceptions;
using NHibernate.Criterion;

namespace ITJakub.DataEntities.Database.Repositories
{
    [Transactional]
    public class CategoryRepository : NHibernateTransactionalDao
    {
        public CategoryRepository(ISessionManager sessManager)
            : base(sessManager)
        {
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void SaveOrUpdate(Category category)
        {
            using (var session = GetSession())
            {
                var tmpCategory = FindByXmlId(category.XmlId);
                if (tmpCategory != null)
                {
                    tmpCategory.Description = category.Description;
                    session.SaveOrUpdate(tmpCategory);
                }
                else
                {
                    session.SaveOrUpdate(category);
                }
            }
        }

        [Transaction(TransactionMode.Requires)]
        public virtual Category FindByXmlId(string xmlId)
        {
            using (var session = GetSession())
            {
                return session.QueryOver<Category>()
                    .Where(category => category.XmlId == xmlId)
                    .SingleOrDefault<Category>();
            }
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void SetBookTypeToRootCategoryIfNotKnown(BookType bookType, Category rootCategory)
        {
            using (var session = GetSession())
            {
                var rootCategoryWithBookType =
                    session.QueryOver<Category>()
                        .Where(cat => cat.ParentCategory == null && cat.BookType.Id == bookType.Id)
                        .SingleOrDefault<Category>();

                if (rootCategoryWithBookType != null && rootCategoryWithBookType.Id != rootCategory.Id)
                {
                    throw new BookTypeIsAlreadyAssociatedWithAnotherCategoryException(bookType.Id,
                        rootCategoryWithBookType.Id);
                }

                var categoryToSave = session.Merge(rootCategory);
                categoryToSave.BookType = bookType;
                session.Update(categoryToSave);
            }
        }

        [Transaction(TransactionMode.Requires)]
        public virtual IList<Category> FindCategoriesByBookTypeii(BookTypeEnum type)
        {
            using (var session = GetSession())
            {
                Category categoryAlias = null;
                BookType bookTypeAlias = null;

                var rootCategory = session.QueryOver(() => categoryAlias)
                    .JoinAlias(x => x.BookType, () => bookTypeAlias)
                    .Where(() => bookTypeAlias.Type == type && categoryAlias.ParentCategory == null)
                    .SingleOrDefault<Category>();

                if (rootCategory == null)
                {
                    return new List<Category>();
                }

                var resultCategories = new List<Category>();
                IList<Category> childCategories = new List<Category> {rootCategory};

                while (childCategories != null && childCategories.Count() != 0)
                {
                    resultCategories.AddRange(childCategories);

                    IList<int> parentCategoriesIds = childCategories.Select(childCategory => childCategory.Id).ToList();
                    var ids = parentCategoriesIds;
                    childCategories = session.QueryOver(() => categoryAlias)
                        .Where(() => categoryAlias.ParentCategory.Id.IsIn(ids.ToArray()))
                        .List<Category>();
                }

                return resultCategories;
            }
        }

        public virtual IList<Category> FindCategoriesByBookType(BookTypeEnum type)
        {
            using (var session = GetSession())
            {
                Category categoryAlias = null;
                BookType bookTypeAlias = null;

                var rootCategory = session.QueryOver(() => categoryAlias)
                    .JoinAlias(x => x.BookType, () => bookTypeAlias)
                    .Where(() => bookTypeAlias.Type == type && categoryAlias.ParentCategory == null)
                    .SingleOrDefault<Category>();

                if (rootCategory == null)
                    return new List<Category>();

                return session.QueryOver<Category>()
                    .WhereRestrictionOn(x => x.Path)
                    .IsLike(rootCategory.Path, MatchMode.Start)
                    .List<Category>();
            }
        }


        [Transaction(TransactionMode.Requires)]
        public virtual BookType FindBookTypeByCategory(Category category)
        {
            using (var session = GetSession())
            {
                var resultCategory = session.QueryOver<Category>().Where(cat => cat.Id == category.Id).SingleOrDefault();
                return resultCategory == null ? null : resultCategory.BookType;
            }
        }

        [Transaction(TransactionMode.Requires)]
        public virtual BookType FindBookTypeByType(BookTypeEnum bookTypeEnum)
        {
            using (var session = GetSession())
            {
                return session.QueryOver<BookType>()
                    .Where(x => x.Type == bookTypeEnum)
                    .SingleOrDefault();
            }
        }

        [Transaction(TransactionMode.Requires)]
        public virtual IList<int> GetAllSubcategoryIds(IList<int> categoryIds)
        {
            using (var session = GetSession())
            {
                if (categoryIds == null || categoryIds.Count == 0)
                    return new List<int>();

                return session.GetNamedQuery("GetCategoryHierarchy")
                    .SetParameterList("categoryIds", categoryIds)
                    .List<int>();
            }
        }

        [Transaction(TransactionMode.Requires)]
        public virtual IList<long> GetBookIdsFromCategory(IList<int> categoryIds)
        {
            using (var session = GetSession())
            {
                return session.GetNamedQuery("GetBookIdsFromCategoryHierarchy")
                    .SetParameterList("categoryIds", categoryIds)
                    .List<long>();
            }
        }
    }
}