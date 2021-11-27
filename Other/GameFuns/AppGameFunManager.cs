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
using WPFCheatUITemplate.Other.Tools.Extensions;
using static CheatUITemplt.HotKey;

namespace CheatUITemplt
{

    class AppGameFunManager
    {

        List<GameFunUI> gameFunUIs = new List<GameFunUI>();

        MainWindow mainWindow;

        CreateLayout createLayout;

        IntPtr handle;
        public IntPtr Handle { get { return handle; } set{ handle = value; }}

        int pid;
        public int Pid { set => pid = value; get => pid; }

        SoundEffect soundEffect;

        UILangerManger uILangerManger;

        //注册热键
        public HotSystem hotSystem;

        MyButtonManger myButtonManger;

        CreateUIGrid createUIGrid;

        LanguageUI messageBoxMessage;

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
                }
                return instance;
            }
        }


        #endregion

        #region 语言设置
        public void SetTraditionalChinese()
        {

            foreach (var item in gameFunUIs)
            {
                if (item.gameFun != null)
                {
                    item.showDescription.keyDescription.Text = item.keylanguageUI.Description_TC;
                    item.showDescription.funDescription.Text = item.funlanguageUI.Description_TC;
                }
            }

            uILangerManger.SetTraditionalChinese();
        }

        public void SetEnglish()
        {
            foreach (var item in gameFunUIs)
            {
                if (item.gameFun != null)
                {
                    item.showDescription.keyDescription.Text = item.keylanguageUI.Description_EN;
                    item.showDescription.funDescription.Text = item.funlanguageUI.Description_EN;
                    item.showDescription.keyDescription.FontSize = 17;
                    item.showDescription.funDescription.FontSize = 17;
                }
            }

            uILangerManger.SetEnglish();
        }

        public void SetSimplifiedChinese()
        {
            foreach (var item in gameFunUIs)
            {
                if (item.gameFun != null)
                {
                    item.showDescription.keyDescription.Text = item.keylanguageUI.Description_SC;
                    item.showDescription.funDescription.Text = item.funlanguageUI.Description_SC;
                }
                   
            }

            uILangerManger.SetSimplifiedChinese();
        }

        #endregion

        public void SetViewPid()
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
            handle = CheatTools.GetProcessHandle(pid);

            GameInformationInit();

            foreach (var item in gameFunUIs)
            {
                if (item.gameFun != null)
                {
                    item.gameFun.gameFunDateStruct.Handle = handle;

                    item.gameFun.gameFunDateStruct.Pid = pid;

                    item.gameFun.gameFunDateStruct.GetGameDate(GameVersion.GetCurrentVersion(handle));

                    item.gameFun.Awake();
                }
            }

        }

        private void GameInformationInit()
        {
            GameInformation.InitInformation(handle, pid);
            DataManagerInit();
        }

        public void GetAllGameFunData()
        {

            foreach (var item in gameFunUIs)
            {
                if (item.gameFun != null)
                {
                    GameData gameDate = item.gameFun.gameFunDateStruct.GetGameDate(GameVersion.GetCurrentVersion(handle));
                   
                    if (gameDate!=null)
                    {
                        item.gameFun.GetGameData();
                    }

                   
                }
                
            }

        }

        public void RegisterWindow(Window window)
        {
            this.mainWindow = (MainWindow)window;
        }

        public void RegisterManger(CreateLayout createLayout, Grid grid)
        {
            this.createLayout = createLayout;
            //createLayout.SetGrid(grid);
            createUIGrid = new CreateUIGrid(grid, this.createLayout);
        }

        public void RegisterManger(UILangerManger uILangerManger)
        {
            messageBoxMessage = new LanguageUI()
            {
                textBlock = new TextBlock(),
                Description_SC = "未检测到游戏进程，请先运行游戏在激活修改功能@错误",
                Description_TC = "未檢測到遊戲進程，請先運行遊戲在激活修改功能@錯誤",
                Description_EN = "Game is not detected,please launch the game before activating cheats.@Error"
            };

            this.uILangerManger = uILangerManger;
            uILangerManger.RegisterLanguageUI(messageBoxMessage);
        }

        public void RegisterManger(SoundEffect soundEffect)
        {
            this.soundEffect = soundEffect;
        }

        public void StartUI(System.Action action)
        {
            action?.Invoke();
            DrawUI();
        }

        void DataManagerInit()
        {
            Type[] types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (var type in types)
            {
                if (type.Name == "DataManager")
                {
                    MethodInfo init = type.GetMethod("Init", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);
                    if (init.IsStatic)
                    {
                        init.Invoke(null, null);
                    }
                    MethodInfo getVersion = type.GetMethod("GetVersion", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);
                    if (getVersion.IsStatic)
                    {
                        var pra = new object[] { GameInformation.CurentVersion };
                        getVersion.Invoke(null, pra);
                    }
                }
            }
        }

        private void DrawUI()
        {
            foreach (var item in gameFunUIs)
            {
                if (item.gameFun!=null)
                {
                    if (!item.gameFun.gameFunDateStruct.uIData.IsHide)
                    {
                        LanguageUI funlanguageUI = new LanguageUI()
                        {
                            Description_EN = item.gameFun.gameFunDateStruct.uIData.FunDescribe_EN,
                            Description_SC = item.gameFun.gameFunDateStruct.uIData.FunDescribe_SC,
                            Description_TC = item.gameFun.gameFunDateStruct.uIData.FunDescribe_TC
                        };

                        LanguageUI keylanguageUI = new LanguageUI()
                        {
                            Description_EN = item.gameFun.gameFunDateStruct.uIData.KeyDescription_EN,
                            Description_SC = item.gameFun.gameFunDateStruct.uIData.KeyDescription_SC,
                            Description_TC = item.gameFun.gameFunDateStruct.uIData.KeyDescription_TC
                        };
                        item.funlanguageUI = funlanguageUI;
                        item.keylanguageUI = keylanguageUI;


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
                else if (item.keylanguageUI!=null)
                {
                    createLayout.CreatSeparate(item.keylanguageUI,item.SeparateOffset);
                   
                }else
                {
                    createLayout.CreatSeparate(item.SeparateOffset);
                }
                
            }
        }

        public void RegisterGameFun(GameFun a)
        {

            GameFunUI gameFunUI = new GameFunUI();
            gameFunUI.gameFun = a;

            gameFunUIs.Add(gameFunUI);

        }

        #region 布局相关
        public void CreatSeparate(int offset=15)
        {
            GameFunUI gameFunUI = new GameFunUI();
            gameFunUI.SeparateOffset = offset < 0 ? 15 : offset;
            gameFunUIs.Add(gameFunUI);

        }

        public void CreatSeparateEx(string Description_SC, string Description_TC = "", string Description_EN = "",int offset = 30)
        {
            GameFunUI gameFunUI = new GameFunUI();
           
            LanguageUI languageUI = new LanguageUI()
            {
                Description_EN = Description_EN,
                Description_SC = Description_SC,
                Description_TC = Description_TC
            };
            gameFunUI.keylanguageUI = languageUI;

            gameFunUI.SeparateOffset = offset < 30 ? 30 : offset;

            gameFunUIs.Add(gameFunUI);

            uILangerManger.RegisterLanguageUI(languageUI);

        }

        public void CreatSeparate(string Description_SC, string Description_EN = "", int offset = 30)
        {
            CreatSeparateEx(Description_SC, Description_SC.ToTraditional(), Description_EN, offset);
        }

        public void NextPage(int offset = 0)
        {
            GameFunUI gameFunUI = new GameFunUI();
            gameFunUI.doNextPage = true;
            gameFunUI.nextPageOffset = offset;
            gameFunUIs.Add(gameFunUI);
        }

        #endregion

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
                if (item.gameFun!=null)
                {
                    if (item.gameFun.gameFunDateStruct != null)
                    {
                        RefHotKey refHotKey = item.gameFun.gameFunDateStruct.refHotKey;
                        if (item.gameFun.gameFunDateStruct.uIData.IsTrigger)
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
                if (item.myStackPanel.button!=null)
                {
                    item.myStackPanel.button.IsEnabled = true;
                    item.myStackPanel.button.Click += ButtonHandlerNoGamePro;
                }

                if (item.myStackPanel.checkBox!=null)
                {
                    item.myStackPanel.checkBox.IsEnabled = true;
                    item.myStackPanel.checkBox.Click += CheckBoxHandlerNoGamePro;
                }
            }


        }

        public void EndHotsystem()
        {
            mainWindow.EndHotsystem();
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
            id = hotSystem.RegisterHotKey(mainWindow.Hwnd, fsModifiers, vk, fun);

            myButtonManger.SetButtonFun(buttonClick, id, hotSystem);

            //ButtonFunId.Add(id);
            //index += 1;
            //buttonClick.SetOnClick(index, (i) => { hotSystem.ClickHotKeyFun(ButtonFunId[i]); });
        }

        private void RegisterHotKey(KeyModifiers fsModifiers, Keys vk, HotSystemFun fun)
        {
            hotSystem.RegisterHotKey(mainWindow.Hwnd, fsModifiers, vk, fun);
        }


        void ButtonHandlerNoGamePro(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show(messageBoxMessage.textBlock.Text.Split('@')[0], messageBoxMessage.textBlock.Text.Split('@')[1], MessageBoxButton.OK, MessageBoxImage.Error);
        }

        void CheckBoxHandlerNoGamePro(object sender, RoutedEventArgs e)
        {
            var check = sender as System.Windows.Controls.CheckBox;
            System.Windows.MessageBox.Show(messageBoxMessage.textBlock.Text.Split('@')[0], messageBoxMessage.textBlock.Text.Split('@')[1], MessageBoxButton.OK, MessageBoxImage.Error);
            check.IsChecked = false;
        }
    }






}
