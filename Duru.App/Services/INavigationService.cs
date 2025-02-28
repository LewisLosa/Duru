using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duru.App.Services
{
    public interface INavigationService
    {
        Task NavigateToAsync(string route);
        Task GoBackAsync();
        Task NavigateToRootAsync();
    }
}
