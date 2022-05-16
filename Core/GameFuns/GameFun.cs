using System;
using System.Collections.Generic;
using WPFCheatUITemplate.Core.GameFuns;

namespace WPFCheatUITemplate.Core.GameFuns
{
    /// <summary>
    /// 游戏功能基类
    /// </summary>
    abstract class GameFun : GameFunBase
    {
        /// <summary>
        /// 设置UI后续才能起作用---显示ui，设置data后gameDataAddress才有内容，
        /// 或者可以使用使用GetCheckButtonDateStruct/GetButtonDateStruct得到gameFunDataAndUIStruct用来显示UI
        /// 利用AddData添加数据地址，使用WriteMemoryByID来写地址，GetAddress来得到地址
        /// </summary>
        public GameFunDataAndUIStruct gameFunDataAndUIStruct;

        /// <summary>
        /// 游戏数据----设置了gameFunDataAndUIStruct的GameData后，可直接定位到地址，也可自定义数据
        /// </summary>
        public GameDataAddress gameDataAddress;

        public bool IsStartRun;

        public GameFun()
        {
            AppGameFunManager.Instance.RegisterGameFun(this);
            IsStartRun = false;
        }

        public void GetGameData()
        {
            if (gameFunDataAndUIStruct != null)
            {
                if (gameFunDataAndUIStruct.currentGameDate!=null)
                {
                    gameDataAddress = gameFunDataAndUIStruct.currentGameDate.GetDataAddress();
                }
            }
           
        }
        /// <summary>
        /// 可以实现自定义数据获取，列如人造指针等，当第一次功能启用前调用
        /// </summary>
        public virtual void Start() { }

        /// <summary>
        /// 可以实现自定义数据获取，列如人造指针等，当游戏启动时调用
        /// </summary>
        public virtual void Awake() { }

        /// <summary>
        /// 初次点击函数
        /// </summary>
        /// <param name="value">IsAcceptValue为真时 value为slider的值 否则为0</param>
        public abstract void DoFirstTime(double value);

        /// <summary>
        /// 再次点击函数，需设置gameFunDataAndUIStruct中的UIData的IsTrigger为假
        /// ----或者使用GetCheckButtonDateStruct得到gameFunDataAndUIStruct
        /// </summary>
        /// <param name="value">IsAcceptValue为真时 value为slider的值 否则为0</param>
        public abstract void DoRunAgain(double value);
        /// <summary>
        /// 用来释放Awake、Start方法中的一些资源等.当游戏结束时调用
        /// </summary>
        public virtual void Ending() { }

    }
}
