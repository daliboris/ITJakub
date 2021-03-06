using Vokabular.DataEntities;
using Vokabular.MainService.Core;
using Vokabular.Shared.Container;

namespace Vokabular.MainService
{
    public class MainServiceContainerRegistration : IContainerInstaller
    {
        public void Install(IIocContainer container)
        {
            new MainServiceCoreContainerRegistration().Install(container);
            new DataEntitiesContainerRegistration().Install(container);
        }
    }
}