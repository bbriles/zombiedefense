using Godot;
using System;
using System.Collections.Generic;
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
        queue.Append<string>(soundPath);
    }

    public override void _Process(double delta)
    {
        if (queue.Count() > 0 && available.Count() > 0)
        {
            var player = available.Dequeue();
            player.Stream = (AudioStream)GD.Load(queue.Dequeue());
            player.Play();
        }
    }
}

/*extends Node

var num_players = 8
var bus = "master"

var available = []  # The available players.
var queue = []  # The queue of sounds to play.


func _ready():
    # Create the pool of AudioStreamPlayer nodes.
    for i in num_players:
        var p = AudioStreamPlayer.new()
        add_child(p)
        available.append(p)
        p.finished.connect(_on_stream_finished.bind(p))
        p.bus = bus


func _on_stream_finished(stream):
    # When finished playing a stream, make the player available again.
    available.append(stream)


func play(sound_path):
    queue.append(sound_path)


func _process(delta):
	# Play a queued sound if any players are available.
    if not queue.empty() and not available.empty():
        available[0].stream = load(queue.pop_front())
        available[0].play()
        available.pop_front()*/
