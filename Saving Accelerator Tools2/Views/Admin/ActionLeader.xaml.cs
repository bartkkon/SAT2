using Saving_Accelerator_Tools2.Core.Controllers;
using Saving_Accelerator_Tools2.Core.Controllers.Users;
using Saving_Accelerator_Tools2.Core.Data;
using Saving_Accelerator_Tools2.Core.Models.Other;
using Saving_Accelerator_Tools2.Core.Models.Other.InterTable;
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

namespace Saving_Accelerator_Tools2.Views.Admin
{
    /// <summary>
    /// Interaction logic for ActionLeader.xaml
    /// </summary>
    public partial class ActionLeader : UserControl
    {
        private ActionLeader_DB Leader;

        public ActionLeader()
        {
            InitializeComponent();
            InitializeData();
        }

        private void InitializeData()
        {
            ICollection<ActionLeader_DB> LeaderList = ActionLeaderController.ActionLeader_LeadAll();

            LoadLeader_Leaders_ComboBox.ItemsSource = LeaderList;
            LoadLeader_Leaders_ComboBox.DisplayMemberPath = "FullName";
        }

        private void NewLeaderClear()
        {
            Name_TextBox.Text = string.Empty;
            Surname_TextBox.Text = string.Empty;
            Electronic_Devision_CheckBox.IsChecked = false;
            Mechanic_Devision_CheckBox.IsChecked = false;
            NVR_Devision_CheckBox.IsChecked = false;
            PLV_Plant_CheckBox.IsChecked = false;
            ZM_Plant_CheckBox.IsChecked = false;
            Active_Leader_CheckBox.IsChecked = false;
        }

        private void New_Leader_Button_Click(object sender, RoutedEventArgs e)
        {
            NewLeaderClear();
            Save_Button.Content = "Add Leader";
            Active_Leader_CheckBox.IsChecked = true;
            Leader = new ActionLeader_DB();
            LoadLeader_Leaders_ComboBox.SelectedItem = -1;
            NewLEader_Grid.IsEnabled = true;
        }

        private void LoadData()
        {
            Name_TextBox.Text = Leader.Name;
            Surname_TextBox.Text = Leader.Surname;
            Active_Leader_CheckBox.IsChecked = Leader.Active;

            foreach (ActionLeader_Devision_DB Devisions in Leader.Leader_Devision)
            {
                if (Devisions.Devision.Devision == "Electronic")
                    Electronic_Devision_CheckBox.IsChecked = true;
                if (Devisions.Devision.Devision == "Mechanic")
                    Mechanic_Devision_CheckBox.IsChecked = true;
                if (Devisions.Devision.Devision == "NVR")
                    NVR_Devision_CheckBox.IsChecked = true;
            }

            foreach (ActionLeader_Plant_DB Plant in Leader.Leader_Plant)
            {
                if (Plant.Factory.Plant == "PLV")
                    PLV_Plant_CheckBox.IsChecked = true;
                if (Plant.Factory.Plant == "ZM")
                    ZM_Plant_CheckBox.IsChecked = true;
            }

        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Save_Button.Content.ToString() == "Add Leader")
            {
                AddNewLeader();

                LoadLeader_Leaders_ComboBox.ItemsSource = ActionLeaderController.ActionLeader_LeadAll();
                LoadLeader_Leaders_ComboBox.SelectedIndex = -1;
                NewLeaderClear();
                NewLEader_Grid.IsEnabled = false;
            }
            else
            {
                UpdateUser();

                NewLeaderClear();
                NewLEader_Grid.IsEnabled = false;
            }
        }

        private void UpdateUser()
        {
            var context = new DataBaseConnetionContext();
            if (Leader.Name != Name_TextBox.Text)
            {
                Leader.Name = Name_TextBox.Text;
                Leader.FullName = Name_TextBox.Text + " " + Surname_TextBox.Text;
            }
            if (Leader.Surname != Surname_TextBox.Text)
            {
                Leader.Surname = Surname_TextBox.Text;
                Leader.FullName = Name_TextBox.Text + " " + Surname_TextBox.Text;
            }
            if (Leader.Active != Active_Leader_CheckBox.IsChecked)
                Leader.Active = Active_Leader_CheckBox.IsChecked.GetValueOrDefault();

            //Devision update
            UpdateDevision(context);

            //Plant Update
            UpdatePlant(context);

            context.SaveChanges();

        }

