using Microsoft.Extensions.Options;

using Saving_Accelerator_Tools2.Contracts.Services;
using Saving_Accelerator_Tools2.Contracts.ViewModels;
using Saving_Accelerator_Tools2.Core.Controllers.Users;
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

        }

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

        private bool _electronic = false;
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

        private bool _maechanic = false;
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

        private bool _nvr = false;
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

        private bool _plv = false;
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

        private bool _zm = false;
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

        private List<string> LoadActionLeader()
        {
            return ActionLeaderController.Load_WithAll(_electronic, _maechanic, _nvr, _plv, _zm).ToList();
        }

    }
}
