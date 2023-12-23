using Godot;
using System;

public partial class level : Node3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		// LEVEL EDITOR
		
		// FLOOR TILES
		// 0 is blank, 1 is a floor 
		int[,] LevelGen = { {1,0,0,1,0,0,0}, 
							{0,1,1,1,1,0,0}, 
							{0,1,1,1,0,0,0}, 
							{1,0,0,1,0,0,0} };

		// ENTITIES
		// 0 is blank, 1 is player
		int[,] EntitiesGen = { {0,0,0,0},
							   {0,1,0,0},
							   {0,0,0,0},
							   {0,0,0,0} };


		// LEVEL EDITOR COMPLETE

		// LEVEL GENERATION
		// THIS SHOULD BE A FUNCTION
		// i is COLUMN, j is ROW
		for (int i = 0; i < LevelGen.GetLength(0); i++)
		{
			for (int j = 0; j < LevelGen.GetLength(1); j++)
			{
				int s = LevelGen[i, j];
				if(s == 1)
				{
					var Floor = ResourceLoader.Load<PackedScene>("res://Scenes/floor.tscn").Instantiate<Node3D>();
					AddChild(Floor);
					GD.Print(Floor.Position);
					Floor.Position = new Vector3(j,0,i);
				}
				GD.Print(s);
			}
		}
		
		// THIS SHOULD BE A FUNCTION

		for (int i = 0; i < EntitiesGen.GetLength(0); i++)
		{
			for (int j = 0; j < EntitiesGen.GetLength(1); j++)
			{
				int s = EntitiesGen[i, j];
				if(s == 1)
				{
					var player = ResourceLoader.Load<PackedScene>("res://Scenes/player.tscn").Instantiate<Node3D>();
					AddChild(player);
					GD.Print(player.Position);
					player.Position = new Vector3(j,0.5f,i);
				}
				GD.Print(s);
			}
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
