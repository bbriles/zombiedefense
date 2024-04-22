using Godot;
using System;

public partial class Gun : Node2D
{
	[Export] PackedScene bulletScene;
	[Export] float bulletSpeed = 600f;
	[Export] float bulletsPerSecond = 5f;
	[Export] float damage = 30f;

	float fireRate;
	float timeToFire = 0f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		fireRate = 1 / bulletsPerSecond;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("p1_shoot") && timeToFire > fireRate)
		{
			var bullet = bulletScene.Instantiate<RigidBody2D>();

			bullet.Rotation = GlobalRotation;
			bullet.GlobalPosition = GlobalPosition;
			bullet.LinearVelocity = bullet.Transform.X * bulletSpeed;

			GetTree().Root.AddChild(bullet);

			timeToFire = 0f;
		}
		else
		{
			timeToFire += (float)delta;
		}
	}
}
