using Saving_Accelerator_Tools2.Core.Models.Users;
using Saving_Accelerator_Tools2.Core.Controllers;
using System;
using System.Collections.Generic;
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
using Saving_Accelerator_Tools2.Core.User;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Saving_Accelerator_Tools2.Core.Models.Other;
using MahApps.Metro.Controls.Dialogs;
using Saving_Accelerator_Tools2.Core.Data;
using Saving_Accelerator_Tools2.Core.Models.Users.InterTable;

namespace Saving_Accelerator_Tools2.Views.Admin
{
    public partial class AddUsers : UserControl
    {
        private Users_DB LoadUser;

        public AddUsers()
        {
            InitializeComponent();
            InitializeData();
        }

        private void InitializeData()
        {
            ICollection<Users_DB> UsersList = UserController.LoadAllUsers();
            ICollection<Role_DB> Role_Exist = RoleController.LoadAllRole();

            LoadUser_addUser_ComboBox.ItemsSource = UsersList;
            LoadUser_addUser_ComboBox.DisplayMemberPath = "Login";

            Role_ComboBox.ItemsSource = Role_Exist;
            Role_ComboBox.DisplayMemberPath = "Role";
        }

        private void NewUserClear()
        {
            Login_TextBox.Text = string.Empty;
            Name_TextBox.Text = string.Empty;
            Surname_TextBox.Text = string.Empty;
            Mail_TextBox.Text = string.Empty;
            Role_ComboBox.SelectedIndex = -1;
            Action_CheckBox.IsChecked = false;
            Summary_CheckBox.IsChecked = false;
            Statistic_CheckBox.IsChecked = false;
            Admin_CheckBox.IsChecked = false;
            Electronic_Devision_CheckBox.IsChecked = false;
            Mechanic_Devision_CheckBox.IsChecked = false;
            NVR_Devision_CheckBox.IsChecked = false;
            PLV_Plant_CheckBox.IsChecked = false;
            ZM_Plant_CheckBox.IsChecked = false;

        }

        private void LoadData()
        {
            Login_TextBox.Text = LoadUser.Login;
            Name_TextBox.Text = LoadUser.Name;
            Surname_TextBox.Text = LoadUser.Surname;
            Mail_TextBox.Text = LoadUser.Mail;

  
            if (LoadUser.User_Role.Count == 1)
            {
                var SpecificRole = Role_ComboBox.Items.OfType<Role_DB>().FirstOrDefault(c => c.Role == LoadUser.User_Role[0].Role.Role);
                Role_ComboBox.SelectedIndex = (SpecificRole != null) ? Role_ComboBox.Items.IndexOf(SpecificRole) : -1;
            }
            else
                Role_ComboBox.SelectedIndex = -1;

            foreach (User_Pages_DB PageVisibility in LoadUser.User_Pages)
            {
                if (PageVisibility.Page.Tab == "Action")
                    Action_CheckBox.IsChecked = true;
                if (PageVisibility.Page.Tab == "Summary")
                    Summary_CheckBox.IsChecked = true;
                if (PageVisibility.Page.Tab == "Statistic")
                    Statistic_CheckBox.IsChecked = true;
                if (PageVisibility.Page.Tab == "Admin")
                    Admin_CheckBox.IsChecked = true;
            }

            foreach (User_Devision_DB devisions in LoadUser.User_Devisions)
            {
                if (devisions.Devisions.Devision == "Electronic")
                    Electronic_Devision_CheckBox.IsChecked = true;
                if (devisions.Devisions.Devision == "Mechanic")
                    Mechanic_Devision_CheckBox.IsChecked = true;
                if (devisions.Devisions.Devision == "NVR")
                    NVR_Devision_CheckBox.IsChecked = true;
            }
            foreach (UserPlant_DB plant in LoadUser.User_Plant)
            {
                if (plant.Plant.Plant == "PLV")
                    PLV_Plant_CheckBox.IsChecked = true;
                if (plant.Plant.Plant == "ZM")
                    ZM_Plant_CheckBox.IsChecked = true;
            }
        }

