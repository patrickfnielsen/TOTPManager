using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TOTPManager.Services.Windows
{
    public interface IWindowFactory
    {
        Window CreateWindow(NotifyBase model);
    }
}
