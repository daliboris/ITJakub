using System.Collections.Generic;
using Castle.MicroKernel;

namespace ITJakub.FileProcessing.Core.Processors.Header
{
    public class TitleStmtProcessor : ProcessorBase
    {
        public TitleStmtProcessor(IKernel container) : base(container)
        {
        }

        protected override string NodeName
        {
            get { return "titleStmt"; }
        }

        protected override IEnumerable<ProcessorBase> SubProcessors
        {
            get
            {
                return new List<ProcessorBase>
                {
                    Container.Resolve<TitleProcessor>(),
                    Container.Resolve<AuthorProcessor>()
                };
            }
        }
    }
}