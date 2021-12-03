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


        public static event OnGameRunHandler OnGameRunEvent;

        public static event OnGameEndHandler OnGameEndEvent;

        public static event OnRunGameFunsHandler OnRunGameFunsEvent;

        public static event OnZeroAddressExceptionHandler OnZeroAddressExceptionEvent;


        public static async void DoOnGameRunEventAsync()
        {
            var t = Task.Run(() =>
            {
               OnGameRunEvent?.Invoke();
            });
            await t;
        }
        public static async void DoOnGameEndEventAsync()
        {
            var t = Task.Run(() =>
            {
                OnGameEndEvent?.Invoke();
            });
            await t;
        }
        public static async void DoRunGameFunsEventAsync(GameFun gameFun, bool isTrigger, bool isActive)
        {
            var t = Task.Run(() =>
            {
                OnRunGameFunsEvent?.Invoke(gameFun, isTrigger, isActive);
            });
            await t;
        }
        public static async void DoZeroAddressExceptionEventAsync(GameData gameData)
        {
            var t = Task.Run(() =>
            {
                OnZeroAddressExceptionEvent?.Invoke(gameData);
            });
            await t;
        }



    }
}
