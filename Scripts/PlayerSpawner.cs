using Godot;
using System;
using System.Diagnostics;

public partial class PlayerSpawner : Node2D
{
	[Export] PackedScene player1Scene;
	[Export] PackedScene player2Scene;
	[Export] Node2D[] spawnPoints;

	Level level;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		level = GetTree().Root.GetNode<Level>("Level");

		var gameProperties = GetNode<GameProperties>("/root/GameProperties");
		
		var player = (Player)player1Scene.Instantiate();
		player.GlobalPosition = spawnPoints[0].GlobalPosition;
		GetTree().Root.AddChild(player);
		level.AddPlayer(player);

		if (gameProperties.NumberOfPlayers > 1)
		{
			player = (Player)player2Scene.Instantiate();
			player.GlobalPosition = spawnPoints[1].GlobalPosition;
			GetTree().Root.AddChild(player);
			level.AddPlayer(player);
		}
	}
}
