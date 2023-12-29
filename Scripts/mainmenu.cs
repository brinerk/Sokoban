using Godot;
using System;

public partial class mainmenu : Control
{

	private Button PlayButton;
	private PackedScene LevelScene = ResourceLoader.Load<PackedScene>("res://Scenes/level.tscn");

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();

		PlayButton = GetNode<Button>("Button");

		PlayButton.Pressed += ButtonPressed;


	}

	private void ButtonPressed()
	{
		var LevelSceneInstance = LevelScene.Instantiate<level>();
		LevelSceneInstance.LevelID = 0;
		GetTree().Root.AddChild(LevelSceneInstance);
		GetNode("/root/Main/main_menu").QueueFree();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

}
