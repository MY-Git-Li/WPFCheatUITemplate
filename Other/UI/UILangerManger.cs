using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCheatUITemplate.Other.UI
{
    class UILangerManger
    {
        List<LanguageUI> languageUIs = new List<LanguageUI>();
        public void RegisterLanguageUI(LanguageUI languageUI)
        {
            if (languageUI.ShowText == "")
            {
                languageUI.ShowText = languageUI.Description_SC;
            }
            languageUIs.Add(languageUI);
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
