using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TerrorConsole
{
    public class OrderLeverController : MonoBehaviour
    {
        [SerializeField] private List<SwitchObject> levers;

        [SerializeField] private List<string> _correctOrder;
        private List<string> _currentOrder = new List<string>();
        
        [SerializeField] private int leverCountToActivate = 3;
        [SerializeField] private bool _allowAnyOrder = false;
        
        [SerializeField] public UnityEvent OnOrderSuccessful;
        [SerializeField] public UnityEvent OnOrderUnsuccesful;
        
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
            CheckForCorrectOrder();
        }

        private void OnLeverDeactivated(string leverId)
        {
            _currentOrder.Remove(leverId);
            CheckForCorrectOrder();
        }

        private void CheckForCorrectOrder()
        {
            if (_allowAnyOrder)
            {
                if (_currentOrder.Count >= leverCountToActivate)
                {
                    Debug.Log("Order without any correct order");
                    OnOrderSuccessful?.Invoke();
                }
                return;
            }
            
            if (_currentOrder.Count != _correctOrder.Count)
                return;

            for (int i = 0; i < _correctOrder.Count; i++)
            {
                if (_currentOrder[i] != _correctOrder[i])
                {
                    OnOrderUnsuccesful?.Invoke();
                    ResetAllLevers();
                    return;
                }
            }

            OnOrderSuccessful?.Invoke();
        }

        private void ResetAllLevers()
        {
            _currentOrder.Clear();

            foreach (var lever in levers)
            {
                lever.ResetLever();
            }
        }
        
    }
}
