using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CheatUITemplt
{
    class InvestigateGame
    {
        string processName;
        //MyTimer timer;
        bool isRegistered;
        bool isUnRegistered;

        BackgroundWorker startFindGame;
        BackgroundWorker findGameing;

        public InvestigateGame(string processName)
        {
            this.processName = processName;

            //Timer a = new Timer();
            //a.Interval = 100;
            //this.timer = new MyTimer(a);
            startFindGame = new BackgroundWorker();
            findGameing = new BackgroundWorker();
        }


        public void FindingGame()
        {
            //timer.StartTimer();
            //timer.AddTickEvents(findGame);

            startFindGame.RunWorkerCompleted += new RunWorkerCompletedEventHandler(startFindGame_RunWorkerCompleted);
            startFindGame.DoWork += new DoWorkEventHandler(startFindGame_DoWork);
            startFindGame.RunWorkerAsync();

            findGameing.RunWorkerCompleted += new RunWorkerCompletedEventHandler(findGameing_RunWorkerCompleted);
            findGameing.DoWork += new DoWorkEventHandler(findGameing_DoWork);
        }

        private void findGameing_DoWork(object sender, DoWorkEventArgs e)
        {
            int pid = CheatTools.GetPidByProcessName(processName);
            while (pid != 0)
            {
                pid = CheatTools.GetPidByProcessName(processName);
                System.Threading.Thread.Sleep(100);
            }

        }

        private void findGameing_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            GameFunManger.Instance.DisableControl();
            GameFunManger.Instance.MainWindow.EndHotsystem();
            GameFunManger.Instance.Pid = 0;
            GameFunManger.Instance.SetViewPid();

            startFindGame.RunWorkerAsync();
        }

        private void startFindGame_DoWork(object sender, DoWorkEventArgs e)
        {
            int pid = CheatTools.GetPidByProcessName(processName);
            while(pid == 0)
            {
                pid = CheatTools.GetPidByProcessName(processName);
                System.Threading.Thread.Sleep(100);
            }
            e.Result = pid;
           
        }

        private void startFindGame_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            GameFunManger.Instance.Pid = (int)e.Result;
            GameFunManger.Instance.SetViewPid();
            GameFunManger.Instance.SetAllGameFun();
            GameFunManger.Instance.EnableControl();
            GameFunManger.Instance.RegisterAllHotKey();


            findGameing.RunWorkerAsync();
        }

        //void findGame()
        //{

        //    int pid = CheatTools.GetPidByProcessName(processName);
        //    if (pid != 0)
        //    {
        //        if (!isRegistered)
        //        {
        //            isRegistered = true;
        //            isUnRegistered = false;

        //            GameFunManger.Instance.Pid = pid;
        //            GameFunManger.Instance.SetViewPid();
        //            GameFunManger.Instance.SetAllGameFun();
        //            GameFunManger.Instance.EnableControl();
        //            GameFunManger.Instance.RegisterAllHotKey();

                   
        //        }
               
        //    }
        //    else
        //    {

        //        if(!isUnRegistered)
        //        {
        //            isRegistered = false;
        //            isUnRegistered = true;

        //            GameFunManger.Instance.DisableControl();
        //            GameFunManger.Instance.MainWindow.EndHotsystem();
        //            GameFunManger.Instance.Pid = 0;
        //            GameFunManger.Instance.SetViewPid();
        //        }

               
        //    }


        //}


    }
}
