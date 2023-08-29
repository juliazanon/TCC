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
using TCC.Classes;

namespace TCC
{
    /// <summary>
    /// Interaction logic for CylindricalLayerWindow.xaml
    /// </summary>
    public partial class CylindricalLayerWindow : Window
    {
        public double Length { get; set; }
        public double Radius { get; set; }
        public double Thickness { get; set; }
        public int FourierOrder { get; set; }
        public int RadialDivisions { get; set; }
        public int AxialDivisions { get; set; }
        public Area[] Areas { get; set; }
        public string Label { get; set; }
        public string Type { get; set; }
        public int MaterialID { get; set; }
        public double[] BodyLoad { get; set; }
        public CylindricalLayerWindow(Dictionary<int, LayerMaterial> materials)
        {
            InitializeComponent();
        }

        //  Material
        private void MaterialComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MaterialComboBox.SelectedItem != null)
            {
                // Get the selected Material instance.
                LayerMaterial selectedMaterial = (LayerMaterial)MaterialComboBox.SelectedItem;

                // Access the selected ID and Name.
                int selectedMaterialID = selectedMaterial.ID;

                // USE THIS VALUE AS MATERIAL ID

                // teste.Text = selectedID.ToString();
            }
        }

        //  Areas
        //  Internal
        private void ButtonNewAreaInternal(object sender, RoutedEventArgs e)
        {
            string areaType = "Internal";
            CylindricalAreasWindow windowArea = new CylindricalAreasWindow(areaType);

            //windowArea.SubmitButtonClick += SubmitAreaInternalButtonClick;
            windowArea.Show();
        }

        //  Bottom
        private void ButtonNewAreaBottom(object sender, RoutedEventArgs e)
        {
            string areaType = "Bottom";
            CylindricalAreasWindow windowArea = new CylindricalAreasWindow(areaType);

            //windowArea.SubmitButtonClick += SubmitAreaBottomButtonClick;
            windowArea.Show();
        }

        //  External
        private void ButtonNewAreaExternal(object sender, RoutedEventArgs e)
        {
            string areaType = "External";
            CylindricalAreasWindow windowArea = new CylindricalAreasWindow(areaType);

            //windowArea.SubmitButtonClick += SubmitAreaExternalButtonClick;
            windowArea.Show();
        }

        //  Top
        private void ButtonNewAreaTop(object sender, RoutedEventArgs e)
        {
            string areaType = "Top";
            CylindricalAreasWindow windowArea = new CylindricalAreasWindow(areaType);

            //windowArea.SubmitButtonClick += SubmitAreaTopButtonClick;
            windowArea.Show();
        }
    }
}
