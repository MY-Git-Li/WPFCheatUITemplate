using System;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading;
using Veldrid;
using Veldrid.Sdl2;
using Veldrid.StartupUtilities;

using static ImGuiNET.ImGuiNative;

namespace ImGuiNET
{
    class Program
    {
        private static Sdl2Window _window;
        private static GraphicsDevice _gd;
        private static CommandList _cl;
        private static ImGuiController _controller;
        static public Action DrawMenu;
        static public Action DrawBack;
        // UI state
        private static float _f = 0.0f;
        private static int _counter = 0;
        private static int _dragInt = 0;
        private static Vector3 _clearColor = new Vector3(0f, 0f, 0f);
        private static bool _showImGuiDemoWindow = false;
        private static bool _showAnotherWindow = false;
        private static bool _showMemoryEditor = false;
       
        private static uint s_tab_bar_flags = (uint)ImGuiTabBarFlags.Reorderable;
        static bool[] s_opened = { true, true, true, true }; // Persistent user state


        static bool ShowmFun = true;
        static bool OnEsp = false;
        //static bool EndPro=false;
        static void SetThing(out float i, float val) { i = val; }

       
        [DllImport("user32.dll")]
        public static extern int SetWindowLong(
            IntPtr hWnd,
            int nIndex,
            int dwNewLong
            );
        [DllImport("user32.dll")]
        public static extern bool SetLayeredWindowAttributes(
                IntPtr hwnd,
                int crKey,
                byte bAlpha,
                int dwFlags
                );

        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndlnsertAfter, int X, int Y, int cx, int cy, uint Flags);
        //导入dll文件

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        //函数声明
        public static extern int GetAsyncKeyState(int vKey);

       public static void MainDraw()
        {
            // Create window, GraphicsDevice, and all resources necessary for the demo.
            VeldridStartup.CreateWindowAndGraphicsDevice(
                new WindowCreateInfo(5, 5, 1920, 1080, WindowState.Normal, "ImGui.NET Sample Program"),
                new GraphicsDeviceOptions(true, null, true, ResourceBindingModel.Improved, true, true),
                out _window,
                out _gd);
            _window.Resized += () =>
            {
                _gd.MainSwapchain.Resize((uint)_window.Width, (uint)_window.Height);
                _controller.WindowResized(_window.Width, _window.Height);
            };
            _cl = _gd.ResourceFactory.CreateCommandList();
            _controller = new ImGuiController(_gd, _gd.MainSwapchain.Framebuffer.OutputDescription, _window.Width, _window.Height);


            #region 设置透明窗体

            SetWindowLong(_window.Handle, -16, (int)0x00000000L);
            SetWindowLong(_window.Handle, -20, (int)(0x00080000 | 0x00000000L | 0x00000080L));
            SetLayeredWindowAttributes(_window.Handle, 0, 0, 0x1);
            SetWindowPos(_window.Handle, (IntPtr)(-1), 0, 0, 1920, 1080, 0x0040|0x1);

            #endregion
           
            // Main application loop
            while (_window.Exists)
            {
                InputSnapshot snapshot = _window.PumpEvents();
                if (!_window.Exists) { break; }
                _controller.Update(1f / 144f, snapshot); // Feed the input events to our ImGui controller, which passes them through to ImGui.

                SubmitUI();
                //if (EndPro)
                //    break;


                _cl.Begin();
                _cl.SetFramebuffer(_gd.MainSwapchain.Framebuffer);
                _cl.ClearColorTarget(0, new RgbaFloat(_clearColor.X, _clearColor.Y, _clearColor.Z, 1f));
                _controller.Render(_gd, _cl);
                _cl.End();
                _gd.SubmitCommands(_cl);
                _gd.SwapBuffers(_gd.MainSwapchain);
            }

            // Clean up Veldrid resources
            _gd.WaitForIdle();
            _controller.Dispose();
            _cl.Dispose();
            _gd.Dispose();
        }

        private static unsafe void SubmitUI()
        {
            // Demo code adapted from the official Dear ImGui demo program:
            // https://github.com/ocornut/imgui/blob/master/examples/example_win32_directx11/main.cpp#L172

            // 1. Show a simple window.
            // Tip: if we don't call ImGui.BeginWindow()/ImGui.EndWindow() the widgets automatically appears in a window called "Debug".

            DrawMenu?.Invoke();
            DrawBack?.Invoke();

            if (GetAsyncKeyState(36) != 0)
            {
                ShowmFun = !ShowmFun;
                Thread.Sleep(200);
            }
            //if (GetAsyncKeyState(35) != 0)
            //{
            //    EndPro = true;
            //}

            if (OnEsp)
            {
                DrawBack?.Invoke();
                ImGui.GetBackgroundDrawList().AddCircle(new Vector2(500, 500), 50, ImGui.ColorConvertFloat4ToU32(new Vector4(0.1f, 0.8f, 0.3f, 1f)), 0, 2);
                ImGui.GetBackgroundDrawList().AddText(new Vector2(370, 370), ImGui.ColorConvertFloat4ToU32(new Vector4(0.1f, 0.8f, 0.3f, 1f)), "我爱你爱着你");
                ImGui.GetBackgroundDrawList().AddLine(new Vector2(470, 470), new Vector2(150, 300), ImGui.ColorConvertFloat4ToU32(new Vector4(0.1f, 0.8f, 0.3f, 1f)), 2);
                ImGui.GetBackgroundDrawList().AddRectFilled(new Vector2(300, 560), new Vector2(320, 800), ImGui.ColorConvertFloat4ToU32(new Vector4(0.1f, 0.8f, 0.3f, 1f)), 0, 0);
                ImGui.GetBackgroundDrawList().AddRect(new Vector2(300, 500), new Vector2(320, 800), ImGui.ColorConvertFloat4ToU32(new Vector4(0.1f, 0.8f, 0.3f, 1f)), 0, 0, 1);
            }
            if (!ShowmFun)
                SetWindowLong(_window.Handle, -20, (int)(0x00080000 | 0x00000000L | 0x00000080L| 0x20));
            else
                SetWindowLong(_window.Handle, -20, (int)(0x00080000 | 0x00000000L | 0x00000080L));

            if (ShowmFun)
            {
                ImGui.Begin("无敌辅助 Home键显示菜单", ref ShowmFun);
                
                DrawMenu?.Invoke();

                if (ImGui.CollapsingHeader("功能选项"))
                {
                    if (ImGui.BeginTable("功能", 3))//这里的3表示几列
                    {
                        bool tow = false;
                        ImGui.TableNextColumn(); ImGui.Checkbox("透视", ref OnEsp);
                        ImGui.TableNextColumn(); ImGui.Checkbox("自瞄", ref tow);
                        ImGui.EndTable();
                    }
                }
                if (ImGui.CollapsingHeader("按键"))
                {
                    if (ImGui.BeginTable("检测A按键", 1))
                    {
                        ImGui.TableNextColumn();
                        ImGui.Text("检测a键中:");
                        if (ImGui.IsKeyDown(ImGui.GetKeyIndex(ImGuiKey.A)))
                        {
                            ImGui.SameLine();
                            ImGui.Text("你按下了A键 " + ImGui.GetIO().KeysDownDuration[igGetKeyIndex(ImGuiKey.A)].ToString("0.00") + "sec");
                        }
                        ImGui.EndTable();
                    }
                }

                ImGuiIOPtr io = ImGui.GetIO();
                SetThing(out io.DeltaTime, 2f);

                ImGui.End();
            }

        }
    }
}
