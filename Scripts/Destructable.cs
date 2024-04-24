using Godot;
using System.Diagnostics;

public partial class Destructable : Node2D
{
	Health health;
	AudioStreamPlayer damageSound;

    public override void _Ready()
    {
        health = GetNode<Health>("Health");
		damageSound = GetNode<AudioStreamPlayer>("DamageSound");

		health.Damaged += OnDamaged;
        health.HealthDepleted += OnDestruction;
    }
    public void OnDestruction()
	{
		Debug.Print("Door Destroyed");
		QueueFree();
	}

	public void OnDamaged()
	{
		damageSound.Play();
	}
}
