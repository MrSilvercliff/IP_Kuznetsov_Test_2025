using Defective.JSON;
using UnityEngine;
using ZerglingUnityPlugins.Balance_JSON_Object.Scripts.JSONParse;

namespace _Project.Scripts.Application.Project.Services.Balance
{
    public class JsonParseHelper : JSONParseHelperBase
    {
        protected override T ParseItem<T>(JSONObject itemJson)
        {
            return default(T);
        }
    }
}
