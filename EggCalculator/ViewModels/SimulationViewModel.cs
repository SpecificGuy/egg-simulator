using EggCalculator.Constants;
using EggCalculator.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF.EventCalendar;

namespace EggCalculator.ViewModels
{
    public class SimulationViewModel : ViewModelBase
    {

        public event EventHandler<List<ICalendarEvent>> OnSimulationEnd;

        private CalendarMonth calendar;
        private List<ICalendarEvent> events;
        private Simulation selectedSimulation;
        private Simulation startingSimulation;
        private int simulationDays = 365;
        private Rates rates = new();
        private bool metamonAutoBuy = true;
        private Rarity metamonAutoBuyRarity = Rarity.NORMAL;
        private bool autoRevenue = true;
        private bool autoSellEggs = true;
        private bool autoMintEggs = true;
        private bool autoBuyPotions = true;
        private bool autoLevelUp = true;

        public bool AutoLevelUp
        {
            get { return autoLevelUp; }
            set { autoLevelUp = value;
                OnPropertyChanged("AutoLevelUp");
            }
        }

        public bool AutoBuyPotions
        {
            get { return autoBuyPotions; }
            set { autoBuyPotions = value;
                OnPropertyChanged("AutoBuyPotions");
            }
        }


        public bool AutoMintEggs
        {
            get { return autoMintEggs; }
            set { autoMintEggs = value;
                OnPropertyChanged("AutoMintEggs");
            }
        }


        public bool AutoSellEggs
        {
            get { return autoSellEggs; }
            set { autoSellEggs = value;
                OnPropertyChanged("AutoSellEggs");
            }
        }


        public bool AutoRevenue
        {
            get { return autoRevenue; }
            set { autoRevenue = value;
                OnPropertyChanged("AutoRevenue");
            }
        }

        public Rarity MetamonAutoBuyRarity
        {
            get { return metamonAutoBuyRarity; }
            set { metamonAutoBuyRarity = value;
                OnPropertyChanged("MetamonAutoBuyRarity");
            }
        }

        public bool MetamonAutoBuy
        {
            get { return metamonAutoBuy; }
            set { metamonAutoBuy = value;
                OnPropertyChanged("MetamonAutoBuy");
            }
        }

        public Rates Rates
        {
            get { return rates; }
            set { rates = value; }
        }


        public int SimulationDays
        {
            get { return simulationDays; }
            set { simulationDays = value;
                OnPropertyChanged("SimulationDays");
            }
        }

        public Simulation StartingSimulation
        {
            get { return startingSimulation; }
            set { startingSimulation = value;
                OnPropertyChanged("StartingSimulation");
            }
        }

        public List<ICalendarEvent> Events
        {
            get { return events; }
            set { events = value;
                OnPropertyChanged("Events");
                // redraw days with events when Events property changes
                calendar.DrawDays();
            }
        }

        public Simulation SelectedSimulation
        {
            get { return selectedSimulation; }
            set
            {
                selectedSimulation = value;
                OnPropertyChanged("SelectedSimulation");
            }
        }

        public SimulationViewModel(CalendarMonth c)
        {
            calendar = c;
            DateTime actualDate = DateTime.Now.Date;

            StartingSimulation = new Simulation
            {
                DateFrom = actualDate,
                DateTo = actualDate.AddMinutes(1),
                Label = "Simulation",
                AccountState = new AccountState
                {
                    RacaBalance = 330000,
                    EggBalance = 0,
                    FragmentBalance = 872,
                    PotionBalance = 0,
                    YellowDiamondBalance = 0,
                    PurpleDiamondBalance = 0,
                    BlackDiamondBalance = 0,
                    Metamons = new ObservableCollection<Metamon>()
                }
            };

            StartingSimulation.AccountState.Metamons.Add(new Metamon(Rarity.NORMAL, 1, "Metamon", 100));
            calendar.CalendarDayMouseLeftClickedEvent += Calendar_CalendarEventDoubleClickedEvent;
        }

        internal void Simulate(DateTime? dateFrom)
        {
            int eventIndex = Events.IndexOf(Events.Find(x => x.DateFrom.Value.Equals(dateFrom.Value.AddDays(1))));
            Events.RemoveRange(eventIndex, Events.Count - eventIndex);

            Simulation actualSimulation = (Simulation)Events.Find(x => x.GetType().Equals(typeof(Simulation)) && x.DateFrom.Value.Equals(dateFrom.Value));
            CoreSimulation(actualSimulation);
            calendar.DrawDays();
            OnSimulationEnd?.Invoke(this, Events);
        }

        private void CoreSimulation(Simulation actualSimulation)
        {
            for (int i = 0; i < SimulationDays; i++)
            {
                int metamonQuantity = 0;
                int racaSold = 0;
                int eggQuantity = 0;

                actualSimulation = InitializeNextSimulation(actualSimulation);

                actualSimulation.LogInitialState();

                GameResult gr = actualSimulation.PlayGame(AutoBuyPotions, AutoLevelUp);

                if(AutoMintEggs)
                    eggQuantity = actualSimulation.MintEggs();

                if(AutoSellEggs)
                    actualSimulation.SellEggs();

                if(AutoRevenue)
                    racaSold = actualSimulation.GetRevenue();

                if(MetamonAutoBuy)
                    metamonQuantity = actualSimulation.BuyMetamon(MetamonAutoBuyRarity);

                Events.Add(actualSimulation);

                if (gr.MetamonLeagueUp > 0)
                    Events.Add(new CalendarMark
                    {
                        DateFrom = actualSimulation.DateFrom,
                        DateTo = actualSimulation.DateTo,
                        Label = gr.MetamonLeagueUp + " League up"
                    });

                if (eggQuantity > 0)
                    Events.Add(new CalendarMark
                    {
                        DateFrom = actualSimulation.DateFrom,
                        DateTo = actualSimulation.DateTo,
                        Label = eggQuantity + " Egg"
                    });

                if (metamonQuantity > 0)
                    Events.Add(new CalendarMark
                    {
                        DateFrom = actualSimulation.DateFrom,
                        DateTo = actualSimulation.DateTo,
                        Label = metamonQuantity + " Metamon"
                    });

                if (racaSold > 0)
                    Events.Add(new CalendarMark
                    {
                        DateFrom = actualSimulation.DateFrom,
                        DateTo = actualSimulation.DateTo,
                        Label = "Revenue Day"
                    });

            }
        }

  
        private void ResetCalendar()
        {
            Events = new List<ICalendarEvent>();
            SelectedSimulation = null;
        }

        private Simulation InitializeNextSimulation(Simulation previousSimulation)
        {
            DateTime actualDate = previousSimulation.DateFrom.Value.AddDays(1);
            Simulation temp = new Simulation
            {
                DateFrom = actualDate,
                DateTo = actualDate.AddMinutes(1),
                Label = "Simulation"
            };
            temp.AccountState = previousSimulation.AccountState.Copy();
            return temp;
        }

        public void Simulate()
        {
            ResetCalendar();

            Simulation actualSimulation = StartingSimulation;
            Events.Add(actualSimulation);

            CoreSimulation(actualSimulation);

            OnSimulationEnd?.Invoke(this, Events);
        }

        private void Calendar_CalendarEventDoubleClickedEvent(object sender, CalendarEventView e)
        {
            if (e.DataContext is Simulation calendarEvent)
            {
                //MessageBox.Show($"'{((Simulation)calendarEvent).AccountState.ToString()}'");
                SelectedSimulation = calendarEvent;
            }
        }

    }
}
