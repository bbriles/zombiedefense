using Godot;
using System;

public partial class Gun : Node2D
{
	[Export] int playerNumber = 1;
	[Export] PackedScene bulletScene;
	[Export] float bulletSpeed = 600f;
	[Export] float bulletsPerSecond = 5f;
	[Export] float damage = 30f;

	float fireRate;
	float timeToFire = 0f;

	AudioStreamPlayer shootSound;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		fireRate = 1 / bulletsPerSecond;

		shootSound = GetParent().GetNode<AudioStreamPlayer>("GunShotSound");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		bool shootPressed = false;

		if ((playerNumber == 1 && Input.IsActionJustPressed("p1_shoot")) || (playerNumber == 2 && Input.IsActionJustPressed("p2_shoot")))
		{
			shootPressed = true;
		}

		if (shootPressed && timeToFire > fireRate)
		{
			var bullet = bulletScene.Instantiate<RigidBody2D>();

			bullet.Rotation = GlobalRotation;
			bullet.GlobalPosition = GlobalPosition;
			bullet.LinearVelocity = bullet.Transform.X * bulletSpeed;

			GetTree().Root.AddChild(bullet);

			shootSound.Play();

			timeToFire = 0f;
		}
		else
		{
			timeToFire += (float)delta;
		}
	}
}
