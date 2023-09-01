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
using System.Windows.Threading;
using TCC.Classes;

namespace TCC
{
    /// <summary>
    /// Interaction logic for SectionListWindow.xaml
    /// </summary>
    public partial class SectionListWindow : Window
    {
        private string sectionName;
        private Cable cable;
        private List<Section> sections;
        private ObservableCollection<Layer> observableLayer;
        private ObservableCollection<LayerConnection> observableConnection;
        ObservableCollection<Section> observableSections = new ObservableCollection<Section>();
        bool isChildWindowOpen = false;

        public SectionListWindow(Cable cable, ObservableCollection<Layer> observableLayer, ObservableCollection<LayerConnection> observableConnection)
        {
            InitializeComponent();
            sections = cable.Sections;
            this.cable = cable;
            this.observableLayer = observableLayer;
            this.observableConnection = observableConnection;

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
                        PopUpTextBlock.Text = windowMaterial.RectangularSection.Name + " Edited Successfully";
                        popup.IsOpen = true;
                    }
                    else if (sections[i].Type == "tubular")
                    {
                        sections[i] = windowMaterial.TubularSection;
                        observableSections[i] = windowMaterial.TubularSection;
                        PopUpTextBlock.Text = windowMaterial.TubularSection.Name + " Edited Successfully";
                        popup.IsOpen = true;
                    }
                }
            }
        }

        Layer sectionLayer;
        private void DeleteSectionButton(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            sectionName = button.Tag.ToString();
            bool foundLayer = false;
            foreach (Layer l in cable.Layers)
            {
                if (l.Type == "helix" || l.Type == "armor")
                {
                    HelixLayer layer = l as HelixLayer;
                    if (layer.Section.Name == sectionName)
                    {
                        foundLayer = true;
                        sectionLayer = layer;
                    }
                }
            }

            if (!foundLayer)
            {
                // Delete Section if no layer is found with it
                for (int i = 0; i < sections.Count; i++)
                {
                    if (sections[i].Name == sectionName)
                    {
                        observableSections.Remove(sections[i]);
                        PopUpTextBlock.Text = sectionName + " Deleted Successfully";
                        sections.Remove(sections[i]);
                        popup.IsOpen = true;
                    }
                }
            }
            else
            {
                WarningWindow windowWarning = new WarningWindow(
                    "This Section is part of a layer. Deleting it will also delete the layer. " +
                    "If the layer has a connection, it will also be deleted. Are you sure you want to continue?"
                    );
                windowWarning.ConfirmButtonClick += ConfirmButtonClick;
                windowWarning.CancelButtonClick += CancelButtonClick;
                windowWarning.Show();
            }
        }
        private void ConfirmButtonClick(object sender, EventArgs e)
        {
            // First delete layer
            cable.Layers.Remove(sectionLayer);
            observableLayer.Remove(sectionLayer);
            // Delete also connection if it exists
            for (int i = 0; i < cable.LayerConnections.Count; i++)
            {
                if (cable.LayerConnections[i].FirstLayer == sectionLayer.Name || cable.LayerConnections[i].SecondLayer == sectionLayer.Name)
                {
                    observableConnection.Remove(cable.LayerConnections[i]);
                    PopUpTextBlock.Text = cable.LayerConnections[i].Name + " Deleted Successfully";
                    cable.LayerConnections.Remove(cable.LayerConnections[i]);
                    popup.IsOpen = true;
                }
            }

            // Then delete section after confirmation
            for (int i = 0; i < sections.Count; i++)
            {
                if (sections[i].Name == sectionName)
                {
                    observableSections.Remove(sections[i]);
                    PopUpTextBlock.Text = sectionName + " Deleted Successfully";
                    sections.Remove(sections[i]);
                    popup.IsOpen = true;
                }
            }
        }
        private void CancelButtonClick(object sender, EventArgs e)
        {
            return;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (isChildWindowOpen) e.Cancel = true;
        }

        private void Popup_Opened(object sender, EventArgs e)
        {
            StartCloseTimer();
        }

        private void StartCloseTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3d);
            timer.Tick += TimerTick;
            timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            DispatcherTimer timer = (DispatcherTimer)sender;
            timer.Stop();
            timer.Tick -= TimerTick;
            popup.IsOpen = false;
        }
    }
}
