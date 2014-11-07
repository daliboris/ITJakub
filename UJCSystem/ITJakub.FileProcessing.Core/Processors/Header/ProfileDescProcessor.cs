using System.Collections.Generic;
using Castle.MicroKernel;
using ITJakub.FileProcessing.Core.XSLT;

namespace ITJakub.FileProcessing.Core.Processors.Header
{
    public class ProfileDescProcessor : ProcessorBase
    {
        public ProfileDescProcessor(XsltTransformationManager xsltTransformationManager, IKernel container)
            : base(xsltTransformationManager, container)
        {
        }

        protected override string NodeName
        {
            get { return "profileDesc"; }
        }

        protected override IEnumerable<ProcessorBase> SubProcessors
        {
            get { return new List<ProcessorBase>(); } //TODO
        }
    }
}