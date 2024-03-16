using Godot;
using System;

public abstract partial class Entity : CharacterBody2D
{
	public float speed;
	public float jumpVelocity;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	// Get the animation child node
	protected AnimatedSprite2D animation;

	// Initializer for Godot node when it enters scene
	public override void _Ready()
	{
		speed = 0f;
		jumpVelocity = 0f;

		animation = null;
	}

	// Physics handler which updates every tick
	public override void _PhysicsProcess(double delta)
	{
		Movement(delta);
		MoveAndSlide();

		Animation();
	}

	// Handles gravity and controls
	protected abstract void Movement(double delta);

	// Updates sprite animation according to movement
	protected abstract void Animation();
}
