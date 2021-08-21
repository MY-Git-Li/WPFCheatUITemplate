﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheatUITemplt.MyGameFuns
{
    class AllowBackground:GameFun
    {
       
        public AllowBackground()
        {
            this.gameFunDateStruct = new WPFCheatUITemplate.Other.GameFunDateStruct()
            {
                ModuleName = "PlantsVsZombies.exe",
                ModuleOffsetAddress = 0x5D040,
                IsSignatureCode = false,
                IsIntPtr = false,

                Vk = Keys.NumPad6,
                FsModifiers = HotKey.KeyModifiers.Ctrl,

                KeyDescription_SC = "Ctrl+数字键6",
                FunDescribe_SC = "允许后台运行",

                KeyDescription_TC = "Ctrl+數字鍵6",
                FunDescribe_TC = "允許後臺運行",

                KeyDescription_EN = "Ctrl+Number 6",
                FunDescribe_EN = "Running in background",

                IsTrigger = false,

            };

        GameFunManger.Instance.RegisterGameFun(this);
        }

        public override void Awake()
        {

        }

        public override void DoFirstTime(double value)
        {
            CheatTools.WriteMemoryByte(gameFunDateStruct.GameDataAddress.Address, gameFunDateStruct.Handle, new byte[] { 0xC2, 0x04, 0x00 });
        }

        public override void DoRunAgain(double value)
        {
            CheatTools.WriteMemoryByte(gameFunDateStruct.GameDataAddress.Address, gameFunDateStruct.Handle, new byte[] { 0x55, 0x8B, 0xEC });
           
        }
        public override void Ending()
        {

        }
    }
}

