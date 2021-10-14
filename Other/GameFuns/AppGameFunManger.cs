using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using WPFCheatUITemplate;
using WPFCheatUITemplate.Other;
using static CheatUITemplt.HotKey;

namespace CheatUITemplt
{

    class AppGameFunManger
    {

        List<GameFunUI> gameFunUIs = new List<GameFunUI>();

        MainWindow mainWindow;

        CreateLayout createLayout;

        int pid;
        public int Pid { set => pid = value; get => pid; }

        SoundEffect soundEffect;

        UILangerManger uILangerManger;

        //注册热键
        public HotSystem hotSystem;

        MyButtonManger myButtonManger;


        #region 单例模式
        //单例模式
        private static AppGameFunManger instance;
        private AppGameFunManger() { }
        public static AppGameFunManger Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AppGameFunManger();
                    instance.myButtonManger = new MyButtonManger();
                    instance.hotSystem = new HotSystem();
                }
                return instance;
            }
        }

        #endregion

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

        public void SetAllGameFunEnding()
        {
            foreach (var item in gameFunUIs)
            {
                if (item.gameFun != null)
                {
                    item.gameFun.Ending();
                }
            }

        }

        public void SetAllGameFunAwake()
        {
            foreach (var item in gameFunUIs)
            {
                if (item.gameFun != null)
                {
                    item.gameFun.Awake();
                }
            }

        }

        public void SetAllGameFunData()
        {

            foreach (var item in gameFunUIs)
            {
                if (item.gameFun != null)
                {
                    item.gameFun.gameFunDateStruct.Handle = CheatTools.GetProcessHandle(pid);
                    item.gameFun.gameFunDateStruct.ModuleAddress = CheatTools.GetProcessModuleHandle((uint)pid, item.gameFun.gameFunDateStruct.ModuleName);
                    item.gameFun.GetGameData();
                }
                
            }

        }

        public void RegisterWindow(Window window)
        {
            this.mainWindow = (MainWindow)window;
        }

        public void RegisterManger(CreateLayout createLayout)
        {
            this.createLayout = createLayout;
        }
        public void RegisterManger(UILangerManger uILangerManger)
        {
            this.uILangerManger = uILangerManger;
        }
        public void RegisterManger(SoundEffect soundEffect)
        {
            this.soundEffect = soundEffect;
        }

        public void InitUI()
        {
            foreach (var item in gameFunUIs)
            {
                if (item.gameFun!=null)
                {
                    LanguageUI funlanguageUI = new LanguageUI()
                    {
                        Description_EN = item.gameFun.gameFunDateStruct.FunDescribe_EN,
                        Description_SC = item.gameFun.gameFunDateStruct.FunDescribe_SC,
                        Description_TC = item.gameFun.gameFunDateStruct.FunDescribe_TC
                    };

                    LanguageUI keylanguageUI = new LanguageUI()
                    {
                        Description_EN = item.gameFun.gameFunDateStruct.KeyDescription_EN,
                        Description_SC = item.gameFun.gameFunDateStruct.KeyDescription_SC,
                        Description_TC = item.gameFun.gameFunDateStruct.KeyDescription_TC
                    };
                    item.funlanguageUI = funlanguageUI;
                    item.keylanguageUI = keylanguageUI;


                    createLayout.AddRowDefin();

                    item.showDescription = createLayout.CreatShowDescription(item.gameFun);

                    item.myStackPanel = createLayout.CreatMyStackPanel(item.gameFun, item);

                    createLayout.UpDateRow();
                }
                else if (item.keylanguageUI!=null)
                {
                    createLayout.AddRowDefin();
                    createLayout.CreatSeparate(item.keylanguageUI);
                    createLayout.UpDateRow();
                }else
                {
                    createLayout.AddRowDefin();
                    createLayout.CreatSeparate();
                    createLayout.UpDateRow();
                }
                
            }
        }

        public void RegisterGameFun(GameFun a)
        {

            GameFunUI gameFunUI = new GameFunUI();
            gameFunUI.gameFun = a;

            gameFunUIs.Add(gameFunUI);

        }

        public void CreatSeparate()
        {
            GameFunUI gameFunUI = new GameFunUI();
            gameFunUIs.Add(gameFunUI);

        }

        public void CreatSeparate(string Description_SC, string Description_TC = "", string Description_EN = "")
        {
            GameFunUI gameFunUI = new GameFunUI();
           
            LanguageUI languageUI = new LanguageUI()
            {
                Description_EN = Description_EN,
                Description_SC = Description_SC,
                Description_TC = Description_TC
            };
            gameFunUI.keylanguageUI = languageUI;

            gameFunUIs.Add(gameFunUI);

            uILangerManger.RegisterLanguageUI(languageUI);

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
                    if (item.gameFun.gameFunDateStruct.IsTrigger)
                    {
                        RegisterHotKey(item.gameFun.gameFunDateStruct.FsModifiers, item.gameFun.gameFunDateStruct.Vk, new MyButton(item.myStackPanel.button),
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
                        RegisterHotKey(item.gameFun.gameFunDateStruct.FsModifiers, item.gameFun.gameFunDateStruct.Vk, new MyButton(item.myStackPanel.checkBox),
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

        public void EnableControl()
        {
            foreach (var item in gameFunUIs)
            {
                SetControlEnable(item.myStackPanel.button, true);
                SetControlEnable(item.myStackPanel.checkBox, true);
            }
        }
        public void DisableControl()
        {
            foreach (var item in gameFunUIs)
            {
                SetControlEnable(item.myStackPanel.button, false);
                SetControlEnable(item.myStackPanel.checkBox, false);
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


      
    }






}
