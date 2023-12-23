using Godot;
using System;

public partial class player : Node3D
{

	private float _t = 0.0f;
	private Vector3 NewPos = Vector3.Zero;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		_t += (float)delta * 0.4f;

		if (Input.IsActionJustPressed("ui_left")) 
		{
			_t = 0.0f;
			NewPos = Position - new Vector3(1,0,0);
		}
		if (Input.IsActionJustPressed("ui_right"))
		{
			_t = 0.0f;
			NewPos = Position + new Vector3(1,0,0);
		}
		if (Input.IsActionJustPressed("ui_up"))
		{
			_t = 0.0f;
			NewPos = Position - new Vector3(0,0,1);
		}
		if (Input.IsActionJustPressed("ui_down"))
		{
			_t = 0.0f;
			NewPos = Position + new Vector3(0,0,1);
		}

		Move(NewPos, _t);

	}

	void Move(Vector3 NewPos, float _t)
	{
		Position = Position.Lerp(NewPos, _t);
	}
}
