using Godot;
using System;
using System.Diagnostics;

public partial class Health : Node2D
{
	[Signal] public delegate void HealthDepletedEventHandler();
	[Signal] public delegate void DamagedEventHandler();
	[Signal] public delegate void HealthUpdatedEventHandler(float health);

	[Export] public float MaxHealth = 100f;
	float health;

	public override void _Ready()
	{
		health = MaxHealth;
	}

	public void Damage(float damage)
	{
		health -= damage;
		EmitSignal(SignalName.Damaged);
		EmitSignal(SignalName.HealthUpdated,health);

		if (health <= 0)
		{
			EmitSignal(SignalName.HealthDepleted);
		}
	}

}
