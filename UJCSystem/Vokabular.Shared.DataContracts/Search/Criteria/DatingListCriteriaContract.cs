using System.Collections.Generic;
using System.Runtime.Serialization;
using Vokabular.Shared.DataContracts.Search.CriteriaItem;

namespace Vokabular.Shared.DataContracts.Search.Criteria
{
    [DataContract]
    public class DatingListCriteriaContract : SearchCriteriaContract
    {
        [DataMember]
        public IList<DatingCriteriaContract> Disjunctions { get; set; }
    }
}