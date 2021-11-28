using CheatUITemplt;
using System;
using System.Collections.Generic;

namespace WPFCheatUITemplate.Other
{
    class FastGameFun : GameFun
    {
        /// <summary>
        /// awake为Awake的委托，ending为Ending的委托
        /// </summary>
        public Action<FastGameFun> awake, ending;
        /// <summary>
        /// doFirstTime为DoFirstTime的委托，doRunAgain为DoRunAgain的委托
        /// </summary>
        public Action<FastGameFun, double> doFirstTime, doRunAgain;
        /// <summary>
        /// 存放自定义的数据地址
        /// </summary>
        public List<GameDataAddress> gameDataAddresseList;

        public Dictionary<GameVersion.Version,GameData> gameDates;

        public Action<FastGameFun> setGameDate;

        public FastGameFun()
        {
            gameDataAddresseList = new List<GameDataAddress>();
            gameDates = new Dictionary<GameVersion.Version, GameData>();
            awake = null;
            ending = null;
            setGameDate = null;
        }

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
