using Godot;
using System;

public partial class player : Node3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("ui_left"))
				Position = Position - new Vector3(1,0,0);
		if (Input.IsActionJustPressed("ui_right"))
				Position = Position + new Vector3(1,0,0);
		if (Input.IsActionJustPressed("ui_up"))
				Position = Position - new Vector3(0,0,1);
		if (Input.IsActionJustPressed("ui_down"))
				Position = Position + new Vector3(0,0,1);
	}
}
