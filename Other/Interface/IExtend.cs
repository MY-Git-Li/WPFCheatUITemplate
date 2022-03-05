using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCheatUITemplate.Other.Interface
{
    public interface IExtend
    { 
        /// <summary>
        /// 相当于Main--异步
        /// </summary>
        void StartAsync();
        /// <summary>
        /// 当程序关闭时--异步
        /// </summary>
        void EndAsync();
        /// <summary>
        /// 相当于Main--同步
        /// </summary>
        void Start();
        /// <summary>
        /// 当程序关闭时--同步
        /// </summary>
        void End();
        /// <summary>
        /// 当游戏运行时
        /// </summary>
        void OnGameRunAsync();
    }
}
