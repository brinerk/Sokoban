using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using static DefinedGlobals;

public partial class GameManager : Node3D
{

	private PackedScene LevelScene = ResourceLoader.Load<PackedScene>("res://Scenes/level.tscn");
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void UpdateLevel()
	{
		level CurrentLevel = (level)GetNode("/root/Level");
		GlobalLevelID = CurrentLevel.LevelID;
		GlobalLevelID += 1;
		CurrentLevel.Name = "Delete";
		GetNode("/root/Delete").QueueFree();
	}

	public void ClearArrays()
	{
		GoalCoords.Clear();
		Array.Clear(LevelOne, 0, LevelOne.Length);
		Array.Clear(Entities, 0, Entities.Length);
		Array.Clear(EntitiesGen, 0, EntitiesGen.Length);
		Boxes.Clear();
	}

	public void InstantiateLevel()
	{
		var LevelSceneInstance = LevelScene.Instantiate<level>();
		LevelSceneInstance.LevelID = GlobalLevelID;
		GetTree().Root.AddChild(LevelSceneInstance);
		LevelSceneInstance.Name = "Level";
	}

	public void CheckWin()
	{

		int GoalNum = 0;

		//to improve, make a class with coords and active flag
		//change active to false

		List<(int X, int Y)> GoalCoordsCopy = new List<(int X, int Y)>(GoalCoords);


		foreach (var coord in GoalCoordsCopy)
		{
			if(EntitiesGen[coord.Y,coord.X] > 1)
			{
				GoalNum+=1;
			}
			if(GoalNum == Boxes.Count)
			{
				UpdateLevel();
				ClearArrays();
				InstantiateLevel();
			}
		}
	}


}
