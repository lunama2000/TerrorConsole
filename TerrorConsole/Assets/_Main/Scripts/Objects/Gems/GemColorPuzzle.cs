using System.Collections.Generic;
using UnityEngine;

namespace TerrorConsole
{
    public enum LightPaths
    {
        A,
        B,
        C
    }
    public class GemColorPuzzle : Singleton<IGemsColorPuzzleSource>, IGemsColorPuzzleSource
    {
        [SerializeField] private Door _doorToOpen;
        [SerializeField] private List<GemColors> _correctColors = new List<GemColors>(3);
        private List<GemColors> _currentColors = new List<GemColors>(3);

        protected override void Awake()
        {
            base.Awake();

            for (int i = 0; i < _correctColors.Count; i++)
            {
                _currentColors.Add(GemColors.None);
            }
        }
        private void Start()
        {
            if (_correctColors.Count != 3)
            {
                Debug.LogError("The 'Correct Colors' list need to have 3 items");
            }
        }

        public void UpdateColorA(GemColors newColor)
        {
            _currentColors[0] = newColor;
            CheckToOpenDoor();
        }

        public void UpdateColorB(GemColors newColor)
        {
            _currentColors[1] = newColor;
            CheckToOpenDoor();
        }

        public void UpdateColorC(GemColors newColor)
        {
            _currentColors[2] = newColor;
            CheckToOpenDoor();
        }

        void CheckToOpenDoor()
        {
            if (CheckIfCompleted())
            {
                _doorToOpen.OpenDoor();
            }
        }

        bool CheckIfCompleted()
        {
            for(int i = 0; i < _currentColors.Count; i++)
            {
                if(_correctColors[i] != _currentColors[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
