using Godot;
using System;
using static DefinedGlobals;

public partial class player : Node3D
{

	private float _t = 0.0f;
	private Vector3 NewPos;
	private Vector3 ActualPosition;
	private Vector3 Left;
	private Vector3 Right;
	private Vector3 Down;
	private Vector3 Up;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{	
		NewPos = Position;
		ActualPosition = Position;
		Left = new Vector3(-1,0,0);
		Right = new Vector3(1,0,0);
		Up = new Vector3(0,0,-1);
		Down = new Vector3(0,0,1);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		_t += (float)delta * 2.0f;

		if (Input.IsActionJustPressed("left")) 
		{
			CheckMovement(Left);
			//Add opposite dir to array
		}

		if (Input.IsActionJustPressed("right")) 
		{
			CheckMovement(Right);
		}
		if (Input.IsActionJustPressed("down")) 
		{
			CheckMovement(Down);
		}
		if (Input.IsActionJustPressed("up")) 
		{
			CheckMovement(Up);
		}
		if (Input.IsActionJustPressed("restart"))
		{
			Restart();
		}

		for(int i = 0; i < EntitiesGen.GetLength(0); i++)
		{
			for(int j = 0; j < EntitiesGen.GetLength(1); j++)
			{
				if(EntitiesGen[i,j] == 1)
				{
					NewPos = new Vector3(j,0.5f,i);
				}
			}
		}

		if(ActualPosition != NewPos)
		{
			_t = 0.0f;
			ActualPosition = NewPos;
		}

		Move(NewPos, _t);
		CheckWin();
	}

	void CheckWin()
	{
		//to improve, make a class with coords and active flag
		//change active to false 

		foreach (var coord in GoalCoords)
		{
			if(EntitiesGen[coord.Y,coord.X] > 1)
			{
				//GoalCoords.Remove(coord);
				GoalNum+=1;
			}
			if(GoalNum == Boxes.Count)
			{
				GD.Print("YOU WIN");
			}
		}
		GoalNum = 0;
		//GD.Print(Boxes.Count, ", ", GoalNum);
	}

	void Restart() 
	{
		for(int row = 0; row < Entities.GetLength(0); row++)
		{
			for(int col = 0; col < Entities.GetLength(1); col++)
			{
				EntitiesGen[row,col] = Entities[row,col];
			}
		}
	}

	void CheckMovement(Vector3 dir)
	{

		_t = 0.0f;
		Vector3 PotentialGridPos = ActualPosition + dir;
		var CheckEnt = EntitiesGen[(int)PotentialGridPos.Z,(int)PotentialGridPos.X];
		var CurrentGrid = EntitiesGen[(int)ActualPosition.Z,(int)ActualPosition.X];

		if(LevelOne[(int)PotentialGridPos.Z,(int)PotentialGridPos.X] == 0)
		{
			CurrentGrid = 1;
			CheckEnt = 0;
		}
		else
		{
			if(CheckEnt == 0)
			{
				//make previous pos 0
				EntitiesGen[(int)ActualPosition.Z,(int)ActualPosition.X]=0;
				//make new pos 1
				EntitiesGen[(int)PotentialGridPos.Z,(int)PotentialGridPos.X] = 1;
			}
			else if(CheckEnt == 1)
			{
				;
			}
			else if(CheckEnt > 1)
			{
				if(LevelOne[(int)PotentialGridPos.Z+(int)dir.Z,(int)PotentialGridPos.X+(int)dir.X] != 0 && EntitiesGen[(int)PotentialGridPos.Z+(int)dir.Z,(int)PotentialGridPos.X+(int)dir.X] == 0)
				{
					EntitiesGen[(int)ActualPosition.Z,(int)ActualPosition.X] = 0;

					EntitiesGen[(int)PotentialGridPos.Z,(int)PotentialGridPos.X] = 0;
					EntitiesGen[(int)PotentialGridPos.Z+(int)dir.Z,(int)PotentialGridPos.X+(int)dir.X] = CheckEnt;

					EntitiesGen[(int)PotentialGridPos.Z,(int)PotentialGridPos.X] = 1;

				}
				else
				{
					ActualPosition = ActualPosition;
				}

			}
		}
	}


	void Move(Vector3 NewPos, float _t)
	{


		Position = Position.Lerp(NewPos, _t);
	}
}
