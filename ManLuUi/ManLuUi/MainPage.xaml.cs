using ManLuUi.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ManLuUi
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Fuck(object sender, EventArgs e)
        {
            ring.IsActive = true;
        }

        private void CheckButton_CheckChanged(object sender, EventArgs e)
        {
            if((sender as CheckButton).IsChecked)
            {
                ring.IsActive = true;
            }
            else
            {
                ring.IsActive = false;
            }
        }
    }
}
