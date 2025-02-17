using Godot;
using System;

public partial class GameManager : Node
{
    public static GameManager Instance;

    [Export] Label CurrentScore;
	[Export] Label HighScore;
	[Export] AnimationPlayer HeartLostAnimation;
	
	bool firstStart = true;
	bool gameEnd = false;
	int scores = 0;
	public int lives = 3;
	int highScore;
	int currentHeart = 3;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		Instance = this;
		GetTree().Paused = true;
    }

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey button && button.Pressed)
		{
			if (button.Keycode == Key.Escape)
			{
				GetTree().Quit();
			}
			else if (button.Keycode == Key.Space)
			{
				if (firstStart && lives != 0)
				{
					GetTree().Paused = false;
					firstStart = false;
				}
				else if (gameEnd)
				{
					ReloadGame();
				}
			}
		}
	}


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{

	}

	public void OnDeadZoneBodyEntered()
	{
		LoseLife();
		firstStart = true;
		GD.Print("Game stop!");
        GetTree().Paused = true;
    }

	public void GetOnePoint()
	{
		scores++;
		CurrentScore.Text = scores.ToString();
    }

    public void LoseLife()
    {
		lives--;
		HeartLostAnimation.Play("Life-Lost_" + currentHeart.ToString());
		currentHeart--;

		if (lives == 0)
		{
			GetTree().Paused = true;
			gameEnd = true;
			if (scores > highScore)
			{
                highScore = scores;
                HighScore.Text = highScore.ToString();
			}
		}
    }

	public void ReloadGame()
	{
		GetTree().ReloadCurrentScene();
	}
}
