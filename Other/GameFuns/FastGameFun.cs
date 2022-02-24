﻿using CheatUITemplt;
using System;
using System.Collections.Generic;

namespace WPFCheatUITemplate.Other
{
    class FastGameFun : GameFun
    {
        /// <summary>
        /// awake为Awake的委托，ending为Ending的委托,start为Start的委托
        /// </summary>
        public Action<FastGameFun> awake, ending, start;
        /// <summary>
        /// doFirstTime为DoFirstTime的委托，doRunAgain为DoRunAgain的委托
        /// </summary>
        public Action<FastGameFun, double> doFirstTime, doRunAgain;

        #region 陈旧--目前存放数据建议使用AddData方法

        /// <summary>
        /// 存放自定义的数据地址
        /// </summary>
        public List<GameDataAddress> gameDataAddresseList;

        public Dictionary<GameVersion.Version,GameData> gameDates;

        public Action<FastGameFun> setGameDate;

        #endregion


        public FastGameFun(GameFunDataAndUIStruct gameFunDataAndUIStruct, params string[] Id)
        {
            this.gameFunDataAndUIStruct = gameFunDataAndUIStruct;

            gameDataAddresseList = new List<GameDataAddress>();
            gameDates = new Dictionary<GameVersion.Version, GameData>();

            doFirstTime = (i,v) => 
            {
                for (int j = 0; j < Id.Length; j++)
                {
                    i.memory.WriteMemoryByID(Id[j]);
                }

            };

            doRunAgain = (i, v) =>
            {
                for (int j = 0; j < Id.Length; j++)
                {
                    i.memory.WriteMemoryByID(Id[j],true);
                }
            };

        }
            
        public FastGameFun()
        {
            gameDataAddresseList = new List<GameDataAddress>();
            gameDates = new Dictionary<GameVersion.Version, GameData>();
            awake = null;
            ending = null;
            setGameDate = null;
            start =null;
        }
        /// <summary>
        /// 兼容以前的存放数据方法，如果使用AddData方法存放数据，可以不调用此方法
        /// </summary>
        public void Go()
        {
            SetGameData();
        }

        private void SetGameData()
        {
            setGameDate?.Invoke(this);
            foreach (var item in gameDates)
            {
                gameFunDataAndUIStruct.AddData(item.Key, item.Value);
            }
        }

        public override void Start()
        {
            start?.Invoke(this);
        }


        public override void Awake()
        {
            awake?.Invoke(this);
        }

        public override void DoFirstTime(double value)
        {
            doFirstTime?.Invoke(this, value);
        }

        public override void DoRunAgain(double value)
        {
            doRunAgain?.Invoke(this, value);
        }

        public override void Ending()
        {
            ending?.Invoke(this);
            gameDataAddresseList.Clear();
            gameDates.Clear();
        }
    }
}
