using Godot;
using System;

public partial class player : CharacterBody2D
{
	public const float Speed = 400.0f;
	public const float JumpVelocity = 400.0f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	// Get the animation child node
	AnimatedSprite2D animation = null;

	public override void _Ready()
	{
		animation = GetNode<AnimatedSprite2D>("animation");
	}

	public override void _PhysicsProcess(double delta)
	{
		Movement(delta);
		Animation();
	}

	private void Movement(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
			velocity.Y = -JumpVelocity;

		// Get the input direction and handle the movement/deceleration.
		float direction = Input.GetAxis("left", "right");
		if (direction != 0)
		{
			velocity.X = direction * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed / 20);
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	private void Animation()
	{
		// Mirrors sprite to match movement
		if (Velocity.X != 0)
			animation.FlipH = Velocity.X < 0;

		// Handles the animation state to match player controls
		
		if (Math.Abs(Velocity.X) > Speed / 100)
		{
			animation.Animation = "running";
			
		} else {
			animation.Animation = "default";
		}

		if (Velocity.Y < 0) {
			animation.Animation = "jumping";
		}
		else if (Velocity.Y > 0) {
			animation.Animation = "falling";
		}
	}
}
