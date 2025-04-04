using UnityEngine;

namespace TerrorConsole
{
    public class EquipableItem : MonoBehaviour
    {
        [SerializeField] protected bool _freezeInput;

        protected IInputSource _inputSource;

        protected virtual void Start()
        {
            _inputSource = InputManager.Source;
            LevelManager.Source.OnLevelStateChange += OnLevelStateChange;
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
