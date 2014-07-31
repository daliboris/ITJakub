﻿using System;
using System.Collections.Generic;
using Castle.MicroKernel;
using ITJakub.MobileApps.Core.Authentication;
using ITJakub.MobileApps.DataContracts;
using ITJakub.MobileApps.DataEntities;
using ITJakub.MobileApps.DataEntities.AzureTables.Daos;
using ITJakub.MobileApps.DataEntities.AzureTables.Entities;
using ITJakub.MobileApps.DataEntities.Database.Repositories;
using DE = ITJakub.MobileApps.DataEntities.Database.Entities;

namespace ITJakub.MobileApps.Core
{
    public class MobileServiceManager : IMobileAppsService
    {
        private readonly UsersRepository m_usersRepository;
        private readonly SynchronizedObjectRepository m_synchronizedObjectRepository;
        private readonly InstitutionRepository m_institutionRepository;
        private readonly GroupRepository m_groupRepository;
        private readonly TaskRepository m_taskRepository;
        private readonly ApplicationRepository m_applicationRepository;
        private readonly AzureTableTaskDao m_azureTableTaskDao;
        private readonly AzureTableSynchronizedObjectDao m_azureTableSynchronizedObjectDao;
        private readonly int m_maxAttemptsToSave;
        private readonly AuthenticationManager m_authenticationManager;
        private readonly UserManager m_userManager;

        public MobileServiceManager(IKernel container, int maxAttemptsToSave)
        {
            m_usersRepository = container.Resolve<UsersRepository>();
            m_synchronizedObjectRepository = container.Resolve<SynchronizedObjectRepository>();
            m_institutionRepository = container.Resolve<InstitutionRepository>();
            m_groupRepository = container.Resolve<GroupRepository>();
            m_taskRepository = container.Resolve<TaskRepository>();
            m_applicationRepository = container.Resolve<ApplicationRepository>();
            m_azureTableTaskDao = container.Resolve<AzureTableTaskDao>();
            m_azureTableSynchronizedObjectDao = container.Resolve<AzureTableSynchronizedObjectDao>();
            m_authenticationManager = container.Resolve<AuthenticationManager>();
            m_userManager = container.Resolve<UserManager>();
            m_maxAttemptsToSave = maxAttemptsToSave;
        }

        public void CreateInstitution(Institution institution)
        {
            var deInstitution = AutoMapper.Mapper.Map<DE.Institution>(institution);
            deInstitution.CreateTime = DateTime.UtcNow;
            var attempt = 0;
            while (attempt < m_maxAttemptsToSave)
            {
                try
                {
                    deInstitution.EnterCode = EnterCodeGenerator.GenerateCode();
                    m_institutionRepository.Create(deInstitution);
                    return;
                }
                catch (CreateEntityFailedException)
                {
                    ++attempt;
                }
            }
            //TODO throw exception or send fail message here
        }

        public InstitutionDetails GetInstitutionDetails(string institutionId)
        {
            var institution = m_institutionRepository.LoadInstitutionWithDetails(long.Parse(institutionId));
            return AutoMapper.Mapper.Map<InstitutionDetails>(institution);
        }

        public void AddUserToInstitution(string enterCode, string userId)
        {
            var institution = m_institutionRepository.FindByEnterCode(enterCode);
            var user = m_usersRepository.FindById(long.Parse(userId));
            user.Institution = institution;
            m_groupRepository.Update(user);
        }

        public void CreateUser(string authenticationProvider, string authenticationProviderToken, User user)
        {
            m_userManager.CreateAccount(authenticationProvider,authenticationProviderToken,user);
        }

      
        public LoginUserResponse LoginUser(UserLogin userLogin)
        {
            m_authenticationManager.AuthenticateByProvider(userLogin.Email, userLogin.AuthenticationToken, userLogin.AuthenticationProvider); //validate user's e-mail via authentication provider 
            return m_userManager.Login(userLogin);
        }

        [Obsolete("Have bad auto-mapper mapping")]
        public UserDetails GetUserDetails(string userId)
        {
            var user = m_usersRepository.LoadUserWithDetails(long.Parse(userId));
            return AutoMapper.Mapper.Map<UserDetails>(user);
        }

