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
      
        BackgroundWorker startFindGame;
        BackgroundWorker findGameing;

        public InvestigateGame(string processName)
        {
            this.processName = processName;

            startFindGame = new BackgroundWorker();
            findGameing = new BackgroundWorker();
        }


        public void FindingGame()
        {
          
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
            
            GameFunManger.Instance.Pid = pid;
            GameFunManger.Instance.SetAllGameFun();
        }

        private void startFindGame_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            GameFunManger.Instance.SetViewPid();
            GameFunManger.Instance.EnableControl();
            GameFunManger.Instance.RegisterAllHotKey();

            findGameing.RunWorkerAsync();
        }

    }
}
