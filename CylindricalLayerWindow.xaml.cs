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
    }
}
