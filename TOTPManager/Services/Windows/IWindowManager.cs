using Framework.View;

namespace TOTPManager.Services.Windows
{
    public interface IWindowManager
    {
        void ShowWindow(NotifyBase viewModel);

        void ShowDialog(NotifyBase viewModel);
    }
}
