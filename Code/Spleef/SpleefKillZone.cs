using Sandbox;

public sealed class SpleefKillZone : Component, Component.ITriggerListener
{
	[Property] SpleefManager Manager { get; set; }

	// Add array to store players that have been eliminated, so we don't log multiple times for the same player
	// Notify the manager when a player is eliminated, so it can check for win conditions

	public void OnTriggerEnter( Collider other )
	{
		if ( IsProxy )
			return;

		if ( !other.GameObject.Tags.Has( "spleef-player" ) )
			return;

		Manager.EliminatePlayer( other.GameObject.Root );
	}
}
