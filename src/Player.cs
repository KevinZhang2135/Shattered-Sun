using Godot;
using System;

public partial class Player : Entity
{
	public Boolean controlInput;
	public override void _Ready()
	{
		health = 3;

		// physics
		speed = 360.0f;
		jumpVelocity = 560.0f;
		frictionCoefficient = 0.95f;

		controlInput = true;

		// child nodes
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

		// Get the input direction and handle the movement/acceleration.
		float direction = Input.GetAxis("left", "right");
		if (direction != 0 && controlInput) {
			velocity.X = Mathf.MoveToward(Velocity.X, direction * speed, speed / 20);

		} else {
			velocity.X = Mathf.MoveToward(Velocity.X, 0, speed * (1 - frictionCoefficient));

			// Regains player control 
			if (Math.Abs(velocity.X) < speed / 20) 
				controlInput = true;
		}

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
		{
			animation.Play("run");
		}
		else
		{
			animation.Play("default");
		}

		if (Velocity.Y < 0)
		{
			animation.Play("jump");
		}
		else if (Velocity.Y > 0)
		{
			animation.Play("fall");
		}


	}
}
