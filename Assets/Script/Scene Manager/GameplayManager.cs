using System;
using UnityEngine;
using System.Collections.Generic;

public sealed class GameplayManager : MonoBehaviour
{
    [SerializeField]
    private InputService _inputService = default;

    [SerializeField]
    private EnemyService _enemyService = default;

    [SerializeField]
    private PlayerService _playerService = default;

    [SerializeField]
    private BulletService _bulletService = default;

    [SerializeField]
    private CameraService _cameraService = default;

    private void Awake()
    {
        _bulletService.Init();
        _enemyService.Init(_playerService.GetMainPlayerPosition);
        _cameraService.Init(_playerService.GetMainPlayerPosition);
        _inputService.Init(new Dictionary<KeyCode, Action>
        {
            { KeyCode.Space, _bulletService.DeactiveAllEnemyBullet },
            { KeyCode.A, () => _bulletService.ActiveBullet(_playerService.GetMainPlayerPosition()) }
        });
    }
}
