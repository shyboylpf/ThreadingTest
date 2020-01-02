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

namespace ThreadTest124
{
    public partial class Form1 : Form
    {
        private TaskScheduler _uiScheduler;

        public Form1()
        {
            InitializeComponent();
            _uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();

            Task.Factory.StartNew<string>(() =>
            {
                Thread.Sleep(1000);
                return "123";
            })
                .ContinueWith(ant => label1.Text = ant.Result, _uiScheduler);
        }
    }
}