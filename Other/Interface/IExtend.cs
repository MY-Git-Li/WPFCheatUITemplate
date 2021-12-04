using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCheatUITemplate.Other.Interface
{
    public interface IExtend
    {
        void StartAsync();

        void EndAsync();

        void Start();

        void End();

        void OnGameRunAsync();
    }
}
