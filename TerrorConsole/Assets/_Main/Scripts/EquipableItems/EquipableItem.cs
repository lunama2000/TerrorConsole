using UnityEngine;

namespace TerrorConsole
{
    public class EquipableItem : MonoBehaviour
    {
        [SerializeField] protected bool _freezeInput;
        [SerializeField] private string _objectName;

        protected IInputSource _inputSource;

        public string GetObjectName()
        {
            return _objectName;
        }

        protected virtual void Start()
        {
            _inputSource = InputManager.Source;
            LevelManager.Source.OnLevelStateChange += OnLevelStateChange;
            if (string.IsNullOrEmpty(_objectName))
            {
                Debug.LogError($"There is no Object Name for {name}, please set the name of the corresponding Pickable Item for this equipable");
                _objectName = _objectName == "" ? transform.name : _objectName;
            }
        }

        protected virtual void OnDestroy()
        {
            LevelManager.Source.OnLevelStateChange -= OnLevelStateChange;
        }
        protected virtual void OnLevelStateChange(LevelState newState)
        {
            switch (newState)
            {
                case LevelState.InDialogue:
                case LevelState.Pause:
                case LevelState.Cinematic:
                    StopInput();
                    break;

                case LevelState.Play:
                    ResumeInput();
                    break;
            }
        }

        protected virtual void StopInput()
        {
            _freezeInput = true;
        }

        protected virtual void ResumeInput()
        {
            _freezeInput = false;
        }
    }
}
