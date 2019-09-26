using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadTest44
{
    class Program
    {
        static readonly object _statusLocker = new object();
        static ProgressStatus _status;
        static void Main(string[] args)
        {

            // 写_status
            var status = new ProgressStatus(50, "Working on it.");
            lock (_statusLocker)
            {
                _status = status;
            }

            // 读取_status
            ProgressStatus statusCopy;
            lock (_statusLocker)
            {
                statusCopy = _status;
            }
            int pc = statusCopy.PercentComplete;
            string msg = statusCopy.StatusMessage;
        }
    }

    class ProgressStatus
    {
        public readonly int PercentComplete;
        public readonly string StatusMessage;
        // this class might have many more fields.
        public ProgressStatus(int percentComplete, string statusMessage)
        {
            PercentComplete = percentComplete;
            StatusMessage = statusMessage;
        }

    }
}
