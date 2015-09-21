/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:ITJakub.MobileApps.MainApp"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using System.Linq;
using System.Reflection;
using GalaSoft.MvvmLight;
using ITJakub.MobileApps.Client.Core;
using ITJakub.MobileApps.Client.Core.Service;
using ITJakub.MobileApps.Client.MainApp.View.Groups;
using ITJakub.MobileApps.Client.MainApp.ViewModel.GroupList;
using ITJakub.MobileApps.Client.MainApp.ViewModel.GroupPage;
using ITJakub.MobileApps.Client.MainApp.ViewModel.Login;
using ITJakub.MobileApps.Client.MainApp.ViewModel.Login.UserMenu;
using ITJakub.MobileApps.Client.MainApp.ViewModel.News;
using Microsoft.Practices.Unity;

namespace ITJakub.MobileApps.Client.MainApp.ViewModel
{
    /// <summary>
    ///     This class contains static references to all the view models in the
    ///     application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        ///     Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            if (ViewModelBase.IsInDesignModeStatic)
            {
                Container.Current.RegisterType<IDataService, DesignDataService>(new ContainerControlledLifetimeManager());
                // Create design time view services and models
            }
            else
            {
                Container.Current.RegisterType<IDataService, DataService>(new ContainerControlledLifetimeManager());
                // Create run time view services and models
            }

            Container.Current.RegisterTypes(
                AllClasses.FromApplication().Where(x => x.GetTypeInfo().IsSubclassOf(typeof (ViewModelBase))),
                WithMappings.None, 
                WithName.Default, 
                WithLifetime.Transient
                );
            Container.Current.RegisterType<INavigationService, NavigationService>(WithLifetime.ContainerControlled(typeof(NavigationService)));
        }

        public LoginViewModel LoginViewModel
        {
            get { return Container.Current.Resolve<LoginViewModel>(); }
        }

        public RegistrationViewModel RegistrationViewModel
        {
            get { return Container.Current.Resolve<RegistrationViewModel>(); }
        }

        public GroupListViewModel GroupListViewModel
        {
            get { return Container.Current.Resolve<GroupListViewModel>(); }
        }

        public NewsViewModel NewsViewModel
        {
            get { return Container.Current.Resolve<NewsViewModel>(); }
        }

        public ApplicationHostViewModel ApplicationHostViewModel
        {
            get { return Container.Current.Resolve<ApplicationHostViewModel>(); }
        }

        public SelectApplicationViewModel SelectApplicationViewModel
        {
            get { return Container.Current.Resolve<SelectApplicationViewModel>(); }
        }

        public GroupPageViewModel GroupPageViewModel
        {
            get { return Container.Current.Resolve<GroupPageViewModel>(); }
        }

        public UserMenuViewModel UserMenuViewModel
        {
            get { return Container.Current.Resolve<UserMenuViewModel>(); }
        }

        public SelectTaskViewModel SelectTaskViewModel
        {
            get { return Container.Current.Resolve<SelectTaskViewModel>(); }
        }

        public EditorHostViewModel EditorHostViewModel
        {
            get { return Container.Current.Resolve<EditorHostViewModel>(); }
        }

        public OwnedTaskListViewModel OwnedTaskListViewModel
        {
            get { return Container.Current.Resolve<OwnedTaskListViewModel>(); }
        }

        public UserAccountSettingsViewModel UserAccountSettingsViewModel
        {
            get { return Container.Current.Resolve<UserAccountSettingsViewModel>(); }
        }

        public TaskPreviewHostViewModel TaskPreviewHostViewModel
        {
            get { return Container.Current.Resolve<TaskPreviewHostViewModel>(); }
        }

        public AdminHostViewModel AdminHostViewModel
        {
            get { return Container.Current.Resolve<AdminHostViewModel>(); }
        }

        public SettingViewModel SettingViewModel
        {
            get { return Container.Current.Resolve<SettingViewModel>(); }
        }

        public ApplicationMenuPageViewModel ApplicationMenuPageViewModel
        {
            get { return Container.Current.Resolve<ApplicationMenuPageViewModel>(); }
        }

        public AdminGroupListViewModel AdminGroupListViewModel
        {
            get { return Container.Current.Resolve<AdminGroupListViewModel>(); }
        }

        public MyGroupListViewModel MyGroupListViewModel
        {
            get { return Container.Current.Resolve<MyGroupListViewModel>(); }
        }
    }
}