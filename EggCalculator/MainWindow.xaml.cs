using EggCalculator.Models;
using EggCalculator.Utility;
using EggCalculator.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace EggCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string donationAddress = "0xDCa11E1108180D9638Efcc71a4c9db39e97A3085";

        public string DonationAddress
        {
            get { return donationAddress; }
            set { donationAddress = value; }
        }

        public MainWindow()
        {
            InitializeComponent();
            SimulationViewModel vm;
            try
            {
                vm = JsonConvert.DeserializeObject<SimulationViewModel>(File.ReadAllText(@"configuration\configuration.json"));
                vm.calendar = Calendar;
                vm.calendar.CalendarDayMouseLeftClickedEvent += vm.Calendar_CalendarDayMouseLeftClickedEvent;
            } catch(Exception e)
            {
               vm = new SimulationViewModel(Calendar);
            }

            DataContext = vm;
            vm.OnSimulationEnd += Charts.OnSimulationEnd;
            vm.Simulate();
        }
    }
}
