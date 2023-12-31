using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static DefinedGlobals;

public partial class player : Node3D
{

	private Queue MoveQueue = new Queue();
	private Stack GameState = new Stack();

	private float _t = 0.0f;
	private float MoveTimer = 0.0f;
	private float MoveCoolDown = 0.0f;
	private Vector3 NewPos;
	private Vector3 ActualPosition;
	private Vector3 Left;
	private Vector3 Right;
	private Vector3 Down;
	private Vector3 Up;

	private PackedScene LevelScene = ResourceLoader.Load<PackedScene>("res://Scenes/level.tscn");

	private GameManager gameManager;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{	

		gameManager = (GameManager)GetNode("/root/Main/GameManager");
		NewPos = Position;
		ActualPosition = Position;
		GameState.Push(Entities);

		Left = new Vector3(-1,0,0);
		Right = new Vector3(1,0,0);
		Up = new Vector3(0,0,-1);
		Down = new Vector3(0,0,1);

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	
		//REALTIME VARIABLES
		_t += (float)delta * 2.0f;
		MoveTimer += (float)delta * 2.0f;




		if(!BlockInput)
		{


			Vector2 DirVector = Input.GetVector("left", "right", "up", "down");
			if(DirVector != Vector2.Zero)
			{
				GD.Print(DirVector);
			}

		//LEFT
			if (Input.IsActionPressed("left")) 
			{
				MoveCoolDown -= (float)delta;

				if(MoveCoolDown <= 0)
				{
					MoveQueue.Enqueue(Left);
					MoveCoolDown = 0.5f;
				}
				//Add opposite dir to undo array
			}

			if (Input.IsActionJustReleased("left"))
			{
				MoveCoolDown = 0.0f;
			}




			//RIGHT
			if (Input.IsActionPressed("right")) 
			{
				MoveCoolDown -= (float)delta;

				if(MoveCoolDown <= 0)
				{
					MoveQueue.Enqueue(Right);
					MoveCoolDown = 0.5f;
				}
				//Add opposite dir to undo array
			}
			if (Input.IsActionJustReleased("right"))
			{
				MoveCoolDown = 0.0f;
			}


			//UP
			if (Input.IsActionPressed("up")) 
			{
				MoveCoolDown -= (float)delta;

				if(MoveCoolDown <= 0)
				{
					MoveQueue.Enqueue(Up);
					MoveCoolDown = 0.5f;
				}
				//Add opposite dir to undo array
			}
			if (Input.IsActionJustReleased("up"))
			{
				MoveCoolDown = 0.0f;
			}



			//DOWN
			if (Input.IsActionPressed("down")) 
			{
				MoveCoolDown -= (float)delta;

				if(MoveCoolDown <= 0)
				{
					MoveQueue.Enqueue(Down);
					MoveCoolDown = 0.5f;
				}
				//Add opposite dir to undo array
			}
			if (Input.IsActionJustReleased("down"))
			{
				MoveCoolDown = 0.0f;
			}



			//RESTART
			if (Input.IsActionJustPressed("restart"))
			{
				Restart();
			}


			//UNDO
			if (Input.IsActionJustPressed("undo") && GameState.Count > 1)
			{
				Undo();
			}
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
		
		gameManager.CheckWin();
	}

	private void CleanLevel() 
	{
		gameManager.UpdateLevel();
		gameManager.ClearArrays();
		gameManager.InstantiateLevel();
	}

	/*void CheckWin()
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
				CleanLevel();
			}
		}
	}*/


	void Restart() 
	{

		GameState.Push(EntitiesGen.Clone());
		for(int row = 0; row < Entities.GetLength(0); row++)
		{
			for(int col = 0; col < Entities.GetLength(1); col++)
			{
				EntitiesGen[row,col] = Entities[row,col];
			}
		}
	}


	void Undo()
	{
		int[,] PreviousGameState = (int[,])GameState.Pop();
		for(int x = 0; x < PreviousGameState.GetLength(0); x++)
		{
			for(int y = 0; y < PreviousGameState.GetLength(1); y++)
			{
				EntitiesGen[x,y] = PreviousGameState[x,y];
			}
		}
	}

	void CheckMovement(Vector3 dir)
	{

		GameState.Push(EntitiesGen.Clone());

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

		//This means that we cannot have objects that move on their own, for undoing
	}


	void Move(Vector3 NewPos, float _t)
	{
		Position = Position.Lerp(NewPos, _t);
	}
}
