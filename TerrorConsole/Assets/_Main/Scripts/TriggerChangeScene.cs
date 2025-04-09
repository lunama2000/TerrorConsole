using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TerrorConsole
{
    public class TriggerChangeScene : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            ScreenTransitionManager.Source.TransitionToScene("TestLoseWin");
        }
    }
}