        private void UpdatePlant(DataBaseConnetionContext context)
        {
            var LeaderPlant = Leader.Leader_Plant;
            var Plants = PlantController.LoadPlants();

            if (PLV_Plant_CheckBox.IsChecked == true)
            {
                if (!LeaderPlant.Any(b => b.Factory.Plant == "PLV") || LeaderPlant.Count == 0)
                {
                    var NewLeaderPlant  = new ActionLeader_Plant_DB
                    {
                        Leader = Leader,
                        Factory = Plants.Where(c => c.Plant == "PLV").FirstOrDefault(),
                    };

                    Leader.Leader_Plant.Add(NewLeaderPlant);
                    context.ActionLeader.Update(Leader);
                    context.SaveChanges();
                }
            }
            else
            {
                if (LeaderPlant.Any(c => c.Factory.Plant == "PLV"))
                {
                    context.Remove(LeaderPlant.Where(c => c.Factory.Plant == "PLV" && c.Leader.LeaderID == Leader.LeaderID).First());
                    context.SaveChanges();
                }
            }

            if (ZM_Plant_CheckBox.IsChecked == true)
            {
                if (!LeaderPlant.Any(b => b.Factory.Plant == "ZM") || LeaderPlant.Count == 0)
                {
                    var NewLeaderPlant = new ActionLeader_Plant_DB
                    {
                        Leader = Leader,
                        Factory = Plants.Where(c => c.Plant == "ZM").FirstOrDefault(),
                    };

                    Leader.Leader_Plant.Add(NewLeaderPlant);
                    context.ActionLeader.Update(Leader);
                    context.SaveChanges();
                }
            }
            else
            {
                if (LeaderPlant.Any(c => c.Factory.Plant == "ZM"))
                {
                    context.Remove(LeaderPlant.Where(c => c.Factory.Plant == "ZM" && c.Leader.LeaderID == Leader.LeaderID).First());
                    context.SaveChanges();
                }
            }
        }

        private void UpdateDevision(DataBaseConnetionContext context)
        {
            var LeaderDevision = Leader.Leader_Devision;
            var Devision = DevisionController.LoadDevisions();

            if(Electronic_Devision_CheckBox.IsChecked == true)
            {
                if(!LeaderDevision.Any(b => b.Devision.Devision == "Electronic") || LeaderDevision.Count ==0)
                {
                    var NewLeaderDevision = new ActionLeader_Devision_DB
                    {
                        Leader = Leader,
                        Devision = Devision.Where(c => c.Devision == "Electronic").FirstOrDefault(),
                    };

                    Leader.Leader_Devision.Add(NewLeaderDevision);
                    context.ActionLeader.Update(Leader);
                    context.SaveChanges();
                }
            }
            else
            {
                if(LeaderDevision.Any(c => c.Devision.Devision == "Electronic"))
                {
                    context.Remove(LeaderDevision.Where(c => c.Devision.Devision == "Electronic" && c.Leader.LeaderID == Leader.LeaderID).First());
                    context.SaveChanges();
                }
            }
            if (Mechanic_Devision_CheckBox.IsChecked == true)
            {
                if (!LeaderDevision.Any(b => b.Devision.Devision == "Mechanic") || LeaderDevision.Count == 0)
                {
                    var NewLeaderDevision = new ActionLeader_Devision_DB
                    {
                        Leader = Leader,
                        Devision = Devision.Where(c => c.Devision == "Mechanic").FirstOrDefault(),
                    };

                    Leader.Leader_Devision.Add(NewLeaderDevision);
                    context.ActionLeader.Update(Leader);
                    context.SaveChanges();
                }
            }
            else
            {
                if (LeaderDevision.Any(c => c.Devision.Devision == "Mechanic"))
                {
                    context.Remove(LeaderDevision.Where(c => c.Devision.Devision == "Mechanic" && c.Leader.LeaderID == Leader.LeaderID).First());
                    context.SaveChanges();
                }
            }

            if (NVR_Devision_CheckBox.IsChecked == true)
            {
                if (!LeaderDevision.Any(b => b.Devision.Devision == "NVR") || LeaderDevision.Count == 0)
                {
                    var NewLeaderDevision = new ActionLeader_Devision_DB
                    {
                        Leader = Leader,
                        Devision = Devision.Where(c => c.Devision == "NVR").FirstOrDefault(),
                    };

                    Leader.Leader_Devision.Add(NewLeaderDevision);
                    context.ActionLeader.Update(Leader);
                    context.SaveChanges();
                }
            }
            else
            {
                if (LeaderDevision.Any(c => c.Devision.Devision == "NVR"))
                {
                    context.Remove(LeaderDevision.Where(c => c.Devision.Devision == "NVR" && c.Leader.LeaderID == Leader.LeaderID).First());
                    context.SaveChanges();
                }
            }
        }

