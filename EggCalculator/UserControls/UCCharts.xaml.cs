using EggCalculator.Models;
using EggCalculator.ViewModels;
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
using WPF.EventCalendar;

namespace EggCalculator.UserControls
{
    /// <summary>
    /// Interaction logic for UCCharts.xaml
    /// </summary>
    public partial class UCCharts : UserControl
    {
        private List<ICalendarEvent> Events { get; set; }
        private double[] X { get; set; }

        public UCCharts()
        {
            InitializeComponent();
        }

        private void DrawCharts()
        {
            var x =
            from ev in Events
            where ev.GetType().Equals(typeof(Simulation))
            select ev.DateFrom.Value.Subtract(DateTime.Today).TotalDays;

            X = x.ToArray();

            DrawEggCharts();
            DrawFragmentChart();
            DrawMetamonChart();
            DrawRevenueChart();
            DrawMatchCostChart();
        }
        private void DrawEggCharts()
        {
            var y =
            from ev in Events
            where ev.GetType().Equals(typeof(Simulation))
            select ((Simulation)ev).DailyResult.EggsMinted;

            y = y.ToArray();

            EggChart.Plot(X, y);
        }

        private void DrawFragmentChart()
        {
            var y =
            from ev in Events
            where ev.GetType().Equals(typeof(Simulation))
            select ((Simulation)ev).GameResult.Fragments;

            y = y.ToArray();

            FragmentChart.Plot(X, y);
        }

        private void DrawMetamonChart()
        {
            var y =
            from ev in Events
            where ev.GetType().Equals(typeof(Simulation))
            select ((Simulation)ev).AccountState.Metamons.Count;

            y = y.ToArray();

            MetamonCountChart.Plot(X, y);
        }

        private void DrawRevenueChart()
        {
            var y =
            from ev in Events
            where ev.GetType().Equals(typeof(Simulation))
            select ((Simulation)ev).AccountState.RacaRevenue;

            y = y.ToArray();

            RevenueChart.Plot(X, y);
        }

        private void DrawMatchCostChart()
        {
            var y =
            from ev in Events
            where ev.GetType().Equals(typeof(Simulation))
            select ((Simulation)ev).GameResult.MatchCost;

            y = y.ToArray();

            MatchCostChart.Plot(X, y);
        }

        public void OnSimulationEnd(object sender, List<ICalendarEvent> e)
        {
            Events = e;
            DrawCharts();
        }
    }
}
