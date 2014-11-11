using System.Collections.Generic;
using Castle.MicroKernel;
using ITJakub.FileProcessing.Core.XSLT;

namespace ITJakub.FileProcessing.Core.Processors.Header
{
    public class MsIdentifierProcessor : ProcessorBase
    {
        public MsIdentifierProcessor(XsltTransformationManager xsltTransformationManager, IKernel container)
            : base(xsltTransformationManager, container)
        {
        }

        protected override string NodeName
        {
            get { return "msIdentifier"; }
        }

        protected override IEnumerable<ProcessorBase> SubProcessors
        {
            get
            {
                return new List<ProcessorBase>
                {
                    Container.Resolve<CountryProcessor>(),
                    Container.Resolve<SettlementProcessor>(),
                    Container.Resolve<RepositoryProcessor>(),
                    Container.Resolve<IdnoProcessor>(),
                };
            }
        }
    }
}