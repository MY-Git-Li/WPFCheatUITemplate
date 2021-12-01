using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using WPFCheatUITemplate;
using WPFCheatUITemplate.GameMode;
using WPFCheatUITemplate.Other;
using WPFCheatUITemplate.Other.Interface;
using static CheatUITemplt.HotKey;

namespace CheatUITemplt
{

    class AppGameFunManager
    {

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

        LanguageUI messageBoxMessage;

        InvestigateGame investigateGame;

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

        void StartExtends()
        {
            foreach (var item in extends)
            {
                Task.Factory.StartNew(() => item.Start());
            }
        }

        void EndExtends()
        {
            foreach (var item in extends)
            {
                Task.Factory.StartNew(() => item.End());
            }
        }

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

            RegisteMessageBoxMessage();

            StartExtends();

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

        void RegisteMessageBoxMessage()
        {
            messageBoxMessage = new LanguageUI()
            {
                Description_SC = "未检测到游戏进程，请先运行游戏在激活修改功能@错误",
                Description_TC = "未檢測到遊戲進程，請先運行遊戲在激活修改功能@錯誤",
                Description_EN = "Game is not detected,please launch the game before activating cheats.@Error"
            };
            uILangerManger.RegisterLanguageUI(messageBoxMessage);
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

        public void StartUI(System.Action action)
        {
            action?.Invoke();
            DrawUI();
            GetAllExtend();
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
                            new HotSystemFun(async () =>
                            {

                                Slider slider = item.myStackPanel.ValueEntered;

                                item.gameFun.DoFirstTime(slider == null ? 0 : slider.Value);

                                soundEffect.PlayTurnOnEffect();

                                await Task.Delay(500);

                            }));
                        }
                        else
                        {
                            RegisterHotKey(refHotKey.FsModifiers, refHotKey.Vk, new MyButton(item.myStackPanel.checkBox),
                            new HotSystemFun(() =>
                            {


                                Slider slider = item.myStackPanel.ValueEntered;

                                item.gameFun.DoFirstTime(slider == null ? 0 : slider.Value);

                                System.Windows.Controls.CheckBox checkBox = item.myStackPanel.checkBox;
                                checkBox.IsChecked = true;


                                if (slider != null)
                                {
                                    slider.IsEnabled = !slider.IsEnabled;
                                }

                                soundEffect.PlayTurnOnEffect();

                            }, () =>
                            {
                                Slider slider = item.myStackPanel.ValueEntered;

                                System.Windows.Controls.CheckBox checkBox = item.myStackPanel.checkBox;
                                checkBox.IsChecked = false;

                                item.gameFun.DoRunAgain(slider == null ? 0 : slider.Value);

                                if (slider != null)
                                {
                                    slider.IsEnabled = !slider.IsEnabled;
                                }

                                soundEffect.PlayTurnOffEffect();

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
            System.Windows.MessageBox.Show(messageBoxMessage.ShowText.Split('@')[0], messageBoxMessage.ShowText.Split('@')[1], MessageBoxButton.OK, MessageBoxImage.Error);
        }

        void CheckBoxHandlerNoGamePro(object sender, RoutedEventArgs e)
        {
            var check = sender as System.Windows.Controls.CheckBox;
            System.Windows.MessageBox.Show(messageBoxMessage.ShowText.Split('@')[0], messageBoxMessage.ShowText.Split('@')[1], MessageBoxButton.OK, MessageBoxImage.Error);
            check.IsChecked = false;
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
