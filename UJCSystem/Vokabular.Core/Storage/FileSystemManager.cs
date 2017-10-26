﻿using System;
using System.Collections.Generic;
using System.IO;
using log4net;
using Microsoft.Extensions.Options;
using Vokabular.Core.Storage.PathResolvers;
using Vokabular.Core.Storage.Resources;
using Vokabular.Shared.DataContracts.Types;

namespace Vokabular.Core.Storage
{
    public class FileSystemManager
    {
        private static readonly ILog m_log = LogManager.GetLogger(typeof(FileSystemManager));

        private readonly Dictionary<ResourceType, IResourceTypePathResolver> m_resourceTypePathResolvers;
        private readonly string m_rootFolderPath;

        public FileSystemManager(IList<IResourceTypePathResolver> resourceTypePathResolvers, IOptions<PathConfiguration> pathOptions)
        {
            m_rootFolderPath = pathOptions.Value.FileStorageRootFolder;
            m_resourceTypePathResolvers = new Dictionary<ResourceType, IResourceTypePathResolver>();
            foreach (var pathResolver in resourceTypePathResolvers)
            {
                m_resourceTypePathResolvers.Add(pathResolver.ResolvingResourceType(), pathResolver);
            }
        }

        public Stream GetResource(string bookId, string bookVersionId, string fileName, ResourceType resourceType)
        {
            var pathResolver = GetPathResolver(resourceType);
            var relativePath = pathResolver.ResolvePath(bookId, bookVersionId, fileName);
            var fullPath = GetFullPath(relativePath);
            if (File.Exists(fullPath))
                return File.Open(fullPath, FileMode.Open, FileAccess.Read, FileShare.Read);

            if (m_log.IsWarnEnabled)
                m_log.WarnFormat("Trying to access non existing resource file: '{0}'", fullPath);

            return Stream.Null;
        }

        public void SaveResource(string bookId, string bookVersionId, FileResource resource)
        {
            var pathResolver = GetPathResolver(resource.ResourceType);

            if (pathResolver == null)
            {
                var message = $"Resource with type '{resource.ResourceType}' does not have rule for resolving path and will be skipped";
                if (m_log.IsWarnEnabled)
                    m_log.WarnFormat(message);
                return;
            }
            var relativePath = pathResolver.ResolvePath(bookId, bookVersionId, resource.FileName);
            var fullPath = GetFullPath(relativePath);
            CreateDirsIfNotExist(fullPath);
            using (var sourceStream = File.Open(resource.FullPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var writeStream = File.Create(fullPath))
                {
                    sourceStream.CopyTo(writeStream);
                }
            }
        }

        private string GetFullPath(string relativePath)
        {
            return Path.Combine(m_rootFolderPath, relativePath);
        }

        private void CreateDirsIfNotExist(string path)
        {
            var dirPath = Path.GetDirectoryName(path);
            if (dirPath == null) return;
            if (!Directory.Exists(path))
            {
                if (m_log.IsInfoEnabled)
                    m_log.InfoFormat("Creating directory for resources in: '{0}'", dirPath);

                try
                {
                    Directory.CreateDirectory(dirPath);
                }
                catch (IOException ex)
                {
                    if (m_log.IsFatalEnabled)
                        m_log.FatalFormat("Cannot create directory on path: '{0}'. Exception: '{1}'", dirPath,
                            ex);
                    throw;
                }
                catch (UnauthorizedAccessException ex)
                {
                    if (m_log.IsFatalEnabled)
                        m_log.FatalFormat("Cannot create directory on path: '{0}'. Exception: '{1}'", dirPath,
                            ex);
                    throw;
                }
                catch (NotSupportedException ex)
                {
                    if (m_log.IsFatalEnabled)
                        m_log.FatalFormat("Cannot create directory on path: '{0}'. Exception: '{1}'", dirPath,
                            ex);
                    throw;
                }
                catch (ArgumentException ex)
                {
                    if (m_log.IsFatalEnabled)
                        m_log.FatalFormat("Cannot create directory on path: '{0}'. Exception: '{1}'", dirPath,
                            ex);
                    throw;
                }
            }
        }

        private IResourceTypePathResolver GetPathResolver(ResourceType resourceType)
        {
            IResourceTypePathResolver pathResolver;
            m_resourceTypePathResolvers.TryGetValue(resourceType, out pathResolver);
          
            return pathResolver;
        }
    }
}