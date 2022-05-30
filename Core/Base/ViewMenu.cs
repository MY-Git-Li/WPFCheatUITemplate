using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFCheatUITemplate.Core.GameFuns;
using WPFCheatUITemplate.Core.Tools;
using WPFCheatUITemplate.Core.UI;
using WPFCheatUITemplate.Core.Tools.Extensions;
namespace WPFCheatUITemplate.Core
{
    abstract class ViewMenu : DataBase
    {
        /// <summary>
        /// 设置下一个的快捷键
        /// </summary>
        /// <param name="keyModifiers"></param>
        protected static void SetCurentKeyModifiers(HotKey.KeyModifiers keyModifiers)
        {
            UIManager.SetCurentKeyModifiers(keyModifiers);
        }
        /// <summary>
        /// 设置下一个的快捷键
        /// </summary>
        /// <param name="keys"></param>
        protected static void SetCurentKeyModifiers(System.Windows.Forms.Keys keys)
        {
            UIManager.SetCurentKeyModifiers(keys);
        }
        /// <summary>
        /// 设置下一个的快捷键
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="keyModifiers"></param>
        protected static void SetCurentKeyModifiers(System.Windows.Forms.Keys keys, HotKey.KeyModifiers keyModifiers)
        {
            UIManager.SetCurentKeyModifiers(keys, keyModifiers);
        }
        /// <summary>
        /// 设置下一个的快捷键
        /// </summary>
        /// <param name="keyModifiers"></param>
        /// <param name="keys"></param>
        protected static void SetCurentKeyModifiers(HotKey.KeyModifiers keyModifiers, System.Windows.Forms.Keys keys)
        {
            UIManager.SetCurentKeyModifiers(keyModifiers, keys);
        }
        /// <summary>
        /// 恢复到上一次快捷键
        /// </summary>
        protected static void SetCurentKeyModifiersByReverseFormer()
        {
            UIManager.SetCurentKeyModifiersByReverseFormer();
        }
        /// <summary>
        /// 创建空白隔间
        /// </summary>
        /// <param name="offset"></param>
        protected static void CreatSeparate(int offset = 15)
        {
            UIManager.CreatSeparate(offset);
        }
        /// <summary>
        /// 创建空白隔间
        /// </summary>
        /// <param name="Description_SC">隔间中文简体</param>
        /// <param name="Description_TC">隔间中文繁体</param>
        /// <param name="Description_EN">隔间英文</param>
        /// <param name="offset"></param>
        protected static void CreatSeparateEx(string Description_SC, string Description_TC = "", string Description_EN = "", int offset = 30)
        {
            UIManager.CreatSeparateEx(Description_SC, Description_TC, Description_EN, offset);
        }
        /// <summary>
        /// 创建空白隔间
        /// </summary>
        /// <param name="Description_SC">隔间中文简体</param>
        /// <param name="Description_EN">隔间英文</param>
        /// <param name="offset"></param>
        protected static void CreatSeparate(string Description_SC, string Description_EN = "", int offset = 30)
        {
            UIManager.CreatSeparate(Description_SC, Description_EN, offset);
        }
        /// <summary>
        /// 创建下一个页面
        /// </summary>
        /// <param name="offset"></param>
        protected static void NextPage(int offset = 0)
        {
            UIManager.NextPage(offset);
        }

