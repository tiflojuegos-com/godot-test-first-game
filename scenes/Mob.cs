using Godot;
using System;
using tfj.exploudEngine;

public class Mob : RigidBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Export]
    public int MinSpeed;
    [Export]
    public int MaxSpeed;
    private String[] _mobTypes = { "walk", "swim", "fly" };
    private eSound voicesound;
    private eInstance voiceinstance;
        static private Random _random = new Random();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        voicesound = globals.engine.loadSound("sounds/" + _mobTypes[_random.Next(0, _mobTypes.Length)]+".mp3");
        voiceinstance = voicesound.play3d(Position.x, 0, Position.y, loopMode.simpleLoop);
        voiceinstance.maxDistance = 5000;
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
        voiceinstance.x = Position.x;
        voiceinstance.z = Position.y;
  }

    public void _on_Visibility_screen_exited()
    {
        QueueFree();
        voiceinstance.stop();
    }

}
