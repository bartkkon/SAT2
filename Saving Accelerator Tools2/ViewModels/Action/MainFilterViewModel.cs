using Microsoft.Extensions.Options;

using Saving_Accelerator_Tools2.Contracts.Services;
using Saving_Accelerator_Tools2.Contracts.ViewModels;
using Saving_Accelerator_Tools2.Core.Controllers.Users;
using Saving_Accelerator_Tools2.Core.User;
using Saving_Accelerator_Tools2.Helpers;
using Saving_Accelerator_Tools2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Saving_Accelerator_Tools2.ViewModels.Action
{
    public class MainFilterViewModel : INotifyProperty
    {
        public MainFilterViewModel()
        {
            Leaders = LoadActionLeader();
        }
        public bool VisibleAdmin = User.Logged.User_Role.Any(role => role.RoleID == 5);

        private bool _active = true;
        public bool Active
        {
            set
            {
                _active = value;
                RisePropoertyChanged();
            }
            get
            {
                return _active;
            }
        }

        private bool _idea = false;
        public bool Idea
        {
            set
            {
                _idea = value;
                RisePropoertyChanged();
            }
            get
            {
                return _idea;
            }
        }

        private decimal _year = DateTime.UtcNow.Year;
        public decimal Year
        {
            set
            {
                _year = value;
                RisePropoertyChanged();
            }
            get
            {
                return _year;
            }
        }

        private bool _electronic = User.Logged.User_Devisions.Any(Dev => Dev.DevisionID == 1);
        public bool Electronic
        {
            set
            {
                _electronic = value;
                Leaders = LoadActionLeader();
                RisePropoertyChanged();
            }
            get
            {
                return _electronic;
            }
        }

        private bool _maechanic = User.Logged.User_Devisions.Any(Dev => Dev.DevisionID == 2);
        public bool Mechanic
        {
            set
            {
                _maechanic = value;
                Leaders = LoadActionLeader();
                RisePropoertyChanged();
            }
            get
            {
                return _maechanic;
            }
        }

        private bool _nvr = User.Logged.User_Devisions.Any(Dev => Dev.DevisionID == 3);
        public bool NVR
        {
            set
            {
                _nvr = value;
                Leaders = LoadActionLeader();
                RisePropoertyChanged();
            }
            get
            {
                return _nvr;
            }
        }

        private bool _plv = User.Logged.User_Plant.Any(Plant => Plant.PlantID == 1);
        public bool PLV
        {
            set
            {
                _plv = value;
                Leaders = LoadActionLeader();
                RisePropoertyChanged();
            }
            get
            {
                return _plv;
            }
        }

        private bool _zm = User.Logged.User_Plant.Any(Plant => Plant.PlantID == 2);
        public bool ZM
        {
            set
            {
                _zm = value;
                Leaders = LoadActionLeader();
                RisePropoertyChanged();
            }
            get
            {
                return _zm;
            }
        }

        private List<string> _leaders = new List<string>();
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

        private int _LeaderSelectedIndex = 0;
        public int LeaderSelectedIndex
        {
            set { 
                _LeaderSelectedIndex = value;
                RisePropoertyChanged();
            }
            get
            {
                return _LeaderSelectedIndex;
            }
        }

        private List<string> LoadActionLeader()
        {
            LeaderSelectedIndex = 0;
            return ActionLeaderController.Load_WithAll(_electronic, _maechanic, _nvr, _plv, _zm).ToList();
        }

    }
}
