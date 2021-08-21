using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheatUITemplt
{
    /// <summary>
    /// 需要在类的构造函数中最后一行添加GameFunManger.Instance.RegisterGameFun(this);启用
    /// 必须给初始化gameFunDateStruct类对象进行赋值。
    /// </summary>
    abstract class GameFun
    {
        /// <summary>
        /// 必须设置属性后续才能起作用。
        /// </summary>
        public WPFCheatUITemplate.Other.GameFunDateStruct gameFunDateStruct;
        public void GetGameData()
        {
            if (gameFunDateStruct!=null)
                if (!gameFunDateStruct.IsSignatureCode)
                {
                    if (gameFunDateStruct.IsIntPtr)
                    {
                        if (gameFunDateStruct.GameDataAddress == null)
                            this.gameFunDateStruct.GameDataAddress = new GameDataAddress(gameFunDateStruct.Handle, gameFunDateStruct.ModuleAddress + gameFunDateStruct.ModuleOffsetAddress, gameFunDateStruct.IntPtrOffset);

                    }
                    else
                    {
                        if (gameFunDateStruct.GameDataAddress == null)
                            this.gameFunDateStruct.GameDataAddress = new GameDataAddress(gameFunDateStruct.Handle, gameFunDateStruct.ModuleAddress + gameFunDateStruct.ModuleOffsetAddress);
                    }
                }
                else
                {
                    if (gameFunDateStruct.GameDataAddress == null)
                        this.gameFunDateStruct.GameDataAddress = new GameDataAddress(gameFunDateStruct.Handle, CheatTools.FindData(gameFunDateStruct.Handle, gameFunDateStruct.ModuleAddress, gameFunDateStruct.ModuleAddress + 0x4000000, gameFunDateStruct.SignatureCode)[0] + gameFunDateStruct.SignatureCodeOffset);
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
