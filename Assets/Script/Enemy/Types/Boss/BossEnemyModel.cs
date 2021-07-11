using System;
using UnityEngine;

[Serializable]
internal sealed class BossEnemyModel
{
	[SerializeField]
	internal float Speed;

	[SerializeField]
	internal float MaxShield;

	internal float Shield;

	internal void InitModel() =>
		Shield = MaxShield;
}
