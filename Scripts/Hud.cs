using Godot;
using System;
using System.Diagnostics;

public partial class Hud : Control
{
    [Export] TextureProgressBar healthP1;
    [Export] TextureProgressBar healthP2;

    Player player1;
    Player player2;

    public void OnPlayerInitiated(Player player)
    {
        if (player.PlayerNumber == 1)
        {
            player.Health.HealthUpdated += OnPlayer1HealthUpdate;
            player1 = player;
        }
        else if (player.PlayerNumber == 2)
        {
            var container =  (Container)GetNode("P2_Info");
            container.Visible = true;

            player.Health.HealthUpdated += OnPlayer2HealthUpdate;
            player2 = player;
        }
    }


    void OnPlayer1HealthUpdate(float health)
    {
        healthP1.Value = health * 100 / player1.Health.MaxHealth;
    }

    void OnPlayer2HealthUpdate(float health)
    {
        healthP2.Value = health * 100 / player2.Health.MaxHealth;
    }
}
