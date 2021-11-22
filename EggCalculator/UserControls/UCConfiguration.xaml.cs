using EggCalculator.Constants;
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
    /// Interaction logic for UCConfiguration.xaml
    /// </summary>
    /// 

    public partial class UCConfiguration : UserControl
    {
        public Simulation Configuration
        {
            get { return (Simulation)GetValue(ConfigurationProperty); }
            set { SetValue(ConfigurationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Configuration.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ConfigurationProperty =
            DependencyProperty.Register("Configuration", typeof(Simulation), typeof(UCConfiguration));



        public Rates Rates
        {
            get { return (Rates)GetValue(RatesProperty); }
            set { SetValue(RatesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Rates.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RatesProperty =
            DependencyProperty.Register("Rates", typeof(Rates), typeof(UCConfiguration), new PropertyMetadata(new Rates()));

        public UCConfiguration()
        {
            InitializeComponent();
            CmbRarity.ItemsSource = Enum.GetValues(typeof(Rarity));
        }

        private void AddMetamon(string rarity)
        {
            Enum.TryParse(rarity, out Rarity r);
            Configuration.AccountState.Metamons.Add(new Metamon(r, 1, "Metamon", 0));
        }

        private void Simulate ()
        {
            ((SimulationViewModel)DataContext).Simulate();
        }
        private void DeleteMetamon(int index)
        {
            Configuration.AccountState.Metamons.RemoveAt(index);

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
                        param => true
                    );
                }
                return addMetamonCommand;
            }
        }

        private ICommand simulateCommand;

        public ICommand SimulateCommand
        {
            get
            {
                if (simulateCommand == null)
                {
                    simulateCommand = new RelayCommand(
                        param => this.Simulate(),
                        param => true
                    );
                }
                return simulateCommand;
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
