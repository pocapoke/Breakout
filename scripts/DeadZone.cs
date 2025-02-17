using Godot;
using System;

public partial class DeadZone : Node2D
{
    [Signal] public delegate void BallOutEventHandler();
    [Export] AudioStreamMP3 LoseBallSFX;
    [Export] AudioStreamMP3 LoseGameSFX;

    private AudioStreamPlayer lose;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        lose = GetNode<AudioStreamPlayer>("Lose");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public void OnBodyEntered(Node2D body)
    {
        if (GameManager.Instance.lives > 1)
        {
            lose.Stream = LoseBallSFX;
            lose.Play();
        }
        else
        {
            lose.Stream = LoseGameSFX;
            lose.Play();
        }

        //EmitSignal(SignalName.BallOut);
    }
}
