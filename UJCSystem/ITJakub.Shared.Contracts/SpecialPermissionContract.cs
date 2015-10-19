﻿using System.Runtime.Serialization;

namespace ITJakub.Shared.Contracts
{
    [DataContract]
    [KnownType(typeof (NewsPermissionContract))]
    [KnownType(typeof (UploadBookPermissionContract))]
    [KnownType(typeof (ManagePermissionsPermissionContract))]
    [KnownType(typeof (FeedbackPermissionContract))]
    [KnownType(typeof (CardFilePermissionContract))]
    [KnownType(typeof (AutoImportCategoryPermissionContract))]
    public class SpecialPermissionContract
    {
        [DataMember]
        public int Id { get; set; }
    }

    [DataContract]
    public class NewsPermissionContract : SpecialPermissionContract
    {
        [DataMember]
        public bool CanAddNews { get; set; }
    }

    [DataContract]
    public class UploadBookPermissionContract : SpecialPermissionContract
    {
        [DataMember]
        public bool CanUploadBook { get; set; }
    }

    [DataContract]
    public class ManagePermissionsPermissionContract : SpecialPermissionContract
    {
        [DataMember]
        public bool CanManagePermissions { get; set; }
    }

    [DataContract]
    public class FeedbackPermissionContract : SpecialPermissionContract
    {
        [DataMember]
        public bool CanManageFeedbacks { get; set; }
    }

    [DataContract]
    public class CardFilePermissionContract : SpecialPermissionContract
    {
        [DataMember]
        public bool CanReadCardFile { get; set; }

        [DataMember]
        public string CardFileId { get; set; }

        [DataMember]
        public string CardFileName { get; set; }
    }

    [DataContract]
    public class AutoImportCategoryPermissionContract : SpecialPermissionContract
    {
        [DataMember]
        public bool AutoImportIsAllowed { get; set; }

        [DataMember]
        public CategoryContract Category { get; set; }
    }
}