using Sandbox;

public sealed class PlayerHarvest : Component
{
	[Property] public string HarvestActionName { get; set; } = "Attack1";
	[Property] public SoundEvent HarvestSound {get; set;}

	private PlayerController _playerController;
	private SkinnedModelRenderer _modelRenderer;

	private bool StartedHarvest() => Input.Pressed(HarvestActionName);
	
	protected override void OnStart()
	{
		base.OnStart();

		_playerController = GetComponentInChildren<PlayerController>();
		if (_playerController == null)
        {
			Log.Error("Failed to get PlayerController");
			return;
        }

		_modelRenderer = GetComponentInChildren<SkinnedModelRenderer>();
		if (_modelRenderer == null)
        {
			Log.Error("Failed to get SkinnedModelRenderer");
			return;
        }
	}
	
	protected override void OnFixedUpdate()
	{
		if (StartedHarvest() && !IsProxy)
		{
			RequestHarvest();
		}
	}

    [Rpc.Broadcast]
	private void RequestHarvest()
    {
		_modelRenderer.Set("holdtype", 5);
		_modelRenderer.Set("b_attack", true);

		var direction = _playerController.EyeAngles.Forward;
		var start = _playerController.EyePosition;
		var end = start + direction * 100f;
		var traceResult = Scene.Trace.Ray(start, end)
			.IgnoreGameObjectHierarchy(GameObject)
			.Run();
		
		if (!traceResult.Hit) return;
		
		var damageable = traceResult.GameObject.GetComponent<IDamageable>();
		if (damageable != null)
		{
			
			var damageInfo = new DamageInfo(10f, null, null);
			damageable.OnDamage(damageInfo);
		}

		Sound.Play(HarvestSound, WorldPosition);
    }
}
