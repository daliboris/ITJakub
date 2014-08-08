using System;

namespace ITJakub.MobileApps.Client.Core.Manager.Authentication
{
    public class LoginResult
    {
        public string CommunicationToken { get; set; }
        public DateTime EstimatedExpirationTime { get; set; }
        public long UserId { get; set; }

        public string UserAvatarUrl { get; set; }

        public UserRole UserRole { get; set; }
    }
}