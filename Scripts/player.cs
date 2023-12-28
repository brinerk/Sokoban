using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static DefinedGlobals;

public partial class player : Node3D
{

	private Queue MoveQueue = new Queue();

	private float _t = 0.0f;
	private float MoveTimer = 0.0f;
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
		MoveTimer += (float)delta * 2.0f;

		if (Input.IsActionJustPressed("left")) 
		{
			MoveQueue.Enqueue(Left);
			//CheckMovement(Left);
			//Add opposite dir to undo array
		}

		if (Input.IsActionJustPressed("right")) 
		{
			MoveQueue.Enqueue(Right);
			//CheckMovement(Right);
		}
		if (Input.IsActionJustPressed("down")) 
		{
			MoveQueue.Enqueue(Down);
			//CheckMovement(Down);
		}
		if (Input.IsActionJustPressed("up")) 
		{
			MoveQueue.Enqueue(Up);
			//CheckMovement(Up);
		}
		if (Input.IsActionJustPressed("restart"))
		{
			Restart();
		}

		//Handle moves in buffer
		List<Vector3> MoveList = new List<Vector3>(MoveQueue.Cast<Vector3>().ToList());
		foreach (Vector3 move in MoveList)
		{
			if(MoveTimer > 0.1f) 
			{
				CheckMovement(move);
			}
		}

		//SPAWN PLAYER
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

		//Setup Lerp
		if(ActualPosition != NewPos)
		{
			_t = 0.0f;
			ActualPosition = NewPos;
		}

		//Move and Lerp
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

		//One tile away from player in direction of motion
		Vector3 PotentialGridPos = ActualPosition + dir;

		//Entites at player location and one tile away
		var CheckEnt = EntitiesGen[(int)PotentialGridPos.Z,(int)PotentialGridPos.X];
		var CurrentGrid = EntitiesGen[(int)ActualPosition.Z,(int)ActualPosition.X];

		//Check if there is a floor tile to move to
		if(LevelOne[(int)PotentialGridPos.Z,(int)PotentialGridPos.X] == 0)
		{
			//No motion
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

		MoveTimer = 0.0f;
		MoveQueue.Dequeue();
	}


	void Move(Vector3 NewPos, float _t)
	{
		Position = Position.Lerp(NewPos, _t);
	}
}
