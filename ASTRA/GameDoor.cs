﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASTRA
{
    class GameDoor : GameObject, IDrawable, ICollidable
    {
        // weather or not to draw the door
        /// <summary>
        /// tells weather or not to draw door/
        /// </summary>
        internal Listener<bool> Active = new Listener<bool>(true);

        internal Color DrawColor;

        /// <summary>
        /// TODO: This class is temporary for playtesting. We should remove this an place in a more stable structure
        /// </summary>
        /// <param name="position"></param>
        public GameDoor(Vector2 position, Vector2 size, string textureName) : base(position, ComponentOrigin.Center)
        {

            LocalContentManager lcm = LocalContentManager.Shared;

            //TODO: get a default asset. Comment this out if need be.
            Image = lcm.GetTexture(textureName);

            DrawColor = Color.White;
            Active.OnValueChanged += () =>
            {
                if (Active.Value)
                {
                    DrawColor = Color.White;
                }
                else
                {
                    DrawColor = Color.Gray;
                }
            };

            //Size = new Vector2(Image.Width, Image.Height);
            this.Size = size;
        }

        /// <summary>
        /// The collision bounds. In this instance, it takes up the whole width and height of the element.
        /// </summary>
        public Rectangle CollisionBounds
        {
            get
            {
                return new Rectangle(TopLeftCorner.ToPoint(), Size.ToPoint());
            }
        }

        /// <summary>
        /// The image of the player.
        /// </summary>
        public Texture2D Image {get;}

        /// <summary>
        /// Handles all collisions (without updating the other object's status!).
        /// Should be called by a manager class which checks the collision between the player and all other objects.
        /// This way, the player knows how to react when it collides with something, but does not handle checking collision.
        /// </summary>
        /// <param name="other"></param>
        public void Collide(ICollidable other)
        {
            
        }

        /// <summary>
        /// Updates the object. (Does nothing as a wall just kind of... sits there.)
        /// </summary>
        /// <param name="gameTime"></param>
        internal override void Update(GameTime gameTime)
        {
        }

        /// <summary>
        /// Draws out the asset
        /// </summary>
        /// <param name="batch"></param>
        public void Draw(SpriteBatch batch)
        {
            batch.Draw(Image, new Rectangle(TopLeftCorner.ToPoint(), Size.ToPoint()), DrawColor);
            
        }

        public bool CollidesWith(ICollidable other)
        {
            return Active.Value && CollisionBounds.Intersects(other.CollisionBounds);
        }

        public void OpenDoor(object a, EventArgs e) 
        {
            Active.Value = false;
        }

        internal override void Reset()
        {
            Active.Value = true;
        }
    }
}