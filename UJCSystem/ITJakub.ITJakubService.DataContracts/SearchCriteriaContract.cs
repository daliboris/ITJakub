﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ITJakub.ITJakubService.DataContracts
{
    [DataContract]
    [KnownType(typeof(DatingListCriteriaContract))]
    [KnownType(typeof(WordListCriteriaContract))]
    public abstract class SearchCriteriaContract
    {
        [DataMember]
        public CriteriaKey Key { get; set; }
    }

    [DataContract]
    public class DatingListCriteriaContract : SearchCriteriaContract
    {
        [DataMember]
        public IList<DatingCriteriaContract> Values { get; set; }
    }

    [DataContract]
    public class DatingCriteriaContract
    {
        [DataMember]
        public DateTime? NotBefore { get; set; }

        [DataMember]
        public DateTime? NotAfter { get; set; }
    }

    [DataContract]
    public class WordListCriteriaContract : SearchCriteriaContract
    {
        [DataMember]
        public IList<WordCriteriaContract> Values { get; set; }
    }

    [DataContract]
    public class WordCriteriaContract
    {
        [DataMember]
        public string StartsWith { get; set; }

        [DataMember]
        public IList<string> Contains { get; set; }

        [DataMember]
        public string EndsWith { get; set; }
    }

	[DataContract]
    public enum CriteriaKey
    {
        [EnumMember] Author = 0,
        [EnumMember] Title = 1,
        [EnumMember] Editor = 2,
        [EnumMember] Dating = 3,
        [EnumMember] Fulltext = 4,
        [EnumMember] Heading = 5,
        [EnumMember] Sentence = 6
    }
}