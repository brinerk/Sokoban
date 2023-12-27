using Godot;
using System;
using System.IO;
using System.Collections.Generic;

public static class DefinedGlobals
{
	public const string TestString = "test";

	public static List<(int X, int Y)> GoalCoords = new List<(int X, int Y)>{};

	public static List<box> Boxes = new List<box>{};

	public static int GoalNum = 0;
	
	//Define level
	// 0 is empty. 1 is floor. 2 is goal.
	public static int[,] LevelOne = new int[64,64];

	public static int[,] Entities = new int[64,64];

	public static int[,] EntitiesGen = new int[64,64];

}


public partial class globals : Node3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
