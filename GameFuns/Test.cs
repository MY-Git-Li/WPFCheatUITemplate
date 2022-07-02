using WPFCheatUITemplate.Core.Tools.Extensions;
using WPFCheatUITemplate.Core.GameFuns;
using WPFCheatUITemplate.Core.Tools.ASM;
using WPFCheatUITemplate.Core.Extends;
using WPFCheatUITemplate.Core.Net.DownFile;
using System;

namespace WPFCheatUITemplate.GameFuns
{
    class Test : Extends
    {
        //ASM asm;
        public Test()
        {
            //asm = new ASM();
        }
        public override void StartAsync()
        {
            //#region KeybdTest

            //Keybd.KeyClick(System.Windows.Forms.Keys.LWin, System.Windows.Forms.Keys.R);
            //System.Threading.Thread.Sleep(100);
            //Keybd.KeyWrite("notepad");
            //Keybd.KeyClick(System.Windows.Forms.Keys.Space);
            //System.Threading.Thread.Sleep(100);
            //Keybd.KeyClick(System.Windows.Forms.Keys.Enter);
            //System.Threading.Thread.Sleep(200);

            //Keybd.KeyWrite("世界上最伟大的人，是谁呢？你肯定知道的", 100);
            //Keybd.KeyClick(KeyValueEnum.vbKeyReturn);
            //Keybd.keyClickFormStr("whoisyourdady");
            //Keybd.KeyClick(KeyValueEnum.vbKeyReturn);

            //#endregion

            //#region mouseTest

            //mouse.mouse_move(new System.Drawing.Point(10, 10), new System.Drawing.Point(100, 100), 5);

            //#endregion

            //var dd = "12".NumberToChinese();

            //AppGameFunManager.Instance.UILangerManger.AddString("gg", "你好", "hi");

            //System.Windows.Forms.MessageBox.Show(AppGameFunManager.Instance.UILangerManger.GetString("gg"));


            //下载器测试

            //string url = "https://down.rbread02.cn/down/pcsoft/7/24/wjnmzhgj.zip?timestamp=611f17b3&md5hash=b9909cc46beb9b8038fe0bf1f6f88221";
            //var mtd = new MultiThreadDownloader(url, Environment.GetEnvironmentVariable("temp"), "E:\\wjnmzhgj.zip", 8);

            //mtd.Configure(req =>
            //{

            //    req.Referer = "https://masuit.com";
            //    req.Headers.Add("Origin", "https://baidu.com");

            //});

            //mtd.TotalProgressChanged += (sender, e) =>
            //{
            //    var downloader = sender as MultiThreadDownloader;
            //    Console.WriteLine("下载进度：" + downloader.TotalProgress + "%");
            //    Console.WriteLine("下载速度：" + downloader.TotalSpeedInBytes / 1024 / 1024 + "MBps");
            //};

            //mtd.FileMergedComplete += (sender, e) =>
            //{
            //    Console.WriteLine("文件合并完成");
            //};
            //mtd.Start();//开始下载


            AppGameFunManager.Instance.CheakVersion();
        }
       
    }
}
