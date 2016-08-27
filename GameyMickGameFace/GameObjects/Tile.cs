﻿using GameyMickGameFace.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameyMickGameFace.GameObjects
{
    public class Tile
    {
        public Body Body;

        public Tile(Point position, int width, int height, float restitution, int mass)
        {
            Body = new Body(position, width, height, restitution, mass);
        }

        public void Draw(GameTime time, SpriteBatch batch)
        {
            batch.Draw(Media.Textures.PlayerTemp, Body.PhysicsBody.Location.ToVector2(), Color.White);
        }
    }
}
