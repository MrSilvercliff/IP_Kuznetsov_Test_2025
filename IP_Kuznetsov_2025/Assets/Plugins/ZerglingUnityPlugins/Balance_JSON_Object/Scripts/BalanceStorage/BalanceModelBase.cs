using System;
using Defective.JSON;
using UnityEngine;
using ZerglingUnityPlugins.Balance_JSON_Object.Scripts.JSONParse;
using ZerglingUnityPlugins.Tools.Scripts.Log;

namespace ZerglingUnityPlugins.Balance_JSON_Object.Scripts.BalanceStorage
{
    public interface IBalanceModelBase
    {
        void DebugPrint();

        bool TrySetup(JSONObject json, IJSONParseHelper parseHelper);
    }

    public abstract class BalanceModelBase : IBalanceModelBase
    {
        public abstract void DebugPrint();

        public bool TrySetup(JSONObject json, IJSONParseHelper parseHelper)
        {
            var result = true;

            try
            {
                OnTrySetup(json, parseHelper);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
                Debug.LogError(ex.StackTrace);
                result = false;
            }

            return result;
        }

        protected abstract void OnTrySetup(JSONObject json, IJSONParseHelper parseHelper);
    }
}
