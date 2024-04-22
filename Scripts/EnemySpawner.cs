using Godot;
using System;

public partial class EnemySpawner : Node2D
{
	[Export] PackedScene enemyScene;
	[Export] Node2D[] spawnPoints;
	[Export] float enemiesPerSecond = 2f;

	float spawnRate;
	float timeTillSpawn = 0f;

	
	public override void _Ready()
	{
		spawnRate = 1 / enemiesPerSecond;
	}

    public override void _Process(double delta)
    {
        if (timeTillSpawn > spawnRate)
		{
			Spawn();
			timeTillSpawn = 0f;
		}
		else
		{
			timeTillSpawn += (float)delta;
		}
    }

	private void Spawn()
	{
		RandomNumberGenerator rng = new RandomNumberGenerator();

		var location = spawnPoints[rng.Randi() % spawnPoints.Length].GlobalPosition;
		var enemy = (Node2D)enemyScene.Instantiate();
		enemy.GlobalPosition = location;
		GetTree().Root.AddChild(enemy);

	}
}
