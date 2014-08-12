﻿using System.Runtime.Serialization;

namespace ITJakub.MobileApps.DataContracts
{
    [DataContract]
    public enum AuthProvidersContract : byte
    {
        [EnumMember]
        ItJakub = 0,
        [EnumMember]
        Google = 1,
        [EnumMember]
        Facebook = 2,
        [EnumMember]
        LiveId = 3
    }
}