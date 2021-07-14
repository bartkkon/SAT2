using Saving_Accelerator_Tools2.DataBaseIServices.Data;
using Saving_Accelerator_Tools2.DataBaseIServices.Others;
using Saving_Accelerator_Tools2.IServices.MessageBox;
using Saving_Accelerator_Tools2.Model.Approvals;
using Saving_Accelerator_Tools2.Model.Others;
using Saving_Accelerator_Tools2.ViewModel.Helpers;
using SavingAcceleratorTools2.ProjectModels.Data;
using SavingAcceleratorTools2.ProjectModels.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Saving_Accelerator_Tools2.ViewModel.Others
{
    public class ApprovalsViewModel : Base
    {
        private readonly IDevisionServices devisionServices;
        private readonly IFactoriesServices factoriesServices;
        private readonly IApprovalsService approvalsService;
        private readonly IMessageBoxService messageBoxService;

        public ApprovalsViewModel(IDevisionServices devisionServices,
                                  IFactoriesServices factoriesServices,
                                  IApprovalsService approvalsService,
                                  IMessageBoxService messageBoxService)
        {
            this.devisionServices = devisionServices;
            this.factoriesServices = factoriesServices;
            this.approvalsService = approvalsService;
            this.messageBoxService = messageBoxService;
            OpenCalculationButton = new RelayCommand(OpenCalculation);
            SaveChangeButton = new RelayCommand(SaveChange);

            InitiationData();
        }
        private void InitiationData()
        {
            Statuses = (Status[])Enum.GetValues(typeof(Status));
            Revision = (Revisions[])Enum.GetValues(typeof(Revisions));
            Months = (Months[])Enum.GetValues(typeof(Months));
            Factories = factoriesServices.Get(true);
            FactorySelected = Factories.FirstOrDefault();
            Year = DateTime.UtcNow.Year;
            StatusSelected = Status.Open;
            RevisonSelected = null;
        }

        private decimal year;
        private ICollection<Factories> factories;
        private Factories factorySelected;
        public ICollection<Status> Statuses { get; private set; }
        public ICollection<Revisions> Revision { get; private set; }
        public ICollection<Months> Months { get; private set; }
        private Status statusSelected;
        private Revisions? revisionSelected;
        private Months? monthSelected;
        private bool monthsEnabled;
        private Approval_SearchCriteria criteria = new Approval_SearchCriteria();
        private ICollection<Approval> approvalList;
        private Approval approvalSelected;

        public Approval ApprovalSelected
        {
            get { return approvalSelected; }
            set { approvalSelected = value; OnPropertyChange(); }
        }



        public ICollection<Approval> ApprovalList
        {
            get => approvalList;
            set { approvalList = value; OnPropertyChange(); }
        }
        public Approval_SearchCriteria Criteria
        {
            get => criteria;
            set
            {
                criteria = value;
                ApprovalList = approvalsService.Get(criteria);
                OnPropertyChange();
            }
        }
        public bool MonthsEnabled
        {
            get => monthsEnabled;
            set
            {
                monthsEnabled = value;
                MonthSelected = value ? (Months)Enum.ToObject(typeof(Months), DateTime.UtcNow.Month - 1) : null;
                OnPropertyChange();
            }
        }
        public Months? MonthSelected
        {
            get => monthSelected;
            set { monthSelected = value; Criteria.Month = value; OnPropertyChange(); }
        }
        public Revisions? RevisonSelected
        {
            get => revisionSelected;
            set
            {
                revisionSelected = value;
                MonthsEnabled = value == Revisions.EA4;
                Criteria.Revision = value;
                OnPropertyChange();
                ApprovalList = approvalsService.Get(criteria);
            }
        }
        public Status StatusSelected
        {
            get => statusSelected;
            set
            {
                statusSelected = value;
                Criteria.Status = value;
                OnPropertyChange();
                ApprovalList = approvalsService.Get(criteria);
            }
        }
        public Factories FactorySelected
        {
            get => factorySelected;
            set
            {
                factorySelected = value;
                Criteria.Factory = value.ID;
                OnPropertyChange();
                ApprovalList = approvalsService.Get(criteria);
            }
        }
        public ICollection<Factories> Factories
        {
            get => factories;
            set
            {
                factories = value;
                OnPropertyChange();
                ApprovalList = approvalsService.Get(criteria);
            }
        }
        public decimal Year
        {
            get => year;
            set
            {
                year = value;
                Criteria.Year = value;
                OnPropertyChange();
                ApprovalList = approvalsService.Get(criteria);
            }
        }

        public ICommand OpenCalculationButton { get; private set; }
        public ICommand SaveChangeButton { get; private set; }
        private void OpenCalculation()
        {
            Approval_SearchCriteria CheckCriteria = new()
            {
                Factory = factorySelected.ID,
                Revision = revisionSelected,
                Month = monthSelected,
                Year = Year,
            };

            if(approvalsService.Get(CheckCriteria).Count != 0)
            {
                MessageBoxResult results = messageBoxService.Ask("To many items", $"For:{Environment.NewLine}Revision: {revisionSelected}{Environment.NewLine}Months: {monthSelected}{Environment.NewLine} Year: {Year}{Environment.NewLine} Plant: {factorySelected.Plant}{Environment.NewLine} Record Exist! Do you sure to add new record?");
                if(results != MessageBoxResult.Yes)
                {
                    return;
                }
            }

            Approval NewRequest = new()
            {
                Status = Status.Open,
                Month = monthSelected,
                Year = year,
                Revision = (Revisions)revisionSelected,
                CreateDate = DateTime.UtcNow,
            };
            NewRequest.Teams = new List<TeamApp>();

            var plantDevisions = devisionServices.Get(factorySelected).ToList();

            foreach(var plantDevision in plantDevisions)
            {
                TeamApp newTeam = new()
                {
                    Devision = plantDevision,
                    Status = Status.Open,
                    Active = true,
                    ChangeBy = Environment.UserName.ToLower(),
                    Date = DateTime.UtcNow,
                };
                NewRequest.Teams.Add(newTeam);
            }

            approvalsService.Set(NewRequest);
        }
        public void SaveChange()
        {
            messageBoxService.ShowConfirmation("Ahtung!", "This option still not working!");
        }
    }
}
