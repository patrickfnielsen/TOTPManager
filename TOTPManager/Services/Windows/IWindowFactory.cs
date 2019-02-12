using System.Windows;

namespace TOTPManager.Services.Windows
{
    public interface IWindowFactory
    {
        Window CreateWindow(NotifyBase model);
    }
}
