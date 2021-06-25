using Autofac;
using Saving_Accelerator_Tools2.DataBaseIServices.Others;
using Saving_Accelerator_Tools2.DataBaseServices.Data;
using Saving_Accelerator_Tools2.DataBaseServices.Others;
using Saving_Accelerator_Tools2.IServices.MessageBox;
using Saving_Accelerator_Tools2.MessageBoxes;
using Saving_Accelerator_Tools2.ViewModel.Actions;
using Saving_Accelerator_Tools2.ViewModel.Others;

namespace Saving_Accelerator_Tools2.ViewModelLocator
{
    public class ViewModelsLocator
    {
        private readonly IContainer container;

        public ViewModelsLocator()
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();

            //DataBase Services
            containerBuilder.RegisterType<ConnectionContext>().SingleInstance();
            containerBuilder.RegisterType<FactoriesServices>().As<IFactoriesServices>();
            containerBuilder.RegisterType<DevisionServices>().As<IDevisionServices>();
            containerBuilder.RegisterType<LeadersServices>().As<ILeadersServices>();
            containerBuilder.RegisterType<MessageBoxServices>().As<IMessageBoxService>();

            //ViewModels
            containerBuilder.RegisterType<MainFilterViewModel>();

            //AdminParts
            containerBuilder.RegisterType<FactoriesViewModel>();
            containerBuilder.RegisterType<DevisionsViewModel>();
            containerBuilder.RegisterType<LeadersViewModel>();

            container = containerBuilder.Build();

        }

        public MainFilterViewModel MainFilterViewModel => container.Resolve<MainFilterViewModel>();


        //Admin Part
        public FactoriesViewModel FactoriesViewModel => container.Resolve<FactoriesViewModel>();
        public DevisionsViewModel DevisionsViewModel => container.Resolve<DevisionsViewModel>();
        public LeadersViewModel LeadersViewModel => container.Resolve<LeadersViewModel>();
    }
}
