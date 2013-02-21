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
    ///  A basic implementation of a weapon inheriting from Item.
    ///  
    /// As more weapons are added, the specific logic of the weapon should be
    /// moved to a subclass and this should be a parent of all weapons.
    /// </summary>
    class Weapon : Item
    {
        /// <summary>
        /// The rate of fire of the gun as the number of shots per second.
        /// </summary>
        protected float fireRate;

        public float FireRate
        {
            get { return fireRate; }
            set { fireRate = value; }
        }

        /// <summary>
        /// The current amount of ammunition available to the player.
        /// </summary>
        protected int currentAmmo;

        public int CurrentAmmo
        {
            get { return currentAmmo; }
            set { currentAmmo = value; }
        }

        /// <summary>
        /// The total amount of ammunition available to the player.
        /// </summary>
        protected int totalAmmo;

        public int TotalAmmo
        {
            get { return totalAmmo; }
            set { totalAmmo = value; }
        }

        /// <summary>
        /// The amount of time since the last shot was fired
        /// </summary>
        private TimeSpan lastShot;

        /// <summary>
        /// Initializes key values of a weapon.
        /// </summary>
        public Weapon()
        {
            lastShot = TimeSpan.Zero;
            fireRate = 5;
            totalAmmo = 10;
            currentAmmo = totalAmmo;
        }

        /// <summary>
        /// Shoot the weapon. That's what this game is really about, right?
        /// 
        /// Also, we need to rethink the way that shooting works right now. Eventually, I think that the
        /// Character Entity class needs an aiming point. This makes it so both Enemies and Players can use
        /// weapons.
        /// </summary>
        /// <param name="gameReference">A reference so we can see where everything is.</param>
        /// <param name="holder">A reference to the character holding the weapon.</param>
        public override void activate(Pantheon gameReference, CharacterEntity holder)
        {
            base.activate(gameReference, holder);

            //Shoot when the cool down has lasted long enough.
            if(lastShot.CompareTo(TimeSpan.Zero) <= 0 && currentAmmo > 0)
            {
                shootABullet(gameReference, holder);
                lastShot = TimeSpan.FromMilliseconds(1000/ fireRate);
            }
        }

        /// <summary>
        /// Updates the weapon, taking care for cooldown and other time sensitive functions.
        /// </summary>
        /// <param name="gameTime">The current game time.</param>
        /// <param name="gameReference">A reference to the entire game.</param>
        public override void Update(GameTime gameTime, Pantheon gameReference)
        {
            if (lastShot.CompareTo(TimeSpan.Zero) > 0)
            {
                lastShot = lastShot.Subtract(gameTime.ElapsedGameTime);
            }
        }

        /// <summary>
        /// Shoots a bullet.
        /// </summary>
        /// <param name="gameReference">A reference to the entire game thiny.</param>
        /// <param name="holder">A reference to the holder character.</param>
        private void shootABullet(Pantheon gameReference, CharacterEntity holder)
        {
            Vector2 cursorLocation = gameReference.controlManager.actions.CursorPosition;
            cursorLocation.X += holder.Location.X - gameReference.GraphicsDevice.Viewport.Width / 2; // + offset.X // Whenever we figure out how to do this...
            cursorLocation.Y += holder.Location.Y - gameReference.GraphicsDevice.Viewport.Height / 2; // + offset.Y // Whenever we figure out how to do this...

            float angle = (float)Math.Atan2(cursorLocation.Y - holder.Location.Y, cursorLocation.X - holder.Location.X);
            double randomDeviation = new Random().NextDouble();
            float randomAngle = (float)(angle + (randomDeviation * .1) - .05);
            // Max deviaion of 1 * .01
            // -.05 to center the deviation around the laser

            //Vector2 velocity = new Vector2(25 * (float)Math.Cos(randomAngle), 25 * (float)Math.Sin(randomAngle));
            //Bullet bullet = new Bullet(holder.Location, velocity);
            Bullet bullet = new Bullet(holder.Location, 25, angle, gameReference);
            bullet.Load(gameReference.Content);

            gameReference.currentLevel.addList.Add("bullet_" + Bullet.NextId, bullet);

            //Drain a bullet from the current ammo
            currentAmmo--;
        }
    }
}
