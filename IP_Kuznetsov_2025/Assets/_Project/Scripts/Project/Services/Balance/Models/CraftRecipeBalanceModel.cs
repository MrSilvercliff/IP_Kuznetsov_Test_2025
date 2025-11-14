using Defective.JSON;
using System.Collections.Generic;
using UnityEngine;
using ZerglingUnityPlugins.Balance_JSON_Object.Scripts.BalanceStorage;
using ZerglingUnityPlugins.Balance_JSON_Object.Scripts.JSONParse;

namespace _Project.Scripts.Project.Services.Balance.Models
{
    public interface ICraftRecipeBalanceModel : IBalanceModelWithIdBase
    { 
        IReadOnlyList<string> CraftRecipeItemsId { get; }
        IReadOnlyList<int> CraftRecipeItemsCount { get; }
        string ResultGameItemId { get; }
        int ResultGameItemCount { get; }
    }

    public class CraftRecipeBalanceModel : BalanceModelWithIdBase, ICraftRecipeBalanceModel
    {
        public IReadOnlyList<string> CraftRecipeItemsId { get; private set; }
        public IReadOnlyList<int> CraftRecipeItemsCount { get; private set; }
        public string ResultGameItemId { get; private set; }
        public int ResultGameItemCount { get; private set; }

        protected override void OnTrySetup(JSONObject json, IJSONParseHelper parseHelper)
        {
            CraftRecipeItemsId = parseHelper.ParseList<string>(json, "recipe_game_items_id");
            CraftRecipeItemsCount = parseHelper.ParseList<int>(json, "recipe_game_items_count");
            ResultGameItemId = json["result_game_item_id"].stringValue;
            ResultGameItemCount = json["result_game_item_count"].intValue;
        }

        public override void DebugPrint()
        {
            
        }
    }
}