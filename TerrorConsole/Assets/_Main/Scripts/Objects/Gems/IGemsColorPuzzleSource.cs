using UnityEngine;

namespace TerrorConsole
{
    public interface IGemsColorPuzzleSource
    {
        public void UpdateColorA(GemColors newColor);
        public void UpdateColorB(GemColors newColor);
        public void UpdateColorC(GemColors newColor);
    }
}