        public IEnumerable<TaskDetails> GetTasksForApplication(string applicationId)
        {
            var application = m_applicationRepository.FindById(long.Parse(applicationId));
            var tasks = m_taskRepository.LoadTasksWithDetailsByApplication(application);
            foreach (var task in tasks) //TODO try to find some better way how to fill Data property
            {
                var taskEntity = m_azureTableTaskDao.FindByRowAndPartitionKey(task.Id.ToString(), task.Application.Id.ToString());
                if (taskEntity != null) task.Data = taskEntity.Data;
            }
            return AutoMapper.Mapper.Map<IList<TaskDetails>>(tasks);
        }

        public void CreateTaskForApplication(string applicationId, string userId, Task apptask)
        {
            var application = m_applicationRepository.FindById(long.Parse(applicationId));
            var user = m_usersRepository.FindById(long.Parse(userId));
            var deTask = AutoMapper.Mapper.Map<DE.Task>(apptask);
            deTask.Application = application;
            deTask.Author = user;
            deTask.CreateTime = DateTime.UtcNow;
            var taskId = m_taskRepository.Create(deTask);
            m_azureTableTaskDao.Create(new TaskEntity(taskId.ToString(), applicationId, apptask.Data));
        }

        public CreateGroupResponse CreateGroup(string userId, string groupName)
        {
            var user = m_usersRepository.FindById(long.Parse(userId));
            var deGroup=new DE.Group {Author = user, CreateTime = DateTime.UtcNow, Name= groupName};
            var attempt = 0;
            while (attempt < m_maxAttemptsToSave)
            {
                try
                {
                    deGroup.EnterCode = EnterCodeGenerator.GenerateCode();
                    m_groupRepository.Create(deGroup);
                    return new CreateGroupResponse { EnterCode = deGroup.EnterCode };
                }
                catch (CreateEntityFailedException)
                {
                    ++attempt;
                }
            }
            return null; //TODO return exception here or fail message here
        }

        public void AssignTaskToGroup(string groupId, string taskId, string userId)
        {
            var group = m_groupRepository.FindById(long.Parse(groupId));
            var user = m_usersRepository.FindById(long.Parse(userId));
            if (!user.Equals(group.Author)) return;
            var task = m_taskRepository.FindById(long.Parse(taskId));
            group.Task = task;
            m_groupRepository.Update(group);
        }

        public void AddUserToGroup(string enterCode, string userId)
        {
            var group = m_groupRepository.FindByEnterCode(enterCode);
            var user = m_usersRepository.FindById(long.Parse(userId));
            group.Members.Add(user);
            m_groupRepository.Update(group);
        }

        public GroupDetails GetGroupDetails(string groupId)
        {
            var group = m_groupRepository.LoadGroupWithDetails(long.Parse(groupId));
            return AutoMapper.Mapper.Map<GroupDetails>(group);
        }

        public IEnumerable<SynchronizedObjectDetails> GetSynchronizedObjects(string groupId, string applicationId, string objectType, string since)
        {
            var group = m_groupRepository.FindById(long.Parse(groupId));
            var application = m_applicationRepository.FindById(long.Parse(applicationId));
            var sinceTime = DateTime.Parse(since);
            var syncObjs = m_synchronizedObjectRepository.LoadSyncObjectsWithDetails(group, application, objectType, sinceTime);
            foreach (var syncObj in syncObjs) //TODO try to find some better way how to fill Data property
            {
                var syncObjEntity = m_azureTableSynchronizedObjectDao.FindByRowAndPartitionKey(syncObj.Id.ToString(), syncObj.Group.Id.ToString());
                if (syncObjEntity != null) syncObj.Data = syncObjEntity.Data;
            }
            return AutoMapper.Mapper.Map<IList<SynchronizedObjectDetails>>(syncObjs);
        }

        public void CreateSynchronizedObject(string groupId, string applicationId, string userId, SynchronizedObject synchronizedObject)
        {
            var application = m_applicationRepository.FindById(long.Parse(applicationId));
            var user = m_usersRepository.FindById(long.Parse(userId));
            var group = m_groupRepository.FindById(long.Parse(userId));
            var deSyncObject = AutoMapper.Mapper.Map<DE.SynchronizedObject>(synchronizedObject);
            deSyncObject.Application = application;
            deSyncObject.Author = user;
            deSyncObject.Group = group;
            deSyncObject.CreateTime = DateTime.UtcNow;
            var syncObjId = m_synchronizedObjectRepository.Create(deSyncObject);
            m_azureTableSynchronizedObjectDao.Create(new SynchronizedObjectEntity(syncObjId.ToString(), groupId, synchronizedObject.Data));
        }
    }
}