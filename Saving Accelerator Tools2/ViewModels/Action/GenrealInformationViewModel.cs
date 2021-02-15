using Prism.Events;
using Saving_Accelerator_Tools2.Contracts.Services;
using Saving_Accelerator_Tools2.Core.Controllers;
using Saving_Accelerator_Tools2.Core.Controllers.Action.Specyfication;
using Saving_Accelerator_Tools2.Core.Controllers.Users;
using Saving_Accelerator_Tools2.Core.Models.Users.InterTable;
using Saving_Accelerator_Tools2.Models.Action;
using Saving_Accelerator_Tools2.Views.Admin;
using Saving_Accelerator_Tools2.Mediator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Saving_Accelerator_Tools2.Core.User;
using Saving_Accelerator_Tools2.Core.Models.Other;
using Saving_Accelerator_Tools2.Core.Models.Action.Specification;

namespace Saving_Accelerator_Tools2.ViewModels.Action
{
    public class GenrealInformationViewModel : INotifyProperty
    {
        #region Constructors
        public GenrealInformationViewModel()
        {
            Mediator.Mediator.Register("General_Information_Load", Load);
            Mediator.Mediator.Register("General_Information_Save", Save);
            Mediator.Mediator.Register("Get_Year", Get_Year);
        }

        ~GenrealInformationViewModel()
        {
            Mediator.Mediator.Unregister("General_Information_Load", Load);
            Mediator.Mediator.Unregister("General_Information_Save", Save);
            Mediator.Mediator.Unregister("Get_Year", Get_Year);
        }
        #endregion

        #region Private Variables
        private int _ID = 0;
        private bool _View_Enabled = true;
        private string _Name = string.Empty;
        private string _Description = string.Empty;
        private string _ActionID = string.Empty;
        private bool _ActionID_Enabled = User.Logged.User_Role.Any(b => b.RoleID == 5) ? true : false;
        private decimal _StartYear = DateTime.UtcNow.Year;
        private decimal _StartMonth = DateTime.UtcNow.Month - 1;
        private List<Tag_DB> _Tags = Tag_Controller.LoadTagToAction(DateTime.UtcNow.Year);
        private Tag_DB _Tags_SelectedItem;
        private List<Devision_DB> _Devisions = DevisionController.LoadForAction().ToList();
        private Devision_DB _Devisions_SelectedItem;
        private readonly int _Devisions_SelectedIndex = 0;
        private List<Plant_DB> _Plants = PlantController.LoadForAction();
        private Plant_DB _Plants_SelectedItem;
        private readonly int _Plants_SelectedIndex = 0;
        private List<ActionLeader_DB> _Leaders = new List<ActionLeader_DB>();
        private ActionLeader_DB _Leaders_SelectedItem;
        private bool _Active = true;
        private bool _Idea = false;
        #endregion

