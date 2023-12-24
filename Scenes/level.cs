using Godot;
using System;
using static DefinedGlobals;

public partial class level : Node3D
{
	private PackedScene Box = ResourceLoader.Load<PackedScene>("res://Scenes/box.tscn");

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		// LEVEL GENERATION
		// THIS SHOULD BE A FUNCTION
		// i is COLUMN, j is ROW
		for (int i = 0; i < LevelOne.GetLength(0); i++)
		{
			for (int j = 0; j < LevelOne.GetLength(1); j++)
			{
				int s = LevelOne[i, j];
				switch(s)
				{
					case 1:
						{
							var Floor = ResourceLoader.Load<PackedScene>("res://Scenes/floor.tscn").Instantiate<Node3D>();
							AddChild(Floor);
							Floor.Position = new Vector3(j,0,i);
							break;
						}
					case 2:
						{
							var Goal = ResourceLoader.Load<PackedScene>("res://Scenes/goal.tscn").Instantiate<Node3D>();
							AddChild(Goal);
							Goal.Position = new Vector3(j,0,i);
							break;
						}
				}
			}
		}
		
		// THIS SHOULD BE A FUNCTION
		for (int i = 0; i < EntitiesGen.GetLength(0); i++)
		{
			for (int j = 0; j < EntitiesGen.GetLength(1); j++)
			{
				int s = EntitiesGen[i, j];
				switch(s)
				{
					case 1:
					{
						var player = ResourceLoader.Load<PackedScene>("res://Scenes/player.tscn").Instantiate<Node3D>();
						player.Position = new Vector3(j,0.5f,i);
						AddChild(player);
						break;
					}
					case 2:
					{
						var BoxInstance = Box.Instantiate<Node3D>();
						BoxInstance.Position = new Vector3(j,0.25f,i);
						AddChild(BoxInstance);
						break;
					}
				}
			}
		}
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
