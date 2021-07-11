using System;
using UnityEngine;

[Serializable]
public sealed class PlayerModel
{
	[SerializeField]
	internal float Speed;

	[SerializeField]
	internal float MaxHealth;

	internal float Health;

	internal void InitModel() =>
		Health = MaxHealth;
}
