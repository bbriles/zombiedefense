using Godot;
using System;
using System.Diagnostics;

public partial class Health : Node2D
{
	[Signal] public delegate void HealthDepletedEventHandler();
	[Signal] public delegate void DamagedEventHandler();

	[Export] float maxHealth = 100f;
	float health;

	public override void _Ready()
	{
		health = maxHealth;
	}

	public void Damage(float damage)
	{
		Debug.Print("Taking Damage");
		health -= damage;
		EmitSignal(SignalName.Damaged);

		if (health <= 0)
		{
			EmitSignal(SignalName.HealthDepleted);
		}
	}

}
