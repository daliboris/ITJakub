﻿using System.Runtime.Serialization;

namespace ITJakub.MobileApps.MobileContracts
{
    [DataContract]
    public enum BookTypeContract : byte
    {
        [EnumMember]
        Edition = 0,
        
        [EnumMember]
        Dictionary = 1,
        
        [EnumMember]
        Grammar = 2,

        [EnumMember]
        ProfessionalLiterature = 3
    }
}