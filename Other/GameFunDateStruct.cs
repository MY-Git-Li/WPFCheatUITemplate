using CheatUITemplt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCheatUITemplate.Other
{
    class GameFunDateStruct
    {
        #region 游戏数据块

        /// <summary>
        /// 游戏的句柄----不用赋值，可直接使用
        /// </summary>
        public IntPtr Handle { get; set; }

        /// <summary>
        /// 模块名字-----必填
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// 模块地址
        /// </summary>
        public uint ModuleAddress { get; set; }

        /// <summary>
        /// 模块偏移----必填
        /// </summary>
        public uint ModuleOffsetAddress { get; set; }

        /// <summary>
        /// 是否启用指针
        /// </summary>
        public bool IsIntPtr { get; set; }

        /// <summary>
        /// 指针偏移----当启用指针时填写
        /// </summary>
        public uint[] IntPtrOffset { get; set; }

        /// <summary>
        /// 是否是特征码定位----可选，使用此则无需填写模块偏移等
        /// </summary>
        public bool IsSignatureCode { get; set; }

        /// <summary>
        /// 特征码字符串----当特征码定位为真时启用，填写特征码字符串
        /// </summary>
        public string SignatureCode { get; set; }

        /// <summary>
        /// 特征码偏移----当特征码定位为真时启用，填写特征码地址后续偏移
        /// </summary>
        public uint SignatureCodeOffset { get; set; }


        /// <summary>
        /// 游戏数据----设置了前面的属性后不用赋值，可直接定位到地址，也可自定义数据
        /// </summary>
        public GameDataAddress GameDataAddress { get; set; }

        #endregion

        #region UI界面块
       
        /// <summary>
        /// 快捷键描述(繁体)----可选填用于界面展示
        /// </summary>
        public  string KeyDescription_TC { get; set; }

        /// <summary>
        /// 功能描述(繁体)----可选填用于界面展示
        /// </summary>
        public  string FunDescribe_TC { get; set; }

        /// <summary>
        /// 是否为触发器----是否使用DoRunAgain函数
        /// </summary>
        public  bool IsTrigger { get; set; }
        /// <summary>
        /// 是否接受外部的值，将决定是否创建slide
        /// </summary>
        public bool IsAcceptValue { get; set; }

        /// <summary>
        /// 快捷键描述(简体)----可选填用于界面展示
        /// </summary>
        public string KeyDescription_SC { get; set; }
        /// <summary>
        /// 功能描述(简体)----可选填用于界面展示
        /// </summary>
        public string FunDescribe_SC { get; set; }
        /// <summary>
        /// 快捷键描述(英文)----可选填用于界面展示
        /// </summary>
        public string KeyDescription_EN { get; set; }
        /// <summary>
        /// 功能描述(英文)----可选填用于界面展示
        /// </summary>
        public string FunDescribe_EN { get; set; }
        /// <summary>
        /// 当IsAcceptValue真时起效，设置数据的最大值 默认100
        /// </summary>
        public double SliderMaxNum { get; set; }
        /// <summary>
        /// 当IsAcceptValue真时起效，设置数据的最小值 默认1
        /// </summary>
        public double SliderMinNum { get; set; }

        #endregion

        #region 快捷键块

        /// <summary>
        /// 启用的主键----必填 比如一些特定的按键比如ALT等
        /// </summary>
        public HotKey.KeyModifiers FsModifiers { get; set; }

        /// <summary>
        /// 启用的复键----必填 比如数字键1
        /// </summary>
        public System.Windows.Forms.Keys Vk { get; set; }

        #endregion
    }
}
