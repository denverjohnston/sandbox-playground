using System;
using System.Runtime.CompilerServices;
using Sandbox;

public sealed class Harvestable : Component, Component.IDamageable
{
	[Property] GameObject TargetDrop {get; set;}
	[Property] SoundEvent BreakSound {get; set;}

	float _currentHealth = 30f;

	public void OnDamage( in DamageInfo damageInfo )
	{
		_currentHealth -= damageInfo.Damage;

		if (_currentHealth < 0)
		{
			Log.Info("Spawn drops");
			SpawnDrops();
			SpawnDrops();
			SpawnDrops();
			SpawnDrops();
			SpawnDrops();
			Sound.Play(BreakSound, WorldPosition);
			GameObject.Destroy();
		}
	}

	public void SpawnDrops()
	{
		TargetDrop.Clone();
		TargetDrop.WorldPosition = LocalPosition;
		TargetDrop.WorldRotation = Rotation.Random;
		TargetDrop.GetComponentInChildren<Rigidbody>().ApplyImpulse(WorldTransform.Forward.RotateAround(Vector3.Up, Rotation.Random) * 10f);
	}
}
