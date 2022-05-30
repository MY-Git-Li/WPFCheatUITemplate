using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WPFCheatUITemplate.Core.GameFuns;

namespace WPFCheatUITemplate.Core.UI
{
    /// <summary>
    /// 创建UI——ToolTip提示
    /// </summary>
    static class ToolTipUI
    {
        static List<LanguageUI> toolTiplanguageUIs = new List<LanguageUI>();

        static Dictionary<LanguageUI, string> toolTiplanguageUI_map = new Dictionary<LanguageUI,string>();

        public static void RegisterToolTipLanguageUI(LanguageUI toolTiplanguageUI)
        {
            toolTiplanguageUIs.Add(toolTiplanguageUI);
        }


        public static void RegisterToolTipLanguageUI(LanguageUI toolTiplanguageUI,string id)
        {
            if(id == null)
            {
                RegisterToolTipLanguageUI(toolTiplanguageUI);
            }
            else
            {
                toolTiplanguageUI_map[toolTiplanguageUI] = id;
            }
            
        }

        public static void SetToolTip(string Description_SC, string Description_TC, string Description_EN)
        {
            var uidata = UIManager.GetLastUIData();
            uidata.IsHaveToolTip = true;
            uidata.ToolTipDescription_TC = Description_TC;
            uidata.ToolTipDescription_SC = Description_SC;
            uidata.ToolTipDescription_EN = Description_EN;

        }


        public static void SetToolTipByID(string ID)
        {
            var uidata = UIManager.GetLastUIData();
            uidata.IsHaveToolTip = true;
            uidata.ToolTipStringID = ID;

        }

        private static void ChangeToolTiplanguageUI_map()
        {
            foreach (var item in toolTiplanguageUI_map.Keys)
            {
                var obj = AppGameFunManager.Instance.UILangerManger;
                (item.textBlock.ToolTip as ToolTip).Content = obj.GetString(toolTiplanguageUI_map[item]);
            }
        }


        public static void SetSimplifiedChinese()
        {
            foreach (var item in toolTiplanguageUIs)
            {
                item.ShowText = item.Description_SC;
                (item.textBlock.ToolTip as ToolTip).Content = item.Description_SC;
            }
            ChangeToolTiplanguageUI_map();
        }
        public static void SetEnglish()
        {
            foreach (var item in toolTiplanguageUIs)
            {
                item.ShowText = item.Description_EN;
                (item.textBlock.ToolTip as ToolTip).Content = item.Description_EN;
            }
            ChangeToolTiplanguageUI_map();
        }
        public static void SetTraditionalChinese()
        {
            foreach (var item in toolTiplanguageUIs)
            {
                item.ShowText = item.Description_TC;
                (item.textBlock.ToolTip as ToolTip).Content = item.Description_TC;
            }
            ChangeToolTiplanguageUI_map();
        }

    }
}
