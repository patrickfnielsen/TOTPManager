using System.Windows;
using TOTPManager.Interfaces;

namespace TOTPManager.Views
{
    /// <summary>
    /// Interaction logic for RenameAccountWindow.xaml
    /// </summary>
    public partial class RenameAccountWindow : Window, IClosable
    {
        public RenameAccountWindow()
        {
            InitializeComponent();
        }
    }
}
