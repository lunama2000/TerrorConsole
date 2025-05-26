using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TerrorConsole
{
    public class UIMenusManager : Singleton<IUImenusSource>, IUImenusSource
    {
        private Stack<UIController> menuStack = new Stack<UIController>();

        protected override void Awake()
        {
            base.Awake();

            Cursor.visible = false;
        }

        public void CloseMenu(UIController menuToClose)
        {
            if (menuStack.Count <= 0)
                return;

            while (menuStack.Peek() != menuToClose)
            {
                CloseMenuOnTop();
            }
            CloseMenuOnTop();
        }

        public void CloseMenuOnTop()
        {
            if (menuStack.Count == 0) return;
            menuStack.Pop().gameObject.SetActive(false);
            if (menuStack.Count > 0)
            {
                SetMenuInteractable(menuStack.Peek().GetCanvasGroup(),true);
                EventSystem.current.SetSelectedGameObject(menuStack.Peek().GetDefaultSelectedUI());
            }
        }

        public int GetOpenedMenusCount()
        {
            return menuStack.Count;
        }

        public void OpenNewMenuOnTop(UIController newTopMenu)
        {
            if (menuStack.Count > 0)
            {
                SetMenuInteractable(menuStack.Peek().GetCanvasGroup(), false);
            }
            menuStack.Push(newTopMenu);
            newTopMenu.gameObject.SetActive(true);
            newTopMenu.transform.root.gameObject.SetActive(false);
            newTopMenu.transform.root.gameObject.SetActive(true);
            SetMenuInteractable(menuStack.Peek().GetCanvasGroup(), true);
            EventSystem.current.SetSelectedGameObject(newTopMenu.GetDefaultSelectedUI());
        }

        public void ResetMenuStack()
        {
            menuStack.Clear();
        }

        private void SetMenuInteractable(CanvasGroup menu, bool value)
        {
            menu.interactable = value;
            menu.blocksRaycasts = value;
        }
    }
}
