﻿using GameyMickGameFace.Media;
using GameyMickGameFace.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameyMickGameFace.GameObjects
{
    public class PowerUp
    {
        public string Name { get; set; }
        public Texture2D Image { get; set; }
        public Body PhysicsBody { get; set; }
        Audio se;

        public PowerUp()
        {
            PhysicsBody = new Body(new Point(200, 200), 95, 86, 0, 100, .85f, this);
            se = new Audio();
        }

        public void OnPickup(Player effectedPlayer)
        {
            //do something
            Media.Audio.damage.Play();
        }

        public void Draw(GameTime time, SpriteBatch batch)
        {
            batch.Draw(Image, PhysicsBody.Position, Color.White);
        }

        public void Update(GameTime time)
        {

        }
    }
}
