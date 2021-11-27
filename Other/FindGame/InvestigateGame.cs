﻿using System.ComponentModel;


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
            AppGameFunManager.Instance.DisableControl();

            startFindGame.RunWorkerCompleted += new RunWorkerCompletedEventHandler(startFindGame_RunWorkerCompleted);
            startFindGame.DoWork += new DoWorkEventHandler(startFindGame_DoWork);
            startFindGame.RunWorkerAsync();

            findGameing.RunWorkerCompleted += new RunWorkerCompletedEventHandler(findGameing_RunWorkerCompleted);
            findGameing.DoWork += new DoWorkEventHandler(findGameing_DoWork);
        }
        /// <summary>
        /// 找到游戏后判断游戏是否结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void findGameing_DoWork(object sender, DoWorkEventArgs e)
        {
            int pid = CheatTools.GetPidByProcessName(processName);
            while (pid != 0)
            {
                pid = CheatTools.GetPidByProcessName(processName);
                System.Threading.Thread.Sleep(100);
            }

        }
        /// <summary>
        /// 找到游戏后结束后，开始继续寻找游戏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void findGameing_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            AppGameFunManager.Instance.EndHotsystem();
            AppGameFunManager.Instance.DisableControl();
            AppGameFunManager.Instance.Pid = 0;
            AppGameFunManager.Instance.SetViewPid();
            AppGameFunManager.Instance.StartFlashAnimation();
            AppGameFunManager.Instance.RunAllGameFunEnding();

            startFindGame.RunWorkerAsync();
        }

        /// <summary>
        /// 开始寻找游戏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startFindGame_DoWork(object sender, DoWorkEventArgs e)
        {
            int pid = CheatTools.GetPidByProcessName(processName);
            while(pid == 0)
            {
                pid = CheatTools.GetPidByProcessName(processName);
                System.Threading.Thread.Sleep(100);
            }
            
            AppGameFunManager.Instance.Pid = pid;
            AppGameFunManager.Instance.RunAllGameFunAwake();
            AppGameFunManager.Instance.GetAllGameFunData();
        }

        /// <summary>
        /// 找到游戏后执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startFindGame_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            AppGameFunManager.Instance.SetViewPid();
            AppGameFunManager.Instance.StopFlashAnimation();
            AppGameFunManager.Instance.EnableControl();
            AppGameFunManager.Instance.RegisterAllHotKey();

            findGameing.RunWorkerAsync();
        }

    }
}
