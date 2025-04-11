using System.Collections.Generic;
using UnityEngine;

namespace TerrorConsole
{
    public class OrderLeverController : MonoBehaviour
    {
        [SerializeField] private List<SwitchObject> levers;
        
        [SerializeField] private List<string> _correctOrder;
        private List<string> _currentOrder = new List<string>();
        
        [SerializeField] private OrderSwitchLockedDoor _lockDoor;
            
        private void Start()
        {
            foreach (var lever in levers)
            {
                lever.OnActivated.AddListener(OnLeverActivated);
                lever.OnDeactivated.AddListener(OnLeverDeactivated);
            }
        }

        private void OnLeverActivated(string leverId)
        {
            _currentOrder.Add(leverId);
            Debug.Log($"OnLeverActivated: {leverId}");
            CheckForCorrectOrder();
        }

        private void OnLeverDeactivated(string leverId)
        {
            _currentOrder.Remove(leverId);
            Debug.Log($"Lever {leverId} deactivated");
            CheckForCorrectOrder();
        }

        private void CheckForCorrectOrder()
        {
            if (_currentOrder.Count != _correctOrder.Count)
                return;

            for (int i = 0; i < _correctOrder.Count; i++)
            {
                if (_currentOrder[i] != _correctOrder[i])
                {
                    Debug.Log("Wrong order:" + _currentOrder[i]);
                    return;
                }
            }
            Debug.Log("Correct order");
            _lockDoor.UnlockOrderPuzzleDoor();
        }
    }
}
