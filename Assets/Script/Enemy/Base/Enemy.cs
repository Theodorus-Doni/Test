using System;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
	[SerializeField]
	private EnemyModel _model;

	internal event Action<Vector3> OnEnemyDead;

	internal event Action<GameObject> OnEnemyOutOfRange;

	protected virtual void Awake() =>
		_model.InitModel();

	protected virtual void Update()
    {
		if (IsOutOfRange(transform.position))
			OnEnemyOutOfRange?.Invoke(gameObject);
	}

    private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag(GameManager.GetInstance().TagConfig.Player))
		{
			_model.Health -= _model.CollisionDamage;
			if (_model.Health <= 0)
				OnEnemyDead?.Invoke(transform.position);
		}
	}

	private bool IsOutOfRange(Vector3 position) =>
		position.x < 0 || position.x > _model.BoundaryLevel.x || position.y < 0 || position.y > _model.BoundaryLevel.y;
}
