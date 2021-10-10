using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using WPFCheatUITemplate;
using WPFCheatUITemplate.Other;
using static CheatUITemplt.HotKey;

namespace CheatUITemplt
{
    
    class GameFunManger
    {
       
        List<GameFunUI> gameFunUIs = new List<GameFunUI>();

        MainWindow mainWindow;
        public MainWindow MainWindow { get => mainWindow; set => mainWindow = value; }
      
        CreateLayout createLayout;
        public CreateLayout CreateLayout { get => createLayout; set => createLayout = value; }

        int pid;
        public int Pid{ set => pid = value; get => pid; }

        SoundEffect soundEffect;
        public SoundEffect SoundEffect { get => soundEffect; set => soundEffect = value; }

        UILangerManger uILangerManger;
        public UILangerManger UILangerManger { get => uILangerManger; set => uILangerManger = value; }

        //注册热键
        public HotSystem hotSystem;

        MyButtonManger myButtonManger;


        #region 单例模式
        //单例模式
        private static GameFunManger instance;
        private GameFunManger() { }
        public static GameFunManger Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameFunManger();
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
                mainWindow.lbl_processID.Text ="";
            }else
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

            StartFlashAnimation();
        }

        public void SetAllGameFunAwake()
        {
            foreach (var item in gameFunUIs)
            {
                item.gameFun.Awake();
            }

            StopFlashAnimation();
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


        public void RegisterGameFun(GameFun a)
        {
            
            GameFunUI gameFunUI = new GameFunUI();
            gameFunUI.gameFun = a;

            CreateLayout.AddRowDefin();

            gameFunUI.simplifiedChinese = CreateLayout.CreatDescription_SC(a);
            gameFunUI.traditionalChinese = CreateLayout.CreatDescription_TC(a);
            gameFunUI.englishDescription = CreateLayout.CreatDescription_EN(a);

            gameFunUI.showDescription = CreateLayout.CreatShowDescription(a);

            gameFunUI.myStackPanel = CreateLayout.CreatMyStackPanel(a, gameFunUI);

            CreateLayout.UpDateRow();

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

                        item.gameFun.DoFirstTime(slider==null?0:slider.Value);

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

        void SetControlEnable(System.Windows.Controls.Control control, bool enable)
        {
            if (control != null)
            {
                control.IsEnabled = enable;
            }
           
        }


        void RegisterHotKey(KeyModifiers fsModifiers, Keys vk, MyButton buttonClick, HotSystemFun fun)
        {
            int id;
            id = hotSystem.RegisterHotKey(mainWindow.Hwnd, fsModifiers, vk, fun);

            myButtonManger.SetButtonFun(buttonClick, id, hotSystem);

            //ButtonFunId.Add(id);
            //index += 1;
            //buttonClick.SetOnClick(index, (i) => { hotSystem.ClickHotKeyFun(ButtonFunId[i]); });
        }

        void RegisterHotKey(KeyModifiers fsModifiers, Keys vk, HotSystemFun fun)
        {
            hotSystem.RegisterHotKey(mainWindow.Hwnd, fsModifiers, vk, fun);
        }


        void StartFlashAnimation()
        {
            mainWindow.PlayFlashAinimation();
        }

        void StopFlashAnimation()
        {
            mainWindow.StopFlashAinimation();
        }
    }


    


   
}
