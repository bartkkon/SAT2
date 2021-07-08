using Saving_Accelerator_Tools2.DataBaseIServices.Others;
using Saving_Accelerator_Tools2.IServices.MessageBox;
using Saving_Accelerator_Tools2.Model.Others;
using Saving_Accelerator_Tools2.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Saving_Accelerator_Tools2.ViewModel.Others
{
    public class TagViewModel : Base
    {
        private readonly ITagService tagService;
        private readonly IFactoriesServices factoriesServices;
        private readonly IMessageBoxService messageBoxService;

        public TagViewModel(ITagService tagService, IFactoriesServices factoriesServices, IMessageBoxService messageBoxService)
        {
            New = new RelayCommand(NewTag);
            Add = new RelayCommand(AddTag);
            Update = new RelayCommand(UpdateTag);
            this.tagService = tagService;
            this.factoriesServices = factoriesServices;
            this.messageBoxService = messageBoxService;

            SearchFactories = this.factoriesServices.Get(onlyActive);


            searchCriteria = new TagSearchCriteria() { Active = true, From = searchFrom, Until = searchUntil, };
            SearchCriteria = searchCriteria;
        }

        private decimal searchFrom = DateTime.UtcNow.Year;
        private decimal searchUntil = DateTime.UtcNow.Year;
        private ICollection<Factories> searchFactoriers;
        private Factories searchFactory;
        private bool onlyActive = true;
        private TagSearchCriteria searchCriteria;
        private ICollection<Tag> tagsList;
        private Tag tagSelected;
        private bool enabled_change;
        private Visibility addButtonVisibility = Visibility.Collapsed;
        private Visibility updateButtonVisibility = Visibility.Visible;

        public TagSearchCriteria SearchCriteria
        {
            get => searchCriteria;
            set
            {
                searchCriteria = value;
                TagsList = tagService.Get(searchCriteria);
                OnPropertyChange();
            }
        }
        public Visibility UpdateButtonVisibility
        {
            get { return updateButtonVisibility; }
            set
            {
                updateButtonVisibility = value;
                OnPropertyChange();
            }
        }

        public Visibility AddButtonVisibility
        {
            get { return addButtonVisibility; }
            set
            {
                addButtonVisibility = value;
                OnPropertyChange();
            }
        }
        public bool Enabled_change
        {
            get { return enabled_change; }
            set { enabled_change = value; OnPropertyChange(); }
        }


        public Tag TagSelected
        {
            get { return tagSelected; }
            set
            {
                tagSelected = value;
                OnPropertyChange();
            }
        }
        public ICollection<Tag> TagsList
        {
            get { return tagsList; }
            set
            {
                tagsList = value;
                OnPropertyChange();
            }
        }
        public bool OnlyActive
        {
            get { return onlyActive; }
            set
            {
                onlyActive = value;
                SearchCriteria.Active = onlyActive;
                OnPropertyChange();
            }
        }
        public Factories SearchFactory
        {
            get { return searchFactory; }
            set
            {
                searchFactory = value;
                searchCriteria.FactoryID = searchFactory?.ID;
                SearchCriteria = searchCriteria;
                OnPropertyChange();
            }
        }
        public ICollection<Factories> SearchFactories
        {
            get { return searchFactoriers; }
            set { searchFactoriers = value; OnPropertyChange(); }
        }


        public decimal SearchUntil
        {
            get { return searchUntil; }
            set 
            {
                searchUntil = value;
                searchCriteria.Until = searchUntil;
                SearchCriteria = searchCriteria;
                OnPropertyChange();
            }
        }


        public decimal SearchFrom
        {
            get { return searchFrom; }
            set
            {
                searchFrom = value;
                searchCriteria.From = searchFrom;
                SearchCriteria = searchCriteria;
                OnPropertyChange();
            }
        }



        public ICommand New { get; private set; }
        public ICommand Add { get; private set; }
        public ICommand Update { get; private set; }
        private void UpdateTag()
        {
            if (!string.IsNullOrEmpty(TagSelected.Name) && TagSelected.Facotry != null)
            {
                tagService.Update(TagSelected);
                messageBoxService.ShowConfirmation("Done", $"Tag {tagSelected.Name} updated!");
                TagSelected = null;
                TagsList = tagService.Get(searchCriteria);
                Enabled_change = false;
            }
        }

        private void AddTag()
        {
            if (!string.IsNullOrEmpty(TagSelected.Name) && TagSelected.Facotry != null)
            {
                tagService.Add(TagSelected);
                messageBoxService.ShowConfirmation("Done", $"New Tag {tagSelected.Name} added!");
                TagsList = tagService.Get(searchCriteria);
                AddButtonVisibility = Visibility.Collapsed;
                UpdateButtonVisibility = Visibility.Visible;
                TagSelected = null;
                Enabled_change = false;
            }
        }

        private void NewTag()
        {
            TagSelected = new Tag() { ID = 0, From = DateTime.UtcNow.Year, Until = DateTime.UtcNow.Year, Active = true, };
            Enabled_change = true;
            AddButtonVisibility = Visibility.Visible;
            UpdateButtonVisibility = Visibility.Collapsed;
        }
    }
}
