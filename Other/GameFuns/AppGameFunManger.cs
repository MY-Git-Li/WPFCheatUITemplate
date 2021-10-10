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
                item.showDescription.keyDescription.Text = item.traditionalChinese.keyDescription.Text;
                item.showDescription.funDescription.Text = item.traditionalChinese.funDescription.Text;
            }

            uILangerManger.SetTraditionalChinese();
        }

        public void SetEnglish()
        {
            foreach (var item in gameFunUIs)
            {
                item.showDescription.keyDescription.Text = item.englishDescription.keyDescription.Text;
                item.showDescription.funDescription.Text = item.englishDescription.funDescription.Text;
                item.showDescription.keyDescription.FontSize = 17;
                item.showDescription.funDescription.FontSize = 17;
            }

            uILangerManger.SetEnglish();
        }

        public void SetSimplifiedChinese()
        {
            foreach (var item in gameFunUIs)
            {
                item.showDescription.keyDescription.Text = item.simplifiedChinese.keyDescription.Text;
                item.showDescription.funDescription.Text = item.simplifiedChinese.funDescription.Text;
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
                item.gameFun.Ending();
            }

        }

        public void SetAllGameFunAwake()
        {
            foreach (var item in gameFunUIs)
            {
                item.gameFun.Awake();
            }

        }

        public void SetAllGameFunData()
        {

            foreach (var item in gameFunUIs)
            {
                item.gameFun.gameFunDateStruct.Handle = CheatTools.GetProcessHandle(pid);
                item.gameFun.gameFunDateStruct.ModuleAddress = CheatTools.GetProcessModuleHandle((uint)pid, item.gameFun.gameFunDateStruct.ModuleName);
                item.gameFun.GetGameData();
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

        public void RegisterGameFun(GameFun a)
        {

            GameFunUI gameFunUI = new GameFunUI();
            gameFunUI.gameFun = a;

            createLayout.AddRowDefin();

            gameFunUI.simplifiedChinese = createLayout.CreatDescription_SC(a);
            gameFunUI.traditionalChinese = createLayout.CreatDescription_TC(a);
            gameFunUI.englishDescription = createLayout.CreatDescription_EN(a);

            gameFunUI.showDescription = createLayout.CreatShowDescription(a);

            gameFunUI.myStackPanel = createLayout.CreatMyStackPanel(a, gameFunUI);

            createLayout.UpDateRow();

            gameFunUIs.Add(gameFunUI);

        }


        public void CreatSeparate()
        {
            createLayout.AddRowDefin();
            createLayout.CreatSeparate();
            createLayout.UpDateRow();
        }

        public void CreatSeparate(string Description_SC, string Description_TC = "", string Description_EN = "")
        {

            LanguageUI languageUI = new LanguageUI()
            {
                Description_EN = Description_EN,
                Description_SC = Description_SC,
                Description_TC = Description_TC
            };


            uILangerManger.RegisterLanguageUI(languageUI);

            createLayout.AddRowDefin();
            createLayout.CreatSeparate(languageUI);
            createLayout.UpDateRow();
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

            //问题解决办法：用for是对数组操作，容易引发数组越界问题，故使用foreach 直接对指针进行操作。
            //for (int i = 0; i < gameFunUIs.Count; i++)
            //{
            //    if (gameFunUIs[i].gameFun.IsTrigger)
            //    {
            //        RegisterHotKey(gameFunUIs[i].gameFun.FsModifiers, gameFunUIs[i].gameFun.Vk, new MyButton(gameFunUIs[i].myPanel.button),
            //        new HotSystemFun(async () =>
            //        {
            //            gameFunUIs[i].gameFun.DoFirstTime();
            //            ChangeLabelColor(gameFunUIs[i], runColor);

            //            await Task.Delay(500);
            //            ChangeLabelColor(gameFunUIs[i], defaultColor);
            //        }));
            //    }
            //    else
            //    {
            //        RegisterHotKey(gameFunUIs[i].gameFun.FsModifiers, gameFunUIs[i].gameFun.Vk, new MyButton(gameFunUIs[i].myPanel.button),
            //        new HotSystemFun(() =>
            //        {
            //            gameFunUIs[i].gameFun.DoFirstTime();
            //            ChangeLabelColor(gameFunUIs[i], runColor);

            //        }, () =>
            //        {

            //            gameFunUIs[i].gameFun.DoRunAgain();
            //            ChangeLabelColor(gameFunUIs[i], defaultColor);

            //        }));
            //    }


            foreach (var item in gameFunUIs)
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
