using Godot;
using System;
using static DefinedGlobals;

public partial class level : Node3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		// ENTITIES
		// 0 is blank, 1 is player
		int[,] EntitiesGen = { {0,0,0,0},
							   {0,0,0,0},
							   {1,0,1,0},
							   {0,0,0,0} };


		// LEVEL GENERATION
		// THIS SHOULD BE A FUNCTION
		// i is COLUMN, j is ROW
		for (int i = 0; i < LevelOne.GetLength(0); i++)
		{
			for (int j = 0; j < LevelOne.GetLength(1); j++)
			{
				int s = LevelOne[i, j];
				if(s == 1)
				{
					var Floor = ResourceLoader.Load<PackedScene>("res://Scenes/floor.tscn").Instantiate<Node3D>();
					AddChild(Floor);
				Floor.Position = new Vector3(j,0,i);
				}
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
					player.Position = new Vector3(j,0.5f,i);
					AddChild(player);
				}
			}
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
