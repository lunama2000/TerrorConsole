using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace TerrorConsole
{
    public class BossfightPhaseThree : BossPhaseBase
    {
        [Header("Fase 3 Configuración")]
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private Transform _playerCenterPoint;
        [SerializeField] private GameObject _enemyToDisable;
        [Header("Ataque del Boss")]
        [SerializeField] private Transform[] _attackPoints;
        [SerializeField] private GameObject _bossAttackPrefab; 
        [SerializeField] private float _attackCooldown = 3f;
        [SerializeField] private int _damagePerHit = 10;

        private float _attackTimer;
        private bool _isPhaseActive;
        private float _phaseStartHealth;
        
        public UnityEvent onBossDefeated;
        [SerializeField] private string _victoryMusic;

        private void OnEnable()
        {
            LevelManager.Source.OnPlayerRespawn += OnPlayerRespawn;
        }

        private void OnDisable()
        {
            LevelManager.Source.OnPlayerRespawn -= OnPlayerRespawn;
        }

        private void OnPlayerRespawn()
        {
            BeginBossFightPhaseThree();
            
            LevelManager.Source.ChangeLevelState(LevelState.BossPhase);
        }
        
        private void Update()
        {
            if (!_isPhaseActive) return;
            
            if (LevelManager.Source.GetCurrentLevelState() != LevelState.BossPhase) return;

            _attackTimer += Time.deltaTime;

            if (_attackTimer >= _attackCooldown)
            {
                _attackTimer = 0f;
                SpawnAttack();
            }
        }

        public void BeginBossFightPhaseThree()
        {
            this.enabled = true;
            _phaseStartHealth = _healthSlider.value;

            StartPhase(useHealthUI: true, resetHealth: false, useTimer: false);

            if (_playerTransform && _playerCenterPoint)
            {
                _playerTransform.position = _playerCenterPoint.position;
            }
            
            if (_enemyToDisable != null)
            {
                _enemyToDisable.SetActive(false);
            }
            
            var lantern = FindObjectOfType<EquipableLantern>();
            if (lantern != null)
            {
                TooltipsManager.Source.StashCurrentUITooltips();
                lantern.TurnLight(true);
                lantern.ForceDisableInput();
            }
            
            LevelManager.Source.ChangeLevelState(LevelState.BossPhase);

            _attackTimer = 0f;
            _isPhaseActive = true;
        }

        private void SpawnAttack()
        {
            int index = Random.Range(0, _attackPoints.Length);
            Transform spawnPoint = _attackPoints[index];

            GameObject obj = Instantiate(_bossAttackPrefab, spawnPoint.position, Quaternion.identity);
            var attack = obj.GetComponent<BossAttackPhase3>();
            attack.Setup(this, _playerTransform);
        }

        public void RegisterLightHit()
        {
            ReceiveDamage(_damagePerHit);
        }

        protected override void OnBossDefeated()
        {
            _isPhaseActive = false;
            base.OnBossDefeated();
    
            Debug.Log("Boss Phase 3 terminado.");
            AudioManager.Source.PlayMusic(_victoryMusic);
            CameraSystemManager.Source.ShakeCamera(10f);
            onBossDefeated?.Invoke();
            var lantern = FindObjectOfType<EquipableLantern>();
            if (lantern != null)
            {
                lantern.ForceEnableInput();
            }
            TooltipsManager.Source.UnStashUITooltips();
            LevelManager.Source.ChangeLevelState(LevelState.Play);
        }
    }
}
