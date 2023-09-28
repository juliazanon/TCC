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
using System.Windows.Shapes;

namespace TCC
{
    /// <summary>
    /// Interaction logic for WarningWindow.xaml
    /// </summary>
    public partial class WarningWindow : Window
    {
        public event EventHandler ConfirmButtonClick;
        public event EventHandler CancelButtonClick;

        public WarningWindow(string text)
        {
            InitializeComponent();
            ContentTextBlock.Text = text;
        }

        private void YesButtonClick(object sender, RoutedEventArgs e)
        {
            ConfirmButtonClick?.Invoke(this, EventArgs.Empty);
            DialogResult = true;
            Close();
        }
        private void NoButtonClick(object sender, RoutedEventArgs e)
        {
            CancelButtonClick?.Invoke(this, EventArgs.Empty);
            DialogResult = false;
            Close();
        }
    }
}
