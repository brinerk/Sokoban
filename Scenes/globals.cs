using Godot;
using System;

public static class DefinedGlobals
{
	public const string TestString = "test";
	
	//Define level
	// 0 is empty. 1 is floor. 2 is goal.
	public static int[,] LevelOne = {{1,0,0,2,0,0,0},
					    		     {0,1,1,1,1,1,1},
								  	 {0,1,1,1,0,1,1},
								     {0,1,1,1,0,1,1},
								     {0,1,1,1,1,1,0},
								     {0,1,1,1,0,1,0},
								     {1,0,0,1,1,1,0} };

	public static int[,] EntitiesGen = { {0,0,0,0,0,0,0},
					    		     	 {0,0,0,0,0,0,0},
								  	 	 {0,0,0,0,0,0,0},
								     	 {0,0,1,0,0,0,0},
								     	 {0,0,0,2,0,0,0},
								     	 {0,0,0,0,0,0,0},
								     	 {0,0,0,0,0,0,0} };
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
