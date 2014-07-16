using System;
using ITJakub.MobileApps.MainApp.Enum;
using ITJakub.MobileApps.MainApp.Manager;

namespace ITJakub.MobileApps.MainApp.ViewModel
{
    public class DataService : IDataService
    {
        private LoginManager m_loginManager;

        public DataService()
        {
            m_loginManager = new LoginManager();
        }

        public async void LoginAsync(LoginProvider loginProvider, Action<UserInfo, Exception> callback)
        {
            UserInfo userInfo = null;
            switch (loginProvider)
            {
                case LoginProvider.LiveId:
                    userInfo = await m_loginManager.LoginLiveId();
                    break;
                case LoginProvider.Facebook:
                    userInfo = await m_loginManager.LoginFacebookAsync();
                    break;
                case LoginProvider.Google:
                    userInfo = await m_loginManager.LoginGoogle();
                    break;
            }

            if (userInfo != null)
            {
                callback(userInfo, null);
            }
        }
    }
}