        private void AddNewUsers()
        {
            Users_DB NewUser = new Users_DB
            {
                Login = Login_TextBox.Text.ToLower(),
                Name = Name_TextBox.Text,
                Surname = Surname_TextBox.Text,
                Mail = Mail_TextBox.Text,
            };

            var context = new DataBaseConnetionContext();
            context.User.Add(NewUser);
            context.SaveChanges();

            if (Role_ComboBox.SelectedIndex != -1)
            {
                var NewRole = new User_Role_DB
                {
                    User = NewUser,
                    Role = Role_ComboBox.SelectedItem as Role_DB,
                };

                NewUser.User_Role.Add(NewRole);
            }

            var Pages = PageVisibilityController.LoadPages();

            if (Action_CheckBox.IsChecked == true)
            {
                var NewPage = new User_Pages_DB
                {
                    User = NewUser,
                    Page = Pages.Where(c => c.Tab == "Action").FirstOrDefault(),
                };

                NewUser.User_Pages.Add(NewPage);
            }
            if (Summary_CheckBox.IsChecked == true)
            {
                var NewPage = new User_Pages_DB
                {
                    User = NewUser,
                    Page = Pages.Where(c => c.Tab == "Summary").FirstOrDefault(),
                };

                NewUser.User_Pages.Add(NewPage);
            }
            if (Statistic_CheckBox.IsChecked == true)
            {
                var NewPage = new User_Pages_DB
                {
                    User = NewUser,
                    Page = Pages.Where(c => c.Tab == "Statistic").FirstOrDefault(),
                };

                NewUser.User_Pages.Add(NewPage);
            }
            if (Admin_CheckBox.IsChecked == true)
            {
                var NewPage = new User_Pages_DB
                {
                    User = NewUser,
                    Page = Pages.Where(c => c.Tab == "Admin").FirstOrDefault(),
                };

                NewUser.User_Pages.Add(NewPage);
            }

            var Devisions = DevisionController.LoadDevisions();

            if (Electronic_Devision_CheckBox.IsChecked == true)
            {
                var NewDevision = new User_Devision_DB
                {
                    Users = NewUser,
                    Devisions = Devisions.Where(c => c.Devision == "Electronic").FirstOrDefault()
                };

                NewUser.User_Devisions.Add(NewDevision);
            }
            if (Mechanic_Devision_CheckBox.IsChecked == true)
            {
                var NewDevision = new User_Devision_DB
                {
                    Users = NewUser,
                    Devisions = Devisions.Where(c => c.Devision == "Mechanic").FirstOrDefault()
                };

                NewUser.User_Devisions.Add(NewDevision);
            }
            if (NVR_Devision_CheckBox.IsChecked == true)
            {
                var NewDevision = new User_Devision_DB
                {
                    Users = NewUser,
                    Devisions = Devisions.Where(c => c.Devision == "NVR").FirstOrDefault()
                };

                NewUser.User_Devisions.Add(NewDevision);
            }

            var Plants = PlantController.LoadPlants();

            if (PLV_Plant_CheckBox.IsChecked == true)
            {
                var NewPlant = new UserPlant_DB
                {
                    User = NewUser,
                    Plant = Plants.Where(c => c.Plant == "PLV").FirstOrDefault()
                };

                NewUser.User_Plant.Add(NewPlant);
            }
            if (ZM_Plant_CheckBox.IsChecked == true)
            {
                var NewPlant = new UserPlant_DB
                {
                    User = NewUser,
                    Plant = Plants.Where(c => c.Plant == "ZM").FirstOrDefault()
                };

                NewUser.User_Plant.Add(NewPlant);
            }

            context.SaveChanges();
            MessageBox.Show("New User Added:" + Environment.NewLine + NewUser.Login + "  " + NewUser.Name + "  " + NewUser.Surname, "Added Users");
        }
        
