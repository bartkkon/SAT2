using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Saving_Accelerator_Tools2.Contracts.Services;
using Saving_Accelerator_Tools2.Core.Controllers;
using Saving_Accelerator_Tools2.Core.Controllers.Action.Specyfication;
using Saving_Accelerator_Tools2.Core.Controllers.Users;
using Saving_Accelerator_Tools2.Core.Data;
using Saving_Accelerator_Tools2.Core.LoadAction;
using Saving_Accelerator_Tools2.Core.Models.Action;
using Saving_Accelerator_Tools2.Core.Models.Other;
using Saving_Accelerator_Tools2.Core.Models.Users;
using Saving_Accelerator_Tools2.Core.Models.Users.InterTable;
using Saving_Accelerator_Tools2.Core.User;
using Saving_Accelerator_Tools2.Models.Action;
using Saving_Accelerator_Tools2.Tasks;
using Saving_Accelerator_Tools2.ViewModels.Action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Saving_Accelerator_Tools2.Views.Action
{
    /// <summary>
    /// Interaction logic for MainFilter.xaml
    /// </summary>
    public partial class MainFilter : UserControl
    {
        int count = 0;
        public MainFilter()
        {
            InitializeComponent();
        }

        private void ActiveAction_CB_Checked(object sender, RoutedEventArgs e)
        {
            //IdeaAction_CB.IsChecked = false;
        }

        private void IdeaAction_CB_Checked(object sender, RoutedEventArgs e)
        {
            //ActiveAction_CB.IsChecked = false;
        }

        private void NewAction_Button_Click(object sender, RoutedEventArgs e)
        {
            if (count == 0)
            {
                var NewModel = new General_Information_Model()
                {
                    ID = 0,
                    ActionID = 1,
                    Name = "to jest inna nowa akcja",
                    Description = "Jakiś opis dodany",
                    StartYear = 2020,
                    Month = 6,
                    Devision = DevisionController.LoadByName("NVR"),
                    Plant = PlantController.LoadByName("ZM"),
                    Leader = ActionLeaderController.LoadSingle("Konrad Bartkowiak"),
                    Tag = Tag_Controller.LoadByName("Quality"),
                    Active = false,
                };
                Mediator.Mediator.NotifyColleagues("General_Information_Load", NewModel);
                count++;
            }
            else if (count ==1)
            {
                var Save = new SaveAction();
                Save.Save();
            }
        }

        private void TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {
            
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

        }
    }
}
