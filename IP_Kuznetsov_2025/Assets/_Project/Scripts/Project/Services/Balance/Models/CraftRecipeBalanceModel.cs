using Defective.JSON;
using System.Collections.Generic;
using System.Text;
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
            _id = json["id"].stringValue;
            CraftRecipeItemsId = parseHelper.ParseList<string>(json, "recipe_game_items_id");
            CraftRecipeItemsCount = parseHelper.ParseList<int>(json, "recipe_game_items_count");
            ResultGameItemId = json["result_game_item_id"].stringValue;
            ResultGameItemCount = json["result_game_item_count"].intValue;
        }

        public override void DebugPrint()
        {
            var builder = new StringBuilder();
            builder.AppendLine($"_id = {_id}");
            builder.AppendLine($"ResultGameItemId = {ResultGameItemId}");
            builder.AppendLine($"ResultGameItemCount = {ResultGameItemCount}");

            var addBuilder = new StringBuilder();
            for (int i = 0; i < CraftRecipeItemsId.Count; i++)
                addBuilder.AppendLine($"CraftRecipeItemsId[{i}] : [{CraftRecipeItemsId[i]}]");

            builder.AppendLine($"CraftRecipeItemsId:");
            builder.AppendLine(addBuilder.ToString());

            addBuilder.Clear();

            for (int i = 0; i < CraftRecipeItemsCount.Count; i++)
                addBuilder.AppendLine($"CraftRecipeItemsCount[{i}] : [{CraftRecipeItemsCount[i]}]");

            builder.AppendLine($"CraftRecipeItemsCount:");
            builder.AppendLine(addBuilder.ToString());

            var result = builder.ToString();
            Debug.LogError(result);
        }
    }
}