        private void Update_User()
        {
            var context = new DataBaseConnetionContext();

            if (LoadUser.Login != Login_TextBox.Text)
                LoadUser.Login = Login_TextBox.Text;

            if (LoadUser.Name != Name_TextBox.Text)
                LoadUser.Name = Name_TextBox.Text;

            if (LoadUser.Surname != Surname_TextBox.Text)
                LoadUser.Surname = Surname_TextBox.Text;

            if (LoadUser.Mail != Mail_TextBox.Text)
                LoadUser.Mail = Mail_TextBox.Text;

            //Plants
            UpdateUser_Plant(context);

            //Devisions
            UpdateUser_Devision(context);

            //Pages Visibility
            UpdateUser_Pages(context);

            //Role
            UpdateUser_Role(context);

            context.User.Update(LoadUser);
            context.SaveChanges();

            MessageBox.Show("User Updated:" + Environment.NewLine + LoadUser.Login + "  " + LoadUser.Name + "  " + LoadUser.Surname, "User Updeted");
        }

        private void UpdateUser_Role(DataBaseConnetionContext context)
        {
            if (LoadUser.User_Role.Count == 1)
            {
                if (Role_ComboBox.SelectedIndex != -1)
                {
                    if (LoadUser.User_Role[0].Role.Role != (Role_ComboBox.SelectedItem as Role_DB).Role)
                    {
                        context.Remove(LoadUser.User_Role[0]);
                        var NewRole = new User_Role_DB
                        {
                            User = LoadUser,
                            Role = Role_ComboBox.SelectedItem as Role_DB,
                        };
                        LoadUser.User_Role.Add(NewRole);
                        context.User.Update(LoadUser);
                    }
                }
                else
                {
                    context.Remove(LoadUser.User_Role[0]);
                }
            }
            else 
            {
                if(Role_ComboBox.SelectedIndex != -1)
                {
                    var NewRole = new User_Role_DB
                    {
                        User = LoadUser,
                        Role = Role_ComboBox.SelectedItem as Role_DB,
                    };
                    LoadUser.User_Role.Add(NewRole);
                    context.User.Update(LoadUser);
                }
            }

        }

        private void UpdateUser_Pages(DataBaseConnetionContext context)
        {

            var UserPages = LoadUser.User_Pages;
            var Pages = PageVisibilityController.LoadPages();

            if (Action_CheckBox.IsChecked == true)
            {
                if (!UserPages.Any(b => b.Page.Tab == "Action") || UserPages.Count == 0)
                {
                    var NewPage = new User_Pages_DB
                    {
                        User = LoadUser,
                        Page = Pages.Where(c => c.Tab == "Action").FirstOrDefault()
                    };

                    LoadUser.User_Pages.Add(NewPage);
                    context.User.Update(LoadUser);
                }
            }
            else
            {
                if (UserPages.Any(b => b.Page.Tab == "Action"))
                {
                    context.Remove(UserPages.Where(c => c.Page.Tab == "Action" && c.User.UserID == LoadUser.UserID).First());
                }
            }

            if (Summary_CheckBox.IsChecked == true)
            {
                if (!UserPages.Any(b => b.Page.Tab == "Summary") || UserPages.Count == 0)
                {
                    var NewPage = new User_Pages_DB
                    {
                        User = LoadUser,
                        Page = Pages.Where(c => c.Tab == "Summary").FirstOrDefault()
                    };

                    LoadUser.User_Pages.Add(NewPage);
                    context.User.Update(LoadUser);
                }
            }
            else
            {
                if (UserPages.Any(b => b.Page.Tab == "Summary"))
                {
                    context.Remove(UserPages.Where(c => c.Page.Tab == "Summary" && c.User.UserID == LoadUser.UserID).First());
                }
            }

            if (Statistic_CheckBox.IsChecked == true)
            {
                if (!UserPages.Any(b => b.Page.Tab == "Statistic") || UserPages.Count == 0)
                {
                    var NewPage = new User_Pages_DB
                    {
                        User = LoadUser,
                        Page = Pages.Where(c => c.Tab == "Statistic").FirstOrDefault()
                    };

                    LoadUser.User_Pages.Add(NewPage);
                    context.User.Update(LoadUser);
                }
            }
            else
            {
                if (UserPages.Any(b => b.Page.Tab == "Statistic"))
                {
                    context.Remove(UserPages.Where(c => c.Page.Tab == "Statistic" && c.User.UserID == LoadUser.UserID).First());
                }
            }

            if (Admin_CheckBox.IsChecked == true)
            {
                if (!UserPages.Any(b => b.Page.Tab == "Admin") || UserPages.Count == 0)
                {
                    var NewPage = new User_Pages_DB
                    {
                        User = LoadUser,
                        Page = Pages.Where(c => c.Tab == "Admin").FirstOrDefault()
                    };

                    LoadUser.User_Pages.Add(NewPage);
                    context.User.Update(LoadUser);
                }
            }
            else
            {
                if (UserPages.Any(b => b.Page.Tab == "Admin"))
                {
                    context.Remove(UserPages.Where(c => c.Page.Tab == "Admin" && c.User.UserID == LoadUser.UserID).First());
                }
            }
        }

