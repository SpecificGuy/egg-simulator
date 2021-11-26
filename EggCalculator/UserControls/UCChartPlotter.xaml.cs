using InteractiveDataDisplay.WPF;
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

namespace EggCalculator.UserControls
{
    /// <summary>
    /// Interaction logic for UCChartPlotter.xaml
    /// </summary>
    public partial class UCChartPlotter : UserControl
    {
        public LineGraph LineGraph { get; set; }
        public UCChartPlotter(LineGraph lg)
        {
            InitializeComponent();
            LineGraph = lg;
            ChartInstance.Children.Add(lg);
        }
    }
}
