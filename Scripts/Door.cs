using Godot;
using System;
using System.Diagnostics;

public partial class Door : Activatable
{
    public bool IsOpen = false;
    public override void Activate(Node2D activator)
    {
        
        if(IsOpen)
        {
            Debug.Print("Closing a door");
            // Unrotate the door
            Rotate(-(float)Math.PI/2);

            // Turn on collision
            //var collisionShape = GetNode<CollisionShape2D>("CollisionShape2D");
            //collisionShape.Disabled = false;

            IsOpen = false;
        }
        else
        {
            Debug.Print("Opening a door");
            // Rotate the door
            Rotate((float)Math.PI/2);
            
            // Turn off collision
            //var collisionShape = GetNode<CollisionShape2D>("CollisionShape2D");
            //collisionShape.Disabled = true;

            IsOpen = true;
        }
    }
}
