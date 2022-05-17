using GameOverlay.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using WPFCheatUITemplate.Core;
using WPFCheatUITemplate.Core.Draw;

namespace WPFCheatUITemplate
{
    class DrawManager:DataBase
    {
       
        DrawWindow drawWindow;
        public Dictionary<string, SolidBrush> _brushes;
        public Dictionary<string, Font> _fonts;


        public void Init(int maxFPS)
        {
            drawWindow = new DrawWindow(maxFPS);
            ShowFPS(true);
            _brushes = drawWindow._brushes;
            _fonts = drawWindow._fonts;
        }


        public void DrawFun(Action<Graphics> action)
        {
            if (drawWindow != null)
                drawWindow.DrawCallBack = action;
        }

        public void SetBrushes(Action<Graphics> action)
        {
            if (drawWindow !=null)
            {
                drawWindow.SetBrushes = action;
            }
        }

        public void SetFonts(Action<Graphics> action)
        {
            if (drawWindow != null)
            {
                drawWindow.SetFonts = action;
            }
        }


        public void Close()
        {
            drawWindow.Dispose();
        }

        public void Run()
        {
            drawWindow.Run();
        }

        public void ShowFPS(bool isopen)
        {
            drawWindow.ShowFPS = isopen;
        }

       
    }
}
