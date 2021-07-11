using System;
using UnityEngine;

public sealed class Bullet : MonoBehaviour
{
	public event Action<GameObject> OnCollisionWithOtherBullet; 

	private void OnTriggerEnter(Collider collider)
	{
		if (collider.CompareTag(GameManager.GetInstance().TagConfig.Bullet))
			OnCollisionWithOtherBullet.Invoke(collider.gameObject);
	}
}