using Godot;
using Godot.Collections;
using System;
using System.Diagnostics;

public partial class Level : Node
{
	[Signal] public delegate void GameOverEventHandler();
	public Array<Player> Players = new Array<Player>();

	public override void _Ready()
	{
		//UpdatePlayerList();
	}

	public void UpdatePlayerList()
	{
		var playerList = GetTree().GetNodesInGroup("player");

		Players = new Array<Player>();
		foreach (var node in playerList)
		{
			if(IsInstanceValid(node))
			{
				Players.Add((Player)node);
			}
			
		}
		Debug.Print("Updated Player List. # of Players: " + Players.Count);
	}

	public void AddPlayer(Player player)
	{
		Players.Add(player);
		Debug.Print("Player Added");
	}

	public void RemovePlayer(Player player)
	{
		Players.Remove(player);
		Debug.Print("Removed Player. # of Players: " + Players.Count);
		if (Players.Count <= 0)
		{
			EmitSignal(SignalName.GameOver);
		}
	}
}
