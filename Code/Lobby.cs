using Sandbox;

public sealed class Lobby : Component
{
	[Property] SceneFile targetScene { get; set; }
	private TimeUntil _gameStart;
	protected override void OnStart()
	{
		_gameStart = 3f;
	}
	protected override void OnUpdate()
	{
		if ( _gameStart )
		{
			Log.Info( _gameStart );
			var options = new SceneLoadOptions();
			options.SetScene( targetScene );
			Game.ChangeScene( options );
		}
	}
}
