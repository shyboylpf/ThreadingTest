using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadTest77
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * 单线程的Timer
             * System.Windows.Threading.DispatcherTimer (WPF)
             * System.Windows.Forms.Timer (Windows Forms)
             * 跟System.Timers.Timer的方法成员(Interval , Tick , start , stop)差不多的调用方法
             * 但是内部实现机制不一样
             * 不用线程池来实现timer的event , 而是用依赖于用户接口模型(user interface model)的消息泵送机制(message pumping mechanism)
             * 这意味着一般情况下, 在普通应用程序中，与用于管理所有用户界面元素和控件的线程相同
             * 
             * 有以下好处
             * 1. 肯定线程安全(Thread-safe)
             * 2. 在旧的Tick触发结束之前, 不会触发新的Tick
             * 3. 可以从Tick里直接更新用户界面, 而不需要调用Control.Invoke和Dispatcher.Invoke.
             * 
             * 坏处:
             * 1. 真的没有并发
             * 2. 如果Tick执行比较费时, 那么用户界面就卡了.
            */
        }
    }
}
