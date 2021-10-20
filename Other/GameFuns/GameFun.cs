﻿
using System;
using System.Collections.Generic;
using WPFCheatUITemplate.Other;

namespace CheatUITemplt
{
    /// <summary>
    /// 游戏功能基类
    /// </summary>
    abstract class GameFun
    {
        /// <summary>
        /// 必须设置属性后续才能起作用。
        /// </summary>
        public WPFCheatUITemplate.Other.GameFunDateStruct gameFunDateStruct;
        /// <summary>
        /// 读写内存实例
        /// </summary>
        public WPFCheatUITemplate.Other.Draw.Memory memory;

        /// <summary>
        /// 游戏数据----设置了前面的属性后不用赋值，可直接定位到地址，也可自定义数据
        /// </summary>
        public GameDataAddress gameDataAddress;

        public GameFun()
        {
            AppGameFunManger.Instance.RegisterGameFun(this);
        }

        public void GetGameData()
        {
            if (gameFunDateStruct != null)
            {
                if (gameFunDateStruct.currentGameDate!=null)
                {
                    gameDataAddress = gameFunDateStruct.currentGameDate.GetDataAddress(gameFunDateStruct.Handle);
                }
               
                memory = new WPFCheatUITemplate.Other.Draw.Memory();
                memory.SetProcessHandle(gameFunDateStruct.Handle);
            }
        }

        /// <summary>
        /// 可以实现自定义数据获取，列如人造指针等，运行一次
        /// </summary>
        public abstract void Awake();

        /// <summary>
        /// 初次点击函数
        /// </summary>
        /// <param name="value">IsAcceptValue为真时 value为slider的值 否则为0</param>
        public abstract void DoFirstTime(double value);

        /// <summary>
        /// 再次点击函数，需设置触发器为假
        /// </summary>
        /// <param name="value">IsAcceptValue为真时 value为slider的值 否则为0</param>
        public abstract void DoRunAgain(double value);
        /// <summary>
        /// 与Awake 相对应的Ending方法，用来释放Awake方法中的一些资源等
        /// </summary>
        public abstract void Ending();

    }
}
