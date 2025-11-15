using UnityEngine;

namespace _Project.Scripts.GameScene.Configs
{
    public interface IDragAndDropConfig
    { 
        float DragThreshold { get; }
        float DragThresholdSquared { get; }
    }

    [CreateAssetMenu(fileName = "DragAndDropConfig", menuName = "Project/Configs/Game Scene/Drag And Drop Config")]
    public class DragAndDropConfig : ScriptableObject, IDragAndDropConfig
    {
        public float DragThreshold => _dragThreshold;

        public float DragThresholdSquared
        {
            get
            {
                if (_dragThresholdSquared < 0f)
                    _dragThresholdSquared = _dragThreshold * _dragThreshold;

                return _dragThresholdSquared;
            }
        }

        [SerializeField] private float _dragThreshold;

        private float _dragThresholdSquared = float.MinValue;
    }
}