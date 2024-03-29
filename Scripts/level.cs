using Godot;
using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using static DefinedGlobals;

public partial class level : Node3D
{

	public int LevelID {get; set;}

	public int MapReader()
	{

		MapFiles = MapFiles.OrderBy( f => Path.GetFileName(f)).ToArray();


		int z = 0;

		string[] lines = File.ReadAllLines(MapFiles[LevelID]);


		//SET TILES
		for(int i = 0; i<lines.GetLength(0); i++)
		{
			string[] entries = lines[i].Split(',');
			if(entries[0] == "ENTITIES")
			{
				z = i;
				break;
			}
			else {
				LevelOne[Int32.Parse(entries[1]),Int32.Parse(entries[0])] = Int32.Parse(entries[2]);
			}
		}


		//SET ENTITIES
		for(int j = z+1; j<lines.GetLength(0);j++)
		{
			string[] entries = lines[j].Split(',');

			//IF PLAYER
			if(Int32.Parse(entries[2]) == 1) 
			{
				//SET IMMUTABLE ENTITIES
				Entities[Int32.Parse(entries[1]),Int32.Parse(entries[0])] = Int32.Parse(entries[2]);
			}
			else 
			{
				//SET IMMUTABLE ENTITIES
				Entities[Int32.Parse(entries[1]),Int32.Parse(entries[0])] = Int32.Parse(entries[2]);
			}
		}


		for(int p = 0; p<Entities.GetLength(0);p++){
			for(int x = 0; x<Entities.GetLength(0);x++){

				EntitiesGen[x,p] = Entities[x,p];
			}
		}

		return 0;

	}


	private PackedScene Box = ResourceLoader.Load<PackedScene>("res://Scenes/box.tscn");

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		MapReader();
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
							GoalCoords.Add(new (j,i));
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
				if(EntitiesGen[i,j] == 1)
				{
					var player = ResourceLoader.Load<PackedScene>("res://Scenes/player.tscn").Instantiate<Node3D>();
					player.Position = new Vector3(j,0.5f,i);
					AddChild(player);
				}
				else if(EntitiesGen[i,j] != 0)
				{
					var BoxInstance = Box.Instantiate<box>();
					BoxInstance.Position = new Vector3(j,0.25f,i);
					BoxInstance.ID = Entities[i,j];
					AddChild(BoxInstance);
					Boxes.Add(BoxInstance);
				}
			}
		}
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

}
