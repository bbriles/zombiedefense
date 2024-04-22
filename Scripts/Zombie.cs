using Godot;
using Godot.Collections;
using System;
using System.Diagnostics;
using System.Linq;

public partial class Zombie : CharacterBody2D
{
    Level level;
    Health health;
    Player currentTarget;

    [Export] float speed = 100f;
    [Export] float damage = 10f;
    [Export] float attacksPerSecond = 2;

    float attackSpeed;
    float timeToAttack = 0f;
    bool withinAttackRange = false;

    public override void _Ready()
    {
        attackSpeed = 1 / attacksPerSecond;

        level = GetTree().Root.GetNode<Level>("Level");
        health = GetChild<Health>(0);

        health.HealthDepleted += OnDeath;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (level != null)
        {
            float minDistance = float.MaxValue;
            Player nearestPlayer = null;
            // find nearest player
            foreach (Player player in level.Players)
            {
                if (IsInstanceValid(player))
                {
                    var distance = GlobalPosition.DistanceSquaredTo(player.GlobalPosition);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        nearestPlayer = player;
                    }
                }
            }
            if(nearestPlayer != null)
            {
                LookAt(nearestPlayer.Position);

                var direction = (nearestPlayer.GlobalPosition - GlobalPosition).Normalized();
                Velocity = direction * speed;
            }
            else
            {
                Velocity = Vector2.Zero;
            }
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
        if(currentTarget != null) {
            currentTarget.GetNode<Health>("Health").Damage(damage);
        }
    }

    public void OnAttackRangeBodyEnter(Node2D body)
    {
        if (body.IsInGroup("player"))
        {
            currentTarget = (Player)body;
            Debug.Print("Player in range");
            withinAttackRange = true;
        }
    }

    public void OnAttackRangeBodyExit(Node2D body)
    {
        if (body.IsInGroup("player"))
        {
            currentTarget = null;
            Debug.Print("Player out of range");
            withinAttackRange = false;
            timeToAttack = attackSpeed;
        }
    }

    public void OnDeath()
    {
        Debug.Print("Zombie Killed");
        QueueFree();
    }
}
