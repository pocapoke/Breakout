using Godot;
using System;

public partial class Menu : Control
{

	public void OnStartGamePressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/Main.tscn");
	}

	public void OnSettingsPressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/Settings.tscn");
	}

	public void OnQuitGamePressed()
	{
		GetTree().Quit();
	}
}
