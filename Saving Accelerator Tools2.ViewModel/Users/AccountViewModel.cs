using Saving_Accelerator_Tools2.DataBaseIServices.Others;
using Saving_Accelerator_Tools2.DataBaseIServices.Users;
using Saving_Accelerator_Tools2.IServices.MessageBox;
using Saving_Accelerator_Tools2.Model;
using Saving_Accelerator_Tools2.Model.Others;
using Saving_Accelerator_Tools2.ViewModel.Helpers;
using SavingAcceleratorTools2.ProjectModels.Users;
using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Saving_Accelerator_Tools2.ViewModel.Users
{
    public class AccountViewModel : Base
    {
        public AccountViewModel(IFactoriesServices factoriesServices,
                                IDevisionServices devisionServices,
                                IUserServices userServices,
                                IMessageBoxService messageBoxService)
        {
            this.factoriesServices = factoriesServices;
            this.devisionServices = devisionServices;
            this.userServices = userServices;
            this.messageBoxService = messageBoxService;
            Initialize();
        }

        private void Initialize()
        {
            New = new RelayCommand(NewUser);
            Add = new RelayCommand(AddUser);
            Update = new RelayCommand(UpdateUser);

            factories = factoriesServices.Get(true);
            devisions = devisionServices.Get(true);
            userList = new List<User>();
            userList = userServices.Get(searchActive);
            subscriptions = new List<SubscriptionCheckBoxClass>();

            foreach (Subscription enumname in (Subscription[])Enum.GetValues(typeof(Subscription)))
            {
                subscriptions.Add(new() { Name = enumname, Selected = false });
            }
            Subscriptions = subscriptions;
        }

        private string searchString;
        private bool searchActive = true;
        private ICollection<User> userList;
        private User selectedUser;
        private ICollection<Factories> factories;
        private Factories selectedFactory;
        private ICollection<Devision> devisions;
        private readonly IFactoriesServices factoriesServices;
        private readonly IDevisionServices devisionServices;
        private readonly IUserServices userServices;
        private readonly IMessageBoxService messageBoxService;
        private bool enabled = false;
        private SolidColorBrush addButtonBackground = new SolidColorBrush(Colors.LightGray);
        private ICollection<SubscriptionCheckBoxClass> subscriptions;

        public ICollection<SubscriptionCheckBoxClass> Subscriptions
        {
            get => subscriptions;
            set { subscriptions = value; OnPropertyChange(); }
        }


        public SolidColorBrush AddButtonBackground
        {
            get => addButtonBackground;
            set { addButtonBackground = value; OnPropertyChange(); }
        }


        private Visibility addButtonVisibility = Visibility.Collapsed;
        private Visibility updateButtonVisibility = Visibility.Visible;

        public Visibility UpdateButtonVisibility
        {
            get => updateButtonVisibility;
            set { updateButtonVisibility = value; OnPropertyChange(); }
        }



        public Visibility AddBUttonVisibility
        {
            get => addButtonVisibility;
            set { addButtonVisibility = value; OnPropertyChange(); }
        }


        public bool Enabled
        {
            get => enabled;
            set { enabled = value; OnPropertyChange(); }
        }



        public Factories SelectedFactory
        {
            get => selectedFactory;
            set
            {
                selectedFactory = value;
                OnPropertyChange();
                OnPropertyChange(nameof(Devision));
            }
        }

        public IEnumerable<AccountTypes> AccountTypes
        {
            get => Enum.GetValues(typeof(AccountTypes)).Cast<AccountTypes>();
        }

        public ICollection<Devision> Devisions
        {
            get => selectedFactory != null ? devisions.Where(b => b.Factory == selectedFactory).ToList() : devisions;
            set { devisions = value; }
        }


        public ICollection<Factories> Factories
        {
            get => factories;
            set { factories = value; OnPropertyChange(); }
        }

        public User SelectedUser
        {
            get => selectedUser;
            set
            {
                selectedUser = value;
                Enabled = true;
                if (selectedUser != null && selectedUser.ID != 0)
                {
                    AddBUttonVisibility = Visibility.Collapsed;
                    UpdateButtonVisibility = Visibility.Visible;
                    SelectedFactory = selectedUser.Devision.Factory;

                }
                else if (selectedUser != null && selectedUser.ID == 0)
                {
                    AddBUttonVisibility = Visibility.Visible;
                    UpdateButtonVisibility = Visibility.Collapsed;
                    SelectedFactory = null;
                }
                else
                {
                    Enabled = false;
                }

                foreach (var subrcribsion in subscriptions)
                {
                    if (selectedUser != null)
                    {
                        subrcribsion.Selected = selectedUser.MailSubscription != null ? selectedUser.MailSubscription.Contains(subrcribsion.Name) : false;
                    }
                    else
                    {
                        subrcribsion.Selected = false;
                    }
                }
                Subscriptions = subscriptions;
                OnPropertyChange();
            }
        }


        public ICollection<User> UserList
        {
            get => string.IsNullOrEmpty(searchString) ?
                    userList.Where(u => u.Active = searchActive).ToList() :
                    userList.Where(u => u.Active = searchActive && u.FullName.ToLower().Contains(searchString.ToLower())).ToList();
            set { userList = value; OnPropertyChange(); }
        }


        public bool SearchActive
        {
            get => searchActive;
            set
            {
                searchActive = value;
                OnPropertyChange();
                OnPropertyChange(nameof(UserList));
            }
        }


        public string SearchString
        {
            get => searchString;
            set
            {
                searchString = value;
                OnPropertyChange();
                OnPropertyChange(nameof(UserList));
            }
        }

        public ICommand New { get; private set; }
        public ICommand Add { get; private set; }
        public ICommand Update { get; private set; }

        private void NewUser()
        {
            SelectedUser = new User() { ID = 0, Active = true };
            AddButtonBackground = new SolidColorBrush(Colors.LightBlue);
        }
        private void AddUser()
        {
            foreach (var subscriptionrecord in subscriptions)
            {
                if (subscriptionrecord.Selected)
                {
                    SelectedUser.MailSubscription.Add(subscriptionrecord.Name);
                }
            }
            userServices.Add(SelectedUser);
            messageBoxService.ShowConfirmation("Done!", $"User {SelectedUser.FullName} has been created!");
            UserList = userServices.Get(SearchActive);
            SelectedUser = null;
            AddButtonBackground = new SolidColorBrush(Colors.LightGray);
        }

        private void UpdateUser()
        {
            foreach (var subscriptionrecord in subscriptions)
            {
                if (selectedUser.MailSubscription != null)
                {
                    if (selectedUser.MailSubscription.Any(m => m == subscriptionrecord.Name))
                    {
                        if (!subscriptionrecord.Selected)
                        {
                            selectedUser.MailSubscription.Remove(subscriptionrecord.Name);
                        }
                    }
                    else
                    {
                        if (subscriptionrecord.Selected)
                        {
                            selectedUser.MailSubscription.Add(subscriptionrecord.Name);
                        }
                    }
                }
                else
                {
                    selectedUser.MailSubscription = new List<Subscription>();
                    if (subscriptionrecord.Selected)
                    {
                        selectedUser.MailSubscription.Add(subscriptionrecord.Name);
                    }
                }
            }
            userServices.Set(selectedUser);
            messageBoxService.ShowConfirmation("Done!", $"User {SelectedUser.FullName} updated!");
            UserList = userServices.Get(SearchActive);
            SelectedUser = null;
        }

    }

    public class SubscriptionCheckBoxClass : Base
    {
        private bool selected;

        public Subscription Name { get; set; }
        public bool Selected { get => selected; set { selected = value; OnPropertyChange(); } }
    }
}
