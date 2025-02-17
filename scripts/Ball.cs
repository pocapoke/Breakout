using Godot;
using System;
using System.Collections;
using System.Runtime.CompilerServices;

public partial class Ball : CharacterBody2D
{
	
	[Export] public int startSpeed = 300;
	[Export] RigidBody2D player;
	[Export] AudioStreamMP3 BallHitSFX;
	[Export] AudioStreamWav BreakBrickSFX;
    [Export] AudioStreamWav GetPointSFX;

    private Vector2 dir;
	private int currentSpeed;
	private const int acel = 50;
	private const int maxSpeed = 800;
	public AudioStreamPlayer ballHit;
	public AudioStreamPlayer breakBrick;
    public AudioStreamPlayer getPoint;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		NewBall();
		ballHit = GetNode<AudioStreamPlayer>("BallHit");
		breakBrick = GetNode<AudioStreamPlayer>("BreakBrick");
        getPoint = GetNode<AudioStreamPlayer>("GetPoint");
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

			ballHit.Stream = BallHitSFX;
			ballHit.Play();

			// When the ball hit the paddle
			if (collider == GetNode<RigidBody2D>("../Paddle"))
			{
				dir = NewDirection();
            }
			// When the ball hit the brick and the wall
			else
			{
                // The code check if the collider, which is assigned to a Node typed "bricks", is in Godot Group named "Bricks". This return true if it is, and false if it isnt.
				if (collider is Node bricks && bricks.IsInGroup("Bricks"))
                {
					collider.Free();

					breakBrick.Stream = BreakBrickSFX;
					breakBrick.Play();

					getPoint.Stream = GetPointSFX;
					getPoint.Play();

					GameManager.Instance.GetOnePoint();
					
					currentSpeed += acel;
                }

                dir = dir.Bounce(collision.GetNormal());
            }

			if (currentSpeed >= maxSpeed)
			{
				currentSpeed = maxSpeed;
			}
		}
    }

	public void NewBall()
	{
		//Assign position again
		Position = new Vector2(400, 600);
		
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
