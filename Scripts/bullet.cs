using Godot;
using System;

public partial class Bullet : RigidBody2D
{
	[Export] public float damage = 10f;

	public override void _Ready()
	{
		var timer = GetNode<Timer>("Timer");
		timer.Timeout += () => QueueFree();
	}


	public void OnBodyEntered(Node2D body)
	{
		if (body.IsInGroup("enemy"))
		{
			body.GetNode<Health>("Health").Damage(damage);
		}

		QueueFree();
	}
}
