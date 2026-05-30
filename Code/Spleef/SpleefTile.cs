using Sandbox;

public sealed class SpleefTile : Component, Component.ITriggerListener
{
	[Property] public Collider Trigger { get; set; }
	private bool _canBreak = false;
	private TimeUntil _timeForBreak = 0.6f;
	private TimeUntil _cleanupTime = 1f;
	private PhysicsLock rigidbodyLock = new PhysicsLock();

	protected override void OnAwake()
	{
		rigidbodyLock.Z = false; // Unlock Z axis to allow falling
		Trigger.Enabled = false;
	}

	protected override void OnFixedUpdate()
	{
		if ( !_canBreak )
			return;

		if ( _timeForBreak )
		{
			GetComponent<Rigidbody>().Locking = rigidbodyLock;
			// GetComponent<Rigidbody>().MotionEnabled = false;
			// GetComponent<Rigidbody>().MotionEnabled = true;
			_canBreak = false;
		}
	}

	public void OnTriggerEnter( Collider other )
	{
		if ( IsProxy )
			return;

		if ( !other.GameObject.Tags.Has( "spleef-player" ) || other.GameObject.Tags.Has( "spleef-tile" ) )
			return;

		Trigger.Enabled = false;

		_canBreak = true;
		_timeForBreak = 0.6f;
	}
}
