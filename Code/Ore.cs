using System;
using System.Runtime.CompilerServices;
using Sandbox;

public sealed class Ore : Component, Component.IDamageable
{
	[Property] Prop OreProp {get; set;}
	[Property] GameObject OreDrop {get; set;}
	[Property] SoundEvent OreBreakSound {get; set;}

	float _maxPropHealth = 0f;

	protected override void OnAwake()
	{
		_maxPropHealth = OreProp.Health;
	}

	public void OnDamage( in DamageInfo damageInfo )
	{
		OreProp.Health -= damageInfo.Damage;
		if (OreProp.Health < 0)
		{
			SpawnDrops();
			SpawnDrops();
			SpawnDrops();
			SpawnDrops();
			SpawnDrops();
			Sound.Play(OreBreakSound, WorldPosition);
			GameObject.Destroy();
		}
	}

	public void UpdatePropHealth(DamageInfo damageInfo)
	{
		OreProp.Health -= damageInfo.Damage;
	}

	public void SpawnDrops()
	{
		OreDrop.Clone();
		OreDrop.WorldPosition = LocalPosition;
		OreDrop.WorldRotation = Rotation.Random;
		OreDrop.GetComponentInChildren<Rigidbody>().ApplyImpulse(WorldTransform.Forward.RotateAround(Vector3.Up, Rotation.Random) * 10f);
	}
}
