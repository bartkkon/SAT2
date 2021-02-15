using Saving_Accelerator_Tools2.Contracts.Services;
using Saving_Accelerator_Tools2.Core.Controllers.Action;
using Saving_Accelerator_Tools2.Core.Controllers.Users;
using Saving_Accelerator_Tools2.Core.User;
using Saving_Accelerator_Tools2.Helpers;
using Saving_Accelerator_Tools2.Tasks;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Saving_Accelerator_Tools2.ViewModels.Action
{
    public class MainFilterViewModel : INotifyProperty
    {
        #region Constructor
        public MainFilterViewModel()
        {
            Leaders = LoadActionLeader();
            NewAction = new RelayCommand<object>(LoadNewAction);
            LoadTree();
        }
        #endregion

        #region Private Variables
        private ObservableCollection<ITreeObject> TreeList;
        public bool VisibleAdmin = User.Logged.User_Role.Any(role => role.RoleID == 5);
        private bool _active = true;
        private bool _idea = false;
        private decimal _year = DateTime.UtcNow.Year;
        private bool _electronic = User.Logged.User_Devisions.Any(Dev => Dev.DevisionID == 1);
        private bool _maechanic = User.Logged.User_Devisions.Any(Dev => Dev.DevisionID == 2);
        private bool _nvr = User.Logged.User_Devisions.Any(Dev => Dev.DevisionID == 3);
        private bool _plv = User.Logged.User_Plant.Any(Plant => Plant.PlantID == 1);
        private bool _zm = User.Logged.User_Plant.Any(Plant => Plant.PlantID == 2);
        private List<string> _leaders = new List<string>();
        private int _LeaderSelectedIndex = 0;
        private List<string> LoadActionLeader()
        {
            LeaderSelectedIndex = 0;
            return ActionLeaderController.Load_WithAll(_electronic, _maechanic, _nvr, _plv, _zm).ToList();
        }
        #endregion

        #region Variables
        public bool Active
        {
            set
            {
                _active = value;
                RisePropoertyChanged();
            }
            get { return _active; }
        }
        public bool Idea
        {
            set
            {
                _idea = value;
                RisePropoertyChanged();
            }
            get { return _idea; }
        }
        public decimal Year
        {
            set
            {
                _year = value;
                LoadTree();
                RisePropoertyChanged();
            }
            get { return _year; }
        }
        public bool Electronic
        {
            set
            {
                _electronic = value;
                Leaders = LoadActionLeader();
                LoadTree();
                RisePropoertyChanged();
            }
            get { return _electronic; }
        }
        public bool Mechanic
        {
            set
            {
                _maechanic = value;
                Leaders = LoadActionLeader();
                LoadTree();
                RisePropoertyChanged();
            }
            get { return _maechanic; }
        }
        public bool NVR
        {
            set
            {
                _nvr = value;
                Leaders = LoadActionLeader();
                LoadTree();
                RisePropoertyChanged();
            }
            get { return _nvr; }
        }
        public bool PLV
        {
            set
            {
                _plv = value;
                Leaders = LoadActionLeader();
                LoadTree();
                RisePropoertyChanged();
            }
            get { return _plv; }
        }
        public bool ZM
        {
            set
            {
                _zm = value;
                Leaders = LoadActionLeader();
                LoadTree();
                RisePropoertyChanged();
            }
            get { return _zm; }
        }
        public List<string> Leaders
        {
            set
            {
                _leaders = value;
                RisePropoertyChanged();
            }
            get
            {
                return _leaders;
            }
        }
        public int LeaderSelectedIndex
        {
            set
            {
                _LeaderSelectedIndex = value;
                RisePropoertyChanged();
            }
            get { return _LeaderSelectedIndex; }
        }
        public ObservableCollection<ITreeObject> Tree
        {
            get { return TreeList; }
            set
            {
                TreeList = value;
                RisePropoertyChanged();
            }
        }
        #endregion

        #region Buttons
        public ICommand NewAction
        {
            get; private set;
        }
        #endregion

        #region Functions
        public void LoadTree()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            var NewTree = new ObservableCollection<ITreeObject>();
            if (_electronic)
            {
                var Electronic = new TreeObject()
                {
                    Id = 0,
                    Name = "Electronic",
                };
                NewTree.Add(Electronic);
                var ActionList = Action_Controller.LoadToTree(1, _year, _plv, _zm, _leaders[_LeaderSelectedIndex], _active);
                foreach (var Action in ActionList)
                {
                    var NewItem = new TreeObject()
                    {
                        Id = Action.ActionID,
                        Name = Action.Name,
                    };
                    Electronic.Tree.Add(NewItem);
                }
                var ElectronicCarry = new TreeObject()
                {
                    Id = 0,
                    Name = "Electronic Carry",
                };
                NewTree.Add(ElectronicCarry);
                ActionList = Action_Controller.LoadToTree(1, _year - 1, _plv, _zm, _leaders[_LeaderSelectedIndex], _active);
                foreach (var Action in ActionList)
                {
                    var NewItem = new TreeObject()
                    {
                        Id = Action.ActionID,
                        Name = Action.Name,
                    };
                    ElectronicCarry.Tree.Add(NewItem);
                }
            }
            if (_maechanic)
            {
                var Mechanic = new TreeObject()
                {
                    Id = 0,
                    Name = "Mechanic",
                };
                NewTree.Add(Mechanic);
                var ActionList = Action_Controller.LoadToTree(2, _year, _plv, _zm, _leaders[_LeaderSelectedIndex], _active);
                foreach (var Action in ActionList)
                {
                    var NewItem = new TreeObject()
                    {
                        Id = Action.ActionID,
                        Name = Action.Name,
                    };
                    Mechanic.Tree.Add(NewItem);
                }
                var MechanicCarry = new TreeObject()
                {
                    Id = 0,
                    Name = "Mechanic Carry",
                };
                NewTree.Add(MechanicCarry);
                ActionList = Action_Controller.LoadToTree(2, _year - 1, _plv, _zm, _leaders[_LeaderSelectedIndex], _active);
                foreach (var Action in ActionList)
                {
                    var NewItem = new TreeObject()
                    {
                        Id = Action.ActionID,
                        Name = Action.Name,
                    };
                    MechanicCarry.Tree.Add(NewItem);
                }
            }
            if (_nvr)
            {
                var NVR = new TreeObject()
                {
                    Id = 0,
                    Name = "NVR",
                };
                NewTree.Add(NVR);
                var ActionList = Action_Controller.LoadToTree(3, _year, _plv, _zm, _leaders[_LeaderSelectedIndex], _active);
                foreach (var Action in ActionList)
                {
                    var NewItem = new TreeObject()
                    {
                        Id = Action.ActionID,
                        Name = Action.Name,
                    };
                    NVR.Tree.Add(NewItem);
                }
                var NVRCarry = new TreeObject()
                {
                    Id = 0,
                    Name = "NVR Carry",
                };

                ActionList = Action_Controller.LoadToTree(3, _year - 1, _plv, _zm, _leaders[_LeaderSelectedIndex], _active);
                NewTree.Add(NVRCarry);
                foreach (var Action in ActionList)
                {
                    var NewItem = new TreeObject()
                    {
                        Id = Action.ActionID,
                        Name = Action.Name,
                    };
                    NVRCarry.Tree.Add(NewItem);
                }
            }
            Tree = NewTree;
            Mouse.OverrideCursor = null;
        }
        public void LoadNewAction(object obj)
        {
            new LoadAction(0);
        }
        #endregion
    }


    public interface ITreeObject
    {
        int Id { get; }
        string Name { get; }
        bool IsSelected { get; set; }

        ObservableCollection<ITreeObject> Tree { get; }
    }

    public class TreeObject : INotifyProperty, ITreeObject
    {
        /// <summary>
        /// Element for Tree. Set Action ID and Name to Dispalay
        /// </summary>
        public TreeObject()
        {
            Tree = new ObservableCollection<ITreeObject>();
        }

        #region Variables
        public ObservableCollection<ITreeObject> Tree { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        #endregion

        #region If Selection
        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value != _isSelected)
                {
                    _isSelected = value;
                    if (_isSelected)
                    {
                        RequireLoadAction();
                    }
                    RisePropoertyChanged();
                }
            }
        }
        #endregion

        #region Function when Selected
        private void RequireLoadAction()
        {
            if (Id == 0)
            {
                _isSelected = false;
                return;
            }

            new LoadAction(Id);
        }
        #endregion
    }
}
