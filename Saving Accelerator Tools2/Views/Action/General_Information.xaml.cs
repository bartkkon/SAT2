using Saving_Accelerator_Tools2.Contracts.Services;
using Saving_Accelerator_Tools2.Core.Controllers.Users;
using Saving_Accelerator_Tools2.Core.LoadAction;
using Saving_Accelerator_Tools2.Core.Models.Other;
using Saving_Accelerator_Tools2.Core.Models.Other.InterTable;
using Saving_Accelerator_Tools2.Core.User;
using Saving_Accelerator_Tools2.ViewModels.Action;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for General_Information.xaml
    /// </summary>
    public partial class General_Information : UserControl
    {

        public General_Information()
        {
            InitializeComponent();
        }

        private void ActionName_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Name_Count_TextBlock.Text = ActionName_TextBox.Text.Length + "/" + ActionName_TextBox.MaxLength.ToString();
        }

        private void Description_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Description_Count_TextBlock.Text = Description_TextBox.Text.Length + "/" + Description_TextBox.MaxLength.ToString();
        }

        private void Idea_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (Active_CheckBox != null)
                Active_CheckBox.IsChecked = false;
        }

        private void Active_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (Idea_CheckBox != null)
                Idea_CheckBox.IsChecked = false;
        }

        private void Active_CheckBox_UnChecked(object sender, RoutedEventArgs e)
        {
            if (Idea_CheckBox != null)
                Idea_CheckBox.IsChecked = true;
        }

        private void Idea_CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Active_CheckBox != null)
                Active_CheckBox.IsChecked = true;
        }
    }
}
