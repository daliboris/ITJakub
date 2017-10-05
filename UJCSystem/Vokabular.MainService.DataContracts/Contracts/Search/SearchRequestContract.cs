﻿using System.Collections.Generic;
using Newtonsoft.Json;
using Vokabular.MainService.DataContracts.Utils;
using Vokabular.Shared.DataContracts.Search.Criteria;
using Vokabular.Shared.DataContracts.Types;

namespace Vokabular.MainService.DataContracts.Contracts.Search
{
    public class SearchRequestContract
    {
        public int? Start { get; set; }

        public int? Count { get; set; }

        public HitSettingsContract HitSettingsContract { get; set; }

        public TermsSettingsContract TermsSettingsContract { get; set; }

        public SortTypeEnumContract? Sort { get; set; }

        public SortDirectionEnumContract? SortDirection { get; set; }

        [JsonConverter(typeof(SearchCriteriaJsonConverter))]
        public IList<SearchCriteriaContract> ConditionConjunction { get; set; }
    }

    public class HitSettingsContract
    {
        public int? Count { get; set; }

        public int? Start { get; set; }

        public int ContextLength { get; set; }
    }

    public class TermsSettingsContract
    {
        // count and start ommited because missing use case. This contract is used as request for loading terms.
    }
}