using SharpGL;
using SharpGL.Enumerations;
using GlmNet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Forms;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Media;
using System.Linq;
using Button = System.Windows.Controls.Button;
using TCC.MainClasses;
using Newtonsoft.Json;
using System.IO;
using MessageBox = System.Windows.Forms.MessageBox;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace TCC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Cable cable;
        ObservableCollection<Layer> observableLayer = new ObservableCollection<Layer>();
        ObservableCollection<LayerConnection> observableConnection = new ObservableCollection<LayerConnection>();
        bool isChildWindowOpen = false;
        string selectedLayer = "";

        public MainWindow()
        {
            InitializeComponent();

            itemsControl.ItemsSource = observableLayer;
            connectionsControl.ItemsSource = observableConnection;

            cable = new Cable
            {
                Name = "New Cable",
                Sections = new List<Section>(),
                Layers = new List<Layer>(),
                LayerConnections = new List<LayerConnection>(),
                LayerMaterials = new List<LayerMaterial>()
            };
        }

        //  Menu
        //  Cylinder Layer
        private void ButtonNewCylinder(object sender, RoutedEventArgs e)
        {
            CylindricalLayerWindow windowCylinder = new CylindricalLayerWindow(cable.Layers, cable.LayerMaterials);
            windowCylinder.SubmitButtonClick += SubmitCylinderButtonClick;
            windowCylinder.Closed += ChildWindow_Closed;
            windowCylinder.Show();
            this.IsEnabled = false;
            isChildWindowOpen = true;
        }
        private void SubmitCylinderButtonClick(object sender, EventArgs e)
        {
            CylindricalLayerWindow windowCylinder = sender as CylindricalLayerWindow;
            CylinderLayer layer = windowCylinder.CylinderLayer;

            cable.Layers.Add(layer);
            observableLayer.Add(layer);
            itemsControl.ItemsSource = observableLayer;

            PopUpTextBlock.Text = layer.Name + " Created Successfully";
            popup.IsOpen = true;
        }

        // Helix Layer
        private void ButtonNewHelix(object sender, RoutedEventArgs e)
        {
            HelicalLayerWindow windowHelix = new HelicalLayerWindow(cable.Layers, cable.Sections, cable.LayerMaterials);
            windowHelix.SubmitButtonClick += SubmitHelixButtonClick;
            windowHelix.Closed += ChildWindow_Closed;
            windowHelix.Show();
            this.IsEnabled = false;
            isChildWindowOpen = true;
        }
        private void SubmitHelixButtonClick(object sender, EventArgs e)
        {
            HelicalLayerWindow windowHelix = sender as HelicalLayerWindow;
            HelixLayer layer = windowHelix.HelixLayer;

            cable.Layers.Add(layer);
            observableLayer.Add(layer);
            itemsControl.ItemsSource = observableLayer;

            PopUpTextBlock.Text = layer.Name + " Created Successfully";
            popup.IsOpen = true;
        }

        // Connection Layer
        private void ButtonNewConnection(object sender, RoutedEventArgs e)
        {
            LayerConnectionsWindow windowConnection = new LayerConnectionsWindow(cable.LayerConnections, cable.Layers);
            windowConnection.SubmitButtonClick += SubmitConnectionButtonClick;
            windowConnection.Closed += ChildWindow_Closed;
            windowConnection.Show();
        }
        private void SubmitConnectionButtonClick(object sender, EventArgs e)
        {
            LayerConnectionsWindow windowConnection = sender as LayerConnectionsWindow;
            LayerConnection layerConnection = windowConnection.LayerConnection;

            cable.LayerConnections.Add(layerConnection);
            observableConnection.Add(layerConnection);
            connectionsControl.ItemsSource = observableConnection;

            PopUpTextBlock.Text = layerConnection.Name + " Created Successfully";
            popup.IsOpen = true;
        }

        // Edit / Delete Layers
        string layerName = "";
        private void ButtonEditLayer(object sender, RoutedEventArgs e)
        {
            if (selectedLayer != "")
            {
                foreach (Layer l in cable.Layers)
                {
                    if (l.Name == selectedLayer)
                    {
                        layerName = l.Name;
                        if (l.Type == "helix" || l.Type == "armor")
                        {
                            HelixLayer hl = l as HelixLayer;
                            HelicalLayerWindow windowHelix = new HelicalLayerWindow(cable.Layers, cable.Sections, cable.LayerMaterials, hl);
                            windowHelix.SubmitButtonClick += EditHelixButtonClick;
                            windowHelix.Closed += ChildWindow_Closed;
                            windowHelix.Show();

                            this.IsEnabled = false;
                            isChildWindowOpen = true;
                        }
                        else if (l.Type == "cylinder")
                        {
                            CylinderLayer cl = l as CylinderLayer;
                            CylindricalLayerWindow windowCylinder = new CylindricalLayerWindow(cable.Layers, cable.LayerMaterials, cl);
                            windowCylinder.SubmitButtonClick += EditCylinderButtonClick;
                            windowCylinder.Closed += ChildWindow_Closed;
                            windowCylinder.Show();

                            this.IsEnabled = false;
                            isChildWindowOpen = true;
                        }
                    }
                }
                foreach (LayerConnection lc in cable.LayerConnections)
                {
                    if (lc.Name == selectedLayer)
                    {
                        layerName = lc.Name;
                        LayerConnectionsWindow windowConnection = new LayerConnectionsWindow(cable.LayerConnections, cable.Layers, lc);
                        windowConnection.SubmitButtonClick += EditConnectionButtonClick;
                        windowConnection.Closed += ChildWindow_Closed;
                        windowConnection.Show();

                        this.IsEnabled = false;
                        isChildWindowOpen = true;
                    }
                }
            }
        }
        private void EditCylinderButtonClick(object sender, EventArgs e)
        {
            CylindricalLayerWindow windowCylinder = sender as CylindricalLayerWindow;
            CylinderLayer layer = windowCylinder.CylinderLayer;
            for (int i = 0; i < cable.Layers.Count; i++)
            {
                if (layerName == cable.Layers[i].Name)
                {
                    cable.Layers[i] = layer;
                    observableLayer[i] = layer;
                    PopUpTextBlock.Text = layer.Name + " Edited Successfully";
                    popup.IsOpen = true;

                    previoussrcButton.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xE0, 0xE0, 0xE0));
                    previoussrcButton = null;
                }
            }
        }
        private void EditHelixButtonClick(object sender, EventArgs e)
        {
            HelicalLayerWindow windowHelix = sender as HelicalLayerWindow;
            HelixLayer layer = windowHelix.HelixLayer;
            for (int i = 0; i < cable.Layers.Count; i++)
            {
                if (layerName == cable.Layers[i].Name)
                {
                    cable.Layers[i] = layer;
                    observableLayer[i] = layer;
                    PopUpTextBlock.Text = layer.Name + " Edited Successfully";
                    popup.IsOpen = true;

                    previoussrcButton.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xE0, 0xE0, 0xE0));
                    previoussrcButton = null;
                }
            }
        }
        private void EditConnectionButtonClick(object sender, EventArgs e)
        {
            LayerConnectionsWindow windowConnection = sender as LayerConnectionsWindow;
            LayerConnection connection = windowConnection.LayerConnection;
            for (int i = 0; i < cable.LayerConnections.Count; i++)
            {
                if (layerName == cable.LayerConnections[i].Name)
                {
                    cable.LayerConnections[i] = connection;
                    observableConnection[i] = connection;
                    PopUpTextBlock.Text = connection.Name + " Edited Successfully";
                    popup.IsOpen = true;

                    previoussrcButton.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xE0, 0xE0, 0xE0));
                    previoussrcButton = null;
                }
            }
        }

        private void ButtonDeleteLayer(object sender, RoutedEventArgs e)
        {
            if (selectedLayer != "")
            {
                bool foundConnection = false;
                foreach (LayerConnection lc in cable.LayerConnections)
                {
                    if (lc.FirstLayer == selectedLayer || lc.SecondLayer == selectedLayer)
                    {
                        foundConnection = true;
                    }
                }
                if (!foundConnection)
                {
                    // Delete Layer if no connection is found with it
                    for (int i = 0; i < cable.Layers.Count; i++)
                    {
                        if (cable.Layers[i].Name == selectedLayer)
                        {
                            observableLayer.Remove(cable.Layers[i]);
                            PopUpTextBlock.Text = cable.Layers[i].Name + " Deleted Successfully";
                            cable.Layers.Remove(cable.Layers[i]);
                            popup.IsOpen = true;
                        }
                    }
                    for (int i = 0; i < cable.LayerConnections.Count; i++)
                    {
                        if (cable.LayerConnections[i].Name == selectedLayer)
                        {
                            observableConnection.Remove(cable.LayerConnections[i]);
                            PopUpTextBlock.Text = cable.LayerConnections[i].Name + " Deleted Successfully";
                            cable.LayerConnections.Remove(cable.LayerConnections[i]);
                            popup.IsOpen = true;
                        }
                    }

                }
                else
                {
                    // If connection is found, throw warning
                    WarningWindow windowWarning = new WarningWindow(
                    "This Layer is part of a Connection. Deleting it will also delete the connection. Are you sure you want to continue?"
                    );
                    windowWarning.Owner = this;

                    if (windowWarning.ShowDialog() == true)
                    {
                        for (int i = 0; i < cable.Layers.Count; i++)
                        {
                            if (cable.Layers[i].Name == selectedLayer)
                            {
                                observableLayer.Remove(cable.Layers[i]);
                                PopUpTextBlock.Text = cable.Layers[i].Name + " Deleted Successfully";
                                cable.Layers.Remove(cable.Layers[i]);
                                popup.IsOpen = true;
                            }
                        }
                        for (int i = 0; i < cable.LayerConnections.Count; i++)
                        {
                            if (cable.LayerConnections[i].FirstLayer == selectedLayer || cable.LayerConnections[i].SecondLayer == selectedLayer)
                            {
                                observableConnection.Remove(cable.LayerConnections[i]);
                                PopUpTextBlock.Text = cable.LayerConnections[i].Name + " Deleted Successfully";
                                cable.LayerConnections.Remove(cable.LayerConnections[i]);
                                popup.IsOpen = true;
                            }
                        }
                    }
                }
            }
        }

        //  Materials
        private void ButtonNewMaterial(object sender, RoutedEventArgs e)
        {
            MaterialsWindow windowMaterial = new MaterialsWindow(cable.LayerMaterials);
            windowMaterial.SubmitButtonClick += SubmitMaterialButtonClick;
            windowMaterial.Closed += ChildWindow_Closed;
            windowMaterial.Show();
            this.IsEnabled = false;
            isChildWindowOpen = true;
        }
        private void SubmitMaterialButtonClick(object sender, EventArgs e)
        {
            MaterialsWindow windowMaterial = sender as MaterialsWindow;
            if (windowMaterial.LayerIsotropic != null)
            {
                Isotropic materialIsotropic = windowMaterial.LayerIsotropic;

                cable.LayerMaterials.Add(materialIsotropic);
                PopUpTextBlock.Text = materialIsotropic.Name + " Created Successfully";
                popup.IsOpen = true;
            }

            else if (windowMaterial.LayerOrthotropic != null)
            {
                Orthotropic materialOrthotropic = windowMaterial.LayerOrthotropic;

                cable.LayerMaterials.Add(materialOrthotropic);
                PopUpTextBlock.Text = materialOrthotropic.Name + " Created Successfully";
                popup.IsOpen = true;
            }
        }
        private void ButtonMaterialList(object sender, RoutedEventArgs e)
        {
            MaterialListWindow windowMaterial = new MaterialListWindow(cable);
            windowMaterial.Closed += ChildWindow_Closed;
            windowMaterial.Show();
            this.IsEnabled = false;
            isChildWindowOpen = true;
        }

        // Sections
        private void ButtonNewSection(object sender, RoutedEventArgs e)
        {
            SectionWindow windowSection = new SectionWindow(cable.Sections);
            windowSection.SubmitButtonClick += SubmitSectionButtonClick;
            windowSection.Closed += ChildWindow_Closed;
            windowSection.Show();
            this.IsEnabled = false;
            isChildWindowOpen = true;
        }
        private void SubmitSectionButtonClick(object sender, EventArgs e)
        {
            SectionWindow windowSection = sender as SectionWindow;
            if (windowSection.RectangularSection != null)
            {
                RectangularSection rectangularSection = windowSection.RectangularSection;
                cable.Sections.Add(rectangularSection);
                PopUpTextBlock.Text = rectangularSection.Name + " Created Successfully";
                popup.IsOpen = true;
            }
            else if (windowSection.TubularSection != null)
            {
                TubularSection tubularSection = windowSection.TubularSection;
                cable.Sections.Add(tubularSection);
                PopUpTextBlock.Text = tubularSection.Name + " Created Successfully";
                popup.IsOpen = true;
            }
        }

        private void ButtonSectionList(object sender, RoutedEventArgs e)
        {
            SectionListWindow windowSectionList = new SectionListWindow(cable, observableLayer, observableConnection);
            windowSectionList.Closed += ChildWindow_Closed;
            windowSectionList.Show();
            this.IsEnabled = false;
            isChildWindowOpen = true;
        }

        // Upper menu
        private void SaveButtonClick(object sender, EventArgs e)
        {
            cable.SaveFile(); // Saves json file
        }

        // Open json file and replace current cable
        private void OpenButtonClick(object sender, EventArgs e)
        {
            // If changes are found, throw warning
            if ((cable.Layers.Count != 0 || cable.LayerMaterials.Count != 0 || cable.Sections.Count != 0) && IsNotSavedWork())
            {
                WarningWindow windowWarning = new WarningWindow(
                "This will ovewrite all current changes. Are you sure you want to continue?"
                );
                windowWarning.Owner = this;

                if (windowWarning.ShowDialog() == true)
                {
                    UploadCableFile();
                }
            }
            else
            {
                UploadCableFile();
            }
        }

        private void UploadCableFile()
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                InitialDirectory = @"c:\\",
                Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true
            };
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string filePath = dialog.FileName;
                string fileExtension = Path.GetExtension(filePath);

                if (fileExtension.Equals(".json", StringComparison.OrdinalIgnoreCase))
                {
                    try
                    {
                        string json = File.ReadAllText(filePath);
                        Cable oldCable = cable;
                        cable = JsonConvert.DeserializeObject<Cable>(json);
                        observableConnection.Clear();
                        observableLayer.Clear();

                        if (cable.Layers == null || cable.LayerConnections == null || cable.LayerMaterials == null || cable.Sections == null)
                        {
                            cable = oldCable;
                            throw new Exception();
                        }

                        foreach (Layer l in cable.Layers)
                        {
                            observableLayer.Add(l);
                        }
                        int lccount = 1;
                        foreach (LayerConnection lc in cable.LayerConnections)
                        {
                            observableConnection.Add(lc);
                            lc.Name = "Connection" + lccount.ToString();
                            lccount++;
                        }
                        int mcount = 1;
                        foreach (LayerMaterial m in cable.LayerMaterials)
                        {
                            m.Name = "Material" + mcount.ToString();
                            mcount++;
                        }
                        int scount = 1;
                        foreach (Section s in cable.Sections)
                        {
                            s.Name = "Section" + scount.ToString();
                            scount++;
                        }

                        // set sections and materials from IDs
                        foreach (Layer l in cable.Layers)
                        {
                            foreach (LayerMaterial m in cable.LayerMaterials)
                            {
                                if (m.ID == l.MaterialID) l.Material = m;
                            }
                            if (l.Type == "helix" || l.Type == "armor")
                            {
                                foreach (Section s in cable.Sections)
                                {
                                    HelixLayer layer = l as HelixLayer;
                                    if (s.ID == layer.SectionID) layer.Section = s;
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Please select a JSON file with the correct structure");
                    }
                }
                else
                {
                    // The selected file is not a JSON file
                    MessageBox.Show("Please select a valid JSON file (.json).");
                }
            }
        }

        // List of layers and connections
        Button previoussrcButton;
        private void ButtonSelectLayer(object sender, RoutedEventArgs e)
        {
            if (previoussrcButton != null)
            {
                // Unselect previous layer
                previoussrcButton.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xE0, 0xE0, 0xE0));
            }

            Button srcButton = e.Source as Button;
            if (srcButton == previoussrcButton)
            {
                // If button is already selected, return to original state
                srcButton.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xE0, 0xE0, 0xE0));
                selectedLayer = "";
                previoussrcButton = null;
            }
            else
            {
                // Select button
                srcButton.Background = new SolidColorBrush(Color.FromArgb(0xA0, 0xA0, 0xFF, 0xFF));
                previoussrcButton = srcButton;

                // Retrieve selected layer
                Grid contentGrid = (Grid)(sender as Button).Content;
                TextBlock contentTextBlock = (TextBlock)contentGrid.Children.Cast<UIElement>().FirstOrDefault(f => Grid.GetColumn(f) == 0);
                foreach (Layer l in cable.Layers)
                {
                    if (l.Name == contentTextBlock.Text) selectedLayer = l.Name;
                }
            }
        }

        private void ButtonSelectConnection(object sender, RoutedEventArgs e)
        {
            if (previoussrcButton != null)
            {
                // Unselect previous connection
                previoussrcButton.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xE0, 0xE0, 0xE0));
            }

            Button srcButton = e.Source as Button;
            if (srcButton == previoussrcButton)
            {
                // If button is already selected, return to original state
                srcButton.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xE0, 0xE0, 0xE0));
                selectedLayer = "";
                previoussrcButton = null;
            }
            else
            {
                // Select button
                srcButton.Background = new SolidColorBrush(Color.FromArgb(0xA0, 0xA0, 0xFF, 0xFF));
                previoussrcButton = srcButton;

                // Retrieve selected connection
                Grid contentGrid = (Grid)(sender as Button).Content;
                TextBlock contentTextBlock = (TextBlock)(contentGrid as Grid).Children.Cast<UIElement>().FirstOrDefault(f => Grid.GetColumn(f) == 0);
                foreach (LayerConnection lc in cable.LayerConnections)
                {
                    if (lc.Name == contentTextBlock.Text) selectedLayer = lc.Name;
                }
            }
        }

        // Helper functions
        private void MouseWheelHandler(object sender, MouseWheelEventArgs e)
        {
            int delta = e.Delta;
            if (delta > 0)
            {
                // Zoom in
                scale *= 1.1f;
            }
            else if (delta < 0)
            {
                // Zoom out
                scale /= 1.1f;
            }
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.OemPlus || e.Key == Key.Add)) { scale *= 1.1f; }
            else if ((e.Key == Key.OemMinus || e.Key == Key.Subtract) && scale > 0) { scale /= 1.1f; }
        }

        private void ChildWindow_Closed(object sender, EventArgs e)
        {
            this.IsEnabled = true;
            isChildWindowOpen = false;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (isChildWindowOpen) e.Cancel = true;

            else if ((cable.Layers.Count != 0 || cable.LayerMaterials.Count != 0 || cable.Sections.Count != 0) && IsNotSavedWork())
            {
                WarningWindow windowWarning = new WarningWindow("Your changes will be lost. Are you sure you want to close the application?");
                windowWarning.Owner = this;

                if (windowWarning.ShowDialog() == false) e.Cancel = true;
            }
        }
        private bool IsNotSavedWork()
        {
            string json = JsonConvert.SerializeObject(cable, Formatting.Indented);

            return (json != cable.LastSavedFile);
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

        // Graphics
        // Camera parameters
        float[] _viewPoint = new float[] { 0.0f, 0.0f, 0.0f };
        float[] _position = new float[] { 0.0f, 0.0f, 10.0f };
        float[] _upVector = new float[] { 0.0f, 1.0f, 0.0f };
        float scale = 0.04f;

        private void ZoomButtonClick(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            if (clickedButton.Name == "ButtonZoomIn") { scale += 0.01f; }
            else if (clickedButton.Name == "ButtonZoomOut") { scale -= 0.01f; }
        }

        private void OpenGLDraw(object sender, SharpGL.WPF.OpenGLRoutedEventArgs args)
        {
            OpenGL gl = GLControl.OpenGL;
            float w = (float)GLControl.ActualWidth;
            float h = (float)GLControl.ActualHeight;

            //  Perspective projection
            gl.MatrixMode(MatrixMode.Projection);
            gl.LoadIdentity();
            gl.Perspective(60, (int)w / h, 0.01, 1000);
            gl.Scale(scale, scale, 1);

            //  Camera Position
            gl.LookAt(_position[0], _position[1], _position[2],
                _viewPoint[0], _viewPoint[1], _viewPoint[2],
                _upVector[0], _upVector[1], _upVector[2]);

            gl.MatrixMode(MatrixMode.Modelview);
            gl.LoadIdentity();

            //  Clear The Screen And The Depth Buffer
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            // Background color
            vec3 backrgb = new vec3(225, 227, 226) / 255;
            gl.ClearColor(backrgb.x, backrgb.y, backrgb.z, 0);
            vec3 rgb;

            float prop = w * h / 1000000;

            foreach (Layer l in cable.Layers)
            {
                if (l is HelixLayer)
                {
                    HelixLayer layer = l as HelixLayer;
                    rgb = new vec3(80, 80, 80) / 255;
                    layer.Draw(gl, rgb);
                }
                else if (l is CylinderLayer)
                {
                    CylinderLayer layer = l as CylinderLayer;
                    rgb = new vec3(150, 150, 150) / 255;
                    layer.Draw(gl, rgb, 10000, prop, false);
                }
            }
        }
    }
}
