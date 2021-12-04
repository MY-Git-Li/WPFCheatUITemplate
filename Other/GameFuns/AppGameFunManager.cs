﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using WPFCheatUITemplate;
using WPFCheatUITemplate.GameMode;
using WPFCheatUITemplate.Other;
using WPFCheatUITemplate.Other.Events;
using WPFCheatUITemplate.Other.Exceptions;
using WPFCheatUITemplate.Other.Interface;
using WPFCheatUITemplate.Other.Tools.Extensions;
using static CheatUITemplt.HotKey;

namespace CheatUITemplt
{

    class AppGameFunManager
    {
        #region 引用类型

        List<GameFunUI> gameFunUIs = new List<GameFunUI>();

        List<IExtend> extends = new List<IExtend>();

        MainWindow mainWindow;

        IntPtr Hwnd;

        CreateLayout createLayout;

        SoundEffect soundEffect;

        UILangerManger uILangerManger;

        HotSystem hotSystem;

        MyButtonManger myButtonManger;

        CreateUIGrid createUIGrid;

        InvestigateGame investigateGame;

        #endregion

        #region 事件

        //public event Events.OnGameRunHandler OnGameRunEvent;

        //public event Events.OnGameEndHandler OnGameEndEvent;

        //public event Events.OnRunGameFunsHandler OnRunGameFunsEvent;

        //public event Events.OnZeroAddressExceptionHandler OnZeroAddressExceptionEvent;


        //async void DoOnGameRunEventAsync()
        //{
        //    var t = Task.Run(() =>
        //    {
        //        OnGameRunEvent?.Invoke();
        //    });
        //    await t;
        //}
        //async void DoOnGameEndEventAsync()
        //{
        //    var t = Task.Run(() =>
        //    {
        //        OnGameEndEvent?.Invoke();
        //    });
        //    await t;
        //}
        //async void DoRunGameFunsEventAsync(GameFun gameFun, bool isTrigger, bool isActive)
        //{
        //    var t = Task.Run(() =>
        //    {
        //        OnRunGameFunsEvent?.Invoke(gameFun, isTrigger, isActive);
        //    });
        //    await t;
        //}
        //async void DoZeroAddressExceptionEventAsync(GameData gameData)
        //{
        //    var t = Task.Run(() =>
        //    {
        //        OnZeroAddressExceptionEvent?.Invoke(gameData);
        //    });
        //    await t;
        //}

        #endregion

