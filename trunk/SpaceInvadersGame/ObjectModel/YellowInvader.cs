using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvadersGame.ObjectModel
{
    /// <summary>
    /// A yellow invader in the invaders matrix
    /// </summary>
    public class YellowInvader : Invader
    {
        // TODO: Remove the code
        //private const int k_Score = 100;

        private const int k_TextureYPositionVal = 64;

        public YellowInvader(
            Game i_Game,
            int i_UpdateOrder,
            int i_InvaderListNum)
            : base(i_Game, i_UpdateOrder, i_InvaderListNum)
        {
            TintColor = Color.LightYellow;
            this.SourcePosition = new Vector2(0, k_TextureYPositionVal);
        }

        // TODO: Remove the code

        /// <summary>
        /// A property for the invader score
        /// </summary>
       /* public override int     Score
        {
            get 
            { 
                return k_Score; 
            }
        }*/

        /// <summary>
        /// Returns the invader type (YellowInvader) 
        /// </summary>
        public override eInvadersType InvaderType
        {
            get { return eInvadersType.YellowInvader; }
        }

        /// <summary>
        /// Read only property that gets the yellow invader hit action value
        /// </summary>
        protected override eSoundActions HitAction
        {
            get { return eSoundActions.YellowInvaderHit; }
        }        
    }
}
