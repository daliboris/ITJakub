﻿using System.Collections.Generic;
using Castle.Facilities.NHibernateIntegration;
using Castle.Services.Transaction;
using ITJakub.DataEntities.Database.Daos;
using ITJakub.DataEntities.Database.Entities;
using NHibernate;
using NHibernate.Criterion;

namespace ITJakub.DataEntities.Database.Repositories
{
    [Transactional]
    public class BookVersionRepository : NHibernateTransactionalDao
    {
        public BookVersionRepository(ISessionManager sessManager)
            : base(sessManager)
        {
        }


        [Transaction(TransactionMode.Requires)]
        public virtual long Create(BookVersion bookVersion)
        {
            using (ISession session = GetSession())
            {
                return (long) session.Save(bookVersion);
            }
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void Delete(BookVersion bookVersion)
        {
            using (ISession session = GetSession())
            {
                session.Delete(bookVersion);
            }
        }

        [Transaction(TransactionMode.Requires)]
        public virtual BookVersion FindBookVersionByGuid(string bookVersionGuid)
        {
            using (ISession session = GetSession())
            {
                return
                    session.QueryOver<BookVersion>()
                        .Where(bookVersion => bookVersion.VersionId == bookVersionGuid)
                        .SingleOrDefault<BookVersion>();
            }
        }

        [Transaction(TransactionMode.Requires)]
        public virtual Publisher FindPublisherByText(string text)
        {
            using (ISession session = GetSession())
            {
                return
                    session.QueryOver<Publisher>()
                        .Where(publisher => publisher.Text == text)
                        .SingleOrDefault<Publisher>();
            }
        }

        [Transaction(TransactionMode.Requires)]
        public virtual Responsible FindResponsible(string text, ResponsibleType type)
        {
            
            using (ISession session = GetSession())
            {
                ResponsibleType responsibleType = FindResponsibleType(type) ?? type;
                return
                    session.QueryOver<Responsible>()
                        .Where(
                            responsible =>
                                responsible.Text == text && responsible.ResponsibleType.Id == responsibleType.Id)
                        .Take(1)
                        .SingleOrDefault<Responsible>();
            }
        }

        [Transaction(TransactionMode.Requires)]
        public virtual ResponsibleType FindResponsibleType(ResponsibleType responsibleType)
        {
            using (ISession session = GetSession())
            {
                return
                    session.QueryOver<ResponsibleType>()
                        .Where(
                            respType =>
                                respType.Text == responsibleType.Text ||
                                respType.Type == responsibleType.Type)
                        .Take(1)
                        .SingleOrDefault<ResponsibleType>();
            }
        }

        [Transaction(TransactionMode.Requires)]
        public virtual IList<BookVersion> SearchByCriteria(DetachedCriteria databaseCriteria)
        {
            using (ISession session = GetSession())
            {
                return databaseCriteria.GetExecutableCriteria(session).List<BookVersion>();
            }
        }
    }
}