using Godot;
using System;
using tfj.exploudEngine;

public class Main : Node
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	[Export]
	public PackedScene Mob;
	private int _Score;
	private Random _random = new Random();
	private eSound gameoversound;
	private eInstance gameoverinstance;
	private eMusic gamemusic;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		gameoversound = globals.engine.loadSound("sounds/gameover.mp3");
		gamemusic= globals.engine.loadMusic("sounds/34 Docaty Mountain Railroad.mp3");
	}

	private float RandRange(float min, float max)
	{
		return (float)_random.NextDouble() * max - min + min;
	}

	public void game_over()
	{
		GetNode<Timer>("MobTimer").Stop();
		GetNode<Timer>("ScoreTimer").Stop();
		GetNode<Hud>("Hud").ShowGameOver();
		gameoverinstance = gameoversound.play(0, loopMode.noLoop);
		gamemusic.stop();
			}

	public void new_game()
	{
		_Score = 0;
		GetNode<Timer>("StartTimer").Start();
		var hud = GetNode<Hud>("Hud");
		hud.UpdateScore(_Score);
		hud.ShowMessage("preparate!");

		var player = GetNode<Player>("Player");
		var startPosition = GetNode<Position2D>("StartPosition");
		player.Start(startPosition.Position);
		gamemusic.play();
	}

	public void _on_StartTimer_timeout()    
	{
		GetNode<Timer>("MobTimer").Start();
		GetNode<Timer>("ScoreTimer").Start();
	}

	public void _on_ScoreTimer_timeout()
	{
		_Score++;
		GetNode<Hud>("Hud").UpdateScore(_Score);
	}

	public void _on_MobTimer_timeout()
	{
		var mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");
		mobSpawnLocation.SetOffset(_random.Next());
		var mobInstance = (RigidBody2D)Mob.Instance();
		AddChild(mobInstance);
		var player = GetNode<Player>("Player");
		 player.Connect("Hit", mobInstance, "_on_players_hit");

		float direction = mobSpawnLocation.Rotation + Mathf.Pi / 2;
		mobInstance.Position = mobSpawnLocation.Position;
		direction += RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
		mobInstance.Rotation = direction;
		mobInstance.SetLinearVelocity(new Vector2(RandRange(150f, 250f), 0).Rotated(direction));
	}
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