        private void UpdateUser_Plant(DataBaseConnetionContext context)
        {
            var UserPlants = LoadUser.User_Plant;
            var Plants = PlantController.LoadPlants();

            if (PLV_Plant_CheckBox.IsChecked == true)
            {
                if (!UserPlants.Any(b => b.Plant.Plant == "PLV") || UserPlants.Count == 0)
                {
                    var NewPlant = new UserPlant_DB
                    {
                        User = LoadUser,
                        Plant = Plants.Where(c => c.Plant == "PLV").FirstOrDefault()
                    };

                    LoadUser.User_Plant.Add(NewPlant);
                    context.User.Update(LoadUser);
                }
            }
            else
            {
                if (UserPlants.Any(b => b.Plant.Plant == "PLV"))
                {
                    context.Remove(UserPlants.Where(c => c.Plant.Plant == "PLV" && c.User.UserID == LoadUser.UserID).First());
                }
            }
            if (ZM_Plant_CheckBox.IsChecked == true)
            {
                if (!UserPlants.Any(b => b.Plant.Plant == "ZM") || UserPlants.Count == 0)
                {
                    var NewPlant = new UserPlant_DB
                    {
                        User = LoadUser,
                        Plant = Plants.Where(c => c.Plant == "ZM").FirstOrDefault()
                    };

                    LoadUser.User_Plant.Add(NewPlant);
                    context.User.Update(LoadUser);
                }
            }
            else
            {
                if (UserPlants.Any(b => b.Plant.Plant == "ZM"))
                {
                    context.Remove(UserPlants.Where(c => c.Plant.Plant == "ZM" && c.User.UserID == LoadUser.UserID).First());
                }
            }
        }

