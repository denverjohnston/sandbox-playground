using Sandbox;

public sealed class SpleefManager : Component
{
	[Property] SpleefTileManager TileManager { get; set; }
	[Property] SpleefVictoryUI VictoryUI { get; set; }
	[Property] SceneFile NextSceneFile { get; set; }
	public TimeUntil StartTime = 2f;
	public bool GameOver = false;
	public string Winner { get; private set; }
	private List<GameObject> _eliminatedPlayers = new();
	private bool _gameStarted = false;
	private float _nextSceneTime = 2f;

	protected override void OnAwake()
	{
		VictoryUI.Enabled = false;
		TileManager.Initialize();
	}

	protected override void OnFixedUpdate()
	{
		if ( !_gameStarted && StartTime <= 0 )
		{
			TileManager.Activate();
			_gameStarted = true;
		}

		if ( GameOver )
			if ( _nextSceneTime <= 0 )
			{
				SceneLoadOptions loadOptions = new SceneLoadOptions();
				loadOptions.SetScene( NextSceneFile );
				Game.ChangeScene( loadOptions );
			}
			else
				_nextSceneTime -= Time.Delta;
	}

	public void EliminatePlayer( GameObject player )
	{
		if ( _eliminatedPlayers.Contains( player ) )
			return;

		_eliminatedPlayers.Add( player );

		Log.Info( $"{player.Name} has been eliminated" );

		if ( _eliminatedPlayers.Count >= Connection.All.Count - 1 )
		{
			Log.Info( $"All but one players elimininated, {player.Name} has won" );
			GameOver = true;
			Winner = player.Name;
			VictoryUI.Enabled = true;
		}
	}
}
