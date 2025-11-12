using _Project.Scripts.Project.Services.ServiceInit;
using System.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.GameScene.Scene
{
    public class GameSceneServiceIniter : ServiceIniter
    {
        protected override Task<bool> OnInit()
        {
            return Task.FromResult(true);
        }

        public override async Task<bool> InitServices(int stage)
        {
            var result = true;
            return result;
        }
    }
}