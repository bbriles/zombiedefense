using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public partial class AudioStreamManager : Node
{
    Queue<AudioStreamPlayer> available;
    Queue<string> queue;

    const int numberOfPlayers = 8;

    public override void _Ready()
    {
        available = new Queue<AudioStreamPlayer>();
        for (var i = 0; i < numberOfPlayers; i++)
        {
            var player = new AudioStreamPlayer();
            AddChild(player);
            available.Enqueue(player);
            player.Finished += () => OnStreamFinished(player);
        }

        queue = new Queue<string>();
    }


    private void OnStreamFinished(AudioStreamPlayer player)
    {
        available.Enqueue(player);
    }

    public void Play(string soundPath)
    {
        Debug.Print("Adding \"" + soundPath + "\" to audio queue");
        queue.Enqueue(soundPath);
    }

    public override void _Process(double delta)
    {
        if (queue.Count() > 0 && available.Count() > 0)
        {
            Debug.Print("Playing Audio");
            var player = available.Dequeue();
            player.Stream = (AudioStream)GD.Load(queue.Dequeue());
            player.VolumeDb -= 5;
            player.Play();
        }
    }
}
