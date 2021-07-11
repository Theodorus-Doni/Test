using System;
using UnityEngine;

[Serializable]
internal sealed class EnemyModel
{
    internal float Health;

    [SerializeField]
    internal float MaxHealth;

    [SerializeField]
    internal Vector2 BoundaryLevel;

    internal readonly float CollisionDamage = 10;

    internal void InitModel() =>
        Health = MaxHealth;
}
