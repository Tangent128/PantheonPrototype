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
    /// This is the wrapper class for all information that will go
    /// into the HUD. It will have HUDItems in it, thad can be added
    /// at will and drawn all at once.
    /// </summary>
    class HUD
    {
        /// <summary>
        /// All of the different HUDItems that will need to be drawn.
        /// More or less can be registered.
        /// </summary>
        protected List<HUDItem> hudItems;
        protected ContentManager content;
        protected Texture2D background;
        protected Vector2 HUDcoords;
        protected int SCREEN_WIDTH;
        protected int SCREEN_HEIGHT;

        public HUD(ContentManager Content, int WIDTH, int HEIGHT)
        {
            content = Content;
            SCREEN_WIDTH = WIDTH;
            SCREEN_HEIGHT = HEIGHT;

            background = Content.Load<Texture2D>("HUDbackgroundS");
            hudItems = new List<HUDItem>();
            HUDcoords = new Vector2(20, SCREEN_HEIGHT - background.Height - 20);


            AddItem("ArmorBar", 5, 45);
            AddItem("IndicatorG", 230, 10);
        }

        /// <summary>
        /// This is the method to add more items to the HUD. It will probably have
        /// parameters later, but I'm not sure what yet.
        /// Accepts a string that defines an image in the preloaded content.
        /// </summary>
        public void AddItem(String img, int x, int y)
        {
            hudItems.Add(new HUDItem(content, img, (int)HUDcoords.X + x, (int)HUDcoords.Y + y));
        }

        /// <summary>
        /// The method to update all of the HUDItems' information.
        /// </summary>
        /// <param name="gameTime">The object that holds all the time information.</param>
        public void Update(GameTime gameTime, Level level) 
        {
            PlayerEntity player = (PlayerEntity)(level.Entities["character"]);
            // Set the width of the Armor Bar with respect to the current percent of the player's armor. (Player not implemented yet)
            try
            {
                hudItems[0].Coordinates = new Rectangle(hudItems[0].Coordinates.X, hudItems[0].Coordinates.Y, (int)(hudItems[0].DefaultWidth * ((float)player.CurrentArmor / player.TotalArmor)), hudItems[0].Coordinates.Height);
            }
            catch (DivideByZeroException)
            {
                Console.Write("Total armor is zero!");
            }
        }

        /// <summary>
        /// The method to draw all of the different HUDItems.
        /// </summary>
        /// <param name="spriteBatch">The object that we will use to draw to the screen.</param>
        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.Begin();

            spriteBatch.DrawString(font, "Pantheon Prototype XD", new Vector2(0, 0), Color.Black);
            
            // Draw the Armor Bar (Must be done first due to shape)
            spriteBatch.Draw(hudItems[0].Image, hudItems[0].Coordinates, hudItems[0].Opacity);
            
            // Draw the Background
            spriteBatch.Draw(background, new Rectangle((int)HUDcoords.X, (int)HUDcoords.Y, background.Width, background.Height), Color.White);

            // Draw all the remaining items
            for (int i = 1; i < hudItems.Count; i++)
            {
                spriteBatch.Draw(hudItems[i].Image, hudItems[i].Coordinates, hudItems[i].Opacity);
            }

            spriteBatch.End();
        }
    }
}
