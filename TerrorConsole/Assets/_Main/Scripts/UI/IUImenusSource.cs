using UnityEngine;

namespace TerrorConsole
{
    public interface IUImenusSource
    {
        void OpenNewMenuOnTop(UIController newTopMenu);
        void CloseMenuOnTop();

        void CloseMenu(UIController menuToClose);

        int GetOpenedMenusCount();

        void ResetMenuStack();
    }
}
