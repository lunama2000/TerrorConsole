using UnityEngine;

namespace TerrorConsole
{
    public class ShadowAnimationActivator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private GameObject _shadow;
        [SerializeField] private bool _shouldRepeat;

        private bool _triggered = false;
        
        public void TriggerAnimation()
        {
            if (!_shouldRepeat && _triggered) return;
            
            _animator.Play("Start");
            _triggered = true;
        }

        public void OnAnimationEnd()
        {
            _shadow.gameObject.SetActive(false);
        }
    }
}
