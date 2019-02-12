using System;
using System.Linq;
using System.Windows;

namespace TOTPManager.Services.Windows
{
    public class WindowFactory : IWindowFactory
    {
        public Window CreateWindow(NotifyBase viewModel)
        {
            var modelType = viewModel.GetType();
            var windowTypeName = modelType.Name.Replace("ViewModel", "Window");
            var windowTypes = 
                from t in modelType.Assembly.GetTypes()
                where t.IsClass && t.Name == windowTypeName
                select t;

            return (Window)Activator.CreateInstance(windowTypes.Single());
        }
    }
}
