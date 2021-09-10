using GameOverlay.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using WPFCheatUITemplate.Other.Draw;
using static WPFCheatUITemplate.Other.Draw.Memory;

namespace WPFCheatUITemplate
{
    class DrawManager
    {
        public WindowData _windowData;
        DrawWindow drawWindow;
        public Dictionary<string, SolidBrush> _brushes;
        public Dictionary<string, Font> _fonts;

        public void Init(string WinDowName)
        {
            drawWindow = new DrawWindow(WinDowName);
            _windowData = drawWindow._WindowData;
            _brushes = drawWindow._brushes;
            _fonts = drawWindow._fonts;
        }

        public void DrawFun(Action<Graphics> action)
        {
           
           drawWindow.DrawCallBack = action;
        }

        public void SetBrushes(Action<Graphics> action)
        {
            drawWindow.SetBrushes = action;
        }

        public void SetFonts(Action<Graphics> action)
        {
            drawWindow.SetFonts = action;
        }


        public void Close()
        {
            drawWindow.Dispose();
        }

        public void Run()
        {
            drawWindow.Run();
        }

        public Vector2 WorldToScreen(int MatrixAddress, Vector3 target)
        {
            Vector2 _worldToScreenPos;
            Vector3 _camera;

            float[] viewmatrix = Memory.ReadMatrix<float>(MatrixAddress, 16);

            _camera.Z = viewmatrix[8] * target.X + viewmatrix[9] * target.Y + viewmatrix[10] * target.Z + viewmatrix[11];
            if (_camera.Z < 0.001f)
                return new Vector2(0, 0);

            _camera.X = _windowData.Width / 2;
            _camera.Y = _windowData.Height / 2;
            _camera.Z = 1 / _camera.Z;

            _worldToScreenPos.X = viewmatrix[0] * target.X + viewmatrix[1] * target.Y + viewmatrix[2] * target.Z + viewmatrix[3];
            _worldToScreenPos.Y = viewmatrix[4] * target.X + viewmatrix[5] * target.Y + viewmatrix[6] * target.Z + viewmatrix[7];

            _worldToScreenPos.X = _camera.X + _camera.X * _worldToScreenPos.X * _camera.Z;
            _worldToScreenPos.Y = _camera.Y - _camera.Y * _worldToScreenPos.Y * _camera.Z;

            return _worldToScreenPos;
        }
    }
}
