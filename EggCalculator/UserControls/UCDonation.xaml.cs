using EggCalculator.Utility;
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
    /// Interaction logic for UCDonation.xaml
    /// </summary>
    public partial class UCDonation : UserControl
    {


        public string DonationAddress
        {
            get { return (string)GetValue(DonationAddressProperty); }
            set { SetValue(DonationAddressProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DonationAddress.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DonationAddressProperty =
            DependencyProperty.Register("DonationAddress", typeof(string), typeof(UCDonation), new PropertyMetadata("0xDCa11E1108180D9638Efcc71a4c9db39e97A3085"));


        public UCDonation()
        {
            InitializeComponent();
        }

        private ICommand copyToClipboardCommand;

        public ICommand CopyToClipboardCommand
        {
            get
            {
                if (copyToClipboardCommand == null)
                {
                    copyToClipboardCommand = new RelayCommand(
                        param => this.CopyToClipboard(),
                        param => true

                    );
                }
                return copyToClipboardCommand;
            }
        }

        private void CopyToClipboard()
        {
            Clipboard.SetText(DonationAddress);
        }
    }
}