        private void UpdateUser_Devision(DataBaseConnetionContext context)
        {
            var UserDevision = LoadUser.User_Devisions;
            var Devisions = DevisionController.LoadDevisions();

            if (Electronic_Devision_CheckBox.IsChecked == true)
            {
                if (!UserDevision.Any(b => b.Devisions.Devision == "Electronic") || UserDevision.Count == 0)
                {
                    var NewDevision = new User_Devision_DB
                    {
                        Users = LoadUser,
                        Devisions = Devisions.Where(c => c.Devision == "Electronic").FirstOrDefault()
                    };

                    LoadUser.User_Devisions.Add(NewDevision);
                    context.User.Update(LoadUser);
                }
            }
            else
            {
                if (UserDevision.Any(b => b.Devisions.Devision == "Electronic"))
                {
                    context.Remove(UserDevision.Where(c => c.Devisions.Devision == "Electronic" && c.Users.UserID == LoadUser.UserID).First());
                }
            }

            if (Mechanic_Devision_CheckBox.IsChecked == true)
            {
                if (!UserDevision.Any(b => b.Devisions.Devision == "Mechanic") || UserDevision.Count == 0)
                {
                    var NewDevision = new User_Devision_DB
                    {
                        Users = LoadUser,
                        Devisions = Devisions.Where(c => c.Devision == "Mechanic").FirstOrDefault()
                    };

                    LoadUser.User_Devisions.Add(NewDevision);
                    context.User.Update(LoadUser);
                }
            }
            else
            {
                if (UserDevision.Any(b => b.Devisions.Devision == "Mechanic"))
                {
                    context.Remove(UserDevision.Where(c => c.Devisions.Devision == "Mechanic" && c.Users.UserID == LoadUser.UserID).First());
                }
            }

            if (NVR_Devision_CheckBox.IsChecked == true)
            {
                if (!UserDevision.Any(b => b.Devisions.Devision == "NVR") || UserDevision.Count == 0)
                {
                    var NewDevision = new User_Devision_DB
                    {
                        Users = LoadUser,
                        Devisions = Devisions.Where(c => c.Devision == "NVR").FirstOrDefault()
                    };

                    LoadUser.User_Devisions.Add(NewDevision);
                    context.User.Update(LoadUser);
                }
            }
            else
            {
                if (UserDevision.Any(b => b.Devisions.Devision == "NVR"))
                {
                    context.Remove(UserDevision.Where(c => c.Devisions.Devision == "NVR" && c.Users.UserID == LoadUser.UserID).First());
                }
            }
        }

        //Przerwania

        private void RemoveUsers_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult Results = MessageBox.Show("Do you want Remove User:" + Environment.NewLine + LoadUser.Login + "  " + LoadUser.Name + "  " + LoadUser.Surname, "Delete Users", MessageBoxButton.YesNo);

            if (Results == MessageBoxResult.Yes)
            {
                UserController.DeletedUser(LoadUser);
                ICollection<Users_DB> UsersList = UserController.LoadAllUsers();
                LoadUser_addUser_ComboBox.ItemsSource = UsersList;
                LoadUser_addUser_ComboBox.SelectedIndex = -1;
                NewUserClear();
                NewPerson_Grid.IsEnabled = false;
            }
        }

        private void LoadUser_addUser_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LoadUser_addUser_ComboBox.SelectedIndex != -1)
            {
                NewUserClear();
                Save_Button.Content = "Change Leader";
                LoadUser = new Users_DB();
                LoadUser = UserController.LoadSpecificUser((LoadUser_addUser_ComboBox.SelectedItem as Users_DB).Login);
                if (LoadUser != null)
                {
                    LoadData();
                    NewPerson_Grid.IsEnabled = true;
                    RemoveUsers_Button.IsEnabled = true;
                }
            }
        }

        private void NewUser_AddUser_Button_Click(object sender, RoutedEventArgs e)
        {
            NewUserClear();
            Save_Button.Content = "Add Person";
            LoadUser = new Users_DB();
            LoadUser_addUser_ComboBox.SelectedIndex = -1;
            NewPerson_Grid.IsEnabled = true;
            RemoveUsers_Button.IsEnabled = false;

        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            if (LoadUser_addUser_ComboBox.SelectedIndex == -1)
            {
                AddNewUsers();

                ICollection<Users_DB> UsersList = UserController.LoadAllUsers();
                LoadUser_addUser_ComboBox.ItemsSource = UsersList;
                LoadUser_addUser_ComboBox.SelectedIndex = -1;
                NewUserClear();
                NewPerson_Grid.IsEnabled = false;
            }
            else
            {
                Update_User();

                //UserController.UpdateUser(LoadUser);
                NewUserClear();
                NewPerson_Grid.IsEnabled = false;
            }
        }

    }
}
