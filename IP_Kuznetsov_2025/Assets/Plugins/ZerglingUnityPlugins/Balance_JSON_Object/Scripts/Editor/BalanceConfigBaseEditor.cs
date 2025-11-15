using UnityEngine;
using ZerglingUnityPlugins.Balance_JSON_Object.Scripts.Configs;




#if UNITY_EDITOR
using UnityEditor;

namespace ZerglingUnityPlugins.Balance_JSON_Object.Scripts.Editor
{
    [CustomEditor(typeof(BalanceConfigBase), true)]
    public class BalanceConfigBaseEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("PARSE GOOGLE SHEET"))
                ParseBalance();
        }

        private void ParseBalance()
        {
            var config = (IBalanceConfig)target;
            config.ParseBalance();
        }
    }
}
#endif
