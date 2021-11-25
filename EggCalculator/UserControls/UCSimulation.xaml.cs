using EggCalculator.Constants;
using EggCalculator.Models;
using EggCalculator.Utility;
using EggCalculator.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public partial class UCSimulation : UserControl, INotifyPropertyChanged
    {
        
        public event PropertyChangedEventHandler PropertyChanged;
        private int racaQuantity;

        public int RacaQuantity
        {
            get { return racaQuantity; }
            set { racaQuantity = value; 
                OnPropertyChanged();
            }
        }

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
            ((SimulationViewModel)DataContext).Events.Add(new CalendarMark
            {
                DateFrom = Simulation.DateFrom,
                DateTo = Simulation.DateTo,
                Label = "1 Manual Metamon"
            });

        }

        private void DeleteMetamon(int index)
        {
            Simulation.AccountState.Metamons.RemoveAt(index);

        }

        private void Resimulate()
        {
            ((SimulationViewModel)DataContext).Simulate(Simulation.DateFrom);
        }

        private void LevelUp(int param)
        {
            Metamon m = Simulation.AccountState.Metamons.ElementAt((int)param);

            if (Simulation.AccountState.DecreasePotionBalance(m.Rarity))
                m.LevelUp();
        }

        private void BuyPotions(Rarity rarity)
        {
            Simulation.BuyPotions(rarity);
            UpdateLayout();
        }

        private void SellEggs()
        {
            Simulation.SellEggs();
        }

        private void MintEggs()
        {
            Simulation.MintEggs();
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

        private ICommand mintEggsCommand;

        public ICommand MintEggsCommand
        {
            get
            {
                if (mintEggsCommand == null)
                {
                    mintEggsCommand = new RelayCommand(
                        param => this.MintEggs(),
                        param => Simulation?.AccountState?.FragmentBalance >= Rates.EggFragmentQuantity
                    );
                }
                return mintEggsCommand;
            }
        }

        private ICommand sellEggsCommand;

        public ICommand SellEggsCommand
        {
            get
            {
                if (sellEggsCommand == null)
                {
                    sellEggsCommand = new RelayCommand(
                        param => this.SellEggs(),
                        param => Simulation?.AccountState?.EggBalance > 0
                    );
                }
                return sellEggsCommand;
            }
        }

        private ICommand buyPotionsCommand;

        public ICommand BuyPotionsCommand
        {
            get
            {
                if (buyPotionsCommand == null)
                {
                    buyPotionsCommand = new RelayCommand(
                        param => this.BuyPotions((Rarity)param),
                        param => Rates.GetPotionPrice((Rarity)param) < Simulation?.AccountState?.RacaBalance
                    );
                }
                return buyPotionsCommand;
            }
        }


        private ICommand addRacaCommand;

        public ICommand AddRacaCommand
        {
            get
            {
                if (addRacaCommand == null)
                {
                    addRacaCommand = new RelayCommand(
                        param => racaQuantityPopup.IsOpen = true,
                        param => Simulation is not null
                    );
                }
                return addRacaCommand;
            }
        }


        private ICommand levelUpCommand;

        public ICommand LevelUpCommand
        {
            get
            {
                if (levelUpCommand == null)
                {
                    levelUpCommand = new RelayCommand(
                        param => this.LevelUp((int)param),
                        param => param is not null && (int)param != -1 
                                    && Simulation.AccountState.Metamons.ElementAt((int)param).CanLevelUp() 
                                    && Simulation.AccountState.GetPotionBalance(Simulation.AccountState.Metamons.ElementAt((int)param).Rarity) > 0 
                    );
                }
                return levelUpCommand;
            }
        }

        private ICommand cancelCommand;

        public ICommand CancelCommand
        {
            get
            {
                if (cancelCommand == null)
                {
                    cancelCommand = new RelayCommand(
                        param => racaQuantityPopup.IsOpen = false,
                        param => true
                    );
                }
                return cancelCommand;
            }
        }

        private ICommand confirmCommand;

        public ICommand ConfirmCommand
        {
            get
            {
                if (confirmCommand == null)
                {
                    confirmCommand = new RelayCommand(
                        param => { 
                            Simulation.AccountState.RacaBalance += RacaQuantity;
                            RacaQuantity = 0;
                            racaQuantityPopup.IsOpen = false;
                        },
                        param => true
                    );
                }
                return confirmCommand;
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
