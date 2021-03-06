using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ITJakub.Shared.Contracts.Searching.Criteria
{
    [DataContract]
    public class DatingListCriteriaContract : SearchCriteriaContract
    {
        [DataMember]
        public IList<DatingCriteriaContract> Disjunctions { get; set; }
    }
}