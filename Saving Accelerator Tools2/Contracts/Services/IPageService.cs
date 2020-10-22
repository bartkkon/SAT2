using System;
using System.Windows.Controls;

namespace Saving_Accelerator_Tools2.Contracts.Services
{
    public interface IPageService
    {
        Type GetPageType(string key);

        Page GetPage(string key);
    }
}
