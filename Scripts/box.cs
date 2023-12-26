using Godot;
using System;
using static DefinedGlobals;

public partial class box: Node3D
{
	private float _t = 0.0f;
	private Vector3 NewPos;
	private Vector3 AcPos;
	public int ID {get; set;}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{	
		NewPos = Position;
		AcPos = Position;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_t += (float)delta * 2.0f;

		for(int i = 0; i < EntitiesGen.GetLength(0); i++)
		{
			for(int j = 0; j < EntitiesGen.GetLength(1); j++)
			{
				if (EntitiesGen[i,j] == ID)
				{
					//TODO write current pos to array
					NewPos = new Vector3(j,0.25f,i);
				}
			}
		}

		if(AcPos != NewPos)
		{
			_t = 0.0f;
			AcPos = NewPos;
		}

		Move(NewPos, _t);

	}

	void Move(Vector3 NewPos, float _t)
	{
		Position = Position.Lerp(NewPos, _t);
	}
}
