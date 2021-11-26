using EggCalculator.Constants;
using EggCalculator.Models;
using EggCalculator.ViewModels;
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
using WPF.EventCalendar;

namespace EggCalculator.UserControls
{
    /// <summary>
    /// Interaction logic for UCCharts.xaml
    /// </summary>
    public partial class UCCharts : UserControl
    {
        private List<ICalendarEvent> Events { get; set; }
        public LineGraph EggLineGraph { get; set; } = new LineGraph();
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

            //GetRarityRatio();

            UCChartPlotter ec = new UCChartPlotter(GetEggGraph("Eggs"));
            ChartGrid.Children.Add(ec);
            Grid.SetRow(ec, 1);
            Grid.SetColumn(ec, 0);

            UCChartPlotter ef = new UCChartPlotter(GetFragmentGraph("Fragments"));
            ChartGrid.Children.Add(ef);
            Grid.SetRow(ef, 1);
            Grid.SetColumn(ef, 1);

            UCChartPlotter em = new UCChartPlotter(GetMetamonGraph("Metamons"));
            ChartGrid.Children.Add(em);
            Grid.SetRow(em, 1);
            Grid.SetColumn(em, 2);

            UCChartPlotter er = new UCChartPlotter(GetRevenueChart("Revenue"));
            ChartGrid.Children.Add(er);
            Grid.SetRow(er, 2);
            Grid.SetColumn(er, 0);

            UCChartPlotter emc = new UCChartPlotter(GetMatchCostChart("Match Cost"));
            ChartGrid.Children.Add(emc);
            Grid.SetRow(emc, 2);
            Grid.SetColumn(emc, 1);

            UCChartPlotter edc = new UCChartPlotter(GetDailyCost("Daily Cost"));
            ChartGrid.Children.Add(edc);
            Grid.SetRow(edc, 2);
            Grid.SetColumn(edc, 2);

            UCChartPlotter edg = new UCChartPlotter(GetDailyGain("Daily Gross Gain"));
            ChartGrid.Children.Add(edg);
            Grid.SetRow(edg, 0);
            Grid.SetColumnSpan(edg, 3);

        }

        private LineGraph GetDailyCost(string description)
        {
            var y =
             from ev in Events
             where ev.GetType().Equals(typeof(Simulation))
             select ((Simulation)ev).DailyResult.GrossDailyCost;

             return BuildLineGraph(description, Brushes.DarkBlue, X, y.ToArray().ToDoubleArray());
        }

        private LineGraph GetDailyGain(string description)
        {
            var y =
             from ev in Events
             where ev.GetType().Equals(typeof(Simulation))
             select ((Simulation)ev).DailyResult.GrossDailyGain;

            return BuildLineGraph(description, Brushes.DarkBlue, X, y.ToArray().ToDoubleArray());
        }


        //private void GetRarityRatio()
        //{
        //    GeneralChart.Children.Clear();
        //    MetamonCountChart.Children.Clear();

        //    List<int> levels = new List<int>();

        //    List<double> SRFragments = new List<double>();
        //    List<double> RFragments = new List<double>();
        //    List<double> NFragments = new List<double>();

        //    List<double> SRCount = new List<double>();
        //    List<double> RCount = new List<double>();
        //    List<double> NCount = new List<double>();

        //    int grossSrMetamonGroupCost = Rates.SuperRareMetamonPrice;
        //    int grossRMetamonGroupCost = Rates.RareMetamonPrice;
        //    int grossNMetamonGroupCost = Rates.NormalMetamonPrice;

        //    int srMetamon = 1;
        //    int nMetamon = grossSrMetamonGroupCost / grossNMetamonGroupCost;
        //    int rMetamon = grossSrMetamonGroupCost / grossRMetamonGroupCost;

        //    Metamon SRMet = new Metamon(Rarity.SUPER_RARE, 1, "Super Rare Metamon");
        //    Metamon RMet = new Metamon(Rarity.RARE, 1, "Rare Metamon");
        //    Metamon NMet = new Metamon(Rarity.NORMAL, 1, "Normal Metamon");

        //    for (int i = 1; i <= 4000; i++)
        //    {

        //        levels.Add(i);
        //        SRCount.Add(srMetamon);
        //        RCount.Add(rMetamon);
        //        NCount.Add(nMetamon);

        //        SRFragments.Add(DAFR(SRMet.Rarity,SRMet.Level) * srMetamon);
        //        RFragments.Add(DAFR(RMet.Rarity, RMet.Level) * rMetamon);
        //        NFragments.Add(DAFR(NMet.Rarity, NMet.Level) * nMetamon);

        //        Random rand = new Random();
        //        double matchResult = rand.NextDouble();

        //        SRMet.GainExperience(matchResult <= Rates.GetMatchWinningRate(SRMet.Rarity) ? Rates.GetWinningExperience() : Rates.GetLosingExperience());
        //        RMet.GainExperience(matchResult <= Rates.GetMatchWinningRate(RMet.Rarity) ? Rates.GetWinningExperience() : Rates.GetLosingExperience());
        //        NMet.GainExperience(matchResult <= Rates.GetMatchWinningRate(NMet.Rarity) ? Rates.GetWinningExperience() : Rates.GetLosingExperience());

