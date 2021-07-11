using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class EnemyService : MonoBehaviour
{
	[SerializeField]
	private ObjectPooling _explosionEffectPool = default;

	[SerializeField]
	private ObjectPooling _bossEnemyObjectPool = default;

	[SerializeField]
	private ObjectPooling _regulerEnemyObjectPool = default;

	[SerializeField]
	private List<BossEnemy> _defaultBossEnemyCollection = default;

	[SerializeField]
	private List<RegulerEnemy> _defaultRegulerEnemyCollection = default;

	public void Init(Func<Vector3> getPlayerPosition)
    {
		InitObjectPoolDefaultValue();
		InitDefaultEnemyCollectionEvent(getPlayerPosition);

		//Pool event
		InitExplosionPoolEvent();
		InitRegulerEnemyPoolEvent(); 
		InitBossEnemyPoolEvent(getPlayerPosition);
	}

	private void InitDefaultEnemyCollectionEvent(Func<Vector3> getPlayerPosition)
	{
		foreach (var item in _defaultBossEnemyCollection)
        {
			InitBossEnemyEvent(item);
			item.InitCallback(getPlayerPosition);
		}
			
		foreach (var item in _defaultRegulerEnemyCollection)
			InitRegulerEnemyEvent(item);
	}

	private void InitObjectPoolDefaultValue()
	{
		_bossEnemyObjectPool.InitDefaultValue(_defaultBossEnemyCollection.Select((item) => item.gameObject).ToArray());
		_regulerEnemyObjectPool.InitDefaultValue(_defaultRegulerEnemyCollection.Select((item) => item.gameObject).ToArray());
	}

	#region Pool Event
	private void InitExplosionPoolEvent()
    {
		Action<GameObject> activeExplosionCallback = (explosionObj) => 
			_explosionEffectPool.DeactiveObject(explosionObj);

		_explosionEffectPool.OnObjectCreated += activeExplosionCallback;
		_explosionEffectPool.OnObjectReactivated += activeExplosionCallback;
    }

	private void InitBossEnemyPoolEvent(Func<Vector3> getPlayerPosition)
    {
		_bossEnemyObjectPool.OnObjectCreated += (bossEnemyObj) =>
		{
			var bossEnemy = bossEnemyObj.GetComponent<BossEnemy>();
			bossEnemy.InitCallback(getPlayerPosition);
			InitBossEnemyEvent(bossEnemy);
		};
	}

	private void InitRegulerEnemyPoolEvent()
    {
		_regulerEnemyObjectPool.OnObjectCreated += (regulerEnemyObj) =>
			InitRegulerEnemyEvent(regulerEnemyObj.GetComponent<RegulerEnemy>());
	}
    #endregion

    #region Enemy Event
    private void InitRegulerEnemyEvent(RegulerEnemy regulerEnemy)
    {
		regulerEnemy.OnEnemyDead += (enemyPosition) =>
			_explosionEffectPool.ActiveObject(enemyPosition);

		regulerEnemy.OnEnemyOutOfRange += (enemyObj) =>
			_bossEnemyObjectPool.DeactiveObject(enemyObj);
	}

	private void InitBossEnemyEvent(BossEnemy bossEnemy)
    {
		bossEnemy.OnEnemyDead += (enemyPosition) =>
			_explosionEffectPool.ActiveObject(enemyPosition);

		bossEnemy.OnEnemyOutOfRange += (enemyObj) =>
			_regulerEnemyObjectPool.DeactiveObject(enemyObj);
	}
    #endregion
}