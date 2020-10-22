using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Saving_Accelerator_Tools2.Contracts.Services;
using Saving_Accelerator_Tools2.Core.Controllers;
using Saving_Accelerator_Tools2.Core.Controllers.Users;
using Saving_Accelerator_Tools2.Core.Data;
using Saving_Accelerator_Tools2.Core.LoadAction;
using Saving_Accelerator_Tools2.Core.Models.Action;
using Saving_Accelerator_Tools2.Core.Models.Other;
using Saving_Accelerator_Tools2.Core.Models.Users;
using Saving_Accelerator_Tools2.Core.Models.Users.InterTable;
using Saving_Accelerator_Tools2.Core.User;
using Saving_Accelerator_Tools2.CoreView.Controllers.MainFilter;
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
    public partial class MainFilter : UserControl//, INotifyProperty
    {
        public MainFilter()
        {
            InitializeComponent();
            DataContext = new MainFilterViewModel();

            //InitializeItem();

        }

        //private void InitializeItem()
        //{
        //    Year_MainFilter_Num.Value = DateTime.UtcNow.Year;
        //    ActiveAction_CB.IsChecked = true;

        //    var Role = User.Logged.User_Role;

        //    InitializeData_Devision(Role);

        //    InitializeData_Factory(Role);

        //    InitializeData_ActionLeader();
        //}

        //private void InitializeData_ActionLeader()
        //{
        //    var LeaderList = new List<ActionLeader_DB>();
        //    var context = new DataBaseConnetionContext();

        //    var All = new ActionLeader_DB()
        //    {
        //        FullName = "All",
        //    };
        //    LeaderList.Add(All);

        //    var Devisions = DevisionController.LoadDevisions();
        //    var Plants = PlantController.LoadPlants();

        //    if (Electronic_MainFilter_CB.IsChecked == false)
        //        Devisions.Remove(Devisions.Where(b => b.DevisionID == 1).First());

        //    if (Mechanic_MainFilter_CB.IsChecked == false)
        //        Devisions.Remove(Devisions.Where(b => b.DevisionID == 2).First());

        //    if (NVR_MainFilter_CB.IsChecked == false)
        //        Devisions.Remove(Devisions.Where(b => b.DevisionID == 3).First());

        //    if (PLV_Plant_MainFilter_CB.IsChecked == false)
        //        Plants.Remove(Plants.Where(b => b.PlantID == 1).First());

        //    if (ZM_Plant_MainFilter_CB.IsChecked == false)
        //        Plants.Remove(Plants.Where(b => b.PlantID == 2).First());

        //    foreach (ActionLeader_DB OneLeader in ActionLeaderController.Load_FilteredBY_DevisionPlant(Devisions, Plants))
        //        LeaderList.Add(OneLeader);

        //    LeaderAction_MainFilter_CB.ItemsSource = LeaderList;
        //    LeaderAction_MainFilter_CB.DisplayMemberPath = "FullName";
        //    LeaderAction_MainFilter_CB.SelectedIndex = 0;
        //}

        //private void InitializeData_Factory(List<User_Role_DB> Role)
        //{
        //    //Normalnie ukryty
        //    Plant_Grid.Visibility = Visibility.Hidden;

        //    //Admin ma dostęp do wyboru fabryki
        //    if (Role[0].RoleID == 5)
        //        Plant_Grid.Visibility = Visibility.Visible;

        //    if (Role[0].RoleID != 5)
        //    {
        //        foreach (UserPlant_DB AddPlant in User.Logged.User_Plant)
        //        {
        //            //Wybierz w zależności od przypisanych uprawnień
        //            if (AddPlant.Plant.PlantID == 1)
        //                PLV_Plant_MainFilter_CB.IsChecked = true;

        //            if (AddPlant.Plant.PlantID == 2)
        //                ZM_Plant_MainFilter_CB.IsChecked = true;
        //        }
        //    }
        //    else
        //    {
        //        //Dla admina zaznacz tylko PLV
        //        PLV_Plant_MainFilter_CB.IsChecked = true;
        //    }
        //}

        //private void InitializeData_Devision(List<User_Role_DB> Role)
        //{
        //    //Niewidoczny obiekt
        //    Devision_Grid.Visibility = Visibility.Hidden;

        //    //Widoczny obiekt dla Adminia i Menager PC
        //    if (Role[0].RoleID == 5 || Role[0].RoleID == 4)
        //        Devision_Grid.Visibility = Visibility.Visible;

        //    if (Role[0].RoleID != 5)
        //    {
        //        //zaznacza w zależności od przypisanych uprawnień
        //        foreach (User_Devision_DB AddDevision in User.Logged.User_Devisions)
        //        {
        //            if (AddDevision.Devisions.DevisionID == 1)
        //                Electronic_MainFilter_CB.IsChecked = true;

        //            if (AddDevision.Devisions.DevisionID == 2)
        //                Mechanic_MainFilter_CB.IsChecked = true;

        //            if (AddDevision.Devisions.DevisionID == 3)
        //                NVR_MainFilter_CB.IsChecked = true;
        //        }
        //    }
        //    else
        //    {
        //        //Dla Admina ma zaznaczyć tylko Elektroniczny - jak będzie chciał to sobie zaznaczy inny
        //        Electronic_MainFilter_CB.IsChecked = true;
        //    }
        //}

        private void ActiveAction_CB_Checked(object sender, RoutedEventArgs e)
        {
            IdeaAction_CB.IsChecked = false;
        }

        private void IdeaAction_CB_Checked(object sender, RoutedEventArgs e)
        {
            ActiveAction_CB.IsChecked = false;
        }

        //private void NewAction_Button_Click(object sender, RoutedEventArgs e)
        //{
        //    //MainFilterController.TreeViewActionLoad(this);
        //    //string name = LoadedAction.Current.Name;

        //    //LoadedAction.Load(1);

        //    // name = LoadedAction.Current.Name;
        //    //OnPropertyChanged();
        //    //var NewAction = new Action_DB()
        //    //{
        //    //    Name = "Nowa akcja",
        //    //    Description = "To jest jakiś opis",
        //    //    StartYear = 2020,
        //    //    Month = 9,
        //    //};
        //    //var context = new DataBaseConnetionContext();
        //    //context.Action.Add(NewAction);
        //    //context.SaveChanges();
        //}

        //private void Reload_ActionLeader(object sender, RoutedEventArgs e)
        //{
        //    Mouse.OverrideCursor = Cursors.Wait;
        //    InitializeData_ActionLeader();
        //    Mouse.OverrideCursor = null;
        //}
    }
}
