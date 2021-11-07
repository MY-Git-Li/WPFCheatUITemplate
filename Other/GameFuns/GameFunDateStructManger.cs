﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFCheatUITemplate.Other.Tools.Extensions;
namespace WPFCheatUITemplate.Other.GameFuns
{
    static class GameFunDateStructManger
    {
        static System.Windows.Forms.Keys curentKey = System.Windows.Forms.Keys.NumPad0;
        static CheatUITemplt.HotKey.KeyModifiers curentKeyModifiers = CheatUITemplt.HotKey.KeyModifiers.None;

        public static void SetCurentKeyModifiers(CheatUITemplt.HotKey.KeyModifiers keyModifiers)
        {
            curentKeyModifiers = keyModifiers;
        }

        public static void SetCurentKeyModifiers(System.Windows.Forms.Keys keys,CheatUITemplt.HotKey.KeyModifiers keyModifiers)
        {
            curentKey = keys;
            curentKeyModifiers = keyModifiers;
        }

        public static void SetCurentKeyModifiers(System.Windows.Forms.Keys keys)
        {
            curentKey = keys;
        }



        public static GameFunDateStruct BaseDateStruct(string KeyDescription_SC, string FunDescribe_SC,
        string KeyDescription_TC ,string FunDescribe_TC,string KeyDescription_EN, string FunDescribe_EN, 
        bool IsAcceptValue = true, int SliderMinNum = 1, int SliderMaxNum = 9999, bool IsButton= true)
        {
            var gameFunDateStruct = new Other.GameFunDateStruct();

            gameFunDateStruct.uIData = new Other.UIData()
            {
                KeyDescription_SC = KeyDescription_SC,
                FunDescribe_SC = FunDescribe_SC,

                KeyDescription_TC = KeyDescription_TC,
                FunDescribe_TC = FunDescribe_TC,

                KeyDescription_EN = KeyDescription_EN,
                FunDescribe_EN = FunDescribe_EN,

                IsTrigger = IsButton,


                IsAcceptValue = IsAcceptValue,
                SliderMinNum = SliderMinNum,
                SliderMaxNum = SliderMaxNum

            };
            gameFunDateStruct.refHotKey = new Other.RefHotKey()
            {
                Vk = curentKey,
                FsModifiers = curentKeyModifiers,
            };


            Update();
            return gameFunDateStruct;
        }

        public static GameFunDateStruct BaseDateStruct(string FunDescribe_SC,string FunDescribe_TC, string FunDescribe_EN, 
            bool IsAcceptValue = true, int SliderMinNum = 1, int SliderMaxNum = 9999,
            bool IsButton = true
            )
        {
            var gameFunDateStruct = BaseDateStruct(
                GetKeyDescription_SC(), FunDescribe_SC,
                GetKeyDescription_TC(), FunDescribe_TC,
                GetKeyDescription_EN(), FunDescribe_EN,
                IsAcceptValue, SliderMinNum, SliderMaxNum, IsButton);

            return gameFunDateStruct;
        }


        public static GameFunDateStruct ButtonDateStruct(string FunDescribe_SC,
           string FunDescribe_EN, bool IsAcceptValue = true, int SliderMinNum = 1, int SliderMaxNum = 9999)
        {

            var gameFunDateStruct = BaseDateStruct(FunDescribe_SC, FunDescribe_SC.ToTraditional(),FunDescribe_EN, IsAcceptValue, SliderMinNum, SliderMaxNum,true);

            return gameFunDateStruct;
        }

        public static GameFunDateStruct CheckButtonDateStruct(string FunDescribe_SC,
           string FunDescribe_EN, bool IsAcceptValue = false, int SliderMinNum = 1, int SliderMaxNum = 9999)
        {
            var gameFunDateStruct = BaseDateStruct(FunDescribe_SC, FunDescribe_SC.ToTraditional(), FunDescribe_EN, IsAcceptValue, SliderMinNum, SliderMaxNum,false);

            return gameFunDateStruct;
        }


