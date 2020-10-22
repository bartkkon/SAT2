using Microsoft.EntityFrameworkCore.Internal;
using Saving_Accelerator_Tools2.Core.Controllers.Data;
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
    /// Interaction logic for Currency.xaml
    /// </summary>
    public partial class Currency : UserControl
    {
        public Currency()
        {
            InitializeComponent();
            
            InitializeData();
        }

        private void InitializeData()
        {
            Year_num.Value = DateTime.UtcNow.Year;

            var CurrencyRecord = Currency_Controller.Lead_Year((decimal)Year_num.Value);

            ECCC_TextBox.Text = CurrencyRecord.ECCC.ToString();
            Euro_TextBox.Text = CurrencyRecord.Euro.ToString();
            Dolar_TextBox.Text = CurrencyRecord.Dolar.ToString();
            Sek_TextBox.Text = CurrencyRecord.Sek.ToString();

            Year_num.ValueChanged += Year_num_ValueChanged;
        }

        private void TextBox_Entry(object sender, RoutedEventArgs e)
        {
            TextBox EntryBox = (sender as TextBox);

            if(EntryBox.Text == "0,00")
            {
                EntryBox.Text = string.Empty;
                EntryBox.SelectionStart = 0;
            }
        }

        private void TextBox_Leave(object sender, RoutedEventArgs e)
        {
            TextBox LeaveBox = (sender as TextBox);

            if (LeaveBox.Text == string.Empty)
                LeaveBox.Text = "0,00";
            else
            {
                if (decimal.TryParse(LeaveBox.Text, out decimal Value))
                {
                    if(LeaveBox.Name == "ECCC_TextBox")
                        Value = Math.Round(Value, 5, MidpointRounding.AwayFromZero);
                    else
                        Value = Math.Round(Value, 4, MidpointRounding.AwayFromZero);

                    LeaveBox.Text = Value.ToString();
                }
                else
                    LeaveBox.Text = "0,00";
            }
        }

        private void Value_KeyDown(object sender, KeyEventArgs e)
        {
                if ((e.Key >= Key.D0 && e.Key <= Key.D9) ||
                (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
            {
                //Nic nie robi
            }
            else if (e.Key == Key.OemComma || e.Key == Key.Decimal)
            {
                if (e.OriginalSource.ToString().IndexOf(',') != -1)
                    e.Handled = true;
                if ((e.Source as TextBox).SelectionStart == 0)
                    e.Handled = true;
            }
            else if(e.Key == Key.Tab)
            {
                //Nic nie rób jak jest tab (aby przeskakiwał dalej do innego TextBoxa)
            }
            else
                e.Handled = true;
        }

        private void Save_button_Click(object sender, RoutedEventArgs e)
        {
            if (Currency_Controller.Update_Year(
                (decimal)Year_num.Value, 
                decimal.Parse(ECCC_TextBox.Text), 
                decimal.Parse(Euro_TextBox.Text), 
                decimal.Parse(Dolar_TextBox.Text), 
                decimal.Parse(Sek_TextBox.Text)))
            {
                MessageBox.Show("Data for " + Year_num.Value.ToString() + " Updated", "Update Currency");
            }
            else
            {
                MessageBox.Show("Data for " + Year_num.Value.ToString() + " NOT UPDATED!!!!!!!!", "Error!");
            }
        }

        private void Year_num_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            var CurrencyRecord = Currency_Controller.Lead_Year((decimal)Year_num.Value);

            ECCC_TextBox.Text = CurrencyRecord.ECCC.ToString();
            Euro_TextBox.Text = CurrencyRecord.Euro.ToString();
            Dolar_TextBox.Text = CurrencyRecord.Dolar.ToString();
            Sek_TextBox.Text = CurrencyRecord.Sek.ToString();
        }
    }
}
