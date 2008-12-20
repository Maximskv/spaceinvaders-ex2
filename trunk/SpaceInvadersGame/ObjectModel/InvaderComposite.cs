using System;
using System.Collections.Generic;
using System.Text;
using XnaGamesInfrastructure.ObjectModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvadersGame.Interfaces;
using SpaceInvadersGame.ObjectModel.Screens;
using XnaGamesInfrastructure.ObjectModel.Animations;
using XnaGamesInfrastructure.ObjectModel.Animations.ConcreteAnimations;

namespace SpaceInvadersGame.ObjectModel
{
    /// <summary>
    /// A composite class that holds an invader and his bullets
    /// </summary>
    public class InvaderComposite : CompositeDrawableComponent<IGameComponent>
    {
        private Invader m_Invader;

        public InvaderComposite(Game i_Game, Invader i_Invader)
            : base(i_Game)
        {
            m_Invader = i_Invader;
            m_Invader.ReleasedShot += new AddGameComponentDelegate(invader_ReleasedShot);

            // TODO: Check the dispose
            //m_Invader.Disposed += new EventHandler(invader_Disposed);

            this.Add(m_Invader);
        }

        /// <summary>
        /// Gets the invader that the object holds
        /// </summary>
        public Invader  Invader
        {
            get { return m_Invader; }
        }

        /// <summary>
        /// Catch the ReleasedShot event and adds the new bullet to the 
        /// components list
        /// </summary>
        /// <param name="i_Component">The bullet that the invader shot</param>
        private void invader_ReleasedShot(IGameComponent i_Component)
        {
            this.Add(i_Component);
        }

        // TODO: Check the dispose

        /// <summary>
        /// Catch an invader disposed event and disposes the current object
        /// also
        /// </summary>
        /// <param name="i_Sender">The disposed enemy</param>
        /// <param name="i_EventArgs">The event arguments</param>
        /*private void    invader_Disposed(object i_Sender, EventArgs i_EventArgs)
        {
            Dispose();
        }*/
    }
}