        static void Update()
        {
            if ((int)curentKey == 105)
            {
                curentKey = (System.Windows.Forms.Keys)65;
                UpdateCurentKeyModifiers();

            }else if ((int)curentKey == 90)
            {
                curentKey = (System.Windows.Forms.Keys)96;
            }else
            {
                UpdateCurentKey();
            }

           
        }
        static void UpdateCurentKey()
        {
            if ((int)curentKey <= 90 && (int)curentKey >= 65)
            {
                curentKey++;
            }

            if ((int)curentKey <= 105 && (int)curentKey >= 96)
            {
                curentKey++;
            }

        }
        static void UpdateCurentKeyModifiers()
        {
            switch (curentKeyModifiers)
            {
                case CheatUITemplt.HotKey.KeyModifiers.None:
                    curentKeyModifiers = CheatUITemplt.HotKey.KeyModifiers.Alt;
                    break;
                case CheatUITemplt.HotKey.KeyModifiers.Alt:
                    curentKeyModifiers = CheatUITemplt.HotKey.KeyModifiers.Ctrl;
                    break;
                case CheatUITemplt.HotKey.KeyModifiers.Ctrl:
                    curentKeyModifiers = CheatUITemplt.HotKey.KeyModifiers.Shift;
                    break;
                case CheatUITemplt.HotKey.KeyModifiers.Shift:
                    curentKeyModifiers = CheatUITemplt.HotKey.KeyModifiers.Shift| CheatUITemplt.HotKey.KeyModifiers.Ctrl;
                    break;
                default:
                    break;
            }
        }

