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
    /// An entity designed to trigger and event when another entity collides with it.
    /// </summary>
    class Trigger : Entity
    {
        /// <summary>
        /// Indicates if the trigger is active.
        /// </summary>
        public bool Active
        {
            get;
            set;
        }

        /// <summary>
        /// The constructor for a Trigger entity. Note that the bounding box and drawing box are the same since the Trigger will not be drawn. The location 
        /// is also specified by the location box. The action point is defaultly located in the center of the trigger.
        /// </summary>
        /// <param name="locationBox"></param>
        public Trigger(Rectangle locationBox, Pantheon gameReference)
            : base(
                new Vector2(locationBox.X + locationBox.Width/2, locationBox.Y + locationBox.Height/2),
                locationBox,
                new Rectangle(0, 0, locationBox.Width, locationBox.Height))
        {
            Console.WriteLine("Trigger created drawn at (" + this.DrawingBox.X + ", " + this.DrawingBox.Y + ")");
            characteristics.Add("Triggerable");

            HandleEvent bunnyHandler = bunnies;
            gameReference.EventManager.register("TriggerEventWithBunnies!!!", bunnyHandler);
        }

        public override void Load(ContentManager contentManager)
        {
            base.Load(contentManager);

            this.sprite = new Sprite();
        }

        public override void Update(GameTime gameTime, Pantheon gameReference)
        {
            base.Update(gameTime, gameReference);
        }

        public static void bunnies(Event eventInfo)
        {
            Console.WriteLine("BUNNIES!!!!!!!!!!");
        }
    }
}