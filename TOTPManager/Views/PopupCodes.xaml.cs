using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TOTPManager.Views
{
    /// <summary>
    /// Interaction logic for PopupCodes.xaml
    /// </summary>
    public partial class PopupCodes : UserControl
    {
        public PopupCodes()
        {
            InitializeComponent();
        }

        private void TextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var textblock = (TextBlock)sender;
            Clipboard.SetText(textblock.Text.Replace(" ", ""));
        }
    }
}
