﻿using System;
using Windows.UI.Xaml.Media.Imaging;
using ITJakub.MobileApps.Client.Hangman.DataService;
using ITJakub.MobileApps.Client.Hangman.View;
using ITJakub.MobileApps.Client.Hangman.ViewModel;
using ITJakub.MobileApps.Client.Shared;
using ITJakub.MobileApps.Client.Shared.Communication;
using ITJakub.MobileApps.Client.Shared.Enum;
using ITJakub.MobileApps.Client.Shared.ViewModel;

namespace ITJakub.MobileApps.Client.Hangman
{
    [MobileApplication(ApplicationType.Hangman)]
    public class AppInfo : ApplicationBase
    {
        private readonly HangmanDataService m_dataService;

        public AppInfo(ISynchronizeCommunication applicationCommunication) : base(applicationCommunication)
        {
            m_dataService = new HangmanDataService(applicationCommunication);
        }

        public override string Name
        {
            get { return "Šibenice"; }
        }

        public override ApplicationBaseViewModel ApplicationViewModel
        {
            get { return new HangmanViewModel(m_dataService); }
        }

        public override Type ApplicationDataTemplate
        {
            get { return typeof (HangmanView); }
        }

        public override EditorBaseViewModel EditorViewModel
        {
            get { return new HangmanEditorViewModel(m_dataService); }
        }

        public override Type EditorDataTemplate
        {
            get { return typeof(HangmanEditorView); }
        }

        public override AdminBaseViewModel AdminViewModel
        {
            get { return new HangmanAdminViewModel(m_dataService); }
        }

        public override Type AdminDataTemplate
        {
            get { return typeof (HangmanAdminView); }
        }

        public override TaskPreviewBaseViewModel TaskPreviewViewModel
        {
            get { return new HangmanTaskPreviewViewModel(m_dataService); }
        }

        public override Type TaskPreviewDataTemplate
        {
            get { return typeof (HangmanPreviewView); }
        }

        public override ApplicationRoleType ApplicationRoleType
        {
            get { return ApplicationRoleType.MainApp;}
        }

        public override ApplicationCategory ApplicationCategory
        {
            get { return ApplicationCategory.Game; }
        }

        public override bool IsChatSupported
        {
            get { return true; }
        }

        public override BitmapImage Icon
        {
            get
            {
                var uri = new Uri(BaseUri, "Icon/scythe-128.png");
                return new BitmapImage(uri);
            }
        }
    }
}
