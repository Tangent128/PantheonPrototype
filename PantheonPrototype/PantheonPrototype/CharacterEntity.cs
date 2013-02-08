﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace PantheonPrototype
{
    /// <summary>
    /// The facing directions.
    /// </summary>
    public enum Direction
    {
        forward, forwardLeft, Left, backLeft, back, backRight, Right, forwardRight
    }

    /// <summary>
    /// The character entity provides the properties and methods
    /// used for a character in the game. It inherits the basic
    /// traits of an Entity and adds things such as velocity and healing.
    /// </summary>
    class CharacterEntity : Entity
    {
        /// <summary>
        /// The total armor of the character.
        /// </summary>
        protected int totalArmor;

        public int TotalArmor
        {
            get { return totalArmor; }
            set { totalArmor = value; }
        }

        /// <summary>
        /// The current armor of the character.
        /// </summary>
        protected int currentArmor;

        public int CurrentArmor
        {
            get { return currentArmor; }
            set { currentArmor = value; }
        }

        /// <summary>
        /// The total amount of shield energy available when fully charged.
        /// </summary>
        protected int shieldCapacity;

        public int ShieldCapacity
        {
            get { return shieldCapacity; }
            set { shieldCapacity = value; }
        }

        /// <summary>
        /// The current shield strength for the character.
        /// </summary>
        protected int shieldStrength;

        public int ShieldStrength
        {
            get { return shieldStrength; }
            set { shieldStrength = value; }
        }

        /// <summary>
        /// Flag indicating if the shield is currently on.
        /// </summary>
        protected bool shieldOn;

        public bool ShieldOn
        {
            get { return shieldOn; }
            set { shieldOn = value; }
        }

        /// <summary>
        /// The velocity of the character.
        /// </summary>
        protected Vector2 velocity;

        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        /// <summary>
        /// The direction in which the character is facing.
        /// </summary>
        protected Direction facing;

        public Direction Facing
        {
            get { return facing; }
            set { facing = value; }
        }

        /// <summary>
        /// The constuctor... does nothing except call the parent constructor.
        /// </summary>
        public CharacterEntity(): base()
        {
        }

        /// <summary>
        /// Loads any assets the entity may or may not need.
        /// </summary>
        /// <param name="contentManager">The intialized content manager that will be used to load the asset information.</param>
        /// <exception cref="ContentLoadException">Thrown when the content manager is unable to load the player sprite.</exception>
        public override void Load(ContentManager contentManager)
        {
            base.Load(contentManager);
        }

        /// <summary>
        /// Update the character class.
        /// </summary>
        /// <param name="gameTime">The game time object for letting you know how old you've gotten since starting the game.</param>
        /// <param name="gameReference">A deeper game reference to the game reference of doom.</param>
        public override void Update(GameTime gameTime, Pantheon gameReference)
        {
            base.Update(gameTime, gameReference);

            prevLocation = location;

            //Move the player by velocity
            location.X += (int)velocity.X;
            location.Y += (int)velocity.Y;

            //If the shield is on, drain it
            if (shieldOn)
            {
                shieldStrength--;
            }
            else //Otherwise, charge it
            {
                shieldStrength++;
            }
        }

        /// <summary>
        /// Draw the character class... and override the default behavior...
        /// take over the entity. Let none of it survive... except all of it.
        /// </summary>
        /// <param name="canvas">An initialized SpriteBatch.</param>
        public override void Draw(SpriteBatch canvas)
        {
            base.Draw(canvas);
        }
    }
}
