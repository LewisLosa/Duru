using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duru.App.Services
{
    public interface IDialogService
    {
        void ShowMessage(string message, string title = "Information");
        Task<bool> ShowConfirmationAsync(string title, string message);
        Task<string> ShowInputDialogAsync(string title, string message, string defaultValue = "");
    }
}
