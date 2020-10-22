using Microsoft.EntityFrameworkCore.Storage;
using Saving_Accelerator_Tools2.Core.Controllers.Action.Specyfication;
using Saving_Accelerator_Tools2.Core.Data;
using Saving_Accelerator_Tools2.Core.Models.Action.Specification;
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
    /// Interaction logic for Tag.xaml
    /// </summary>
    public partial class Tag : UserControl
    {
        public Tag()
        {
            InitializeComponent();
            InitializeData();
        }

        private void InitializeData()
        {
            TAG_Year_Numeric.Value = DateTime.UtcNow.Year;
            TAG_New_StartYear_Numeric.Value = DateTime.UtcNow.Year;
            TAG_New_FinishYear_Numeric.Value = DateTime.UtcNow.Year;

            Load_Tags();
        }

        private void Load_Tags()
        {
            var Tags = Tag_Controller.Load_Year((decimal)TAG_Year_Numeric.Value).ToList();

            Tag_ComboBox.ItemsSource = Tags;
            Tag_ComboBox.DisplayMemberPath = "Name";
            Tag_ComboBox.SelectedIndex = -1;
            TAG_StartYear_Numeric.Value = null;
            TAG_FinishYear_Numeric.Value = null;
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;

            if(TAG_New_TextBox.Text != string.Empty)
            {
                var context = new DataBaseConnetionContext();
                var New = new Tag_DB()
                {
                    Name = TAG_New_TextBox.Text,
                    Start = (decimal)TAG_New_StartYear_Numeric.Value,
                    Finish = (decimal)TAG_New_FinishYear_Numeric.Value,
                };
                context.Tag.Add(New);
                context.SaveChanges();

                TAG_New_StartYear_Numeric.Value = DateTime.UtcNow.Year;
                TAG_New_FinishYear_Numeric.Value = DateTime.UtcNow.Year;
                TAG_New_TextBox.Text = string.Empty;
                Load_Tags();
            }

            Mouse.OverrideCursor = null;
        }

        private void Tag_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TAG_StartYear_Numeric.Value = (double)(e.AddedItems[0]as Tag_DB).Start;
            TAG_FinishYear_Numeric.Value = (double)(e.AddedItems[0] as Tag_DB).Finish;
        }
    }
}
