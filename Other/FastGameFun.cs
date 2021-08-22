using CheatUITemplt;
using System;

namespace WPFCheatUITemplate.Other
{
    class FastGameFun : GameFun
    {

        public Action<FastGameFun> awake, ending;
        public Action<FastGameFun, double> doFirstTime, doRunAgain;

        public void Start()
        {
            GameFunManger.Instance.RegisterGameFun(this);
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
        }
    }
}
