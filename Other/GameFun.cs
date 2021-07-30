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
    /// 重写字段格式 列如 public override string ModuleName { get; set; }
    /// </summary>
    abstract class GameFun
    {
        
        /// <summary>
        /// 游戏的句柄----不用赋值，可直接使用
        /// </summary>
        public abstract IntPtr Handle { get ; set ; }

        /// <summary>
        /// 模块名字-----必填
        /// </summary>
        public abstract string ModuleName { get ; set ; }

        /// <summary>
        /// 模块地址
        /// </summary>
        public uint ModuleAddress { get ; set; }

        /// <summary>
        /// 模块偏移----必填
        /// </summary>
        public abstract uint ModuleOffsetAddress { get ; set ; }

        /// <summary>
        /// 是否启用指针
        /// </summary>
        public abstract bool IsIntPtr { get ; set ; }

        /// <summary>
        /// 指针偏移----当启用指针时填写
        /// </summary>
        public abstract uint[] IntPtrOffset { get ; set; }

        /// <summary>
        /// 游戏数据----不用赋值，可直接定位到地址
        /// </summary>
        internal abstract GameDataAddress GameDataAddress { get ; set ; }

        /// <summary>
        /// 启用的主键----必填 比如一些特定的按键比如ALT等
        /// </summary>
        internal abstract HotKey.KeyModifiers FsModifiers { get; set ; }

        /// <summary>
        /// 启用的复键----必填 比如数字键1
        /// </summary>
        public abstract Keys Vk { get; set ; }

        /// <summary>
        /// 快捷键描述(繁体)----可选填用于界面展示
        /// </summary>
        public abstract string KeyDescription_TC { get ; set ; }

        /// <summary>
        /// 功能描述(繁体)----可选填用于界面展示
        /// </summary>
        public abstract string FunDescribe_TC { get ; set ; }

        /// <summary>
        /// 是否是特征码定位----可选，使用此则无需填写模块偏移等
        /// </summary>
        public abstract bool IsSignatureCode { get ; set ; }

        /// <summary>
        /// 特征码字符串----当特征码定位为真时启用，填写特征码字符串
        /// </summary>
        public abstract string SignatureCode { get ; set ; }

        /// <summary>
        /// 特征码偏移----当特征码定位为真时启用，填写特征码地址后续偏移
        /// </summary>
        public abstract uint SignatureCodeOffset { get ; set ; }

        /// <summary>
        /// 是否为触发器----是否使用DoRunAgain函数
        /// </summary>
        public abstract bool IsTrigger { get ; set ; }
        /// <summary>
        /// 是否接受外部的值，将决定是否创建slide
        /// </summary>
        public abstract bool IsAcceptValue { get; set; }

        /// <summary>
        /// 快捷键描述(简体)----可选填用于界面展示
        /// </summary>
        public abstract string KeyDescription_SC { get ; set ; }
        /// <summary>
        /// 功能描述(简体)----可选填用于界面展示
        /// </summary>
        public abstract string FunDescribe_SC { get ; set ; }
        /// <summary>
        /// 快捷键描述(英文)----可选填用于界面展示
        /// </summary>
        public abstract string KeyDescription_EN { get ; set ; }
        /// <summary>
        /// 功能描述(英文)----可选填用于界面展示
        /// </summary>
        public abstract string FunDescribe_EN { get ; set ; }
        /// <summary>
        /// 当IsAcceptValue真时起效，设置数据的最大值 默认100
        /// </summary>
        public abstract double SliderMaxNum { get ; set ; }
        /// <summary>
        /// 当IsAcceptValue真时起效，设置数据的最小值 默认1
        /// </summary>
        public abstract double SliderMinNum { get ; set ; }

        public void GetGameData()
        {
            if (!IsSignatureCode)
            {
                if (IsIntPtr)
                {
                    this.GameDataAddress = new GameDataAddress(Handle, ModuleAddress + ModuleOffsetAddress, IntPtrOffset);
                }
                else
                {
                    this.GameDataAddress = new GameDataAddress(Handle, ModuleAddress + ModuleOffsetAddress);
                }
            }
            else
            {
                this.GameDataAddress = new GameDataAddress(Handle, CheatTools.FindData(Handle, ModuleAddress, ModuleAddress + 0x4000000, SignatureCode)[0] + SignatureCodeOffset);
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
        

    }
}
