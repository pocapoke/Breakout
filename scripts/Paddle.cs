using Godot;
using System;

public partial class Paddle : RigidBody2D
{
	[Export] public float Speed = 500.0f;
	public const float JumpVelocity = -400.0f;
	public float paddleLength;

	public override void _PhysicsProcess(double delta)
	{
		Vector2 currentPosition = Position;
		//Vector2 velocity = Velocity;

		if (Input.IsActionPressed("moveLeft"))
		{
			currentPosition.X -= Speed * (float)delta;
			Position = currentPosition;
			
			//velocity.X = -Speed;
		}
		else if (Input.IsActionPressed("moveRight"))
		{
			currentPosition.X += Speed * (float)delta;
			Position = currentPosition;
			
			//velocity.X = Speed;
		}
		//else velocity.X = 0;

		//Velocity = velocity;
		//MoveAndSlide();
	}
}
