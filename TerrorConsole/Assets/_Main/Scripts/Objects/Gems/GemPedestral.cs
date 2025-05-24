using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;

namespace TerrorConsole
{
    public enum GemColors
    {
        None,
        Red,
        Blue,
        Yellow,
        Green,
        Purple,
        Orange
    }
    public class GemPedestral : MonoBehaviour
    {
        [SerializeField ] private GemColors _pedestralColor;
        [SerializeField] private Tilemap _floorPath;
        [SerializeField] private List<GemInfo> _validGems = new List<GemInfo>();
        private GemInfo _currentGem;
        private int _currentGemIndex;
        private GemColors _currentPathColor;
        [SerializeField] LightPaths _lightPath;
        [SerializeField] SpriteRenderer _currentGemSprite;
        [SerializeField] Light2D _currentGemLight;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                InputManager.Source.OnActivateButton1 += OnPlayerInput;
                TooltipsManager.Source.ShowSpriteTooltip(InputActionsInGame.Button1, "Place Gem", (Vector2)collision.transform.position + Vector2.up);
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                InputManager.Source.OnActivateButton1 -= OnPlayerInput;
                TooltipsManager.Source.HideSpriteTooltip("Place Gem");
            }
        }

        private void OnPlayerInput()
        {
            for (int i = _currentGemIndex; i < _validGems.Count; i++)
            {
                if (Inventory.Source.IsItemInInventory(_validGems[i]))
                {
                    if (_currentGem)
                    {
                        Inventory.Source.AddItemToInventory(_currentGem);
                    }
                    PlaceNewGem(_validGems[i]);
                    Inventory.Source.RemoveItemFromInventory(_validGems[i]);
                    _currentGemIndex = i+1;
                    return;
                }
            }
            _currentGemIndex = 0;
            RemoveGem();
        }

        private void PlaceNewGem(GemInfo newGem)
        {
            _currentGem = newGem;
            _floorPath.color = GetNewPathColor();
            _currentGemSprite.sprite = _currentGem.ItemSprite;
            _currentGemLight.color = _floorPath.color;
            _currentGemSprite.gameObject.SetActive(true);

            switch (_lightPath)
            {
                case LightPaths.A:
                    GemColorPuzzle.Source.UpdateColorA(_currentPathColor);
                    break;
                case LightPaths.B:
                    GemColorPuzzle.Source.UpdateColorB(_currentPathColor);
                    break;
                case LightPaths.C:
                    GemColorPuzzle.Source.UpdateColorC(_currentPathColor);
                    break;
            }
        }

        private void RemoveGem()
        {
            if (_currentGem)
            {
                Inventory.Source.AddItemToInventory(_currentGem);
            }
            _currentGem = null;
            _currentPathColor = GemColors.None;
            _floorPath.color = new Color(1,1,1,0.5f);
            _currentGemSprite.gameObject.SetActive(false);

            switch (_lightPath)
            {
                case LightPaths.A:
                    GemColorPuzzle.Source.UpdateColorA(_currentPathColor);
                    break;
                case LightPaths.B:
                    GemColorPuzzle.Source.UpdateColorB(_currentPathColor);
                    break;
                case LightPaths.C:
                    GemColorPuzzle.Source.UpdateColorC(_currentPathColor);
                    break;
            }
        }

        private Color GetNewPathColor()
        {
            Color pathColor = Color.white; // Valor por defecto
            switch (_currentGem.GemColor)
            {
                case GemColors.Red:
                    switch (_pedestralColor)
                    {
                        case GemColors.Red:
                            pathColor = Color.red;
                            _currentPathColor = GemColors.Red;
                            break;
                        case GemColors.Blue:
                            pathColor = Color.magenta;
                            _currentPathColor = GemColors.Purple;
                            break;
                        case GemColors.Yellow:
                            pathColor = new Color(1f, 0.5f, 0f); // naranja
                            _currentPathColor = GemColors.Orange;
                            break;
                    }
                    break;

                case GemColors.Blue:
                    switch (_pedestralColor)
                    {
                        case GemColors.Red:
                            pathColor = Color.magenta;
                            _currentPathColor = GemColors.Purple;
                            break;
                        case GemColors.Blue:
                            pathColor = Color.blue;
                            _currentPathColor = GemColors.Blue;
                            break;
                        case GemColors.Yellow:
                            pathColor = Color.green;
                            _currentPathColor = GemColors.Green;
                            break;
                    }
                    break;

                case GemColors.Yellow:
                    switch (_pedestralColor)
                    {
                        case GemColors.Red:
                            pathColor = new Color(1f, 0.5f, 0f); // naranja
                            _currentPathColor = GemColors.Orange;
                            break;
                        case GemColors.Blue:
                            pathColor = Color.green;
                            _currentPathColor = GemColors.Green;
                            break;
                        case GemColors.Yellow:
                            pathColor = Color.yellow;
                            _currentPathColor = GemColors.Yellow;
                            break;
                    }
                    break;
            }
            return pathColor;
        }
    }
}
