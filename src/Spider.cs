using Godot;
using System;


public partial class Spider : Entity
{
	private CollisionShape2D detection_rect;
	private Player player;
	private bool chase;
	private bool explode;

	public override void _Ready()
	{   
		// physics
		speed = 540.0f;
		jumpVelocity = 600.0f;
		frictionCoefficient = 0.01f;

		chase = false;
		explode = false;

		// child nodes
		animation = GetNode<AnimatedSprite2D>("animation");
		detection_rect = GetNode<CollisionShape2D>("detection/detection/detection_rect");
		player = GetNode<Player>("/root/level1/player");
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
			velocity.X = (direction.X != 0)
				? direction.X * speed
				: Mathf.MoveToward(Velocity.X, 0, speed * frictionCoefficient);
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
		if (animation == null || explode == true)
			return;

		// Mirrors sprite to match movement
		if (Velocity.X != 0)
		{
			animation.FlipH = Velocity.X < 0;
		}


		// Handles the animation state to match velocity
		if (Math.Abs(Velocity.X) > 0)
			animation.Play("run");

		else
			animation.Play("default");

		if (Velocity.Y < 0)
			animation.Play("jump");

		else if (Velocity.Y > 0)
			animation.Play("fall");

	}

	// chases player upon detection
	private void _on_player_detection_body_entered(Node2D body)
	{
		if (body.Name == "player")
		{
			chase = true;
		}
	}

	// explodes upon contact with player
	private void _on_explosion_detection_body_entered(Node2D body)
	{
		if (body.Name == "player")
		{
			chase = false;
			explode = true;
			player.health--;

			// explosion animation
			animation.Play("explode");

			// deletes itself to save resources
			animation.AnimationFinished += QueueFree;

		}
	}

}

















