using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	public int Speed = 300;

	public void GetInput()
	{
		Vector2 inputDirection = Input.GetVector("p1_left", "p1_right", "p1_up", "p1_down");
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
}
