using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace SharedStylesTest
{
    public partial class Page1 : ContentPage
    {
        public Page1()
        {
            InitializeComponent();

            BindingContext = "This is a blue test";
        }
    }
}
