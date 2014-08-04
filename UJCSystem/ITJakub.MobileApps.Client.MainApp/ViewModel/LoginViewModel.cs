using System.Collections.ObjectModel;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ITJakub.MobileApps.Client.Core.DataService;
using ITJakub.MobileApps.Client.Core.Error;
<<<<<<< HEAD
using ITJakub.MobileApps.Client.Core.Manager;
using ITJakub.MobileApps.Client.Core.Manager.Authentication;
=======
using ITJakub.MobileApps.Client.Core.Manager.Authentication;
using ITJakub.MobileApps.Client.Core.ViewModel.Authentication;
>>>>>>> 76f07b70317554fb477fd5225878b9cf1ddc05ba
using ITJakub.MobileApps.Client.MainApp.View;
using ITJakub.MobileApps.Client.MainApp.View.Login;

namespace ITJakub.MobileApps.Client.MainApp.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IDataService m_dataService;
        private readonly INavigationService m_navigationService;
        private Visibility m_loginDialogVisibility;
        private bool m_loggingIn;

        public LoginViewModel(IDataService dataService, INavigationService navigationService)
        {      
            m_dataService = dataService;
            m_navigationService = navigationService;
            LoggingIn = false;
            LoadInitData();
            ItemClickCommand = new RelayCommand<ItemClickEventArgs>(ItemClick);
            RegistrationCommand = new RelayCommand(() => m_navigationService.Navigate(typeof(RegistrationView)));
        }

        private void LoadInitData()
        {
            LoginProviders = new ObservableCollection<LoginProviderViewModel>();
            m_dataService.GetLoginProviders((loginproviders, exception) =>
            {
                if (exception != null)
                    return;

                foreach (LoginProviderViewModel loginProvider in loginproviders)
                {
                    LoginProviders.Add(loginProvider);
                }
            });
        }


        public ObservableCollection<LoginProviderViewModel> LoginProviders { get; set; }

        public RelayCommand<ItemClickEventArgs> ItemClickCommand { get; private set; }

        public RelayCommand RegistrationCommand { get; private set; }

        public Visibility LoginDialogVisibility
        {
            get { return m_loginDialogVisibility; }
            set
            {
                m_loginDialogVisibility = value;
                RaisePropertyChanged();
            }
        }

        public bool LoggingIn
        {
            get { return m_loggingIn; }
            set
            {
                m_loggingIn = value;
                LoginDialogVisibility = value ? Visibility.Visible : Visibility.Collapsed;
                RaisePropertyChanged();
            }
        }

        private void ItemClick(ItemClickEventArgs args)
        {
            var item = args.ClickedItem as LoginProviderViewModel;
            if (item == null)
                return;

            Login(item.LoginProviderType);
        }

        private void Login(LoginProviderType loginProviderType)
        {
            LoggingIn = true;
            m_dataService.Login(loginProviderType, (info, exception) =>
            {
                LoggingIn = false;
                if (exception != null)
                {
                    if (exception is UserNotRegisteredException)
                        new MessageDialog("Pro p�ihl�en� do aplikace je nutn� se nejd��ve registrovat.", "U�ivatel nen� registrov�n").ShowAsync();
                    return;
                }
                if (info.Success)
                    m_navigationService.Navigate(typeof(GroupListView));
            });
        }
    }
}