        private void AddNewLeader()
        {
            ActionLeader_DB NewLeader = new ActionLeader_DB
            {
                Name = Name_TextBox.Text,
                Surname = Surname_TextBox.Text,
                FullName = Name_TextBox.Text + " " + Surname_TextBox.Text,
                Active = Active_Leader_CheckBox.IsChecked.GetValueOrDefault(),
            };
            var context = new DataBaseConnetionContext();
            ActionLeaderController.Add_ActionLeader(context,NewLeader);

            var Devisions = DevisionController.LoadDevisions();

            if (Electronic_Devision_CheckBox.IsChecked == true)
            {
                var NewDevisions = new ActionLeader_Devision_DB
                {
                    Leader = NewLeader,
                    Devision = Devisions.Where(c => c.Devision == "Electronic").FirstOrDefault(),
                };
                NewLeader.Leader_Devision.Add(NewDevisions);
            }
            if (Mechanic_Devision_CheckBox.IsChecked == true)
            {
                var NewDevisions = new ActionLeader_Devision_DB
                {
                    Leader = NewLeader,
                    Devision = Devisions.Where(c => c.Devision == "Mechanic").FirstOrDefault(),
                };
                NewLeader.Leader_Devision.Add(NewDevisions);
            }
            if (NVR_Devision_CheckBox.IsChecked == true)
            {
                var NewDevisions = new ActionLeader_Devision_DB
                {
                    Leader = NewLeader,
                    Devision = Devisions.Where(c => c.Devision == "NVR").FirstOrDefault(),
                };
                NewLeader.Leader_Devision.Add(NewDevisions);
            }

            var Plant = PlantController.LoadPlants();

            if (PLV_Plant_CheckBox.IsChecked == true)
            {
                var NewPlant = new ActionLeader_Plant_DB
                {
                    Leader = NewLeader,
                    Factory = Plant.Where(c => c.Plant == "PLV").FirstOrDefault(),
                };
                NewLeader.Leader_Plant.Add(NewPlant);
            }
            if (ZM_Plant_CheckBox.IsChecked == true)
            {
                var NewPlant = new ActionLeader_Plant_DB
                {
                    Leader = NewLeader,
                    Factory = Plant.Where(c => c.Plant == "ZM").FirstOrDefault(),
                };
                NewLeader.Leader_Plant.Add(NewPlant);
            }
            context.SaveChanges();
            //ActionLeaderController.Update_Leader(NewLeader);
        }

        private void LoadLeader_Leaders_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LoadLeader_Leaders_ComboBox.SelectedIndex != -1)
            {
                NewLeaderClear();
                Save_Button.Content = "Change Leader";
                Leader = new ActionLeader_DB();
                Leader = ActionLeaderController.LoadSingle((LoadLeader_Leaders_ComboBox.SelectedItem as ActionLeader_DB).FullName);
                if (Leader != null)
                {
                    LoadData();
                    NewLEader_Grid.IsEnabled = true;
                }
            }
        }
    }
}
