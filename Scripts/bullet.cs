using Godot;
using System;

public partial class Bullet : RigidBody2D
{
	public override void _Ready()
	{
		var timer = GetNode<Timer>("Timer");
		timer.Timeout += () => QueueFree();
	}

}
