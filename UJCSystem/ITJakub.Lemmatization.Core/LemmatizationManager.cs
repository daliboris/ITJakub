﻿using System.Collections.Generic;
using AutoMapper;
using ITJakub.Lemmatization.DataEntities;
using ITJakub.Lemmatization.DataEntities.Repositories;
using ITJakub.Lemmatization.Shared.Contracts;

namespace ITJakub.Lemmatization.Core
{
    public class LemmatizationManager
    {
        private readonly LemmatizationRepository m_repository;
        private const int PrefetchRecordCount = 10;

        public LemmatizationManager(LemmatizationRepository repository)
        {
            m_repository = repository;
        }

        private string EscapeQuery(string query)
        {
            return query.Replace("[", "[[]")
                .Replace("%", "[%]")
                .Replace("_", "[_]");
        }

        public IList<TokenContract> GetTypeaheadToken(string query)
        {
            query = EscapeQuery(query);
            var result = m_repository.GetTypeaheadToken(query, PrefetchRecordCount);
            return Mapper.Map<IList<TokenContract>>(result);
        }

        public long CreateToken(string token, string description)
        {
            var newToken = new Token
            {
                Text = token,
                Description = description
            };

            var id = m_repository.Create(newToken);
            return (long) id;
        }

        public IList<TokenCharacteristicContract> GetTokenCharacteristic(long tokenId)
        {
            var result = m_repository.GetTokenCharacteristicDetail(tokenId);
            return Mapper.Map<IList<TokenCharacteristicContract>>(result);
        }

        public long AddTokenCharacteristic(long tokenId, string morphologicalCharacteristic, string description)
        {
            var tokenEntity = m_repository.Load<Token>(tokenId);
            var newTokenCharacteristic = new TokenCharacteristic
            {
                MorphologicalCharakteristic = morphologicalCharacteristic,
                Description = description,
                Token = tokenEntity
            };

            var id = m_repository.Create(newTokenCharacteristic);
            return (long) id;
        }

        public IList<CanonicalFormContract> GetTypeaheadCannonicalForm(CanonicalFormTypeContract type, string query)
        {
            query = EscapeQuery(query);
            var canonicalFormType = Mapper.Map<CanonicalFormType>(type);
            var result = m_repository.GetTypeaheadCannonicalForm(canonicalFormType, query, PrefetchRecordCount);
            return Mapper.Map<IList<CanonicalFormContract>>(result);
        }

        public IList<HyperCanonicalFormContract> GetTypeaheadHyperCannonicalForm(string query)
        {
            query = EscapeQuery(query);
            var result = m_repository.GetTypeaheadHyperCannonicalForm(query, PrefetchRecordCount);
            return Mapper.Map<IList<HyperCanonicalFormContract>>(result);
        }

        public long CreateCanonicalForm(long tokenCharacteristicId, CanonicalFormTypeContract type, string text, string description)
        {
            var tokenCharacteristic = m_repository.Load<TokenCharacteristic>(tokenCharacteristicId);
            var canonicalFormType = Mapper.Map<CanonicalFormType>(type);
            var newCanonicalForm = new CanonicalForm
            {
                Type = canonicalFormType,
                Text = text,
                Description = description,
                CanonicalFormFor = new List<TokenCharacteristic> {tokenCharacteristic}
            };

            var id = m_repository.Create(newCanonicalForm);
            return (long) id;
        }

        public void AddCanonicalForm(long tokenCharacteristicId, long canonicalFormId)
        {
            var tokenCharacteristic = m_repository.GetTokenCharacteristicWithCanonicalForms(tokenCharacteristicId);
            var cannonicalForm = m_repository.Load<CanonicalForm>(canonicalFormId);

            tokenCharacteristic.CanonicalForms.Add(cannonicalForm);
            m_repository.Update(tokenCharacteristic);
        }
    }
}
