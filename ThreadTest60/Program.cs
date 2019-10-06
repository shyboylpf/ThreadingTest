using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ThreadTest60
{
    /// <summary>
    /// Subclassing BackgroundWorker
    /// Subclassing BackgroundWorker is an easy way to implement the EAP, in cases when you need to offer only one asynchronously executing method.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
    public class Client
    {
        //Dictionary<string, int> GetFinancialTotals(int foo, int bar) {  }
        public FinancialWorker GetFinancialWorker(int foo, int bar)
        {
            return new FinancialWorker(foo, bar);
        }
    }

    public class FinancialWorker : BackgroundWorker
    {
        public Dictionary<string, int> Result;
        public readonly int Foo, Bar;

        public FinancialWorker()  //默认构造函数会置进度报告和取消支持为true
        {
            WorkerReportsProgress = true;
            WorkerSupportsCancellation = true;
        }

        public FinancialWorker(int foo, int bar) : this()  // 继承了默认构造函数
        {
            this.Foo = foo;
            this.Bar = bar;
        }

        protected override void OnDoWork(DoWorkEventArgs e)
        {
            ReportProgress(0, "Working hard on this report ...");

            // Initialize financial report data
            // ...

            while (!< finished report >)
    {
                if (CancellationPending) { e.Cancel = true; return; }
                // Perform another calculation step ...
                // ...
                ReportProgress(percentCompleteCalc, "Getting there...");
            }
            ReportProgress(100, "Done!");
            e.Result = Result = < completed report data>;

        }
    }
}
