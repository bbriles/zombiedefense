using Godot;
using System;
using System.Collections;
using System.Diagnostics;

public partial class Door : StaticBody2D
{
	Health health;

    public override void _Ready()
    {
        health = GetNode<Health>("Health");

        health.HealthDepleted += OnDestruction;
    }
    public void OnDestruction()
	{
		Debug.Print("Door Destroyed");
		QueueFree();
	}
}
