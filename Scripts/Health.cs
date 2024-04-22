using Godot;
using System;

public partial class Health : Node2D
{
	[Export] float maxHealth = 100f;
	float health;

	public override void _Ready()
	{
		health = maxHealth;
	}

	public void Damage(float damage)
	{
		health -= damage;

		if (health <= 0)
		{
			GetParent().QueueFree();
		}
	}

}
