using Godot;
using System;
using System.Diagnostics;

public partial class Zombie : CharacterBody2D
{
    Player player;

    [Export] float speed = 200f;
    [Export] float damage = 10f;
    [Export] float attacksPerSecond = 2;

    float attackSpeed;
    float timeToAttack = 0f;
    bool withinAttackRange = false;

    public override void _Ready()
    {
        attackSpeed = 1 / attacksPerSecond;

        // TODO: Handle multiple players
        player = (Player)GetTree().GetFirstNodeInGroup("player");
    }

    public override void _PhysicsProcess(double delta)
    {
        if (player != null)
        {
            LookAt(player.Position);

            var direction = (player.GlobalPosition - GlobalPosition).Normalized();
            Velocity = direction * speed;
        }
        else
        {
            Velocity = Vector2.Zero;
        }

        MoveAndSlide();
    }

    public override void _Process(double delta)
    {
        if (withinAttackRange && timeToAttack <= 0)
        {
            Attack();
            timeToAttack = attackSpeed;
        }
        else 
        {
            timeToAttack -= (float)delta;
        }
    }

    public void Attack()
    {
        Debug.Print("Attack Player");
    }

    public void OnAttackRangeBodyEnter(Node2D body)
    {
        if (body.IsInGroup("player"))
        {
            Debug.Print("Player in range");
            withinAttackRange = true;
        }
    }

    public void OnAttackRangeBodyExit(Node2D body)
    {
        if (body.IsInGroup("player"))
        {
            Debug.Print("Player out of range");
            withinAttackRange = false;
            timeToAttack = attackSpeed;
        }
    }
}
