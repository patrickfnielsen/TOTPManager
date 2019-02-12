using System.Collections.Generic;
using System.Windows;

namespace TOTPManager.Services.Windows
{
    public class WindowManager : IWindowManager
    {
        private readonly Dictionary<NotifyBase, Window> _windows;
        private readonly IWindowFactory _windowFactory;

        public WindowManager(IWindowFactory factory)
        {
            _windows = new Dictionary<NotifyBase, Window>();
            _windowFactory = factory;
        }

        public void ShowDialog(NotifyBase viewModel)
        {
            var window = GetOrCreateWindow(viewModel);
            window.ShowDialog();
        }

        public void ShowWindow(NotifyBase viewModel)
        {
            var window = GetOrCreateWindow(viewModel);
            window.Show();
        }

        private Window GetOrCreateWindow(NotifyBase viewModel)
        {
            if(_windows.TryGetValue(viewModel, out var existingWindow))
            {
                return existingWindow;
            }

            var newWindow = _windowFactory.CreateWindow(viewModel);
            newWindow.DataContext = viewModel;

            _windows.Add(viewModel, newWindow);

            return newWindow;
        }
    }
}
