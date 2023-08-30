using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TCC.Classes;

namespace TCC
{
    /// <summary>
    /// Interaction logic for SectionListWindow.xaml
    /// </summary>
    public partial class SectionListWindow : Window
    {
        ObservableCollection<Section> observableSections = new ObservableCollection<Section>();
        public SectionListWindow(List<Section> sections)
        {
            InitializeComponent();
            for (int i = 0; i < sections.Count(); i++)
            {
                observableSections.Add(sections[i]);
            }
            itemsControl.ItemsSource = observableSections;
        }
    }
}
