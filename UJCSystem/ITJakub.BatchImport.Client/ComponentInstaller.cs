﻿using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ITJakub.BatchImport.Client.BusinessLogic;
using ITJakub.BatchImport.Client.BusinessLogic.Communication;

namespace ITJakub.BatchImport.Client
{
    public class ComponentInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {            
            container.Register(Component.For<FileUploadManager>());
            container.Register(Component.For<CommunicationProvider>());
        }
    }
}