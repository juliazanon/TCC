﻿using System;
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
    /// Interaction logic for SectionWindow.xaml
    /// </summary>
    public partial class SectionWindow : Window
    {
        public SectionWindow()
        {
            InitializeComponent();

            CilyndricalSection.Visibility = Visibility.Collapsed;
            sectionComboBox.SelectionChanged += SectionComboBox_SelectionChanged;
        }

        private void SectionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            string selectedParameter = ((ComboBoxItem)comboBox.SelectedItem).Content.ToString();

            if (selectedParameter == "Section Rectangular")
            {
                // Show Rectangular
                RectangularSection.Visibility = Visibility.Visible;

                // Hide Cilyndrical
                CilyndricalSection.Visibility = Visibility.Collapsed;
            }
            else if (selectedParameter == "Section Cylindrical")
            {
                // Show Cilyndrical
                CilyndricalSection.Visibility = Visibility.Visible;

                // Hide Rectangular
                RectangularSection.Visibility = Visibility.Collapsed;
            }
        }
    }
}
