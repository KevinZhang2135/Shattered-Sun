using Godot;
using System;

public partial class PlayerHealth : Label
{

	private Player player;

	public override void _Ready()
	{
		player = GetNode<Player>("../../player");
	}

	public override void _Process(double delta)
	{
		Text = "Health: " + player.health;
	}
}
