using Cysharp.Threading.Tasks;
using UnityEngine;

namespace TerrorConsole
{
    public class PlayerTeleporterToSpawn : MonoBehaviour
    {
        [SerializeField] private Transform spawnPoint;

        private void OnEnable()
        {
            LevelManager.Source.OnPlayerCaptured.AddListener(PlayerCaptured);
        }

        private void OnDisable()
        {
            LevelManager.Source.OnPlayerCaptured.RemoveListener(PlayerCaptured);
        }

        private void PlayerCaptured()
        {
            StartTeleportToSpawn().Forget();
        }

        private async UniTaskVoid StartTeleportToSpawn()
        {
            ScreenTransitionManager.Source.Transition(
                () =>
                {
                    CameraSystemManager.Source.TeleportPlayerWithCameraReset(transform, spawnPoint.position);
                },
                TransitionType.Slide);
        }
    }
}