        /// <summary>
        /// 得到UI显示---按钮形式
        /// </summary>
        /// <param name="FunDescribe_SC">显示中文</param>
        /// <param name="FunDescribe_EN">显示英文</param>
        /// <param name="SliderMinNum">滑动块最小值</param>
        /// <param name="SliderMaxNum">滑动块最大值</param>
        /// <param name="SliderShowNum">显示滑动块值</param>
        /// <param name="IsHide">是否隐藏</param>
        /// <returns></returns>
        protected static GameFunDataAndUIStruct GetButtonDateStruct(string FunDescribe_SC,
          string FunDescribe_EN,double SliderMinNum, double SliderMaxNum, double SliderShowNum = 1, bool IsHide = false)
        {
            return GetButtonDateStruct(FunDescribe_SC, FunDescribe_EN, true, true, SliderMinNum, SliderMaxNum, SliderShowNum, IsHide);
        }
        /// <summary>
        /// 得到UI显示---按钮形式
        /// </summary>
        /// <param name="FunDescribe_SC">显示中文</param>
        /// <param name="FunDescribe_EN">显示英文</param>
        /// <param name="SliderMinNum">滑动块最小值</param>
        /// <param name="SliderMaxNum">滑动块最大值</param>
        /// <param name="SliderShowNum">显示滑动块值</param>
        /// <param name="IsHide">是否隐藏</param>
        /// <returns></returns>
        protected static GameFunDataAndUIStruct GetButtonDateStruct(string FunDescribe_SC,
          string FunDescribe_EN, int SliderMinNum, int SliderMaxNum, double SliderShowNum = 1, bool IsHide = false)
        {
            return GetButtonDateStruct(FunDescribe_SC, FunDescribe_EN, true, false, SliderMinNum, SliderMaxNum, SliderShowNum, IsHide);
        }
        /// <summary>
        /// 得到UI显示---选择框形式
        /// </summary>
        /// <param name="FunDescribe_SC">显示中文</param>
        /// <param name="FunDescribe_EN">显示英文</param>
        /// <param name="SliderMinNum">滑动块最小值</param>
        /// <param name="SliderMaxNum">滑动块最大值</param>
        /// <param name="SliderShowNum">显示滑动块值</param>
        /// <param name="IsHide">是否隐藏</param>
        /// <returns></returns>
        protected static GameFunDataAndUIStruct GetCheckButtonDateStruct(string FunDescribe_SC,
          string FunDescribe_EN, double SliderMinNum, double SliderMaxNum, double SliderShowNum = 1, bool IsHide = false)
        {
            return GetCheckButtonDateStruct(FunDescribe_SC, FunDescribe_EN, true, true, SliderMinNum, SliderMaxNum, SliderShowNum ,IsHide);
        }
        /// <summary>
        /// 得到UI显示---选择框形式
        /// </summary>
        /// <param name="FunDescribe_SC">显示中文</param>
        /// <param name="FunDescribe_EN">显示英文</param>
        /// <param name="SliderMinNum">滑动块最小值</param>
        /// <param name="SliderMaxNum">滑动块最大值</param>
        /// <param name="SliderShowNum">显示滑动块值</param>
        /// <param name="IsHide">是否隐藏</param>
        /// <returns></returns>
        protected static GameFunDataAndUIStruct GetCheckButtonDateStruct(string FunDescribe_SC,
         string FunDescribe_EN, int SliderMinNum, int SliderMaxNum, double SliderShowNum = 1, bool IsHide = false)
        {
            return GetCheckButtonDateStruct(FunDescribe_SC, FunDescribe_EN, true, false, SliderMinNum, SliderMaxNum, SliderShowNum, IsHide);
        }
        /// <summary>
        /// 隐藏的选择框形式
        /// </summary>
        /// <returns></returns>
        protected static GameFunDataAndUIStruct GetCheckButtonDateStructWithHide()
        {
            return GetCheckButtonDateStruct("","",false,false,1 ,1,1 ,true);
        }
        /// <summary>
        /// 隐藏的按钮形式
        /// </summary>
        /// <returns></returns>
        protected static GameFunDataAndUIStruct GetButtonDateStructWithHide()
        {
            return GetButtonDateStruct("", "", false,false, 1, 1,1 ,true);
        }

