using Prism.Events;
using Saving_Accelerator_Tools2.Contracts.Services;
using Saving_Accelerator_Tools2.Core.Controllers;
using Saving_Accelerator_Tools2.Core.Controllers.Action.Specyfication;
using Saving_Accelerator_Tools2.Core.Controllers.Users;
using Saving_Accelerator_Tools2.Core.Models.Users.InterTable;
using Saving_Accelerator_Tools2.Models.Action;
using Saving_Accelerator_Tools2.Views.Admin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Saving_Accelerator_Tools2.ViewModels.Action
{
    public class GenrealInformationViewModel : INotifyProperty
    {
        private ActionModel Action;

        public GenrealInformationViewModel() {
            //Action = new ActionModel();
        }

        //private bool _View_Enabled = true;
        //private string _Name = string.Empty;
        //private string _Description = string.Empty;
        //private string _ActionID = string.Empty;
        //private decimal _StartYear = DateTime.UtcNow.Year;
        //private decimal _StartMonth = DateTime.UtcNow.Month - 1;
        //private Dictionary<int, string> _Tags = Tag_Controller.LoadTagToAction(DateTime.UtcNow.Year);
        //private int _Tags_SelectedItem = 0;
        //private Dictionary<int, string> _Devisions = DevisionController.LoadForAction();
        //private int _Devisions_SelectedItem = 0;
        //private readonly int _Devisions_SelectedIndex = 0;
        //private Dictionary<int, string> _Plants = PlantController.LoadForAction();
        //private int _Plants_SelectedItem = 0;
        //private readonly int _Plants_SelectedIndex = 0;
        //private Dictionary<int, string> _Leaders = new Dictionary<int, string>();
        //private int _Leaders_SelectedItem = 0;
        //private bool _Active = true;
        //private bool _Idea = false;

        //public bool View_Enabled
        //{
        //    set
        //    {
        //        _View_Enabled = value;
        //        RisePropoertyChanged();
        //    }
        //    get
        //    {
        //        return _View_Enabled;
        //    }
        //}

        //public string Name
        //{
        //    set
        //    {
        //        _Name = value;
        //        RisePropoertyChanged();
        //    }
        //    get
        //    {
        //        return _Name;
        //    }
        //}

        //public string Description
        //{
        //    set
        //    {
        //        _Description = value;
        //        RisePropoertyChanged();
        //    }
        //    get
        //    {
        //        return _Description;
        //    }
        //}

        //public string ActionID
        //{
        //    set
        //    {
        //        _ActionID = value;
        //        RisePropoertyChanged();
        //    }
        //    get
        //    {
        //        return _ActionID;
        //    }
        //}

        //public decimal StartYear
        //{
        //    set
        //    {
        //        _StartYear = value;
        //        Tags = Tag_Controller.LoadTagToAction(value);
        //        RisePropoertyChanged();
        //    }
        //    get
        //    {
        //        return _StartYear;
        //    }
        //}

        //public decimal StartMonth
        //{
        //    set
        //    {
        //        _StartMonth = value;
        //        RisePropoertyChanged();
        //    }
        //    get
        //    {
        //        return _StartMonth;
        //    }
        //}

        //public Dictionary<int, string> Tags
        //{
        //    set
        //    {
        //        _Tags = value;
        //        RisePropoertyChanged();
        //    }
        //    get
        //    {
        //        return _Tags;
        //    }
        //}

        //public Dictionary<int, string> Devision
        //{
        //    set
        //    {
        //        _Devisions = value;
        //        _Devisions_SelectedItem = value.First().Key;
        //        RisePropoertyChanged();
        //    }
        //    get
        //    {
        //        return _Devisions;
        //    }
        //}

        //public Dictionary<int, string> Plants
        //{
        //    set
        //    {
        //        _Plants = value;
        //        RisePropoertyChanged();
        //    }
        //    get
        //    {
        //        return _Plants;
        //    }
        //}

        //public Dictionary<int, string> Leaders
        //{
        //    set
        //    {
        //        _Leaders = value;
        //        Leaders_SelectedItem = value.FirstOrDefault().Key;
        //        RisePropoertyChanged();
        //    }
        //    get
        //    {
        //        return _Leaders;
        //    }
        //}

        //public int Tags_SelectedItem
        //{
        //    set
        //    {
        //        _Tags_SelectedItem = value;
        //        RisePropoertyChanged();
        //    }
        //    get
        //    {
        //        return _Tags_SelectedItem;
        //    }
        //}

        //public int Devisions_SelectedItem
        //{
        //    set
        //    {
        //        _Devisions_SelectedItem = value;
        //        Leaders = ActionLeaderController.ActionLoad(_Devisions_SelectedItem, _Plants_SelectedItem);
        //        RisePropoertyChanged();
        //    }
        //    get
        //    {
        //        return _Devisions_SelectedItem;
        //    }
        //}

        //public int Devision_SelectedIndex
        //{
        //    set
        //    {
        //        RisePropoertyChanged();
        //    }
        //    get
        //    { return _Devisions_SelectedIndex; }
        //}

        //public int Plants_SelectedItem
        //{
        //    set
        //    {
        //        _Plants_SelectedItem = value;
        //        Leaders = ActionLeaderController.ActionLoad(_Devisions_SelectedItem, _Plants_SelectedItem);
        //        RisePropoertyChanged();
        //    }
        //    get
        //    {
        //        return _Plants_SelectedItem;
        //    }
        //}

        //public int Plant_SelectedIndex
        //{
        //    set
        //    {
        //        RisePropoertyChanged();
        //    }
        //    get
        //    { return _Plants_SelectedIndex; }
        //}

        //public int Leaders_SelectedItem
        //{
        //    set
        //    {
        //        _Leaders_SelectedItem = value;
        //        RisePropoertyChanged();
        //    }
        //    get
        //    {
        //        return _Leaders_SelectedItem;
        //    }
        //}

        //public bool Active
        //{
        //    get { return _Active; }
        //    set
        //    {
        //        _Active = value;
        //        RisePropoertyChanged();
        //    }
        //}

        //public bool Idea
        //{
        //    get { return _Idea; }
        //    set
        //    {
        //        _Idea = value;
        //        RisePropoertyChanged();
        //    }
        //}

        
    }
}
