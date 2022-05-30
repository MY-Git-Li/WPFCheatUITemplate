using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFCheatUITemplate.Core.Tools;

namespace WPFCheatUITemplate.Core.GameFuns
{

    class GameFunDataAndUIStruct
    {
        /// <summary>
        /// 游戏的句柄----不用赋值，可直接使用
        /// </summary>
        public IntPtr Handle { get; set; }

        /// <summary>
        /// 游戏的PID----不用赋值，可直接使用
        /// </summary>
        public int Pid { get; set; }

        /// <summary>
        /// 游戏实现功能相关,支持多版本
        /// </summary>
        Dictionary<GameVersion.Version, GameData> gameDates = new Dictionary<GameVersion.Version, GameData>();

        /// <summary>
        /// UI显示相关
        /// </summary>
        public UIData uIData;

        /// <summary>
        /// 设定快捷键相关
        /// </summary>
        public RefHotKey refHotKey;

        /// <summary>
        /// 目前的GameDate
        /// </summary>
        public GameData currentGameDate;


        public void AddData(GameData gameDate)
        {
            gameDates.Add(GameVersion.Version.Default, gameDate);
        }

        public void AddData(GameVersion.Version version, GameData gameDate)
        {
            gameDates.Add(version, gameDate);
        }

        public GameData GetData(GameVersion.Version version)
        {
            if (!gameDates.TryGetValue(version, out currentGameDate))
            {
                if(gameDates.TryGetValue(GameVersion.Version.Default, out currentGameDate))
                {
                    return currentGameDate;
                }else
                {
                    return null;
                }
               
            }else
            {
                return currentGameDate;
            }
        }


    }

    /// <summary>
    /// 游戏数据描述类
    /// </summary>
    public class GameData
    {
        /// <summary>
        /// 模块名字
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// 模块地址
        /// </summary>
        public IntPtr ModuleAddress { get; set; }

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
        /// 特征码处理函数------传入的地址为特征码定位的地址加上特征码偏移，返回值为该GameData储存的地址
        /// 参数一：特征码得到的地址
        /// </summary>
        public Func<IntPtr, IntPtr> SignatureHandle { get; set; }

        public GameDataAddress GetDataAddress()
        {

            IntPtr handle = GameMode.GameInformation.Handle;
            if (ModuleAddress == IntPtr.Zero)
            {
                ModuleAddress = CheatTools.GetProcessModuleHandle((uint)GameMode.GameInformation.Pid, ModuleName);
            }
            
            if (!IsSignatureCode)
            {
                if (IsIntPtr)
                {
                   
                        return new GameDataAddress(handle, (IntPtr)(ModuleAddress.ToInt64() + ModuleOffsetAddress), IntPtrOffset);

                }
                else
                {
                   
                      return new GameDataAddress(handle, (IntPtr)(ModuleAddress.ToInt64() + ModuleOffsetAddress));
                }
            }
            else
            {
                uint mouduleSize = CheatTools.GetProcessModuleSize((uint)GameMode.GameInformation.Pid, ModuleName);

                var offset = CheatTools.FindPattern(handle, ModuleAddress, (IntPtr)(ModuleAddress.ToInt64() + mouduleSize), SignatureCode);

                if (offset == 0)
                {
                    return null;
                }

                IntPtr add;
                if (SignatureHandle != null)
                {
                    add = (IntPtr)(SignatureHandle?.Invoke((IntPtr)(offset + SignatureCodeOffset)));
                }
                else
                {
                    add = (IntPtr)(offset + SignatureCodeOffset);
                }

                var obj = new GameDataAddress(handle, add);

                return obj;

            }


        }

    }
    /// <summary>
    /// UI显示描述类
    /// </summary>
    class UIData
    {
        /// <summary>
        /// 快捷键描述(繁体)----可选填用于界面展示
        /// </summary>
        public string KeyDescription_TC { get; set; }

        /// <summary>
        /// 功能描述(繁体)----可选填用于界面展示
        /// </summary>
        public string FunDescribe_TC { get; set; }

        /// <summary>
        /// 是否为触发器----false启用DoRunAgain函数，
        /// </summary>
        public bool IsTrigger { get; set; }
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
        /// <summary>
        /// 当IsAcceptValue真时起效，设置数据的显示值
        /// </summary>
        public double SliderShowNum { get; set; }
        /// <summary>
        /// 是否隐藏，不在界面显示
        /// </summary>
        public bool IsHide { get; set; }
        /// <summary>
        /// 是否界面显示小数
        /// </summary>
        public bool IsShowDecimal { get; set; }

        public bool IsHaveToolTip { get; set; }

        public string ToolTipDescription_SC { get; set; }

        public string ToolTipDescription_TC { get; set; }

        public string ToolTipDescription_EN { get; set; }

    }
    /// <summary>
    /// 快捷键设置类
    /// </summary>
    class RefHotKey
    {
        /// <summary>
        /// 启用的主键----必填 比如一些特定的按键比如ALT等
        /// </summary>
        public HotKey.KeyModifiers FsModifiers { get; set; }

        /// <summary>
        /// 启用的复键----必填 比如数字键1
        /// </summary>
        public System.Windows.Forms.Keys Vk { get; set; }
    }
}
