using System.Net;
using Vokabular.DataEntities.Database.Entities;
using Vokabular.DataEntities.Database.Repositories;
using Vokabular.DataEntities.Database.UnitOfWork;
using Vokabular.RestClient.Errors;

namespace Vokabular.MainService.Core.Works.Person
{
    public class DeleteOriginalAuthorWork : UnitOfWorkBase
    {
        private readonly PersonRepository m_personRepository;
        private readonly int m_authorId;

        public DeleteOriginalAuthorWork(PersonRepository personRepository, int authorId) : base(personRepository)
        {
            m_personRepository = personRepository;
            m_authorId = authorId;
        }

        protected override void ExecuteWorkImplementation()
        {
            var dbAuthor = m_personRepository.FindById<OriginalAuthor>(m_authorId);
            if (dbAuthor == null)
                throw new HttpErrorCodeException(ErrorMessages.NotFound, HttpStatusCode.NotFound);

            m_personRepository.Delete(dbAuthor);
        }
    }
}