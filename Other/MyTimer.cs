using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheatUITemplt
{
    class MyTimer
    {
        System.Windows.Forms.Timer timer;

        public delegate void MyTimerFun();
        MyTimerFun UpData;

        public MyTimer(System.Windows.Forms.Timer t)
        {
            timer = t;
            timer.Tick += Timer_Tick;
        }
        public void StartTimer()
        {
            timer.Start();
        }

        public void StopTimer()
        {
            timer.Stop();
        }

        public void AddTickEvents(MyTimerFun fun)
        {
            UpData += fun;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            UpData();
        }
    }
}
