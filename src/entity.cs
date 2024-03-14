using Godot;
using System;

public abstract partial class entity : CharacterBody2D
{
	public float Speed;
	public float JumpVelocity;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	// Get the animation child node
	protected AnimatedSprite2D animation;

	// Initializer for Godot node
	public override void _Ready()
	{	
		Speed = 0f;
		JumpVelocity = 0f;

		animation = null;
	}

	// Physics handler for Godot node; updated every tick
	public override void _PhysicsProcess(double delta)
	{
		Movement(delta);
		Animation();
		MoveAndSlide();
	}


	// Movement handler, including gravity and controls
	protected abstract void Movement(double delta);

	// Animation handler
	protected void Animation()
	{
		if (animation == null) 
			return;

		// Mirrors sprite to match movement
		if (Velocity.X != 0)
			animation.FlipH = Velocity.X < 0;

		// Handles the animation state to match player controls
		if (Math.Abs(Velocity.X) > Speed / 100)
		{
			animation.Play("running");

		}
		else
		{
			animation.Play("default");
		}

		if (Velocity.Y < 0)
		{
			animation.Play("jumping");
		}
		else if (Velocity.Y > 0)
		{
			animation.Play("falling");
		}
	}
}
