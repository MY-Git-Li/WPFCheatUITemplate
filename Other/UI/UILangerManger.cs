using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFCheatUITemplate.Other.Tools.Extensions;
namespace WPFCheatUITemplate.Other.UI
{
    class UILangerManger
    {
        List<LanguageUI> languageUIs = new List<LanguageUI>();

        Dictionary<string, LanguageUI> languageUIsDictionary = new Dictionary<string, LanguageUI>();

        public void RegisterLanguageUI(LanguageUI languageUI)
        {
            if (languageUI.ShowText == "")
            {
                languageUI.ShowText = languageUI.Description_SC;
            }
            languageUIs.Add(languageUI);
        }

        public void AddString(string id, string Description_SC, string Description_TC, string Description_EN)
        {
            var obj = new LanguageUI()
            {
                ShowText = Description_SC,
                Description_SC = Description_SC,
                Description_TC = Description_TC,
                Description_EN = Description_EN
            };

            RegisterLanguageUI(obj);

            if (!languageUIsDictionary.ContainsKey(id))
            {
                languageUIsDictionary[id] = obj;
            }else
            {
                throw new ApplicationException($"重复添加ID：{id}，请检查！");
            }

        }

        public void AddString(string id, string Description_SC, string Description_EN)
        {
            AddString(id, Description_SC, Description_SC.ToTraditional(), Description_EN);
        }


        public string GetString(string id)
        {
            if (languageUIsDictionary.ContainsKey(id))
            {
                return languageUIsDictionary[id].ShowText;
            }else
            {
                return "";
            }
        }


        public void SetSimplifiedChinese()
        {
            foreach (var item in languageUIs)
            {
                item.ShowText= item.Description_SC;
                if (item.textBlock != null)
                {
                    item.textBlock.Text = item.Description_SC;
                }
               
            }
        }

        public void SetEnglish()
        {
            foreach (var item in languageUIs)
            {
                item.ShowText = item.Description_EN;
                if (item.textBlock != null)
                {
                    item.textBlock.Text = item.Description_EN;
                }
            }
        }
        public void SetTraditionalChinese()
        {
            foreach (var item in languageUIs)
            {
                item.ShowText = item.Description_TC;
                if (item.textBlock != null)
                {
                    item.textBlock.Text = item.Description_TC;
                }
            }
        }
    }
}
