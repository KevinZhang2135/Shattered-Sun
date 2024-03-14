using Godot;
using System;

public partial class player : entity
{
	
	public override void _Ready()
	{
		Speed = 400.0f;
		JumpVelocity = 480.0f;

		
		animation = GetNode<AnimatedSprite2D>("animation");
	}

	protected override void Movement(double delta)
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
}
