using Autofac;
using Saving_Accelerator_Tools2.DataBaseIServices.ActionService;
using Saving_Accelerator_Tools2.DataBaseIServices.Data;
using Saving_Accelerator_Tools2.DataBaseIServices.Others;
using Saving_Accelerator_Tools2.DataBaseIServices.Users;
using Saving_Accelerator_Tools2.DataBaseServices.Action;
using Saving_Accelerator_Tools2.DataBaseServices.Data;
using Saving_Accelerator_Tools2.DataBaseServices.Others;
using Saving_Accelerator_Tools2.DataBaseServices.ProductionData;
using Saving_Accelerator_Tools2.DataBaseServices.Users;
using Saving_Accelerator_Tools2.FileServices.Files;
using Saving_Accelerator_Tools2.IServices.File;
using Saving_Accelerator_Tools2.IServices.MessageBox;
using Saving_Accelerator_Tools2.IServices.Windows;
using Saving_Accelerator_Tools2.MessageBoxes;
using Saving_Accelerator_Tools2.Model.Action;
using Saving_Accelerator_Tools2.Services;
using Saving_Accelerator_Tools2.ViewModel.Actions;
using Saving_Accelerator_Tools2.ViewModel.Others;
using Saving_Accelerator_Tools2.ViewModel.Users;
using Saving_Accelerator_Tools2.ViewModel.Windows;
using SavingAcceleratorTools2.MailServices;

namespace Saving_Accelerator_Tools2.ViewModelLocator
{
    public class ViewModelsLocator
    {
        private readonly IContainer container;

        public ViewModelsLocator()
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();

            //UserAcces
            containerBuilder.RegisterType<LoggedUserService>().As<ILoginUserService>().SingleInstance();

            //FileServices
            containerBuilder.RegisterType<FileService>().As<IFileService>();
            containerBuilder.RegisterType<FileDialogService>().As<IFileDialogService>();

            //MailSender
            containerBuilder.RegisterType<Sending>();

            //DataBase Services
            containerBuilder.RegisterType<ConnectionContext>().SingleInstance();

            containerBuilder.RegisterType<FactoriesServices>().As<IFactoriesServices>();
            containerBuilder.RegisterType<DevisionServices>().As<IDevisionServices>();
            containerBuilder.RegisterType<LeadersServices>().As<ILeadersServices>();
            containerBuilder.RegisterType<TagServices>().As<ITagService>();
            containerBuilder.RegisterType<UserServices>().As<IUserServices>();
            containerBuilder.RegisterType<CurrienciesServices>().As<ICurrenciesServices>();
            containerBuilder.RegisterType<ConstantVariablesServices>().As<IConstatVariablesServices>();

            //Production Data
            containerBuilder.RegisterType<StandardCostServices>().As<IStandardCostServices>();
            containerBuilder.RegisterType<PNCsServices>().As<IPNCsServices>();
            containerBuilder.RegisterType<ANCsServices>().As<IANCsServices>();
            containerBuilder.RegisterType<PNCPlatformServices>().As<IPNCPlatformServices>();
            containerBuilder.RegisterType<ApprovalsService>().As<IApprovalsService>();

            //Communication
            containerBuilder.RegisterType<MessageBoxServices>().As<IMessageBoxService>();

            //ViewModels
            containerBuilder.RegisterType<MainFilterViewModel>();

            //AdminParts
            containerBuilder.RegisterType<FactoriesViewModel>();
            containerBuilder.RegisterType<DevisionsViewModel>();
            containerBuilder.RegisterType<LeadersViewModel>();
            containerBuilder.RegisterType<TagViewModel>();
            containerBuilder.RegisterType<AccountViewModel>();
            containerBuilder.RegisterType<StandardCostViewModel>();
            containerBuilder.RegisterType<ProductionDataViewModel>();
            containerBuilder.RegisterType<CurrencyViewModel>();
            containerBuilder.RegisterType<ApprovalsViewModel>();
            containerBuilder.RegisterType<ConstantVarViewModel>();

            //Action MainFilter
            containerBuilder.RegisterType<TreeServices>().As<ITreeServices>();


            //Action
            containerBuilder.RegisterType<ActionBase>().SingleInstance();
            containerBuilder.RegisterType<LoadedActionServices>().As<ILoadedAction>();

            //Action tab
            containerBuilder.RegisterType<GeneralInformationViewModel>();


            //Extra windows
            containerBuilder.RegisterType<MonthlyQuantityViewModel>();
            containerBuilder.RegisterType<RevisionQuantityViewModel>();
            containerBuilder.RegisterType<ExtraWindowsServices>().As<IWindowsServices>();

            container = containerBuilder.Build();

        }
        //Action
        public MainFilterViewModel MainFilterViewModel => container.Resolve<MainFilterViewModel>();
        public GeneralInformationViewModel GeneralInformationViewModel => container.Resolve<GeneralInformationViewModel>();


        //Admin Part
        public FactoriesViewModel FactoriesViewModel => container.Resolve<FactoriesViewModel>();
        public DevisionsViewModel DevisionsViewModel => container.Resolve<DevisionsViewModel>();
        public LeadersViewModel LeadersViewModel => container.Resolve<LeadersViewModel>();
        public TagViewModel TagViewModel => container.Resolve<TagViewModel>();
        public AccountViewModel AccountViewModel => container.Resolve<AccountViewModel>();
        public StandardCostViewModel StandardCostViewModel => container.Resolve<StandardCostViewModel>();
        public ProductionDataViewModel ProductionDataViewModel => container.Resolve<ProductionDataViewModel>();
        public MonthlyQuantityViewModel MonthlyQuantityViewModel => container.Resolve<MonthlyQuantityViewModel>();
        public RevisionQuantityViewModel RevisionQuantityViewModel => container.Resolve<RevisionQuantityViewModel>();
        public CurrencyViewModel CurrencyViewModel => container.Resolve<CurrencyViewModel>();
        public ApprovalsViewModel ApprovalsViewModel => container.Resolve<ApprovalsViewModel>();
        public ConstantVarViewModel ConstantVarViewModel => container.Resolve<ConstantVarViewModel>();
    }
}
