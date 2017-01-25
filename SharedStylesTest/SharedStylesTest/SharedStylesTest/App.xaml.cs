using SharedStylesLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace SharedStylesTest
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            SharedStylesLib.Main.Init();

            MainPage = new Page1();                
        }
    }
}