        #region 单例模式
        //单例模式
        private static AppGameFunManager instance;
        private AppGameFunManager() { }
        public static AppGameFunManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AppGameFunManager();
                    instance.myButtonManger = new MyButtonManger();
                    instance.hotSystem = new HotSystem();
                    instance.soundEffect = new SoundEffect();
                    instance.investigateGame = new InvestigateGame(GameInformation.ProcessName);
                    instance.uILangerManger = new UILangerManger();
                }
                return instance;
            }
        }

        internal UILangerManger UILangerManger
        {
            get
            {
                return uILangerManger;
            }

        }


        #endregion

        #region 语言设置


        void SetDefaultLanguage()
        {
            string mode = WPFCheatUITemplate.Properties.Settings.Default.langer;
            if (mode == "SC")
            {
                SetSimplifiedChinese();
            }
            if (mode == "TC")
            {
                SetTraditionalChinese();
            }
            if (mode == "EN")
            {
                SetEnglish();
            }

        }

        void ChangRadioButton(string mode)
        {
            var en = WPFCheatUITemplate.Properties.Settings.Default.RadioButton_EN;
            var sc = WPFCheatUITemplate.Properties.Settings.Default.RadioButton_SC;
            var tc = WPFCheatUITemplate.Properties.Settings.Default.RadioButton_TC;

            if (mode == "SC")
            {
                sc = true;
                en = false;
                tc = false;
            }
            if (mode == "TC")
            {
                sc = false;
                en = false;
                tc = true;
            }
            if (mode == "EN")
            {
                sc = false;
                en = true;
                tc = false;
            }

            WPFCheatUITemplate.Properties.Settings.Default.RadioButton_EN = en;
            WPFCheatUITemplate.Properties.Settings.Default.RadioButton_SC = sc;
            WPFCheatUITemplate.Properties.Settings.Default.RadioButton_TC = tc;

        }

        void SaveLanguage(string langer)
        {
            ChangRadioButton(langer);

            WPFCheatUITemplate.Properties.Settings.Default.langer = langer;

            WPFCheatUITemplate.Properties.Settings.Default.Save();
        }

        public void SetTraditionalChinese()
        {
            UILangerManger.SetTraditionalChinese();

            Changelanguage();

            SaveLanguage("TC");
        }

        public void SetEnglish()
        {
            UILangerManger.SetEnglish();

            Changelanguage();

            SaveLanguage("EN");
        }

        public void SetSimplifiedChinese()
        {
            UILangerManger.SetSimplifiedChinese();

            Changelanguage();

            SaveLanguage("SC");
        }

        void Changelanguage(int size = 17)
        {
            foreach (var item in gameFunUIs)
            {
                if (item.gameFun != null)
                {
                    item.showDescription.keyDescription.Text = item.keylanguageUI.ShowText;
                    item.showDescription.funDescription.Text = item.funlanguageUI.ShowText;
                    item.showDescription.keyDescription.FontSize = size;
                    item.showDescription.funDescription.FontSize = size;
                }

            }
        }


        #endregion

        #region 寻找游戏执行的函数

        /// <summary>
        /// 找到游戏后，后台运行的函数
        /// </summary>
        /// <param name="pid"></param>
        public void startFindGame_DoWork(int pid)
        {
            SetGameInformation(pid);
            DataManagerInit();
            RunAllGameFunAwake();
            GetAllGameFunData();
            //DoOnGameRunEventAsync();
            ExtendsOnGameRun();
        }
        /// <summary>
        /// 找到游戏后，主线程执行的函数，解决跨线程处理ui的问题
        /// </summary>
        public void startFindGame_RunWorkerCompleted()
        {
            SetViewPid(GameInformation.Pid);
            StopFlashAnimation();
            EnableControl();
            RegisterAllHotKey();

        }

        /// <summary>
        /// 找到游戏后，游戏退出后，主线程处理ui问题
        /// </summary>
        public void findGameing_RunWorkerCompleted()
        {
            EndHotsystem();
            DisableControl();
            SetGameInformation(0);
            SetViewPid(GameInformation.Pid);
            StartFlashAnimation();
            RunAllGameFunEnding();
            //DoOnGameEndEventAsync();
        }

        #endregion


        private void SetGameInformation(int pid)
        {
            GameInformation.Pid = pid;
            if (pid!=0)
            {
                GameInformation.Handle = CheatTools.GetProcessHandle(pid);
            }else
            {
                GameInformation.Handle = IntPtr.Zero;
            }
        }

        void DataManagerInit()
        {
            WPFCheatUITemplate.Other.GameFuns.AddressDataManager.Init();
        }

        /// <summary>
        /// 程序在游戏前退出清理资源
        /// </summary>
        void ClearRes()
        {
            if (GameInformation.Pid != 0)
                RunAllGameFunEnding();
        }

        #region 扩展方法相关

        void GetAllExtend()
        {
            Type[] types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (var type in types)
            {
                if (type.IsInterface)
                    continue;
                Type[] ins = type.GetInterfaces();
                foreach (Type ty in ins)
                {
                    if (ty == typeof(IExtend))
                    {
                        if (!type.IsAbstract)
                        {
                            IExtend extend = Activator.CreateInstance(type) as IExtend;
                            extends.Add(extend);
                        }

                    }

                }
            }
        }

        async void StartExtendAsync(IExtend item)
        { 
            Task t = Task.Run(() =>
            {
                item.StartAsync();
            });

            await t;
        }

        async void EndExtendAync(IExtend item)
        {
            Task t = Task.Run(() =>
            {
                item.EndAsync();
            });

            await t;
        }

        async void OnGameRunAsync(IExtend item)
        {
            Task t = Task.Run(() =>
            {
                item.OnGameRunAsync();
            });

            await t;
        }

        void StartExtend(IExtend item)
        {
            item.Start();
        }

        void EndExtend(IExtend item)
        {
            item.End();
        }

        void StartExtends()
        {
            foreach (var item in extends)
            {
                StartExtend(item);
                StartExtendAsync(item);
            }
        }

        void EndExtends()
        {
            foreach (var item in extends)
            {
                EndExtend(item);
                EndExtendAync(item);
            }
        }

       void ExtendsOnGameRun()
       {
            foreach (var item in extends)
            {
                OnGameRunAsync(item);
            }
        }

        #endregion

        void SetViewPid(int pid)
        {
            if (pid == 0)
            {
                mainWindow.lbl_processID.Text = "";
            }
            else
            {
                mainWindow.lbl_processID.Text = pid.ToString();

            }

        }

        public void RunAllGameFunEnding()
        {
            foreach (var item in gameFunUIs)
            {
                if (item.gameFun != null)
                {
                    item.gameFun.Ending();
                }
            }

        }

        public void RunAllGameFunAwake()
        {
           
            foreach (var item in gameFunUIs)
            {
                if (item.gameFun != null)
                {
                    item.gameFun.gameFunDataAndUIStruct.Handle = GameInformation.Handle;

                    item.gameFun.gameFunDataAndUIStruct.Pid = GameInformation.Pid;

                    item.gameFun.gameFunDataAndUIStruct.GetData(GameInformation.CurentVersion);

                    item.gameFun.Awake();
                }
            }

        }

        public void GetAllGameFunData()
        {

            foreach (var item in gameFunUIs)
            {
                if (item.gameFun != null)
                {

                    item.gameFun.gameFunDataAndUIStruct.GetData(GameInformation.CurentVersion);

                    item.gameFun.GetGameData();

                }

            }

        }

        #region 窗口相关

        public void RegisterWindow(Window window, ResourceDictionary Resdictionary,Grid grid)
        {
            this.mainWindow = (MainWindow)window;
            mainWindow.Loaded += mainWindows_Loaded;
            mainWindow.Closing += mainWindows_Closing;

            if (Resdictionary !=  null)
            {
                RegisterManger(new CreateLayout(Resdictionary), grid);
            }

            GetAllExtend();

            StartExtends();
        }

        private void mainWindows_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            EndHotsystem();
            ClearRes();

            EndExtends();
        }

        private void mainWindows_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Interop.WindowInteropHelper wndHelper = new System.Windows.Interop.WindowInteropHelper(mainWindow);
            Hwnd = wndHelper.Handle;

            investigateGame.FindingGame();

            SetDefaultLanguage();
            StartFlashAnimation();
        }

        #endregion

        public void RegisterManger(CreateLayout createLayout, Grid grid)
        {
            if (createLayout != null)
            {
                this.createLayout = createLayout;
            }

            if (grid != null)
            {
                createUIGrid = new CreateUIGrid(grid, this.createLayout);
            }
            
        }


        public void RegisterManger(UILangerManger uILangerManger)
        {
            if(uILangerManger!=null)
                this.uILangerManger = uILangerManger;
        }

        public void RegisterManger(SoundEffect soundEffect)
        {
            if (soundEffect != null)
                this.soundEffect = soundEffect;
        }


        public void StartUI(System.Action action)
        {
            action?.Invoke();
            DrawUI();
        }

        private void DrawUI()
        {
            foreach (var item in gameFunUIs)
            {
                if (item.gameFun != null)
                {
                    if (!item.gameFun.gameFunDataAndUIStruct.uIData.IsHide)
                    {
                        LanguageUI funlanguageUI = new LanguageUI()
                        {
                            Description_EN = item.gameFun.gameFunDataAndUIStruct.uIData.FunDescribe_EN,
                            Description_SC = item.gameFun.gameFunDataAndUIStruct.uIData.FunDescribe_SC,
                            Description_TC = item.gameFun.gameFunDataAndUIStruct.uIData.FunDescribe_TC
                        };

                        LanguageUI keylanguageUI = new LanguageUI()
                        {
                            Description_EN = item.gameFun.gameFunDataAndUIStruct.uIData.KeyDescription_EN,
                            Description_SC = item.gameFun.gameFunDataAndUIStruct.uIData.KeyDescription_SC,
                            Description_TC = item.gameFun.gameFunDataAndUIStruct.uIData.KeyDescription_TC
                        };
                        item.funlanguageUI = funlanguageUI;
                        item.keylanguageUI = keylanguageUI;

                        UILangerManger.RegisterLanguageUI(funlanguageUI);
                        UILangerManger.RegisterLanguageUI(keylanguageUI);

                        createLayout.AddRowDefin();

                        item.showDescription = createLayout.CreatShowDescription(item.gameFun);

                        item.myStackPanel = createLayout.CreatMyStackPanel(item.gameFun, item);

                        createLayout.UpDateRow();
                    }
                }
                else if (item.doNextPage)
                {
                    createUIGrid.NextPage(item.nextPageOffset);
                }
                else if (item.keylanguageUI != null)
                {
                    createLayout.CreatSeparate(item.keylanguageUI, item.SeparateOffset);

                }
                else
                {
                    createLayout.CreatSeparate(item.SeparateOffset);
                }

            }
        }

        #region 布局相关

        public void AddGameFunUIs(GameFunUI gameFunUI)
        {
            gameFunUIs.Add(gameFunUI);
        }

        public void AddLanguageUI(LanguageUI languageUI)
        {
            UILangerManger.RegisterLanguageUI(languageUI);
        }


        #endregion

        public void RegisterGameFun(GameFun a)
        {

            GameFunUI gameFunUI = new GameFunUI();
            gameFunUI.gameFun = a;

            gameFunUIs.Add(gameFunUI);

        }

        public void RegisterAllHotKey()
        {
            #region//快捷键禁用/启用
            {
                RegisterHotKey(HotKey.KeyModifiers.Shift | HotKey.KeyModifiers.Ctrl, System.Windows.Forms.Keys.Home,
                    new HotSystemFun(() =>
                    {
                        //"快捷键禁用";
                        hotSystem.BanOtherHotKeyFun(1);

                        soundEffect.PlayTurnOnEffect();
                    }, () =>
                    {
                        //"快捷键启用";
                        hotSystem.RelieveHotKeyFun();

                        soundEffect.PlayTurnOffEffect();
                    }));
            }
            #endregion
            
            foreach (var item in gameFunUIs)
            {
                if (item.gameFun != null)
                {
                    if (item.gameFun.gameFunDataAndUIStruct != null)
                    {
                        RefHotKey refHotKey = item.gameFun.gameFunDataAndUIStruct.refHotKey;
                        if (item.gameFun.gameFunDataAndUIStruct.uIData.IsTrigger)
                        {
                            RegisterHotKey(refHotKey.FsModifiers, refHotKey.Vk, new MyButton(item.myStackPanel.button),
                            new HotSystemFun(() =>
                            {
                                //DoRunGameFunsEventAsync(item.gameFun,true,true);

                                Slider slider = item.myStackPanel.ValueEntered;

                                try
                                {
                                    item.gameFun.DoFirstTime(slider == null ? 0 : slider.Value);

                                    soundEffect.PlayTurnOnEffect();

                                }
                                catch (ZeroAddressException e)
                                {
                                    HandleZeroAddressExceptionOnRunGameFun(e);
                                }

                            }));
                        }
                        else
                        {
                            RegisterHotKey(refHotKey.FsModifiers, refHotKey.Vk, new MyButton(item.myStackPanel.checkBox),
                            new HotSystemFun(() =>
                            {
                                //DoRunGameFunsEventAsync(item.gameFun,false,true);

                                Slider slider = item.myStackPanel.ValueEntered;

                                System.Windows.Controls.CheckBox checkBox = item.myStackPanel.checkBox;

                                checkBox.IsChecked = true;

                                try
                                {
                                    item.gameFun.DoFirstTime(slider == null ? 0 : slider.Value);

                                    if (slider != null)
                                    {
                                        slider.IsEnabled = !slider.IsEnabled;
                                    }

                                    soundEffect.PlayTurnOnEffect();

                                }catch(ZeroAddressException e)
                                {
                                    HandleZeroAddressExceptionOnRunGameFun(e);
                                }
                                

                            }, () =>
                            {
                                //DoRunGameFunsEventAsync(item.gameFun,false,false);

                                Slider slider = item.myStackPanel.ValueEntered;

                                System.Windows.Controls.CheckBox checkBox = item.myStackPanel.checkBox;

                                checkBox.IsChecked = false;

                                try
                                {
                                    item.gameFun.DoRunAgain(slider == null ? 0 : slider.Value);

                                    if (slider != null)
                                    {
                                        slider.IsEnabled = !slider.IsEnabled;
                                    }

                                    soundEffect.PlayTurnOffEffect();

                                }catch (ZeroAddressException e)
                                {
                                    HandleZeroAddressExceptionOnRunGameFun(e);
                                }
                                
                            }));
                        }
                    }
                }


            }
        }

        public void EnableControl()
        {
            //foreach (var item in gameFunUIs)
            //{
            //    SetControlEnable(item.myStackPanel.button, true);
            //    SetControlEnable(item.myStackPanel.checkBox, true);
            //}

            foreach (var item in gameFunUIs)
            {
                if (item.myStackPanel.button != null)
                {
                    item.myStackPanel.button.IsEnabled = true;
                    item.myStackPanel.button.Click -= ButtonHandlerNoGamePro;
                }

                if (item.myStackPanel.checkBox != null)
                {
                    item.myStackPanel.checkBox.IsEnabled = true;
                    item.myStackPanel.checkBox.Click -= CheckBoxHandlerNoGamePro;
                }
            }

        }
        public void DisableControl()
        {
            //foreach (var item in gameFunUIs)
            //{
            //    SetControlEnable(item.myStackPanel.button, false);
            //    SetControlEnable(item.myStackPanel.checkBox, false);
            //}

            foreach (var item in gameFunUIs)
            {
                if (item.myStackPanel.button != null)
                {
                    item.myStackPanel.button.IsEnabled = true;
                    item.myStackPanel.button.Click += ButtonHandlerNoGamePro;
                }

                if (item.myStackPanel.checkBox != null)
                {
                    item.myStackPanel.checkBox.IsEnabled = true;
                    item.myStackPanel.checkBox.Click += CheckBoxHandlerNoGamePro;
                }
            }


        }

        public void EndHotsystem()
        {
            hotSystem.UnRegisterHotKeyAll(Hwnd);
            hotSystem.CloseHotKeyFunAll();
        }

        public void StartFlashAnimation()
        {
            mainWindow.PlayFlashAinimation();
        }

        public void StopFlashAnimation()
        {
            mainWindow.StopFlashAinimation();
        }

        private void SetControlEnable(System.Windows.Controls.Control control, bool enable)
        {
            if (control != null)
            {
                control.IsEnabled = enable;
            }

        }

        private void RegisterHotKey(KeyModifiers fsModifiers, Keys vk, MyButton buttonClick, HotSystemFun fun)
        {
            int id;
            id = hotSystem.RegisterHotKey(Hwnd, fsModifiers, vk, fun);

            myButtonManger.SetButtonFun(buttonClick, id, hotSystem);

            //ButtonFunId.Add(id);
            //index += 1;
            //buttonClick.SetOnClick(index, (i) => { hotSystem.ClickHotKeyFun(ButtonFunId[i]); });
        }

        private void RegisterHotKey(KeyModifiers fsModifiers, Keys vk, HotSystemFun fun)
        {
            hotSystem.RegisterHotKey(Hwnd, fsModifiers, vk, fun);
        }


        void ButtonHandlerNoGamePro(object sender, RoutedEventArgs e)
        {

            string mode = WPFCheatUITemplate.Properties.Settings.Default.langer;
            string error = "";
            if (mode == "SC")
            {
                error = WPFCheatUITemplate.Properties.Resources.messbox_sc;
            }
            if (mode == "TC")
            {
                error = WPFCheatUITemplate.Properties.Resources.messbox_sc.ToTraditional();
            }
            if (mode == "EN")
            {
                error = WPFCheatUITemplate.Properties.Resources.messbox;
            }



            System.Windows.MessageBox.Show(error.Split('@')[0], error.Split('@')[1], MessageBoxButton.OK, MessageBoxImage.Error);
        }

        void CheckBoxHandlerNoGamePro(object sender, RoutedEventArgs e)
        {

            string mode = WPFCheatUITemplate.Properties.Settings.Default.langer;
            string error = "";
            if (mode == "SC")
            {
                error = WPFCheatUITemplate.Properties.Resources.messbox_sc;
            }
            if (mode == "TC")
            {
                error = WPFCheatUITemplate.Properties.Resources.messbox_sc.ToTraditional();
            }
            if (mode == "EN")
            {
                error = WPFCheatUITemplate.Properties.Resources.messbox;
            }


            var check = sender as System.Windows.Controls.CheckBox;
            System.Windows.MessageBox.Show(error.Split('@')[0], error.Split('@')[1], MessageBoxButton.OK, MessageBoxImage.Error);
            check.IsChecked = false;
        }

        void HandleZeroAddressExceptionOnRunGameFun(ZeroAddressException e)
        {
           
            var g = e.gameData;

            //DoZeroAddressExceptionEventAsync(g);

            #region 详细错误信息

            string mode = WPFCheatUITemplate.Properties.Settings.Default.langer;
            string Details = "";
            string Modulename = "";
            string mouduleOffset = "";
            string PointerOffset = "";
            string Signature = "";
            string Signatureoffset = "";
            string Wrongaddress = "";
            string Exception = "";



            if (mode == "SC")
            {
                Details = WPFCheatUITemplate.Properties.Resources.Details_sc;
                Modulename = WPFCheatUITemplate.Properties.Resources.Modulename_sc;
                mouduleOffset = WPFCheatUITemplate.Properties.Resources.mouduleOffset_sc;
                PointerOffset = WPFCheatUITemplate.Properties.Resources.PointerOffset_sc;
                Signature = WPFCheatUITemplate.Properties.Resources.Signature_sc;
                Signatureoffset = WPFCheatUITemplate.Properties.Resources.Signatureoffset_Sc;
                Wrongaddress = WPFCheatUITemplate.Properties.Resources.Wrongaddress_sc;
                Exception = WPFCheatUITemplate.Properties.Resources.Exception_sc;
            }
            if (mode == "TC")
            {
                Details = WPFCheatUITemplate.Properties.Resources.Details_sc.ToTraditional();
                Modulename = WPFCheatUITemplate.Properties.Resources.Modulename_sc.ToTraditional();
                mouduleOffset = WPFCheatUITemplate.Properties.Resources.mouduleOffset_sc.ToTraditional();
                PointerOffset = WPFCheatUITemplate.Properties.Resources.PointerOffset_sc.ToTraditional();
                Signature = WPFCheatUITemplate.Properties.Resources.Signature_sc.ToTraditional();
                Signatureoffset = WPFCheatUITemplate.Properties.Resources.Signatureoffset_Sc.ToTraditional();
                Wrongaddress = WPFCheatUITemplate.Properties.Resources.Wrongaddress_sc.ToTraditional();
                Exception = WPFCheatUITemplate.Properties.Resources.Exception_sc.ToTraditional();
            }
            if (mode == "EN")
            {
                Details = WPFCheatUITemplate.Properties.Resources.Details;
                Modulename = WPFCheatUITemplate.Properties.Resources.Modulename;
                mouduleOffset = WPFCheatUITemplate.Properties.Resources.mouduleOffset;
                PointerOffset = WPFCheatUITemplate.Properties.Resources.PointerOffset;
                Signature = WPFCheatUITemplate.Properties.Resources.Signature;
                Signatureoffset = WPFCheatUITemplate.Properties.Resources.Signatureoffset;
                Wrongaddress = WPFCheatUITemplate.Properties.Resources.Wrongaddress;
                Exception = WPFCheatUITemplate.Properties.Resources.Exception;
            }


            string text = "";

            text += GameInformation.CurentVersion.ToString();

            

            if (g.IsIntPtr)
            {
                if (g.IntPtrOffset != null)
                {
                    string offset = "";

                    foreach (var item in g.IntPtrOffset)
                    {
                        offset += "0x";
                        offset += item.ToString("X");
                        offset += " ";
                    }

                    text = Wrongaddress + "\n" +
                    Details + "\n" +
                    Modulename + g.ModuleName + "\n" +
                    mouduleOffset + "0x" + g.ModuleOffsetAddress.ToString("X") + "\n" +
                    PointerOffset + offset + "\n";

                }else
                {
                    text = Wrongaddress + "\n" +
                    Details + "\n" +
                    Modulename + g.ModuleName + "\n" +
                    mouduleOffset + "0x" + g.ModuleOffsetAddress.ToString("X");
                }
               
            }else if (g.IsSignatureCode)
            {
                text = Wrongaddress + "\n" +
                Details + "\n" +
                Modulename + g.ModuleName + "\n" +
                mouduleOffset + "0x" + g.ModuleOffsetAddress.ToString("X") + "\n" +
                Signature + g.SignatureCode + "\n" +
                Signatureoffset + "0x" + g.SignatureCodeOffset.ToString("X") + "\n";
            }else
            {
                text = Wrongaddress + "\n" +
                Details + "\n" +
                Modulename + g.ModuleName + "\n" +
                mouduleOffset + "0x" + g.ModuleOffsetAddress.ToString("X") + "\n";
            }

            System.Windows.MessageBox.Show(text, Exception, MessageBoxButton.OK, MessageBoxImage.Error);

            #endregion

            System.Diagnostics.Process.Start(System.Windows.Application.ResourceAssembly.Location);//重启软件

            Environment.Exit(0);//关闭程序

            
        }

        public void WndProcWPF(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            hotSystem.WndProcWPF(hwnd, msg, wParam, lParam, ref handled);
        }

        public void WndProcWinForm(ref Message m)
        {
            hotSystem.WndProcWinForm(ref m);
        }
    }






}
