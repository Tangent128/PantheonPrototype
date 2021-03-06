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
        protected GraphicsDevice graphicsDevice;
        protected Texture2D backing;
        protected Texture2D background;
        protected SpriteFont font;
        protected Vector2 HUDcoords;
        protected int SCREEN_WIDTH;
        protected int SCREEN_HEIGHT;
        protected int danger;

        //private String debugString;

        public HUD(GraphicsDevice graphicsDevice, ContentManager Content, int WIDTH, int HEIGHT, SpriteFont font)
        {
            this.graphicsDevice = graphicsDevice;
            this.font = font;

            SCREEN_WIDTH = WIDTH;
            SCREEN_HEIGHT = HEIGHT;

            backing = Content.Load<Texture2D>("HUD/HUDbacking");
            background = Content.Load<Texture2D>("HUD/HUDbackground");
            hudItems = new List<HUDItem>();
            HUDcoords = new Vector2(0, SCREEN_HEIGHT - background.Height - 20);

            danger = 0;

            AddItem("HUD/ArmorBar", 5, 46, Content);
            AddItem("HUD/IndicatorG", 230, 10, Content);
            AddItem("HUD/IndicatorY", 230, 10, Content);
            AddItem("HUD/IndicatorR", 230, 10, Content);
            AddItem("HUD/IndicatorD", 230, 10, Content);
            AddItem("HUD/IndicatorEmpty", 230, 10, Content);
            AddItem("HUD/AmmoDisplay", 215, 5, font);
            AddItem("HUD/ReloadTimer", 162, 35, Content);
            AddItem("HUD/Null", 0, 0, Content);
        }

        /// <summary>
        /// This is the method to add more items to the HUD. 
        /// Accepts a string that defines an image in the preloaded content,
        /// X and Y HUD coordinates, and a content manager.
        /// </summary>
        public void AddItem(String img, int x, int y, ContentManager content)
        {
            hudItems.Add(new HUDItem(content, img, (int)HUDcoords.X + x, (int)HUDcoords.Y + y));
        }

        /// <summary>
        /// An overload funtion to just draw a string instead of an image.
        /// Accepts a string that defines an image in the preloaded content and
        /// X and Y HUD coordinates.
        /// </summary>
        /// <param name="img"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void AddItem(String text, int x, int y, SpriteFont font)
        {
            hudItems.Add(new HUDItem(font, text, (int)HUDcoords.X + x, (int)HUDcoords.Y + y));
        }

        /// <summary>
        /// The method to update all of the HUDItems' information.
        /// </summary>
        /// <param name="gameTime">The object that holds all the time information.</param>
        public void Update(GameTime gameTime, Pantheon gameReference, Level level) 
        {
            PlayerCharacter player = (PlayerCharacter)(level.Entities["character"]);
            // Set the width of the Armor Bar with respect to the current percent of the player's armor.
            try
            {
                hudItems[0].Coordinates = new Rectangle(hudItems[0].Coordinates.X, hudItems[0].Coordinates.Y, 
                    (int)(hudItems[0].DefaultWidth * ((float)player.CurrentArmor / player.TotalArmor)), hudItems[0].Coordinates.Height);
                
                // Get the Shield item
                Shield shield = (Shield)player.EquippedItems["shield"];

                // Update the Shield indicator
                int shieldPercent = (int)(100 * (float)shield.CurrentShield / shield.TotalShield);
                hudItems[1].SetOpacity((int)((shieldPercent - 55) * 5.66)); // (% - min) * (255 / (max - min))

                if (shieldPercent > 55)
                {
                    hudItems[2].SetOpacity((int)(255 - (shieldPercent - 55) * 5.66));
                }
                else
                {
                    hudItems[2].SetOpacity((int)((shieldPercent - 10) * 5.66));
                }

                if (shieldPercent > 10)
                {
                    hudItems[3].SetOpacity((int)(255 - (shieldPercent - 10) * 5.66));
                }
                else
                {
                    hudItems[3].SetOpacity(0);
                }

                if (shieldPercent <= 10 && shieldPercent > 0)
                {
                    hudItems[4].SetOpacity(danger);
                    danger = (danger + (30 - (shieldPercent * 2))) % 256; 
                }
                else
                {
                    hudItems[4].SetOpacity(0);
                    danger = 0;
                }

                if (shieldPercent == 0)
                {
                    hudItems[5].SetOpacity(255);
                }
                else
                {
                    hudItems[5].SetOpacity(0);
                }

                // Update the Ammo Display
                if (player.ArmedItem.isNull)
                {
                    hudItems[6].Text = "0/0";
                }
                else
                {
                    hudItems[6].Text = (((Weapon)player.ArmedItem).CurrentAmmo.ToString()) +
                        "/" + (((Weapon)player.ArmedItem).TotalAmmo.ToString());

                    // Update the Reload Timer Bar
                    hudItems[7].Coordinates = new Rectangle(hudItems[7].Coordinates.X, hudItems[7].Coordinates.Y,
                        (int)(hudItems[7].DefaultWidth * (((Weapon)player.ArmedItem)).PercentToEndReload()), hudItems[7].Coordinates.Height);   
                }
                hudItems[8].Image = player.ArmedItem.HUDRepresentation;
                hudItems[8].Coordinates = new Rectangle((int)HUDcoords.X, (int)HUDcoords.Y, (int)(SCREEN_WIDTH * .09), (int)(SCREEN_HEIGHT * .05));
                //debugString = (((Weapon)player.ArmedItem).ReloadDelay.Seconds * 1000 + ((Weapon)player.ArmedItem).ReloadDelay.Milliseconds).ToString();

            }
            catch (DivideByZeroException)
            {
                Console.Write("Total armor or shield is zero!");
            }
        }

        /// <summary>
        /// The method to draw all of the different HUDItems.
        /// </summary>
        /// <param name="spriteBatch">The object that we will use to draw to the screen.</param>
        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.Begin();

            string title = "Pantheon DRAGON SPLEAN";

            spriteBatch.DrawString(font, title, new Vector2(1, 1), Color.DarkGray);
            spriteBatch.DrawString(font, title, new Vector2(0, 0), Color.LightGray);

            // Draw the Backing
            spriteBatch.Draw(backing, new Rectangle((int)HUDcoords.X, (int)HUDcoords.Y, background.Width, background.Height), Color.White);

            // Draw the Armor Bar (Must be done first due to shape)
            spriteBatch.Draw(hudItems[0].Image, hudItems[0].Coordinates, hudItems[0].Opacity);
            
            // Draw the Background
            spriteBatch.Draw(background, new Rectangle((int)HUDcoords.X, (int)HUDcoords.Y, background.Width, background.Height), Color.White);

            // Draw all the remaining items
            for (int i = 1; i < hudItems.Count; i++)
            {
                hudItems[i].Draw(spriteBatch);
            }

            spriteBatch.End();
        }
    }
}
