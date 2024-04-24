using Godot;
using System;
using System.Diagnostics;

public partial class Window : StaticBody2D
{
	Health health;

    public override void _Ready()
    {
        health = GetNode<Health>("Health");

        health.HealthDepleted += OnDestruction;
    }
    public void OnDestruction()
	{
		Debug.Print("Window Destroyed");
		QueueFree();
	}
}

