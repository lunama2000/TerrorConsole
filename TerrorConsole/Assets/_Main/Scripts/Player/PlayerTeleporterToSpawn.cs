using UnityEngine;

namespace TerrorConsole
{
    public class PlayerTeleporterToSpawn : MonoBehaviour
    {
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private TransitionType transitionType = TransitionType.Slide;

        public void StartTeleport()
        {
            ScreenTransitionManager.Source.Transition(() =>
            {
                transform.position = spawnPoint.position;
            }, transitionType);
        }
    }
}