        /// <summary>
        /// 得到UI显示---按钮形式
        /// </summary>
        /// <param name="FunDescribe_SC">显示中文</param>
        /// <param name="FunDescribe_EN">显示英文</param>
        /// <param name="IsAcceptValue">是否接受外部输入值</param>
        /// <param name="IsShowDecimal">是否显示小数</param>
        /// <param name="SliderMinNum">滑动块最小值</param>
        /// <param name="SliderMaxNum">滑动块最大值</param>
        /// <param name="SliderShowNum">显示的滑动块值</param>
        /// <param name="IsHide">是否隐藏</param>
        /// <returns></returns>
        protected static GameFunDataAndUIStruct GetButtonDateStruct(string FunDescribe_SC,
          string FunDescribe_EN, bool IsAcceptValue = false, bool IsShowDecimal  = false, double SliderMinNum = 1, double SliderMaxNum = 9999, double SliderShowNum = 1, bool IsHide = false)
        {
            return UIManager.GetButtonDateStruct(FunDescribe_SC, FunDescribe_EN, IsAcceptValue, IsShowDecimal, SliderMinNum, SliderMaxNum, SliderShowNum, IsHide);
        }
        /// <summary>
        /// 得到UI显示---选择框形式
        /// </summary>
        /// <param name="FunDescribe_SC">显示中文</param>
        /// <param name="FunDescribe_EN">显示英文</param>
        /// <param name="IsAcceptValue">是否接受外部输入值</param>
        /// <param name="IsShowDecimal">是否显示小数</param>
        /// <param name="SliderMinNum">滑动块最小值</param>
        /// <param name="SliderMaxNum">滑动块最大值</param>
        /// <param name="SliderShowNum">滑动块</param>
        /// <param name="IsHide">是否隐藏</param>
        /// <returns></returns>
        protected static GameFunDataAndUIStruct GetCheckButtonDateStruct(string FunDescribe_SC,
           string FunDescribe_EN, bool IsAcceptValue = false, bool IsShowDecimal = false, double SliderMinNum = 1, double SliderMaxNum = 9999, double SliderShowNum = 1, bool IsHide = false)
        {
            return UIManager.GetCheckButtonDateStruct(FunDescribe_SC,FunDescribe_EN, IsAcceptValue, IsShowDecimal, SliderMinNum, SliderMaxNum, SliderShowNum, IsHide);
        }
        /// <summary>
        /// 开启声音提示
        /// </summary>
        protected static void SoundEffectOpen()
        {
            AppGameFunManager.Instance.SoundEffectOpen();
        }
        /// <summary>
        /// 关闭声音提示
        /// </summary>
        protected static void SoundEffectClose()
        {
            AppGameFunManager.Instance.SoundEffectClose();
        }
        /// <summary>
        /// 添加多地区语言字符串
        /// </summary>
        /// <param name="id">字符串唯一标识id</param>
        /// <param name="Description_SC">简体</param>
        /// <param name="Description_TC">繁体</param>
        /// <param name="Description_EN">英文</param>
        protected static void AddString(string id, string Description_SC, string Description_TC, string Description_EN)
        {
            AppGameFunManager.Instance.UILangerManger.AddString(id, Description_SC, Description_TC, Description_EN);
        }
        /// <summary>
        /// 添加多地区语言字符串
        /// </summary>
        /// <param name="id">字符串唯一标识id</param>
        /// <param name="Description_SC">简体</param>
        /// <param name="Description_EN">英文</param>
        protected static void AddString(string id, string Description_SC, string Description_EN)
        {
            AppGameFunManager.Instance.UILangerManger.AddString(id, Description_SC, Description_EN);
        }
        /// <summary>
        /// 通过ID得到字符串
        /// </summary>
        /// <param name="id">字符串唯一标识id</param>
        /// <returns></returns>
        protected static string GetString(string id)
        {
            return AppGameFunManager.Instance.UILangerManger.GetString(id);
        }
        /// <summary>
        /// 设置鼠标悬浮提示
        /// </summary>
        /// <param name="Description_SC">悬浮提示中文</param>
        /// <param name="Description_TC">悬浮提示繁体</param>
        /// <param name="Description_EN">悬浮提示英文</param>
        protected static void SetToolTip(string Description_SC, string Description_TC, string Description_EN)
        {
            ToolTipUI.SetToolTip(Description_SC,Description_TC,Description_EN);
        }
        /// <summary>
        /// 设置鼠标悬浮提示
        /// </summary>
        /// <param name="Description_SC">悬浮提示中文</param>
        /// <param name="Description_EN">悬浮提示英文</param>
        protected static void SetToolTip(string Description_SC, string Description_EN)
        {
            ToolTipUI.SetToolTip(Description_SC, Description_SC.ToTraditional(), Description_EN);
        }
        /// <summary>
        /// 设置鼠标悬浮提示
        /// </summary>
        /// <param name="id">悬浮提示中文</param>
        protected static void SetToolTipByID(string id)
        {
            ToolTipUI.SetToolTipByID(id);
        }
    }
}
