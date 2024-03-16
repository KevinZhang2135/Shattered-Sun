using Godot;
using System;

public partial class Player : Entity
{
	public override void _Ready()
	{
		speed = 600.0f;
		jumpVelocity = 720.0f;

		animation = GetNode<AnimatedSprite2D>("animation");
	}

	// Handles gravity and controls
	protected override void Movement(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
			velocity.Y = -jumpVelocity;

		// Get the input direction and handle the movement/deceleration.
		float direction = Input.GetAxis("left", "right");
		velocity.X = (direction != 0) ? direction * speed : Mathf.MoveToward(Velocity.X, 0, speed / 10);
		Velocity = velocity;
	}

	// Updates sprite animation according to movement
	protected override void Animation()
	{
		if (animation == null)
			return;

		// Mirrors sprite to match movement
		if (Velocity.X != 0)
			animation.FlipH = Velocity.X < 0;

		// Handles the animation state to match velocity
		if (Math.Abs(Velocity.X) > speed / 100)
			animation.Play("running");

		else
			animation.Play("default");


		if (Velocity.Y < 0)

			animation.Play("jumping");

		else if (Velocity.Y > 0)

			animation.Play("falling");

	}
}
