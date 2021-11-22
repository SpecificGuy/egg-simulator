﻿using EggCalculator.Constants;
using EggCalculator.Models;
using EggCalculator.Utility;
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

namespace EggCalculator.UserControls
{
    /// <summary>
    /// Interaction logic for UCSimulation.xaml
    /// </summary>
    public partial class UCSimulation : UserControl
    {


        public Simulation Simulation
        {
            get { return (Simulation)GetValue(SimulationProperty); }
            set { SetValue(SimulationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Simulation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SimulationProperty =
            DependencyProperty.Register("Simulation", typeof(Simulation), typeof(UCSimulation), new PropertyMetadata(new Simulation()));


        public UCSimulation()
        {
            InitializeComponent();
        }

        private void AddMetamon(string rarity)
        {
            Enum.TryParse(rarity, out Rarity r);
            //Simulation.AccountState.Metamons.Add(new Metamon(r, 1, "Metamon", 0));
            Simulation.BuyMetamon(r, true);
        }

        private void DeleteMetamon(int index)
        {
            Simulation.AccountState.Metamons.RemoveAt(index);

        }

        private void Resimulate()
        {
            ((SimulationViewModel)DataContext).Simulate(Simulation.DateFrom);
        }

        #region commands
        private ICommand addMetamonCommand;

        public ICommand AddMetamonCommand
        {
            get
            {
                if (addMetamonCommand == null)
                {
                    addMetamonCommand = new RelayCommand(
                        param => this.AddMetamon((string)param),
                        param => 
                        {
                            Enum.TryParse((string)param, out Rarity r);
                            return  Simulation?.AccountState?.RacaBalance > Rates.GetMetamonPrice(r);
                        }
                            
                    );
                }
                return addMetamonCommand;
            }
        }
        private ICommand resimulateCommand;

        public ICommand ResimulateCommand
        {
            get
            {
                if (resimulateCommand == null)
                {
                    resimulateCommand = new RelayCommand(
                        param => this.Resimulate(),
                        param => Simulation is not null
                    );
                }
                return resimulateCommand;
            }
        }

        private ICommand deleteMetamonCommand;

        public ICommand DeleteMetamonCommand
        {
            get
            {
                if (deleteMetamonCommand == null)
                {
                    deleteMetamonCommand = new RelayCommand(
                        param => this.DeleteMetamon((int)param),
                        param => param is not null && (int)param != -1
                    );
                }
                return deleteMetamonCommand;
            }
        }
        #endregion
    }
}
