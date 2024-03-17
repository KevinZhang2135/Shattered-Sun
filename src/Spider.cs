using Godot;
using System;


public partial class Spider : Entity
{
	private CollisionShape2D detection_rect;
	private CharacterBody2D player;
	private bool chase;

	public override void _Ready()
	{
		speed = 540.0f;
		jumpVelocity = 600.0f;

		chase = false;

		animation = GetNode<AnimatedSprite2D>("animation");
		detection_rect = GetNode<CollisionShape2D>("detection/detection/detection_rect");
		player = GetNode<CharacterBody2D>("/root/level1/player");
	}

	// Handles gravity and controls
	protected override void Movement(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;

		if (chase)
		{
			Vector2 direction = (player.Position - Position).Normalized();
			velocity.X =  direction.X * speed; Mathf.MoveToward(0, direction.X * speed, speed / 50);
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, speed / 10);
		}

		Velocity = velocity;
	}

	// Updates sprite animation according to movement
	protected override void Animation()
	{
		if (animation == null)
			return;

		// Mirrors sprite to match movement
		if (Velocity.X != 0) {
			animation.FlipH = Velocity.X < 0;
		}


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

	// chases player upon detection
	private void _on_detection_body_entered(Node2D body)
	{
		if (body.Name == "player") {
			chase = true;
		}
			
	}


	// stops chasing once outside detection range
	private void _on_detection_body_exited(Node2D body)
	{
		// Replace with function body.
		if (body.Name == "player")
			chase = false;
	}

}











