﻿using System.Windows.Controls;

using Saving_Accelerator_Tools2.ViewModels;

namespace Saving_Accelerator_Tools2.Views
{
    public partial class AdminSQLViewPage : Page
    {
        public AdminSQLViewPage(AdminSQLViewViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
