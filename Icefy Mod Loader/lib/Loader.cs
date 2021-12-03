using Icefy_Mod_Loader.lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Icefy_Mod_Loader
{
    public partial class Loader : Form
    {
        public Loader()
        {
            InitializeComponent();
        }

        private async void Loader_Load(object sender, EventArgs e)
        {
            await Task.Delay(8000);
            new Main().Show();
        }
    }
}
