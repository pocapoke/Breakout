using Godot;
using System;

public partial class Settings : Control
{
    public void OnBackPressed()
    {
        GetTree().ChangeSceneToFile("res://scenes/Menu.tscn");
    }
}
