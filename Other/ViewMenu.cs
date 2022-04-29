using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFCheatUITemplate.Other.GameFuns;
using WPFCheatUITemplate.Other.Tools;
using WPFCheatUITemplate.Other.UI;

namespace WPFCheatUITemplate.Other
{
    abstract class ViewMenu : DataBase
    {

        public static void SetCurentKeyModifiers(HotKey.KeyModifiers keyModifiers)
        {
            UIManager.SetCurentKeyModifiers(keyModifiers);
        }

        public static void SetCurentKeyModifiers(System.Windows.Forms.Keys keys)
        {
            UIManager.SetCurentKeyModifiers(keys);
        }

        public static void SetCurentKeyModifiers(System.Windows.Forms.Keys keys, HotKey.KeyModifiers keyModifiers)
        {
            UIManager.SetCurentKeyModifiers(keys, keyModifiers);
        }

        public static void SetCurentKeyModifiers(HotKey.KeyModifiers keyModifiers, System.Windows.Forms.Keys keys)
        {
            UIManager.SetCurentKeyModifiers(keyModifiers, keys);
        }
        public static void SetCurentKeyModifiersByReverseFormer()
        {
            UIManager.SetCurentKeyModifiersByReverseFormer();
        }

        public static void CreatSeparate(int offset = 15)
        {
            UIManager.CreatSeparate(offset);
        }

        public static void CreatSeparateEx(string Description_SC, string Description_TC = "", string Description_EN = "", int offset = 30)
        {
            UIManager.CreatSeparateEx(Description_SC, Description_TC, Description_EN, offset);
        }
        public static void CreatSeparate(string Description_SC, string Description_EN = "", int offset = 30)
        {
            UIManager.CreatSeparate(Description_SC, Description_EN, offset);
        }
        public static void NextPage(int offset = 0)
        {
            UIManager.NextPage(offset);
        }


        public static GameFunDataAndUIStruct GetButtonDateStruct(string FunDescribe_SC,
          string FunDescribe_EN,double SliderMinNum, double SliderMaxNum, double SliderShowNum = 1, bool IsHide = false)
        {
            return GetButtonDateStruct(FunDescribe_SC, FunDescribe_EN, true, true, SliderMinNum, SliderMaxNum, SliderShowNum, IsHide);
        }

        public static GameFunDataAndUIStruct GetButtonDateStruct(string FunDescribe_SC,
          string FunDescribe_EN, int SliderMinNum, int SliderMaxNum, double SliderShowNum = 1, bool IsHide = false)
        {
            return GetButtonDateStruct(FunDescribe_SC, FunDescribe_EN, true, false, SliderMinNum, SliderMaxNum, SliderShowNum, IsHide);
        }

        public static GameFunDataAndUIStruct GetCheckButtonDateStruct(string FunDescribe_SC,
          string FunDescribe_EN, double SliderMinNum, double SliderMaxNum, double SliderShowNum = 1, bool IsHide = false)
        {
            return GetCheckButtonDateStruct(FunDescribe_SC, FunDescribe_EN, true, true, SliderMinNum, SliderMaxNum, SliderShowNum ,IsHide);
        }

        public static GameFunDataAndUIStruct GetCheckButtonDateStruct(string FunDescribe_SC,
         string FunDescribe_EN, int SliderMinNum, int SliderMaxNum, double SliderShowNum = 1, bool IsHide = false)
        {
            return GetCheckButtonDateStruct(FunDescribe_SC, FunDescribe_EN, true, false, SliderMinNum, SliderMaxNum, SliderShowNum, IsHide);
        }

        public static GameFunDataAndUIStruct GetCheckButtonDateStructWithHide()
        {
            return GetCheckButtonDateStruct("","",false,false,1 ,1,1 ,true);
        }

        public static GameFunDataAndUIStruct GetButtonDateStructWithHide()
        {
            return GetButtonDateStruct("", "", false,false, 1, 1,1 ,true);
        }


        public static GameFunDataAndUIStruct GetButtonDateStruct(string FunDescribe_SC,
          string FunDescribe_EN, bool IsAcceptValue = false, bool IsShowDecimal  = false, double SliderMinNum = 1, double SliderMaxNum = 9999, double SliderShowNum = 1, bool IsHide = false)
        {
            return UIManager.GetButtonDateStruct(FunDescribe_SC, FunDescribe_EN, IsAcceptValue, IsShowDecimal, SliderMinNum, SliderMaxNum, SliderShowNum, IsHide);
        }
        public static GameFunDataAndUIStruct GetCheckButtonDateStruct(string FunDescribe_SC,
           string FunDescribe_EN, bool IsAcceptValue = false, bool IsShowDecimal = false, double SliderMinNum = 1, double SliderMaxNum = 9999, double SliderShowNum = 1, bool IsHide = false)
        {
            return UIManager.GetCheckButtonDateStruct(FunDescribe_SC,FunDescribe_EN, IsAcceptValue, IsShowDecimal, SliderMinNum, SliderMaxNum, SliderShowNum, IsHide);
        }
    }
}
