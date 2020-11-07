using Godot;
using System;
using tfj.exploudEngine;

public class Player : Area2D
{
    [Signal]
    public delegate void Hit();

    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    [Export]
    public int Speed;
    private Vector2 _screenSize;
    private float distance;
private eSound movesound;
    private eInstance moveinstance;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _screenSize = GetViewport().GetSize();
        movesound = globals.engine.loadSound("sounds/move.ogg");
        Hide();
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
        var velocity = new Vector2();
        if(Input.IsActionPressed("ui_right"))
        {
            velocity.x += 1;
        }

        if(Input.IsActionPressed("ui_left"))
        {
            velocity.x -= 1;
        }

        if(Input.IsActionPressed("ui_down"))
        {
            velocity.y += 1;
        }

        if(Input.IsActionPressed("ui_up"))
        {
            velocity.y -= 1;
        }

        if(velocity.Length()>0)
        {
            velocity = velocity.Normalized() * Speed;
        }
        Position += velocity * delta;
        distance += velocity.Length();
        if(distance>=4500)
        {
            moveinstance = movesound.play(0, loopMode.noLoop);
            distance = 0;
        }
        Position = new Vector2(x: Mathf.Clamp(Position.x, 0, _screenSize.x), y: Mathf.Clamp(Position.y, 0, _screenSize.y));
        globals.engine.listener.x = Position.x;
        globals.engine.listener.z = Position.y;
  }

public void _on_Player_body_entered(PhysicsBody2D body)
    {
        Hide();
        EmitSignal("Hit");
        GetNode<CollisionShape2D>("CollisionShape2D").Disabled = true;
    }

    public void Start(Vector2 pos)
    {
        Position = pos;
        Show();
        GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
    }

}
