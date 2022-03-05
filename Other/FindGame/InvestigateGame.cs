using System.ComponentModel;
using System.Diagnostics;
using WPFCheatUITemplate.GameMode;

namespace CheatUITemplt
{
    class InvestigateGame
    {
        BackgroundWorker startFindGame;
        BackgroundWorker findGameing;

        public InvestigateGame()
        {
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
        /// <summary>
        /// 找到游戏后判断游戏是否结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void findGameing_DoWork(object sender, DoWorkEventArgs e)
        {
            GetPid_Work(false);
        }
        /// <summary>
        /// 找到游戏后结束后，开始继续寻找游戏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void findGameing_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            AppGameFunManager.Instance.findGameing_RunWorkerCompleted();
          
            startFindGame.RunWorkerAsync();
        }

        /// <summary>
        /// 开始寻找游戏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startFindGame_DoWork(object sender, DoWorkEventArgs e)
        {
            var pid = GetPid_Work(true);

            AppGameFunManager.Instance.startFindGame_DoWork(pid);
        }

        /// <summary>
        /// 找到游戏后执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startFindGame_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            AppGameFunManager.Instance.startFindGame_RunWorkerCompleted();

            findGameing.RunWorkerAsync();
        }

        private int GetPid_Work(bool isFind)
        {
            int pid = GetPid(GameInformation.IsByWindowsNamePrecedence);
            while (isFind ? pid == 0 : pid != 0)
            {
                pid = GetPid(GameInformation.IsByWindowsNamePrecedence);
                System.Threading.Thread.Sleep(100);
            }

            return pid;
        }

        private int GetPid(bool isByWindowsNamePrecedence)
        {
            if (isByWindowsNamePrecedence)
            {
                var pid = CheatTools.GetPidByWindowsName(GameInformation.ClassWindowsName, GameInformation.WindowsName);

                var wphandle = Process.GetProcessById(pid).MainWindowHandle;

                if (wphandle.Equals(System.IntPtr.Zero))
                {
                    if (pid.Equals(0))
                    {
                        return CheatTools.GetPidByProcessName(GameInformation.ProcessName);
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return pid;
                }

            }
            else
            {
                var pid = CheatTools.GetPidByProcessName(GameInformation.ProcessName);

                if (pid == 0)
                {
                    return CheatTools.GetPidByWindowsName(GameInformation.ClassWindowsName, GameInformation.WindowsName);
                }
                else
                {
                    return pid;
                }
            }
        }
    }
}
