using Godot;
using System;

public class Hud : CanvasLayer
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Signal]
    public delegate void StartGame();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GetNode<Button>("StartButton").Show();
    }

    public void ShowMessage(string text)
    {
        var messageLabel = GetNode<Label>("MessageLabel");
        messageLabel.Text = text;
        messageLabel.Show();

        GetNode<Timer>("MessageTimer").Start();
    }

    async public void ShowGameOver()
    {
        ShowMessage("fin de juego! cache te puso los dedos en los ojos... ");

        var messageTimer = GetNode<Timer>("MessageTimer");
        await ToSignal(messageTimer, "timeout");
        var messageLabel = GetNode<Label>("MessageLabel");
        messageLabel.Text = "esquiva a los\nCaches!";
        messageLabel.Show();
        GetNode<Button>("StartButton").Show();
    }

    public void UpdateScore(int score)
    {
        GetNode<Label>("ScoreLabel").Text = score.ToString();
    }

    public void _on_StartButton_pressed()
    {
        GetNode<Button>("StartButton").Hide();
        EmitSignal("StartGame");
    }

    public void _on_MessageTimer_timeout()
    {
        GetNode<Label>("MessageLabel").Hide();
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
        {
        
        Button startButton = GetNode<Button>("StartButton");
        
        if (Input.IsActionPressed("ui_accept") && startButton.Visible)
        {
            
            startButton.EmitSignal("pressed");
        }
        }
}
