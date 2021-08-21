﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheatUITemplt.MyGameFuns
{
    class ArbitrarilyPlant : GameFun
    {
       
        public ArbitrarilyPlant()
        {
            this.gameFunDateStruct = new WPFCheatUITemplate.Other.GameFunDateStruct()
            {
                ModuleName = "PlantsVsZombies.exe",

                IsSignatureCode = true,
                SignatureCodeOffset = 0xe,
                SignatureCode = "8B 54 24 0C 53 52 57",

                IsIntPtr = false,

                Vk = Keys.NumPad1,
                FsModifiers = HotKey.KeyModifiers.Ctrl,

                KeyDescription_SC = "Ctrl+数字键1",
                FunDescribe_SC = "随意种植",

                KeyDescription_TC = "Ctrl+數字鍵1",
                FunDescribe_TC = "隨意種植",

                KeyDescription_EN = "Ctrl+Number 1",
                FunDescribe_EN = "Planting will",

                IsTrigger = false,
            };
           


            GameFunManger.Instance.RegisterGameFun(this);
        }
        public override void Awake()
        {

        }

        public override void DoFirstTime(double value)
        {
            CheatTools.WriteMemoryByte(gameFunDateStruct.GameDataAddress.Address, gameFunDateStruct.Handle, new byte[] { 0xE9, 0x47, 0x09, 0x00, 0x00, 0x90 });

        }

        public override void DoRunAgain(double value)
        {
            CheatTools.WriteMemoryByte(gameFunDateStruct.GameDataAddress.Address, gameFunDateStruct.Handle, new byte[] { 0x0f, 0x84, 0x46, 0x09, 0x00, 0x00 });

        }
        public override void Ending()
        {

        }
    }
}