        static string GetKeyModifierd()
        {
            string heaKeyModifierd = "";
            switch (curentKeyModifiers)
            {
                case CheatUITemplt.HotKey.KeyModifiers.None:
                    heaKeyModifierd = "";
                    break;
                case CheatUITemplt.HotKey.KeyModifiers.Alt:
                    heaKeyModifierd = "Alt";
                    break;
                case CheatUITemplt.HotKey.KeyModifiers.Ctrl:
                    heaKeyModifierd = "Ctrl";
                    break;
                case CheatUITemplt.HotKey.KeyModifiers.Shift:
                    heaKeyModifierd = "Shift";
                    break;
                default:
                    break;
            }
            return heaKeyModifierd;
        }
        static string GetKeyDescription_EN()
        {
            string heaKeyModifierd = GetKeyModifierd();
            string key = "";
            if (heaKeyModifierd != "")
            {
                key = heaKeyModifierd + "+";
            }
            switch (curentKey)
            {
                case System.Windows.Forms.Keys.A:
                    key += "Number A";
                    break;
                case System.Windows.Forms.Keys.B:
                    key += "Number B";
                    break;
                case System.Windows.Forms.Keys.C:
                    key += "Number C";
                    break;
                case System.Windows.Forms.Keys.D:
                    key += "Number D";
                    break;
                case System.Windows.Forms.Keys.E:
                    key += "Number E";
                    break;
                case System.Windows.Forms.Keys.F:
                    key += "Number F";
                    break;
                case System.Windows.Forms.Keys.G:
                    key += "Number G";
                    break;
                case System.Windows.Forms.Keys.H:
                    key += "Number H";
                    break;
                case System.Windows.Forms.Keys.I:
                    key += "Number I";
                    break;
                case System.Windows.Forms.Keys.J:
                    key += "Number J";
                    break;
                case System.Windows.Forms.Keys.K:
                    key += "Number K";
                    break;
                case System.Windows.Forms.Keys.L:
                    key += "Number L";
                    break;
                case System.Windows.Forms.Keys.M:
                    key += "Number M";
                    break;
                case System.Windows.Forms.Keys.N:
                    key += "Number N";
                    break;
                case System.Windows.Forms.Keys.O:
                    key += "Number O";
                    break;
                case System.Windows.Forms.Keys.P:
                    key += "Number P";
                    break;
                case System.Windows.Forms.Keys.Q:
                    key += "Number Q";
                    break;
                case System.Windows.Forms.Keys.R:
                    key += "Number R";
                    break;
                case System.Windows.Forms.Keys.S:
                    key += "Number S";
                    break;
                case System.Windows.Forms.Keys.T:
                    key += "Number T";
                    break;
                case System.Windows.Forms.Keys.U:
                    key += "Number U";
                    break;
                case System.Windows.Forms.Keys.V:
                    key += "Number V";
                    break;
                case System.Windows.Forms.Keys.W:
                    key += "Number W";
                    break;
                case System.Windows.Forms.Keys.X:
                    key += "Number X";
                    break;
                case System.Windows.Forms.Keys.Y:
                    key += "Number Y";
                    break;
                case System.Windows.Forms.Keys.Z:
                    key += "Number Z";
                    break;
                case System.Windows.Forms.Keys.NumPad0:
                    key += "Number 0";
                    break;
                case System.Windows.Forms.Keys.NumPad1:
                    key += "Number 1";
                    break;
                case System.Windows.Forms.Keys.NumPad2:
                    key += "Number 2"; 
                    break;
                case System.Windows.Forms.Keys.NumPad3:
                    key += "Number 3";
                    break;
                case System.Windows.Forms.Keys.NumPad4:
                    key += "Number 4";
                    break;
                case System.Windows.Forms.Keys.NumPad5:
                    key += "Number 5";
                    break;
                case System.Windows.Forms.Keys.NumPad6:
                    key += "Number 6";
                    break;
                case System.Windows.Forms.Keys.NumPad7:
                    key += "Number 7";
                    break;
                case System.Windows.Forms.Keys.NumPad8:
                    key += "Number 8";
                    break;
                case System.Windows.Forms.Keys.NumPad9:
                    key += "Number 9";
                    break;
                default:
                    break;
            }
            return key;
        }
        static string GetKeyDescription_SC()
        {
            string heaKeyModifierd = GetKeyModifierd();

            string key = "";
            if (heaKeyModifierd != "")
            {
                key = heaKeyModifierd + "+";
            }
            switch (curentKey)
            {
                case System.Windows.Forms.Keys.A:
                    key += "字母键A";
                    break;
                case System.Windows.Forms.Keys.B:
                    key += "字母键B";
                    break;
                case System.Windows.Forms.Keys.C:
                    key += "字母键C";
                    break;
                case System.Windows.Forms.Keys.D:
                    key += "字母键D";
                    break;
                case System.Windows.Forms.Keys.E:
                    key += "字母键E";
                    break;
                case System.Windows.Forms.Keys.F:
                    key += "字母键F";
                    break;
                case System.Windows.Forms.Keys.G:
                    key += "字母键G";
                    break;
                case System.Windows.Forms.Keys.H:
                    key += "字母键H";
                    break;
                case System.Windows.Forms.Keys.I:
                    key += "字母键I";
                    break;
                case System.Windows.Forms.Keys.J:
                    key += "字母键J";
                    break;
                case System.Windows.Forms.Keys.K:
                    key += "字母键K";
                    break;
                case System.Windows.Forms.Keys.L:
                    key += "字母键L";
                    break;
                case System.Windows.Forms.Keys.M:
                    key += "字母键M";
                    break;
                case System.Windows.Forms.Keys.N:
                    key += "字母键N";
                    break;
                case System.Windows.Forms.Keys.O:
                    key += "字母键O";
                    break;
                case System.Windows.Forms.Keys.P:
                    key += "字母键P";
                    break;
                case System.Windows.Forms.Keys.Q:
                    key += "字母键Q";
                    break;
                case System.Windows.Forms.Keys.R:
                    key += "字母键R";
                    break;
                case System.Windows.Forms.Keys.S:
                    key += "字母键S";
                    break;
                case System.Windows.Forms.Keys.T:
                    key += "字母键T";
                    break;
                case System.Windows.Forms.Keys.U:
                    key += "字母键U";
                    break;
                case System.Windows.Forms.Keys.V:
                    key += "字母键V";
                    break;
                case System.Windows.Forms.Keys.W:
                    key += "字母键W";
                    break;
                case System.Windows.Forms.Keys.X:
                    key += "字母键X";
                    break;
                case System.Windows.Forms.Keys.Y:
                    key += "字母键Y";
                    break;
                case System.Windows.Forms.Keys.Z:
                    key += "字母键Z";
                    break;
                case System.Windows.Forms.Keys.NumPad0:
                    key += "数字键0";
                    break;
                case System.Windows.Forms.Keys.NumPad1:
                    key += "数字键1";
                    break;
                case System.Windows.Forms.Keys.NumPad2:
                    key += "数字键2";
                    break;
                case System.Windows.Forms.Keys.NumPad3:
                    key += "数字键3";
                    break;
                case System.Windows.Forms.Keys.NumPad4:
                    key += "数字键4";
                    break;
                case System.Windows.Forms.Keys.NumPad5:
                    key += "数字键5";
                    break;
                case System.Windows.Forms.Keys.NumPad6:
                    key += "数字键6";
                    break;
                case System.Windows.Forms.Keys.NumPad7:
                    key += "数字键7";
                    break;
                case System.Windows.Forms.Keys.NumPad8:
                    key += "数字键8";
                    break;
                case System.Windows.Forms.Keys.NumPad9:
                    key += "数字键9";
                    break;
                default:
                    break;
            }
            return key;
        }
        static string GetKeyDescription_TC()
        {
            string heaKeyModifierd = GetKeyModifierd();

            string key = "";
            if (heaKeyModifierd != "")
            {
                key = heaKeyModifierd + "+";
            }
            switch (curentKey)
            {
                case System.Windows.Forms.Keys.A:
                    key += "字母鍵A";
                    break;
                case System.Windows.Forms.Keys.B:
                    key += "字母鍵B";
                    break;
                case System.Windows.Forms.Keys.C:
                    key += "字母鍵C";
                    break;
                case System.Windows.Forms.Keys.D:
                    key += "字母鍵D";
                    break;
                case System.Windows.Forms.Keys.E:
                    key += "字母鍵E";
                    break;
                case System.Windows.Forms.Keys.F:
                    key += "字母鍵F";
                    break;
                case System.Windows.Forms.Keys.G:
                    key += "字母鍵G";
                    break;
                case System.Windows.Forms.Keys.H:
                    key += "字母鍵H";
                    break;
                case System.Windows.Forms.Keys.I:
                    key += "字母鍵I";
                    break;
                case System.Windows.Forms.Keys.J:
                    key += "字母鍵J";
                    break;
                case System.Windows.Forms.Keys.K:
                    key += "字母鍵K";
                    break;
                case System.Windows.Forms.Keys.L:
                    key += "字母鍵L";
                    break;
                case System.Windows.Forms.Keys.M:
                    key += "字母鍵M";
                    break;
                case System.Windows.Forms.Keys.N:
                    key += "字母鍵N";
                    break;
                case System.Windows.Forms.Keys.O:
                    key += "字母鍵O";
                    break;
                case System.Windows.Forms.Keys.P:
                    key += "字母鍵P";
                    break;
                case System.Windows.Forms.Keys.Q:
                    key += "字母鍵Q";
                    break;
                case System.Windows.Forms.Keys.R:
                    key += "字母鍵R";
                    break;
                case System.Windows.Forms.Keys.S:
                    key += "字母鍵S";
                    break;
                case System.Windows.Forms.Keys.T:
                    key += "字母鍵T";
                    break;
                case System.Windows.Forms.Keys.U:
                    key += "字母鍵U";
                    break;
                case System.Windows.Forms.Keys.V:
                    key += "字母鍵V";
                    break;
                case System.Windows.Forms.Keys.W:
                    key += "字母鍵W";
                    break;
                case System.Windows.Forms.Keys.X:
                    key += "字母鍵X";
                    break;
                case System.Windows.Forms.Keys.Y:
                    key += "字母鍵Y";
                    break;
                case System.Windows.Forms.Keys.Z:
                    key += "字母鍵Z";
                    break;
                case System.Windows.Forms.Keys.NumPad0:
                    key += "数字鍵0";
                    break;
                case System.Windows.Forms.Keys.NumPad1:
                    key += "数字鍵1";
                    break;
                case System.Windows.Forms.Keys.NumPad2:
                    key += "数字鍵2";
                    break;
                case System.Windows.Forms.Keys.NumPad3:
                    key += "数字鍵3";
                    break;
                case System.Windows.Forms.Keys.NumPad4:
                    key += "数字鍵4";
                    break;
                case System.Windows.Forms.Keys.NumPad5:
                    key += "数字鍵5";
                    break;
                case System.Windows.Forms.Keys.NumPad6:
                    key += "数字鍵6";
                    break;
                case System.Windows.Forms.Keys.NumPad7:
                    key += "数字鍵7";
                    break;
                case System.Windows.Forms.Keys.NumPad8:
                    key += "数字鍵8";
                    break;
                case System.Windows.Forms.Keys.NumPad9:
                    key += "数字鍵9";
                    break;
                default:
                    break;
            }
            return key;
        }
    }
}
