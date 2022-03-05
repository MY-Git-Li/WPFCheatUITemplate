using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static CheatUITemplt.HotKey;

namespace CheatUITemplt
{
    class HotSystem
    {
        Dictionary<int, HotSystemFun> hotKeyFunDic = new Dictionary<int, HotSystemFun>();
        List<HotSystemFun> hotKeyFunedList = new List<HotSystemFun>();
        public bool enable = true;
        int id = 0;
        public int RegisterHotKey(IntPtr hWnd, KeyModifiers fsModifiers, Keys vk, HotSystemFun fun)
        {
            id += 1;
            HotKey.RegisterHotKey(hWnd, id, fsModifiers | KeyModifiers.NOREPEAT, vk);
            hotKeyFunDic.Add(id, fun);
            return id;
        }

        public void UnRegisterHotKey(IntPtr hWnd, int id)
        {
            HotKey.UnregisterHotKey(hWnd, id);
            hotKeyFunDic.Remove(id);
        }

        public void UnRegisterHotKeyAll(IntPtr hWnd)
        {
            foreach (int id in hotKeyFunDic.Keys.ToList())
            {
                //不需要在进行判断，id 来源于hotKeyFunDic
                //if (hotKeyFunDic.ContainsKey(id))
                //{
                UnRegisterHotKey(hWnd, id);
                //}
            }

        }

        public void RunHotKeyFun(int id)
        {
            if (hotKeyFunDic.ContainsKey(id))
            {
                if (!hotKeyFunDic[id].isRun)
                {
                    if (!hotKeyFunDic[id].locked)
                    {
                        hotKeyFunDic[id].run();

                        if (!hotKeyFunDic[id].isSingle)
                            hotKeyFunedList.Add(hotKeyFunDic[id]);
                    }

                }
                else
                {
                    if (!hotKeyFunDic[id].locked)
                    {
                        hotKeyFunDic[id].runed();
                        hotKeyFunedList.Remove(hotKeyFunDic[id]);
                    }

                }
            }
        }

        public void CloseHotKeyFunAll()
        {
            for (int i = hotKeyFunedList.Count - 1; i >= 0; i--)
            {
                hotKeyFunedList[i].runed();
                hotKeyFunedList.Remove(hotKeyFunedList[i]);
            }
        }

        public void BanOtherHotKeyFun(int id)
        {
            foreach (int banid in hotKeyFunDic.Keys.ToList())
            {
                if (banid != id)
                {
                    hotKeyFunDic[banid].locked = true;
                }
            }
        }
        public void RelieveHotKeyFun()
        {
            foreach (int id in hotKeyFunDic.Keys.ToList())
            {

                hotKeyFunDic[id].locked = false;

            }
        }

        public void ClickHotKeyFun(int id)
        {
            if (hotKeyFunDic.ContainsKey(id))
            {
                if (!hotKeyFunDic[id].isRun)
                {

                    hotKeyFunDic[id].run();

                    if (!hotKeyFunDic[id].isSingle)
                        hotKeyFunedList.Add(hotKeyFunDic[id]);

                }
                else
                {

                    hotKeyFunDic[id].runed();
                    hotKeyFunedList.Remove(hotKeyFunDic[id]);

                }
            }
        }

        public void WndProcWPF(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const int WM_HOTKEY = 0x0312;
            switch (msg)
            {
                case WM_HOTKEY:
                    if (enable)
                    {
                        RunHotKeyFun((int)wParam);
                        handled = true;
                    }
                    break;
            }

        }

        public void WndProcWinForm(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;
            switch (m.Msg)
            {
                case WM_HOTKEY:
                    if (enable)
                        RunHotKeyFun(m.WParam.ToInt32());
                    break;
            }
        }
    }

    class HotSystemFun
    {
        public Action run;
        public Action runed;

        public bool isRun = false;
        public bool locked = false;
        public bool isSingle = false;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="run">首次运行方法</param>
        /// <param name="runed">再次运行方法</param>
        public HotSystemFun(Action run, Action runed)
        {

            this.run = run;
            this.run += () => { isRun = !isRun; };


            this.runed += runed;
            this.runed += () => { isRun = !isRun; };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="run">运行的方法</param>
        public HotSystemFun(Action run)
        {

            this.run = run;
            this.runed = run;
            isSingle = true;
        }


    }

    class HotKey
    {
        //如果函数执行成功，返回值不为0。  
        //如果函数执行失败，返回值为0。要得到扩展错误信息，调用GetLastError。  
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(
            IntPtr hWnd,                 //要定义热键的窗口的句柄  
            int id,                      //定义热键ID（不能与其它ID重复）            
            KeyModifiers fsModifiers,    //标识热键是否在按Alt、Ctrl、Shift、Windows等键时才会生效  
            Keys vk                      //定义热键的内容  
            );

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey(
            IntPtr hWnd,                 //要取消热键的窗口的句柄  
            int id                       //要取消热键的ID  
            );

        //定义了辅助键的名称（将数字转变为字符以便于记忆，也可去除此枚举而直接使用数值）  
        [Flags()]
        public enum KeyModifiers
        {
            None = 0,
            Alt = 1,
            Ctrl = 2,
            Shift = 4,
            WindowsKey = 8,
            NOREPEAT = 0x4000
        }
    }
}
