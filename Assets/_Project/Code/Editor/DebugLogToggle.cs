using UnityEditor;
using UnityEngine;
using System.Linq;
using UnityEditor.Build;

namespace Trivainia
{
    public static class DebugLogToggle
    {
        private const string DEFINE_SYMBOL = "ENABLE_LOGS";
        private const string MENU_PATH = "Tools/Enable Debug Logs";

        private static readonly NamedBuildTarget _buildTarget = NamedBuildTarget.FromBuildTargetGroup(EditorUserBuildSettings.selectedBuildTargetGroup);

        [MenuItem(MENU_PATH)]
        private static void ToggleDefine()
        {
            var defineList = GetCurrentDefines();

            if (HasDefine(defineList))
            {
                SetDefines(defineList.Where(d => d != DEFINE_SYMBOL).ToArray());
                Debug.Log("❌ ENABLE_LOGS removed. Logs will not be shown in the console. Recompile incoming..");
            }
            else
            {
                SetDefines(defineList.Append(DEFINE_SYMBOL).ToArray());
                Debug.Log("✅ ENABLE_LOGS added. Logs will be shown in the console. Recompile incoming..");
            }
        }

        [MenuItem(MENU_PATH, true)]
        private static bool ToggleDefineValidate()
        {
            Menu.SetChecked(MENU_PATH, HasDefine(GetCurrentDefines()));

            return true;
        }

        private static string[] GetCurrentDefines()
        {
            var defines = PlayerSettings.GetScriptingDefineSymbols(_buildTarget);

            return defines.Split(';').Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
        }

        private static void SetDefines(string[] defines) => PlayerSettings.SetScriptingDefineSymbols(_buildTarget, string.Join(";", defines.Distinct()));

        private static bool HasDefine(string[] defineList) => defineList.Contains(DEFINE_SYMBOL);
    }
}