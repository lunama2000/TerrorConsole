using Cysharp.Threading.Tasks;
using UnityEngine;

namespace TerrorConsole
{
    public class PlayerTeleporterToSpawn : MonoBehaviour
    {
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private CapturePlayer _enemy;
        [SerializeField] private float transitionDuration;

        private void OnEnable()
        {
            _enemy.OnplayerCaptured.AddListener(PlayerCaptured);
        }

        private void OnDisable()
        {
            _enemy.OnplayerCaptured.RemoveListener(PlayerCaptured);
        }

        public void PlayerCaptured()
        {
            StartTeleportToSpawn().Forget();
        }

        private async UniTaskVoid StartTeleportToSpawn()
        {
            ScreenTransitionManager.Source.Transition(
                () =>
                {
                    CameraSystemManager.Source.TeleportPlayerWithCameraReset(transform, spawnPoint.position);
                },TransitionType.Slide);
        }
    }
}
