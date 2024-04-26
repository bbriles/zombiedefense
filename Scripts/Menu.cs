using Godot;
using System;

public partial class Menu : Control
{
	public void OnOnePlayerPressed()
	{
		var gameProperties = GetNode<GameProperties>("/root/GameProperties");
		gameProperties.NumberOfPlayers = 1;
		GetTree().ChangeSceneToFile("res://level1.tscn");
	}
	
	public void OnTwoPlayerPressed()
	{
		var gameProperties = GetNode<GameProperties>("/root/GameProperties");
		gameProperties.NumberOfPlayers = 2;
		GetTree().ChangeSceneToFile("res://level1.tscn");
	}
	
	public void OnQuitPressed()
	{
		GetTree().Quit();
	}
}
