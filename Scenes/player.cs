using Godot;
using System;
using static DefinedGlobals;

public partial class player : Node3D
{

	private float _t = 0.0f;
	private Vector3 NewPos;
	private Vector3 ActualPosition;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{	
		NewPos = Position;
		ActualPosition = Position;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		_t += (float)delta * 2.0f;

		if (Input.IsActionJustPressed("ui_left")) 
		{
			_t = 0.0f;
			Vector3 PotentialGridPos = ActualPosition - new Vector3(1,0,0);

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

						NewPos = ActualPosition - new Vector3(1,0,0);
						ActualPosition = ActualPosition - new Vector3(1,0,0);

						EntitiesGen[(int)PotentialGridPos.Z,(int)PotentialGridPos.X] = 1;
						break;
					}
					case 1:
					{
						break;
					}
					case 2:
					{

						if(LevelOne[(int)PotentialGridPos.Z,(int)PotentialGridPos.X-1] == 1)
						{
							EntitiesGen[(int)PotentialGridPos.Z,(int)PotentialGridPos.X] = 0;
							EntitiesGen[(int)PotentialGridPos.Z,(int)PotentialGridPos.X-1] = 2;

							NewPos = ActualPosition - new Vector3(1,0,0);
							ActualPosition = ActualPosition - new Vector3(1,0,0);
						}
						else
						{
							EntitiesGen[(int)PotentialGridPos.Z,(int)PotentialGridPos.X] = 2;
						}
						break;
					}
				}
			}
		}
		

		if (Input.IsActionJustPressed("ui_right")) 
		{
			_t = 0.0f;
			Vector3 PotentialGridPos = ActualPosition + new Vector3(1,0,0);

			if(LevelOne[(int)PotentialGridPos.Z,(int)PotentialGridPos.X] == 0)
			{
				ActualPosition = ActualPosition;
			}
			else
			{
				if(EntitiesGen[(int)PotentialGridPos.Z,(int)PotentialGridPos.X] == 0)
				{

					EntitiesGen[(int)ActualPosition.Z,(int)ActualPosition.X] = 0;

					NewPos = ActualPosition + new Vector3(1,0,0);
					ActualPosition = ActualPosition + new Vector3(1,0,0);

					EntitiesGen[(int)PotentialGridPos.Z,(int)PotentialGridPos.X] = 1;
				}
			}
		}

		if (Input.IsActionJustPressed("ui_up")) 
		{
			_t = 0.0f;
			Vector3 PotentialGridPos = ActualPosition - new Vector3(0,0,1);

			if(LevelOne[(int)PotentialGridPos.Z,(int)PotentialGridPos.X] == 0)
			{
				ActualPosition = ActualPosition;
			}
			else
			{
				//Don't walk into entities
				if(EntitiesGen[(int)PotentialGridPos.Z,(int)PotentialGridPos.X] == 0)
				{

					EntitiesGen[(int)ActualPosition.Z,(int)ActualPosition.X] = 0;

					NewPos = ActualPosition - new Vector3(0,0,1);
					ActualPosition = ActualPosition - new Vector3(0,0,1);

					EntitiesGen[(int)PotentialGridPos.Z,(int)PotentialGridPos.X] = 1;
				}
			}
		}
		if (Input.IsActionJustPressed("ui_down")) 
		{
			_t = 0.0f;
			Vector3 PotentialGridPos = ActualPosition + new Vector3(0,0,1);

			if(LevelOne[(int)PotentialGridPos.Z,(int)PotentialGridPos.X] == 0)
			{
				ActualPosition = ActualPosition;
			}
			else
			{
				//Don't walk into entities
				if(EntitiesGen[(int)PotentialGridPos.Z,(int)PotentialGridPos.X] == 0)
				{

					EntitiesGen[(int)ActualPosition.Z,(int)ActualPosition.X] = 0;

					NewPos = ActualPosition + new Vector3(0,0,1);
					ActualPosition = ActualPosition + new Vector3(0,0,1);

					EntitiesGen[(int)PotentialGridPos.Z,(int)PotentialGridPos.X] = 1;
				}
			}
		}


		Move(NewPos, _t);

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
