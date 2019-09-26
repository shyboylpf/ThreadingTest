using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;

namespace ThreadTest43
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            new Thread(Work).Start();
            Task.Factory.StartNew(()=> { txtMessage.Text = "first answer."; });
            Task.Run(()=> {
                Thread.Sleep(1000);
                txtMessage.Text = "second answer.";
            });
        }

        private void Work()
        {
            Thread.Sleep(5000);    // Simulate time-consuming task
            UpdateMessage("third answer.");
        }

        private void UpdateMessage(string v)
        {
            Action action = () => txtMessage.Text = v;
            this.Invoke(action);
        }
    }
}
