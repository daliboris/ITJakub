﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ITJakub.MobileApps.Client.Core.Manager.Groups;

namespace ITJakub.MobileApps.Client.Core.ViewModel
{
    public class GroupInfoViewModel : ViewModelBase
    {
        private ObservableCollection<GroupMemberViewModel> m_members;
        private int m_memberCount;
        private string m_searchText;

        public GroupInfoViewModel()
        {
            Icon = new BitmapImage(new Uri("ms-appx:///Icon/group-64.png"));

            SearchText = string.Empty;
            SearchCommand = new RelayCommand(() => RaisePropertyChanged(() => FilteredMembers));
        }

        public string GroupName { get; set; }
        public string GroupCode { get; set; }
        public BitmapImage Icon { get; set; }
        public long GroupId { get; set; }
        public GroupType GroupType { get; set; }
        public DateTime CreateTime { get; set; }
        public TaskViewModel Task { get; set; }

        public RelayCommand SearchCommand { get; private set; }

        public string SearchText
        {
            get { return m_searchText; }
            set
            {
                m_searchText = value;
                RaisePropertyChanged();
            }
        }

        public int MemberCount
        {
            get { return m_memberCount; }
            set
            {
                m_memberCount = value;
                RaisePropertyChanged();
                RaisePropertyChanged(() => NoMembersVisibility);
            }
        }

        public ObservableCollection<GroupMemberViewModel> Members
        {
            get { return m_members; }
            set
            {
                if (m_members != null)
                    m_members.CollectionChanged -= MembersChanged;

                m_members = value;
                m_members.CollectionChanged += MembersChanged;
                RaisePropertyChanged();
                RaisePropertyChanged(() => FilteredMembers);
            }
        }

        private void MembersChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            RaisePropertyChanged(() => FilteredMembers);
        }

        public IEnumerable<GroupMemberViewModel> FilteredMembers
        {
            get
            {
                return m_members.Where(member =>
                {
                    var fullName = string.Format("{0} {1}", member.FirstName, member.LastName).ToLower();
                    return fullName.Contains(SearchText.ToLower());
                });
            }
        } 

        public Visibility NoMembersVisibility
        {
            get { return m_memberCount == 0 ? Visibility.Visible : Visibility.Collapsed; }
        }
    }
}