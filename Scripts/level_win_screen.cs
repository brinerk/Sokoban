using Godot;
using System;
using static DefinedGlobals;

public partial class level_win_screen : Control
{
	private Button NextLevel;
	private GameManager gameManager;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		gameManager = (GameManager)GetNode("/root/Main/GameManager");
		NextLevel = GetNode<Button>("next_level");

		NextLevel.Pressed += ButtonPressed;
	}

	private void ButtonPressed()
	{
		level CurrentLevel = (level)GetNode("/root/Level");
		GlobalLevelID = CurrentLevel.LevelID;
		GlobalLevelID += 1;

		gameManager.UpdateLevel();
		gameManager.ClearArrays();
		gameManager.InstantiateLevel();

		this.Visible = false;
		BlockInput = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("space"))
		{
			ButtonPressed();
		}
	}
}
