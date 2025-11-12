using _Project.Scripts.Project.Scenes;
using System.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.GameScene.Scene
{
    public class GameSceneController : SceneController
    {
        protected override Task OnAwake()
        {
            return Task.CompletedTask;
        }

        protected override Task OnStart()
        {
            return Task.CompletedTask;
        }

        protected override Task OnLateStart()
        {
            return Task.CompletedTask;
        }

        protected override void OnFlush()
        {
        }
    }
}