        #region Public Variables
        public bool View_Enabled
        {
            set
            {
                _View_Enabled = value;
                RisePropoertyChanged();
            }
            get
            {
                return _View_Enabled;
            }
        }
        public string Name
        {
            set
            {
                _Name = value;
                RisePropoertyChanged();
            }
            get
            {
                return _Name;
            }
        }
        public string Description
        {
            set
            {
                _Description = value;
                RisePropoertyChanged();
            }
            get
            {
                return _Description;
            }
        }
        public string ActionID
        {
            set
            {
                _ActionID = value;
                RisePropoertyChanged();
            }
            get
            {
                return _ActionID;
            }
        }
        public bool ActionID_Enabled
        {
            get { return _ActionID_Enabled; }
            set
            {
                _ActionID_Enabled = value;
                RisePropoertyChanged();
            }
        }
        public decimal StartYear
        {
            set
            {
                _StartYear = value;
                Tags = Tag_Controller.LoadTagToAction(value);
                Mediator.Mediator.NotifyColleagues("Set_NewYear", _StartYear);
                RisePropoertyChanged();
            }
            get { return _StartYear; }
        }
        public decimal StartMonth
        {
            set
            {
                _StartMonth = value;
                RisePropoertyChanged();
            }
            get
            {
                return _StartMonth;
            }
        }
        public List<Tag_DB> Tags
        {
            set
            {
                _Tags = value;
                RisePropoertyChanged();
            }
            get
            {
                return _Tags;
            }
        }
        public List<Devision_DB> Devision
        {
            set
            {
                _Devisions = value;
                RisePropoertyChanged();
                Devisions_SelectedItem = _Devisions.First();
            }
            get
            {
                return _Devisions;
            }
        }
        public List<Plant_DB> Plants
        {
            set
            {
                _Plants = value;
                RisePropoertyChanged();
            }
            get
            {
                return _Plants;
            }
        }
        public List<ActionLeader_DB> Leaders
        {
            set
            {
                _Leaders = value;
                if (value != null)
                    Leaders_SelectedItem = value.First();
                RisePropoertyChanged();
            }
            get
            {
                return _Leaders;
            }
        }
        public Tag_DB Tags_SelectedItem
        {
            set
            {
                if (value != null)
                {
                    _Tags_SelectedItem = _Tags.Find(b => b.Name == value.Name);
                    RisePropoertyChanged();
                }
            }
            get
            {
                return _Tags_SelectedItem;
            }
        }
        public Devision_DB Devisions_SelectedItem
        {
            set
            {

                _Devisions_SelectedItem = _Devisions.Find(b => b.Devision == value.Devision);
                Leaders = ActionLeaderController.ActionLoad(_Devisions_SelectedItem, _Plants_SelectedItem);
                RisePropoertyChanged();
            }
            get
            {
                return _Devisions_SelectedItem;
            }
        }
        public int Devision_SelectedIndex
        {
            set
            {
                RisePropoertyChanged();
            }
            get
            { return _Devisions_SelectedIndex; }
        }
        public Plant_DB Plants_SelectedItem
        {
            set
            {
                _Plants_SelectedItem = _Plants.Find(b => b.Plant == value.Plant);
                Leaders = ActionLeaderController.ActionLoad(_Devisions_SelectedItem, _Plants_SelectedItem);
                RisePropoertyChanged();
            }
            get { return _Plants_SelectedItem; }
        }
        public int Plant_SelectedIndex
        {
            set
            {
                RisePropoertyChanged();
            }
            get
            { return _Plants_SelectedIndex; }
        }
        public ActionLeader_DB Leaders_SelectedItem
        {
            set
            {
                _Leaders_SelectedItem = _Leaders.Find(b => b.FullName == value.FullName);
                RisePropoertyChanged();
            }
            get
            {
                return _Leaders_SelectedItem;
            }
        }
        public int Leader_SelectedIndex
        {
            set
            {
                RisePropoertyChanged();
            }
            get { return _Devisions_SelectedIndex; }
        }
        public bool Active
        {
            get { return _Active; }
            set
            {
                _Active = value;
                RisePropoertyChanged();
            }
        }
        public bool Idea
        {
            get { return _Idea; }
            set
            {
                _Idea = value;
                RisePropoertyChanged();
            }
        }
        public int ID
        {
            get { return _ID; }
            set
            {
                _ID = value;
                RisePropoertyChanged();
            }
        }
        #endregion

        #region Mediator Function
        public void Load(object show)
        {
            var LoadData = show as General_Information_Model;
            Name = LoadData.Name;
            ID = LoadData.ID;
            ActionID = LoadData.ActionID.ToString();
            Description = LoadData.Description;
            StartYear = LoadData.StartYear;
            StartMonth = LoadData.Month - 1;
            Devisions_SelectedItem = LoadData.Devision;
            Plants_SelectedItem = LoadData.Plant;
            Leaders_SelectedItem = LoadData.Leader;
            Tags_SelectedItem = LoadData.Tag;

            if (LoadData.Active)
                Active = LoadData.Active;
            else
                Idea = !LoadData.Active;
        }
        public void Save(object show)
        {
            int IDAction = 0;
            if (_ActionID != string.Empty)
            {
                IDAction = int.Parse(_ActionID);
            }

            General_Information_Model SaveAction = new General_Information_Model()
            {
                ID = ID,
                Name = _Name,
                ActionID = IDAction,
                Description = _Description,
                StartYear = _StartYear,
                Month = Decimal.ToInt32(_StartMonth) + 1,
                Devision = Devisions_SelectedItem,
                Plant = Plants_SelectedItem,
                Leader = Leaders_SelectedItem,
                Tag = Tags_SelectedItem,
                Active = Active,
            };
            Mediator.Mediator.NotifyColleagues("Set_GeneralInformation", SaveAction);
        }
        public void Get_Year(object show)
        {
            Mediator.Mediator.NotifyColleagues("Set_NewYear", _StartYear);
        }
        #endregion
    }
}
