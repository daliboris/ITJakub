﻿using System;
using System.Collections.Generic;
using System.Linq;
using Castle.MicroKernel;
using ITJakub.DataEntities.Database;
using ITJakub.Shared.Contracts.Searching.Criteria;

namespace ITJakub.ITJakubService.Core.Search
{
    public class MetadataSearchCriteriaDirector
    {
        private readonly Dictionary<CriteriaKey, ICriteriaImplementationBase> m_criteriaImplementations;

        public MetadataSearchCriteriaDirector(IKernel container)
        {
            var criteria = container.ResolveAll<ICriteriaImplementationBase>();
            m_criteriaImplementations = criteria.ToDictionary(x => x.CriteriaKey);
        }

        public SearchCriteriaQuery ProcessCriteria(SearchCriteriaContract searchCriteriaContract, Dictionary<string, object> metadataParameters)
        {
            ICriteriaImplementationBase criteriaImplementation;
            if (!m_criteriaImplementations.TryGetValue(searchCriteriaContract.Key, out criteriaImplementation))
            {
                throw new ArgumentException("Criteria key not found");
            }
            return criteriaImplementation.CreateCriteriaQuery(searchCriteriaContract, metadataParameters);
        }

        public bool IsCriteriaSupported(SearchCriteriaContract searchCriteriaContract)
        {
            var criteriaKey = searchCriteriaContract.Key;
            return m_criteriaImplementations.ContainsKey(criteriaKey);
        }
    }
}
