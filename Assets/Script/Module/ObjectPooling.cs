using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public sealed class ObjectPooling
{
    [SerializeField]
    private GameObject _objectPerfab = default;

    [SerializeField]
    private uint _maxAmmountOfObject = default;

    private ICollection<GameObject> _objectCollection = new List<GameObject>();

    public event Action<GameObject> OnObjectCreated = default;

    public event Action<GameObject> OnObjectReactivated = default;

    public void InitDefaultValue(ICollection<GameObject> objectCollection) =>
        _objectCollection = objectCollection;

    public ICollection<GameObject> GetObjectCollection() =>
        _objectCollection;

    public void ActiveObject(Vector3 spawnPosition) 
    {
        if (_objectCollection.Count < _maxAmmountOfObject)
        {
            var newObj = GameObject.Instantiate(_objectPerfab.gameObject, spawnPosition, Quaternion.identity);
            _objectCollection.Add(newObj);
            OnObjectCreated?.Invoke(newObj);
        }
        else
        {
            var obj = _objectCollection.First((item) => item.activeInHierarchy is false);
            obj.SetActive(true);
            obj.transform.position = spawnPosition;
            OnObjectReactivated?.Invoke(obj);
        }
    }

    public void DeactiveObject(GameObject gameobj) =>
        gameobj.SetActive(false);
}
