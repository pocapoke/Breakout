using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class Ball : CharacterBody2D
{
	[Export] public int startSpeed = 300;
	[Export] RigidBody2D player;

	private Vector2 ogPosition;
	private Vector2 dir;

	private int currentSpeed;
	private const int acel = 50;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		NewBall();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        // MoveAndCollide(Vector2 motion) method INITIALIZES the ball movement and assign its values to "collision KinematicCollision2D". When MoveAndCollide collide an A object, it returns that A object and assign its to collision.
        KinematicCollision2D collision = MoveAndCollide(dir * currentSpeed * (float)delta);
		// This line instantiate an GodotObject named "collider"
		GodotObject collider;

		if (collision != null) // This parameter check collision value
		{
			collider = collision.GetCollider();
			
			if (collider == GetNode<RigidBody2D>("../Paddle"))
			{
				dir = NewDirection();
				//dir = dir.Bounce(collision.GetNormal());
			}
            else dir = dir.Bounce(collision.GetNormal());
        }
    }

	public void NewBall()
	{
		// Assign speed to ball
		currentSpeed = startSpeed;

		// Assgin direction to ball
		dir = new Vector2(0, 1);
	}

	public Vector2 NewDirection()
	{
		float ballX = Position.X;
		float padX = player.Position.X;
		float distance = ballX - padX;
		Vector2 newDir = new Vector2();
		
		newDir.Y = -1;

		newDir.X = (distance / (player.GetNode<CollisionShape2D>("CollisionShape2D").Shape.GetRect().Size.X / 2) * 0.6f);

		return newDir.Normalized();
	}
}
