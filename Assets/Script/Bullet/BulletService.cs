using System;
using UnityEngine;

public class BulletService : MonoBehaviour
{
    [SerializeField]
    private ObjectPooling _bulletObjectPool = default;

    public void Init() =>
        InitBulletPoolEvent();

    public void ActiveBullet(Vector3 playerPosition) =>
        _bulletObjectPool.ActiveObject(playerPosition);

    public void DeactiveAllEnemyBullet()
    {
        foreach (var item in _bulletObjectPool.GetObjectCollection())
        {
            if (item.CompareTag(GameManager.GetInstance().TagConfig.BulletEnemy))
                _bulletObjectPool.DeactiveObject(item);
        }
    }

    private void InitBulletPoolEvent()
    {
        Action<GameObject> activeBulletCallback = (bulletObj) =>
        {
            bulletObj.transform.localScale = new Vector3(0, 0, 0);
            bulletObj.GetComponent<Bullet>().OnCollisionWithOtherBullet += (collisionBulletObj) =>
                _bulletObjectPool.DeactiveObject(collisionBulletObj);
        };

        _bulletObjectPool.OnObjectCreated += activeBulletCallback;
        _bulletObjectPool.OnObjectReactivated += activeBulletCallback;
    }
}
