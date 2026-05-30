using Sandbox;

public sealed class SpleefTileManager : Component
{
	[Property] GameObject TilePrefab { get; set; }
	[Property] int GridSize { get; set; } = 16;
	[Property] int TileSize { get; set; } = 50;

	private List<GameObject> _tiles = new();

	public void Initialize()
	{
		Log.Info( "Initializing tiles" );

		for ( int x = 0; x < GridSize; x++ )
		{
			for ( int y = 0; y < GridSize; y++ )
			{
				GameObject tile = TilePrefab.Clone();
				tile.WorldPosition = new Vector3( x * TileSize, y * TileSize, 0 );
				_tiles.Add( tile );
			}
		}
	}

	public void Activate()
	{
		Log.Info( "Activating tiles" );
		foreach ( var tile in _tiles )
		{
			tile.GetComponent<SpleefTile>().Trigger.Enabled = true;
		}
	}
}
