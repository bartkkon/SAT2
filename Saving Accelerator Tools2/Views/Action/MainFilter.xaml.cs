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
        public MainFilter()
        {
            InitializeComponent();
        }
    }
}
