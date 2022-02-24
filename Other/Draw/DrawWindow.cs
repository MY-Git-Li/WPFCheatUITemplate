using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CheatUITemplt;
using GameOverlay.Drawing;
using GameOverlay.Windows;
using static WPFCheatUITemplate.Other.Draw.Memory;

namespace WPFCheatUITemplate.Other.Draw
{
    class DrawWindow : IDisposable
    {

        private readonly GraphicsWindow _window;

        public readonly Dictionary<string, SolidBrush> _brushes;
        public readonly Dictionary<string, Font> _fonts;
       
        public  WindowData _WindowData;
       
        public Action<Graphics> DrawCallBack;
        public Action<Graphics> SetBrushes;
        public Action<Graphics> SetFonts;

        public Memory memory;

        public DrawWindow(int pid):this(pid,300)
        {

        }

        public DrawWindow(int pid, int maxfps)
        {
            memory = new Memory();

            memory.Initialize(pid);
            memory.SetForegroundWindow();

            _WindowData = memory.GetGameWindowData();


            _brushes = new Dictionary<string, SolidBrush>();
            _fonts = new Dictionary<string, Font>();


            var gfx = new Graphics()
            {
                VSync = false,
                MeasureFPS = true,
                PerPrimitiveAntiAliasing = true,
                TextAntiAliasing = true
            };

            _window = new GraphicsWindow(_WindowData.Left, _WindowData.Top, _WindowData.Width, _WindowData.Height, gfx)
            {
                FPS = maxfps,
                IsTopmost = true,
                IsVisible = true
            };

            _window.SetupGraphics += _window_SetupGraphics;
            _window.DrawGraphics += _window_DrawGraphics;
            _window.DestroyGraphics += _window_DestroyGraphics;
        }

        public DrawWindow(string WinDowName):this(WinDowName,300)
        {
           
        }

        public DrawWindow(string WinDowName,int maxfps)
        {
            memory = new Memory();

            memory.Initialize(WinDowName);
            memory.SetForegroundWindow();

            _WindowData = memory.GetGameWindowData();


            _brushes = new Dictionary<string, SolidBrush>();
            _fonts = new Dictionary<string, Font>();


            var gfx = new Graphics()
            {
                VSync = false,
                MeasureFPS = true,
                PerPrimitiveAntiAliasing = true,
                TextAntiAliasing = true
            };

            _window = new GraphicsWindow(_WindowData.Left, _WindowData.Top, _WindowData.Width, _WindowData.Height, gfx)
            {
                FPS = maxfps,
                IsTopmost = true,
                IsVisible = true
            };

            _window.SetupGraphics += _window_SetupGraphics;
            _window.DrawGraphics += _window_DrawGraphics;
            _window.DestroyGraphics += _window_DestroyGraphics;
        }

        private void _window_SetupGraphics(object sender, SetupGraphicsEventArgs e)
        {
            var gfx = e.Graphics;

            if (e.RecreateResources)
            {
                foreach (var pair in _brushes) pair.Value.Dispose();
            }

            _brushes["black"] = gfx.CreateSolidBrush(0, 0, 0);
            _brushes["white"] = gfx.CreateSolidBrush(255, 255, 255);
            _brushes["red"] = gfx.CreateSolidBrush(255, 0, 98);
            _brushes["green"] = gfx.CreateSolidBrush(0, 128, 0);
            //_brushes["blue"] = gfx.CreateSolidBrush(30, 144, 255);
            _brushes["background"] = gfx.CreateSolidBrush(0x33, 0x36, 0x3F);
            _brushes["grid"] = gfx.CreateSolidBrush(255, 255, 255, 0.2f);
            _brushes["deepPink"] = gfx.CreateSolidBrush(247, 63, 147, 255);

            _brushes["transparency"] = gfx.CreateSolidBrush(0, 0, 0, 0);

            SetBrushes?.Invoke(gfx);


            if (e.RecreateResources) return;

            _fonts["arial"] = gfx.CreateFont("Arial", 12);
            _fonts["Microsoft YaHei"] = gfx.CreateFont("Microsoft YaHei", 12);
            _fonts["consolas"] = gfx.CreateFont("Consolas", 14);

            SetFonts?.Invoke(gfx);


        }

         private void _window_DrawGraphics(object sender, DrawGraphicsEventArgs e)
        {
            var gfx = e.Graphics;
            gfx.ClearScene(_brushes["transparency"]);
            ResizeWindow(gfx);

            gfx.DrawText(_fonts["Microsoft YaHei"], 12, _brushes["deepPink"], 10, 20,
               $"FPS：{gfx.FPS}\n"/*FrameTime：{e.FrameTime}\nFrameCount：{e.FrameCount}\nDeltaTime：{e.DeltaTime}*/);

            DrawCallBack?.Invoke(gfx);

        }


        public void DrawTest(Graphics gfx, int x,int y,string test)
        {
            gfx.DrawText(_fonts["Microsoft YaHei"], 12.0f, _brushes["blue"], x, y,test);
        }


        private void ResizeWindow(Graphics gfx)
        {
            // 窗口移动跟随
            _WindowData = memory.GetGameWindowData();
            _window.X = _WindowData.Left;
            _window.Y = _WindowData.Top;
            _window.Width = _WindowData.Width;
            _window.Height = _WindowData.Height;
            gfx.Resize(_window.Width, _window.Height);
        }

       

        private void _window_DestroyGraphics(object sender, DestroyGraphicsEventArgs e)
        {
            foreach (var pair in _brushes) pair.Value.Dispose();
            foreach (var pair in _fonts) pair.Value.Dispose();
        }
        public void Run()
        {
            _window.Create();
            _window.Join();
        }
        ~DrawWindow()
        {
            Dispose(false);
        }

        #region IDisposable Support
        private bool disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                _window.Dispose();

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
