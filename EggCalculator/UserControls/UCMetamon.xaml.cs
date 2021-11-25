using EggCalculator.Models;
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
    /// Interaction logic for UCMetamon.xaml
    /// </summary>
    /// 


    public partial class UCMetamon : UserControl
    {
        public Metamon Metamon
        {
            get { return (Metamon)GetValue(MetamonProperty); }
            set { SetValue(MetamonProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Metamon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MetamonProperty =
            DependencyProperty.Register("Metamon", typeof(Metamon), typeof(UCMetamon), new PropertyMetadata(default(Metamon)));
        public UCMetamon()
        {
            InitializeComponent();
            CmbRarity.ItemsSource = Enum.GetValues(typeof(Rarity));
        }

        //private ICommand saveMetamonCommand;

        //public ICommand SaveMetamonCommand
        //{
        //    get
        //    {
        //        if (saveMetamonCommand == null)
        //        {
        //            saveMetamonCommand = new RelayCommand(
        //                param => this.AddMetamon((string)param),
        //                param => true
        //            );
        //        }
        //        return saveMetamonCommand;
        //    }
        //}
    }
}
