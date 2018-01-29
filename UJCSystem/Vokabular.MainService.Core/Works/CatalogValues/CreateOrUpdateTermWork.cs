using System.Net;
using Vokabular.DataEntities.Database.Entities;
using Vokabular.DataEntities.Database.Repositories;
using Vokabular.DataEntities.Database.UnitOfWork;
using Vokabular.MainService.DataContracts.Contracts;
using Vokabular.RestClient.Errors;

namespace Vokabular.MainService.Core.Works.CatalogValues
{
    public class CreateOrUpdateTermWork : UnitOfWorkBase<int>
    {
        private readonly CatalogValueRepository m_catalogValueRepository;
        private readonly int? m_termId;
        private readonly TermContract m_data;

        public CreateOrUpdateTermWork(CatalogValueRepository catalogValueRepository, int? termId, TermContract data) : base(catalogValueRepository)
        {
            m_catalogValueRepository = catalogValueRepository;
            m_termId = termId;
            m_data = data;
        }

        protected override int ExecuteWorkImplementation()
        {
            var termEntity = m_termId != null
                ? m_catalogValueRepository.FindById<Term>(m_termId.Value)
                : new Term();

            if (termEntity == null)
                throw new HttpErrorCodeException(ErrorMessages.NotFound, HttpStatusCode.NotFound);

            var termCategory = m_catalogValueRepository.Load<TermCategory>(m_data.CategoryId);
            termEntity.Text = m_data.Name;
            termEntity.Position = m_data.Position;
            termEntity.TermCategory = termCategory;
            
            m_catalogValueRepository.Save(termEntity);

            return termEntity.Id;
        }
    }
}