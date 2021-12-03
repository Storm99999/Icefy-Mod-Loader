using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Icefy_Mod_Loader.lib
{
    public partial class Main : Form
    {
        static Process[] procArr = null;
        static string dllName = null;
        public Main()
        {
            InitializeComponent();
            procArr = Process.GetProcesses();
            listBox1.Items.Clear();
            foreach (Process p in procArr)
            {
                listBox1.Items.Add(p.ProcessName);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Injector.Inject(procArr[listBox1.SelectedIndex].ToString(), dllName);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileInfo file = new FileInfo(openFileDialog.FileName);
                dllName = file.FullName;
                textBox1.Text = file.FullName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            procArr = Process.GetProcesses();
            listBox1.Items.Clear();
            foreach (Process p in procArr)
            {
                listBox1.Items.Add(p.ProcessName);
            }
        }
    }
}
