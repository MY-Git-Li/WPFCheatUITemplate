using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WPFCheatUITemplate.Other.Tools
{
    class mouse
    {
        [DllImport("user32.dll")]
        static extern void mouse_event(int flags, int dX, int dY, int buttons, int extraInfo);
        const int MOUSEEVENTF_MOVE = 0x1;//模拟鼠标移动
        const int MOUSEEVENTF_LEFTDOWN = 0x2;//
        const int MOUSEEVENTF_LEFTUP = 0x4;
        const int MOUSEEVENTF_RIGHTDOWN = 0x8;
        const int MOUSEEVENTF_RIGHTUP = 0x10;
        const int MOUSEEVENTF_MIDDLEDOWN = 0x20;
        const int MOUSEEVENTF_MIDDLEUP = 0x40;
        const int MOUSEEVENTF_WHEEL = 0x800;
        const int MOUSEEVENTF_ABSOLUTE = 0x8000;




        /// <summary>
        /// 控制鼠标移动到指定位置
        /// </summary>
        /// <param name="start"></param>
        /// <param name="End"></param>
        /// <param name="speed"></param>
        public static void mouse_move(Point start, Point End, int speed)
        {
            int startX = start.X;
            int startY = start.Y;
            int EndX = End.X;
            int EndY = End.Y;
            int x = startX;
            int y = startY;
            while (x != EndX || y != EndY)
            {
                if (startX > EndX && x != EndX)
                {
                    x -= speed;
                    if (x <= EndX) x = EndX;
                }
                if (startX < EndX && x != EndX)
                {
                    x += speed;
                    if (x >= EndX) x = EndX;
                }
                if (startY > EndY && y != EndY)
                {
                    y -= speed;
                    if (y <= EndY) y = EndY;
                }
                if (startY < EndY && y != EndY)
                {
                    y += speed;
                    if (y >= EndY) y = EndY;
                }
                mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, x * 65536 / 1920, y * 65536 / 1080, 0, 0);
                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// 按下鼠标左键
        /// </summary>
        public static void mouse_click()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }
    }
}
