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
        private string sectionName;
        private List<Section> sections;
        ObservableCollection<Section> observableSections = new ObservableCollection<Section>();
        bool isChildWindowOpen = false;

        public SectionListWindow(List<Section> sections)
        {
            InitializeComponent();
            this.sections = sections;

            for (int i = 0; i < sections.Count(); i++)
            {
                observableSections.Add(sections[i]);
            }
            itemsControl.ItemsSource = observableSections;
        }

        private void EditSectionButton(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            sectionName = button.Tag.ToString();
            Section section = new Section();
            foreach (Section s in sections) if (s.Name == sectionName) section = s;
            if (section != null)
            {
                SectionWindow windowSection = new SectionWindow(sections, section);
                windowSection.SubmitButtonClick += SubmitSectionButtonClick;
                windowSection.Closed += SectionWindow_Closed;
                windowSection.Show();
            }

            this.IsEnabled = false;
            isChildWindowOpen = true;
        }
        private void SectionWindow_Closed(object sender, EventArgs e)
        {
            this.IsEnabled = true;
            isChildWindowOpen = false;
        }
        private void SubmitSectionButtonClick(object sender, EventArgs e)
        {
            SectionWindow windowMaterial = sender as SectionWindow;
            for (int i = 0; i < sections.Count; i++)
            {
                if (sections[i].Name == sectionName)
                {
                    if (sections[i].Type == "rectangular")
                    {
                        sections[i] = windowMaterial.RectangularSection;
                        observableSections[i] = windowMaterial.RectangularSection;
                    }
                    else if (sections[i].Type == "tubular")
                    {
                        sections[i] = windowMaterial.TubularSection;
                        observableSections[i] = windowMaterial.TubularSection;
                    }
                }
            }
        }

        private void DeleteSectionButton(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            sectionName = button.Tag.ToString();
            for (int i = 0; i < sections.Count; i++)
            {
                if (sections[i].Name == sectionName)
                {
                    observableSections.Remove(sections[i]);
                    sections.Remove(sections[i]);
                }
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (isChildWindowOpen) e.Cancel = true;
        }

    }
}
