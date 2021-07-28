using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCheatUITemplate
{
    class UILangerManger
    {
        List<LanguageUI> languageUIs = new List<LanguageUI>();
        public void RegisterLanguageUI(LanguageUI languageUI)
        {
            languageUIs.Add(languageUI);
        }

        public void SetSimplifiedChinese()
        {
            foreach (var item in languageUIs)
            {
                item.textBlock.Text = item.Description_SC;
            }
        }

        public void SetEnglish()
        {
            foreach (var item in languageUIs)
            {
                item.textBlock.Text = item.Description_EN;
            }
        }
        public void SetTraditionalChinese()
        {
            foreach (var item in languageUIs)
            {
                item.textBlock.Text = item.Description_TC;
            }
        }
    }
}
