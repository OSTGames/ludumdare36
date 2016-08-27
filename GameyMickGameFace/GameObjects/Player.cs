﻿using GameyMickGameFace.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameyMickGameFace.GameObjects
{
    public enum PlayerAnimationState
    {
        Walking, Standing, Jummping, Falling
    }

    public class Player
    {
        public int Heath { get; set; }
        public string Name { get; set; }
       // public Vector2 Position { get; set; }

        public Body PhysicsBody { get; set; }

        public int PlayerNumber { get; set; }
        public GamePadState PreviousState { get; set; }
        public GamePadState PreviousPadState { get; set; }
        public KeyboardState PreviousKeyState { get; set; }

        public PlayerAnimationState AnimationState { get; set; }
        public List<Body> DetectionPhysicsBodies;


        public Player()
        {
            AnimationState = PlayerAnimationState.Standing;
            PhysicsBody = new Body(new Point(100, 100), 95, 86, 0, 100,.85f, this);
            PhysicsBody.reactsToCollision = false;

            // TODO Fix realtive positioning
            // TODO Draw these boxes
            DetectionPhysicsBodies = new List<Body>();
            Body leftDetectionBody = new Body(new Point(0, 0), 95, 86, 0, 100, .85f, this);
            leftDetectionBody.parentOffset = new Vector2(100, 100);
            DetectionPhysicsBodies.Add(leftDetectionBody);
            Body rightDetectionBody = new Body(new Point(0, 0), 95, 86, 0, 100, .85f, this);
            rightDetectionBody.parentOffset = new Vector2(200, 200);
            DetectionPhysicsBodies.Add(rightDetectionBody);
        }

        public void Update(GameTime time)
        {
            GamePadState currentPadState = GamePad.GetState(PlayerNumber);
            KeyboardState currentState = Keyboard.GetState();

            if(currentPadState.DPad.Right == ButtonState.Pressed || (currentState.IsKeyDown(Keys.Right)) || (currentState.IsKeyDown(Keys.D)))
            {
                AnimationState = PlayerAnimationState.Walking;
                PhysicsBody.AddVelocity( new Vector2(100, 0));
            }
            else if (currentPadState.DPad.Left == ButtonState.Pressed || (currentState.IsKeyDown(Keys.Left)) || (currentState.IsKeyDown(Keys.A)))
            {
                AnimationState = PlayerAnimationState.Walking;
                PhysicsBody.AddVelocity(new Vector2( - 100, 0));
            }
            else
            {
                AnimationState = PlayerAnimationState.Standing;
            }

            if (currentPadState.DPad.Down == ButtonState.Pressed || (currentState.IsKeyDown(Keys.Down)) || (currentState.IsKeyDown(Keys.S)))
            {
                AnimationState = PlayerAnimationState.Standing;
                PhysicsBody.AddVelocity( new Vector2(0,  100));
            }
            else if (currentPadState.DPad.Up == ButtonState.Pressed || (currentState.IsKeyDown(Keys.Up)) || (currentState.IsKeyDown(Keys.W)))
            {
                AnimationState = PlayerAnimationState.Standing;
                PhysicsBody.AddVelocity(new Vector2(0, - 100));
            }

            PreviousPadState = currentPadState;
            PreviousKeyState = currentState;

            UpdateBodyPhysicsDetection();
        }

        public void Draw(GameTime time, SpriteBatch batch)
        {
            if (AnimationState == PlayerAnimationState.Walking)
            {
                Media.Animations.PlayerWalk.NextFrame(time);
                batch.Draw(Media.Animations.PlayerWalk.Frame, PhysicsBody.Position, Color.White);
            }
            else
            {
                Media.Animations.PlayerIdel.NextFrame(time);
                batch.Draw(Media.Animations.PlayerIdel.Frame, PhysicsBody.Position, Color.White);
            }
        }

        public void UpdateBodyPhysicsDetection()
        {
            foreach(Body body in DetectionPhysicsBodies)
            {
                body.MotionPhysicsBody = new Rectangle((int)(PhysicsBody.Position.X + body.parentOffset.X), 
                    (int)(PhysicsBody.Position.Y + body.parentOffset.Y), body.width, body.height);
            }
        }
    }
}
