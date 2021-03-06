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
    /// The delegate definition for a function that handles events.
    /// </summary>
    /// <param name="eventInfo">All the information about the event.</param>
    public delegate void HandleEvent(Event eventInfo);

    /// <summary>
    /// Distributes events to the correct locations in Pantheon.
    /// 
    /// Classes should register functions with the EventManager to handle specific types of events.
    /// When an event occurs, the type is identified, and all registered functions are called. They
    /// are passed the event, whose payload gives any other relevant information to the recipient.
    /// </summary>
    public class EventManager
    {
        /// <summary>
        /// A dictionary that maps each type of event to a list of delegate functions to call when
        /// that specific type of event occurs.
        /// </summary>
        private Dictionary<string, List<HandleEvent>> eventHandlers;

        /// <summary>
        /// Registers an event handling function with the EventManager class. When the EventManager
        /// receives an event of the given type, the given handler will be called.
        /// </summary>
        /// <param name="type">A string denoting the event type to trigger the handler.</param>
        /// <param name="handler">The handler function declared in the form: void EventHandler (Event eventInfo);</param>
        public void register(string type, HandleEvent handler)
        {
            if (!eventHandlers.Keys.Contains(type))
            {
                eventHandlers.Add(type, new List<HandleEvent>());
            }

            eventHandlers[type].Add(handler);
        }

        /// <summary>
        /// Notifies the appropriate handlers of an Event.
        /// </summary>
        /// <param name="eventInfo">Information about the event.</param>
        public void notify(Event eventInfo)
        {
            foreach (HandleEvent handler in eventHandlers[eventInfo.Type])
            {
                handler(eventInfo);
            }
        }
    }
}
