﻿using System.Linq;
using System.Threading.Tasks;
using ITJakub.FileProcessing.Core.Sessions.Processors;
using ITJakub.Shared.Contracts.Resources;

namespace ITJakub.FileProcessing.Core.Sessions
{
    public class ResourceProcessorManager
    {
        private readonly AudioBookArchiveProcessor m_audiobookArchiveProcessor;
        private readonly ExistDbStoreProcessor m_existDbStoreProcessor;
        private readonly ExtractableArchiveProcessor m_extractableArchiveProcessor;
        private readonly FileDbStoreProcessor m_fileDbStoreProcessor;
        private readonly MetadataProcessor m_metadataProcessor;
        private readonly RelationalDbStoreProcessor m_relationalDbStoreProcessor;
        private readonly TransformationsProcessor m_transformationsProcessor;
        private readonly XmlConversionProcessor m_xmlConversionProcessor;

        public ResourceProcessorManager(XmlConversionProcessor xmlConversionProcessor,
            MetadataProcessor metadataProcessor, RelationalDbStoreProcessor relationalDbStoreProcessor,
            FileDbStoreProcessor fileDbStoreProcessor, ExistDbStoreProcessor existDbStoreProcessor,
            ExtractableArchiveProcessor extractableArchiveProcessor, TransformationsProcessor transformationsProcessor, 
            AudioBookArchiveProcessor audiobookArchiveProcessor)
        {
            m_xmlConversionProcessor = xmlConversionProcessor;
            m_metadataProcessor = metadataProcessor;
            m_relationalDbStoreProcessor = relationalDbStoreProcessor;
            m_fileDbStoreProcessor = fileDbStoreProcessor;
            m_existDbStoreProcessor = existDbStoreProcessor;
            m_extractableArchiveProcessor = extractableArchiveProcessor;
            m_transformationsProcessor = transformationsProcessor;
            m_audiobookArchiveProcessor = audiobookArchiveProcessor;
        }

        public bool ProcessSessionResources(ResourceSessionDirector resourceDirector)
        {
            if (resourceDirector.Resources.Any(x => x.ResourceType == ResourceType.ExtractableArchive))
                ProcessExtractableArchive(resourceDirector);

            if (resourceDirector.Resources.Any(x => x.ResourceType == ResourceType.SourceDocument))
                ProcessXmlConversion(resourceDirector); //call of library to convert docx to xml resources which are added to resources in paramater

            ProcessMetaData(resourceDirector); //obtain entity information from processing metadata

            ProcessTransformations(resourceDirector); //Process Transformations

            if (resourceDirector.Resources.Any(x => x.ResourceType == ResourceType.Audio))
                GenerateFullBooksResources(resourceDirector); //GenerateFull audio records

            var existTask = Task.Factory.StartNew(() => ProcessExistDbStore(resourceDirector)); //saves xmls to Exist
            var resourceTask = Task.Factory.StartNew(() => ProcessFileDbStore(resourceDirector)); //saves images, docx etc on physical disk
            Task.WaitAll(existTask, resourceTask);

            ProcessRelationalDbStore(resourceDirector); //if everything was ok then saves entity into relational database
            //TODO add try catch with rollback and return false
            return true;
        }

        private void GenerateFullBooksResources(ResourceSessionDirector resourceDirector)
        {
            m_audiobookArchiveProcessor.Process(resourceDirector);
        }

        private void ProcessTransformations(ResourceSessionDirector resourceDirector)
        {
            m_transformationsProcessor.Process(resourceDirector);
        }

        private void ProcessExtractableArchive(ResourceSessionDirector resourceDirector)
        {
            m_extractableArchiveProcessor.Process(resourceDirector);
        }

        private void ProcessFileDbStore(ResourceSessionDirector resourceDirector)
        {
            m_fileDbStoreProcessor.Process(resourceDirector);
        }

        private void ProcessExistDbStore(ResourceSessionDirector resourceDirector)
        {
            m_existDbStoreProcessor.Process(resourceDirector);
        }

        private void ProcessRelationalDbStore(ResourceSessionDirector resourceDirector)
        {
            m_relationalDbStoreProcessor.Process(resourceDirector);
        }

        private void ProcessMetaData(ResourceSessionDirector resourceDirector)
        {
            m_metadataProcessor.Process(resourceDirector);
        }

        private void ProcessXmlConversion(ResourceSessionDirector resourceDirector)
        {
            m_xmlConversionProcessor.Process(resourceDirector);
        }
    }
}