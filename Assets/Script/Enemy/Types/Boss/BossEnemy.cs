using System;
using UnityEngine;

public sealed class BossEnemy : Enemy
{
	[SerializeField]
	private BossEnemyModel _bossModel;

	private Func<Vector3> GetPlayerPosition;

	protected override void Awake()
	{
		base.Awake();
		_bossModel.InitModel();
	}

	protected override void Update()
	{
		base.Update();
		var position = transform.position;
		var playerPosition = GetPlayerPosition();
		var distance = Vector3.Distance(playerPosition, position);
		if (distance > 1)
		{
			var direction = playerPosition - position;
			direction.Normalize();
			transform.position += direction * _bossModel.Speed * Time.fixedDeltaTime;
		}
	}

	internal void InitCallback(Func<Vector3> getPlayerPosition) =>
		GetPlayerPosition = getPlayerPosition;
}
