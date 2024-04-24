using Godot;
using System;
using System.Diagnostics;

public partial class Player : CharacterBody2D
{
	Level level;
	Health health;
	[Export] int playerNumber = 1;
	[Export] public int Speed = 300;

    public override void _Ready()
    {
        level = GetTree().Root.GetNode<Level>("Level");
		health = GetChild<Health>(0);

		health.HealthDepleted += OnDeath;

		// Setup signal events
		TreeExited += OnDeath;
    }

    public void GetInput()
	{
		Vector2 inputDirection;
		if (playerNumber == 1) 
		{
		 	inputDirection = Input.GetVector("p1_left", "p1_right", "p1_up", "p1_down");
		}
		else 
		{
			inputDirection = Input.GetVector("p2_left", "p2_right", "p2_up", "p2_down");
		}
        Velocity = inputDirection * Speed;

		if (Velocity.X != 0 || Velocity.Y != 0)
		{
			Rotation = Velocity.Angle();
		}
	}

	
	public override void _PhysicsProcess(double delta)
	{
		GetInput();
		MoveAndSlide();
	}

	public void OnDeath()
	{
		Debug.Print("Player " + playerNumber + " Has Died");
		level.RemovePlayer(this);
		QueueFree();
	}
}
