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
		Debug.Print("Destructable Object Destroyed");
		var destroyedSound = GetNode<AudioStreamPlayer>("DestroyedSound");
		destroyedSound.Play();
		destroyedSound.Finished += () => { QueueFree(); };
		var collision = GetNode<CollisionShape2D>("CollisionShape2D");
		collision.Disabled = true;
	}

	public void OnDamaged()
	{
		damageSound.Play();
	}
}
