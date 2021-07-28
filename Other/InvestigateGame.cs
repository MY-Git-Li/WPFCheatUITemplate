using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CheatUITemplt
{
    class InvestigateGame
    {
        string processName;
        MyTimer timer;
        bool isRegistered;
        bool isUnRegistered;
        public InvestigateGame(string processName)
        {
            this.processName = processName;

            Timer a = new Timer();
            a.Interval = 100;
            this.timer = new MyTimer(a);

        }


        public void  FindingGame()
        {
            timer.StartTimer();
            timer.AddTickEvents(findGame);
        }

       void findGame()
       {

            int pid = CheatTools.GetPidByProcessName(processName);
            if (pid != 0)
            {
                if (!isRegistered)
                {
                    isRegistered = true;
                    isUnRegistered = false;

                    GameFunManger.Instance.Pid = pid;
                    GameFunManger.Instance.SetViewPid();
                    GameFunManger.Instance.SetAllGameFun();
                    GameFunManger.Instance.EnableControl();
                    GameFunManger.Instance.RegisterAllHotKey();

                   
                }
               
            }
            else
            {

                if(!isUnRegistered)
                {
                    isRegistered = false;
                    isUnRegistered = true;

                    GameFunManger.Instance.DisableControl();
                    GameFunManger.Instance.MainWindow.EndHotsystem();
                    GameFunManger.Instance.Pid = 0;
                    GameFunManger.Instance.SetViewPid();
                }

               
            }


        }


    }
}
