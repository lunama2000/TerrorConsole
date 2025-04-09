using System;
using UnityEngine;

namespace TerrorConsole
{
    public class WInScript : MonoBehaviour
    {
        [SerializeField] private GameObject WinUI;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                WinUI.SetActive(true);
            }
        }
    }
}
