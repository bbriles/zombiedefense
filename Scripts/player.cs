using Godot;
using System;
using System.Diagnostics;

public partial class Player : CharacterBody2D
{
	Level level;
	Health health;
	[Export] int playerNumber = 1;
	[Export] public int Speed = 300;

	Activatable activateTarget = null;
	bool withinActivateRange = false;

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

		// Check for activation
		if ((playerNumber == 1 && Input.IsActionJustPressed("p1_activate")) || (playerNumber == 2 && Input.IsActionJustPressed("p2_activate")))
		{
			Activate();
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

	public void OnActivateBodyRangeEntered(Node2D body)
	{
		if (body.IsInGroup("activatable"))
        {
            activateTarget = (Activatable)body;
            Debug.Print("Activate Target in range");
            withinActivateRange = true;
        }
	}

	public void OnActivateBodyRangeExit(Node2D body)
	{
		if (body.IsInGroup("activatable"))
        {
            activateTarget = null;
            Debug.Print("Activate Target out of range");
            withinActivateRange = false;
        }
	}

	public void Activate()
	{
		if (activateTarget != null)
		{
			Debug.Print("Activating!");
			activateTarget.Activate(this);
		}
	}
}
