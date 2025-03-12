using UnityEngine;

namespace TerrorConsole
{
    public class SwitchButton : SwitchObject
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                On();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Off();
            }
        }
    }
}
