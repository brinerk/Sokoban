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

		if (Input.IsActionJustPressed("ui_left")) 
		{
			CheckMovement(Left);
		}

		if (Input.IsActionJustPressed("ui_right")) 
		{
			CheckMovement(Right);
		}
		if (Input.IsActionJustPressed("ui_down")) 
		{
			CheckMovement(Down);
		}
		if (Input.IsActionJustPressed("ui_up")) 
		{
			CheckMovement(Up);
		}

		Move(NewPos, _t);
	}

	void CheckMovement(Vector3 dir)
	{

		_t = 0.0f;
		Vector3 PotentialGridPos = ActualPosition + dir;

		if(LevelOne[(int)PotentialGridPos.Z,(int)PotentialGridPos.X] == 0)
		{
			ActualPosition = ActualPosition;
		}
		else
		{
			var CheckEnt = EntitiesGen[(int)PotentialGridPos.Z,(int)PotentialGridPos.X];
			switch(CheckEnt)
			{
				case 0:
				{
					EntitiesGen[(int)ActualPosition.Z,(int)ActualPosition.X] = 0;

					NewPos = ActualPosition + dir;
					ActualPosition = ActualPosition + dir;

					EntitiesGen[(int)ActualPosition.Z,(int)ActualPosition.X] = 1;
					break;
				}
				case 1:
				{
					break;
				}
				case 2:
				{

					if(LevelOne[(int)PotentialGridPos.Z+(int)dir.Z,(int)PotentialGridPos.X+(int)dir.X] != 0 )
					{
						EntitiesGen[(int)ActualPosition.Z,(int)ActualPosition.X] = 0;

						EntitiesGen[(int)PotentialGridPos.Z,(int)PotentialGridPos.X] = 0;
						EntitiesGen[(int)PotentialGridPos.Z+(int)dir.Z,(int)PotentialGridPos.X+(int)dir.X] = 2;

						NewPos = ActualPosition + dir;
						ActualPosition = ActualPosition + dir;

						EntitiesGen[(int)ActualPosition.Z,(int)ActualPosition.X] = 1;
					}
					else
					{
						ActualPosition = ActualPosition;
					}
					break;
				}
			}
		}
	}


	void Move(Vector3 NewPos, float _t)
	{

			/*Useless now
			if(Position != NewPos) 
			{
				Moving = true;
			}
			else
			{
				Moving = false;
			}*/

			Position = Position.Lerp(NewPos, _t);
	}
}
