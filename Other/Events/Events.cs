using CheatUITemplt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCheatUITemplate.Other.Events
{
    class Events
    {
        public delegate void OnGameRunHandler();

        public delegate void OnGameEndHandler();

        public delegate void OnRunGameFunsHandler(GameFun gameFun, bool isTrigger,bool isActive);

        public delegate void OnZeroAddressExceptionHandler(GameData gameData);

    }
}
