using Godot;
using System;

public class MobPath : Path2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Curve.AddPoint(new Vector2(0, 0));
        Curve.AddPoint(new Vector2(480, 0));
        Curve.AddPoint(new Vector2(480, 720));
        Curve.AddPoint(new Vector2(0, 720));
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
