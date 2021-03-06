﻿using AutoMapper;
using Vokabular.MainService.Core.AutoMapperProfiles;
using Vokabular.MainService.Core.Communication;
using Vokabular.MainService.Core.Managers;
using Vokabular.Shared.Container;

namespace Vokabular.MainService.Core
{
    public class MainServiceCoreContainerRegistration : IContainerInstaller
    {
        public void Install(IIocContainer container)
        {
            container.AddPerWebRequest<CategoryManager>();
            container.AddPerWebRequest<PersonManager>();
            container.AddPerWebRequest<ProjectManager>();
            container.AddPerWebRequest<ProjectMetadataManager>();
            container.AddPerWebRequest<ProjectResourceManager>();
            container.AddPerWebRequest<UserManager>();

            container.AddPerWebRequest<CommunicationConfigurationProvider>();
            container.AddPerWebRequest<CommunicationProvider>();

            container.AddSingleton<Profile, CategoryProfile>();
            container.AddSingleton<Profile, LiteraryGenreProfile>();
            container.AddSingleton<Profile, LiteraryKindProfile>();
            container.AddSingleton<Profile, MetadataProfile>();
            container.AddSingleton<Profile, OriginalAuthorProfile>();
            container.AddSingleton<Profile, ProjectProfile>();
            container.AddSingleton<Profile, PublisherProfile>();
            container.AddSingleton<Profile, ResponsiblePersonProfile>();
            container.AddSingleton<Profile, UserProfile>();
        }
    }
}
