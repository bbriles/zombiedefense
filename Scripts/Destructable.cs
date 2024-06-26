using Godot;
using System.Diagnostics;

public partial class Destructable : Node2D
{
	[Export] string damageSoundPath;
	[Export] string destroyedSoundPath;

	Health health;

    public override void _Ready()
    {
        health = GetNode<Health>("Health");

		health.Damaged += OnDamaged;
        health.HealthDepleted += OnDestruction;
    }
    public void OnDestruction()
	{
		Debug.Print("Destructable Object Destroyed");

		var audioStreamManager = GetNode<AudioStreamManager>("/root/AudioStreamManager");
		audioStreamManager.Play(destroyedSoundPath);

		QueueFree();
	}

	public void OnDamaged()
	{
		var audioStreamManager = GetNode<AudioStreamManager>("/root/AudioStreamManager");
		audioStreamManager.Play(damageSoundPath);
	}
}
