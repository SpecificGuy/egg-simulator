using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
    /// Interaction logic for UCPrice.xaml
    /// </summary>
    public partial class UCPrice : UserControl
    {
        public UCPrice()
        {
            InitializeComponent();
            //requestURL();
        }
        
        private void requestURL ()
        {
            string sURL;
            sURL = "https://market-api.radiocaca.com/nft-sales?pageNo=1&pageSize=20&sortBy=fixed_price&name=&order=asc&saleType&category=17&tokenType";

            WebRequest wrGETURL;
            wrGETURL = WebRequest.Create(sURL);

            Stream objStream;
            objStream = wrGETURL.GetResponse().GetResponseStream();

            StreamReader objReader = new StreamReader(objStream);

            string sLine = "";
            int i = 0;
            string result = "";
            while (sLine != null)
            {
                i++;
                sLine = objReader.ReadLine();
                if (sLine != null)
                    result += sLine +"\n";
            }

            GetResult.Text = result;
            
        }
    }
}
