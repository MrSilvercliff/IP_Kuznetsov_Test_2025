using _Project.Scripts.Project.Enums;
using Defective.JSON;
using System.Text;
using UnityEngine;
using ZerglingUnityPlugins.Balance_JSON_Object.Scripts.BalanceStorage;
using ZerglingUnityPlugins.Balance_JSON_Object.Scripts.JSONParse;

namespace _Project.Scripts.Project.Services.Balance.Models
{
    public interface IGameItemBalanceModel : IBalanceModelWithIdBase
    {
        GameItemType ItemType { get; }
        string IconId { get; }

        bool IsStackable { get; }
        int StackSize { get; }

        string Name { get; }
        string Description { get; }
    }

    public class GameItemBalanceModel : BalanceModelWithIdBase, IGameItemBalanceModel
    {
        public GameItemType ItemType { get; private set; }
        public string IconId { get; private set; }

        public bool IsStackable { get; private set; }
        public int StackSize { get; private set; }

        public string Name { get; private set; }
        public string Description { get; private set; }

        protected override void OnTrySetup(JSONObject json, IJSONParseHelper parseHelper)
        {
            _id = json["id"].stringValue;
            ItemType = parseHelper.ParseEnum(json, "item_type", GameItemType.NONE);
            IconId = json["icon_id"].stringValue;
            IsStackable = json["stackable"].boolValue;
            StackSize = json["stack_size"].intValue;
            Name = json["name"].stringValue;
            Description = json["description"].stringValue;
        }

        public override void DebugPrint()
        {
            var builder = new StringBuilder();
            builder.AppendLine($"Id = {Id}");
            builder.AppendLine($"ItemType = {ItemType}");
            builder.AppendLine($"IconId = {IconId}");
            builder.AppendLine($"IsStackable = {IsStackable}");
            builder.AppendLine($"StackSize = {StackSize}");
            builder.AppendLine($"Name = {Name}");
            builder.AppendLine($"Description = {Description}");
        }
    }
}