        //        if (SRMet.CanLevelUp())
        //        {
        //            grossSrMetamonGroupCost += Rates.GetPotionPrice(Rarity.SUPER_RARE) * srMetamon;
        //            SRMet.LevelUp();
        //        }

        //        if (RMet.CanLevelUp())
        //        {
        //            grossRMetamonGroupCost += Rates.GetPotionPrice(Rarity.RARE) * rMetamon;
        //            RMet.LevelUp();
        //        }

        //        if (NMet.CanLevelUp())
        //        {
        //            grossNMetamonGroupCost += Rates.GetPotionPrice(Rarity.NORMAL) * nMetamon;
        //            NMet.LevelUp();
        //        }

        //        srMetamon = 1;
        //        nMetamon = grossSrMetamonGroupCost / grossNMetamonGroupCost;
        //        rMetamon = grossSrMetamonGroupCost / grossRMetamonGroupCost;

        //        //SRFragments.Add(DAFR(Rarity.SUPER_RARE, i)*srMetamon);
        //        //RFragments.Add(DAFR(Rarity.RARE, i)*rMetamon);
        //        //NFragments.Add(DAFR(Rarity.NORMAL, i)*nMetamon);
        //    }

        //    LineGraph n = BuildLineGraph($"Normal Metamon - Group Count: {nMetamon}", Brushes.Blue, levels.ToArray().ToDoubleArray(), NFragments.ToArray());
        //    LineGraph r = BuildLineGraph($"Rare Metamon - Group Count: {rMetamon}", Brushes.Red, levels.ToArray().ToDoubleArray(), RFragments.ToArray());
        //    LineGraph s = BuildLineGraph($"Super Rare Metamon - Group Count: {srMetamon}", Brushes.Green, levels.ToArray().ToDoubleArray(), SRFragments.ToArray());

        //    GeneralChart.Children.Add(n);
        //    GeneralChart.Children.Add(r);
        //    GeneralChart.Children.Add(s);

        //    MetamonCountChart.Children.Add(BuildLineGraph($"Normal Metamon", Brushes.Blue, levels.ToArray().ToDoubleArray(), NCount.ToArray()));
        //    MetamonCountChart.Children.Add(BuildLineGraph($"Rare Metamon", Brushes.Red, levels.ToArray().ToDoubleArray(), RCount.ToArray()));
        //    MetamonCountChart.Children.Add(BuildLineGraph($"Super Rare Metamon", Brushes.Green, levels.ToArray().ToDoubleArray(), SRCount.ToArray()));

        //}

        private double DAFR(Rarity rarity, int level)
        {
            return Rates.GetMatchWinningRate(rarity) * Rates.GetWinningFragments(rarity, level) 
                    
                + (1 - Rates.GetMatchWinningRate(rarity)) * Rates.GetLosingFragments(rarity, level);
        }

        private LineGraph BuildLineGraph(string description, Brush brush, double[] x, IEnumerable<double> y)
        {
            var lg = new LineGraph();
            lg.Stroke = brush;
            lg.Description = String.Format(description);
            lg.StrokeThickness = 2;
            lg.Plot(x, y);

            return lg;
        }

        private LineGraph BuildLineLongGraph(string description, Brush brush, double[] x, IEnumerable<long> y)
        {
            var lg = new LineGraph();
            lg.Stroke = brush;
            lg.Description = String.Format(description);
            lg.StrokeThickness = 2;
            lg.Plot(x, y);

            return lg;
        }

        private LineGraph GetEggGraph(string description)
        {
            var y =
            from ev in Events
            where ev.GetType().Equals(typeof(Simulation))
            select ((Simulation)ev).DailyResult.EggsMinted;

            return BuildLineGraph(description, Brushes.DarkBlue, X, y.ToArray().ToDoubleArray());
        }

        private LineGraph GetFragmentGraph(string description)
        {
            var y =
            from ev in Events
            where ev.GetType().Equals(typeof(Simulation))
            select ((Simulation)ev).GameResult.Fragments;

            return BuildLineGraph(description, Brushes.DarkCyan, X, y.ToArray());
        }

        private LineGraph GetMetamonGraph(string description)
        {
            var y =
            from ev in Events
            where ev.GetType().Equals(typeof(Simulation))
            select((Simulation)ev).AccountState.Metamons.Count;

            return BuildLineGraph(description, Brushes.DarkOrange, X, y.ToArray().ToDoubleArray());

        }

        private LineGraph GetRevenueChart(string description)
        {
            var y =
            from ev in Events
            where ev.GetType().Equals(typeof(Simulation))
            select ((Simulation)ev).AccountState.RacaRevenue;

            return BuildLineLongGraph(description, Brushes.DarkRed, X, y);
        }

        private LineGraph GetMatchCostChart(string description)
        {
            var y =
            from ev in Events
            where ev.GetType().Equals(typeof(Simulation))
            select ((Simulation)ev).GameResult.MatchCost;

            return BuildLineGraph(description, Brushes.DarkGreen, X, y.ToArray().ToDoubleArray());
        }

        public void OnSimulationEnd(object sender, List<ICalendarEvent> e)
        {
            Events = e;
            DrawCharts();
        }
    }
}
