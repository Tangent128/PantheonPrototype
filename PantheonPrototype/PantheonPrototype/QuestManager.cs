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
    /// Handles quests as the player accepts, progresses through, and completes them.
    /// </summary>
    class QuestManager
    {
        /// <summary>
        /// All the quests that the player is currently engaged in.
        /// </summary>
        public List<Quest> quests;

        public QuestManager()
        {
            quests = new List<Quest>();
        }

        /// <summary>
        /// Notifies the quest manager that an entity has changed its state.
        /// 
        /// Basically, all relevant quests will be notified that something happened. Then
        /// the quest status is updated, the appropriate objectives are updated/completed
        /// and the quest manager returns to a dormant state.
        /// 
        /// The basic idea was to only update the quests when they need to be updated. This
        /// saves per frame updating costs. Since there aren't going to be that many
        /// quests at a time and the amount of events we can expect per frame are relatively
        /// small, we can hopefully expect this method to scale well.
        /// 
        /// NOTE: The parameters for this function may need to change based on different notification types.
        /// For instance, we may have events not associated with entity state changes.
        /// </summary>
        /// <param name="entity">The entity that has changed state.</param>
        /// <param name="state">The new state of the entity.</param>
        public void Notify(Entity entity, string state)
        {
        }

        /// <summary>
        /// Updates any time sensitive events in quests.
        /// 
        /// Most of the quest updating should occur through the notify function. This
        /// is only for quests that have a timer built in or maybe to change AI states
        /// etc...
        /// </summary>
        /// <param name="gameTime">Time since the last update cycle.</param>
        public void Update(GameTime gameTime)
        {
        }
    }
}
