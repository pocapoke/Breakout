using Godot;
using System;

public partial class Paddle : RigidBody2D
{
	[Export] public float Speed = 500.0f;
	public const float JumpVelocity = -400.0f;
	public float paddleLength;
	private float currentY;
	Vector2 startPos = new Vector2();

	public override void _Ready()
	{
		startPos = Position;
		currentY = Position.Y;
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 currentPosition = Position;


		if (Input.IsActionPressed("moveLeft"))
		{
			currentPosition.X -= Speed * (float)delta;
			//Position = currentPosition;

		}
		else if (Input.IsActionPressed("moveRight"))
		{
			currentPosition.X += Speed * (float)delta;
			//Position = currentPosition;

		}

		currentPosition.Y = currentY;
		Position = currentPosition;
	}

	public void ResetPadle()
	{
		Position = startPos;
	}
}
