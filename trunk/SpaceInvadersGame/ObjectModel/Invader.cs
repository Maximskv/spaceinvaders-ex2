using System;
using System.Collections.Generic;
using System.Text;
using XnaGamesInfrastructure.ObjectModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvadersGame.Interfaces;

namespace SpaceInvadersGame.ObjectModel
{        
    /// <summary>
    /// Used by Invader in order to inform that the he reached the 
    /// invaders allowed screen bounds
    /// </summary>
    /// <param name="i_Invader">The invader that reached the allowed screen bounds</param>
    public delegate void InvaderReachedScreenBoundsDelegate(Invader i_Invader);    

    /// <summary>
    /// An abstract class that all the small invaders in the invaders matrix 
    /// inherits from
    /// </summary>
    public abstract class Invader : Enemy, IShootable
    {
        // Raised when an invader reaches one of the allowed screen bounderies
        public event InvaderReachedScreenBoundsDelegate ReachedScreenBounds;

        private const int k_BulletVelocity = 200;

        private TimeSpan m_TimeBetweenMove = TimeSpan.FromSeconds(0.5f);
        protected TimeSpan m_TimeLeftToNextMove;        

        protected Vector2 m_CurrMotion = new Vector2(500, 0);

        private float m_EnemyMaxPositionYVal;

        public Invader(string i_AssetName, Game i_Game)
            : this(i_AssetName, i_Game, 0, 0)
        {
        }

        public Invader(
            string i_AssetName, 
            Game i_Game, 
            int i_UpdateOrder)
            : this(i_AssetName, i_Game, i_UpdateOrder, 0)
        {
        }        

        public Invader(
            string i_AssetName, 
            Game i_Game, 
            int i_UpdateOrder, 
            int i_DrawOrder)
            : base(i_AssetName, i_Game, i_UpdateOrder, i_DrawOrder)
        {            
            m_TimeLeftToNextMove = m_TimeBetweenMove;            
        }       

        /// <summary>
        /// A property for the maximum value the enemy is allowed to reach
        /// on the Y axis
        /// </summary>
        public float    InvaderMaxPositionY
        {
            get
            {
                return m_EnemyMaxPositionYVal;
            }

            set
            {
                m_EnemyMaxPositionYVal = value;
            }
        }

        /// <summary>
        /// A property for the time the enemy waits between two moves
        /// </summary>
        public TimeSpan     TimeBetweenMoves
        {
            get
            {
                return m_TimeBetweenMove;
            }

            set
            {
                m_TimeBetweenMove = value;
            }
        }

        #region ICollidable Members

        /// <summary>
        /// Check for collision with a given component.        
        /// </summary>
        /// <param name="i_OtherComponent">the component we want to check for collision 
        /// against</param>        
        /// <returns>true in case the invader collides with the given component 
        /// or false in case the given component is an EnemyBullet or there is no collision
        /// between the components </returns>
        public override bool    CheckForCollision(XnaGamesInfrastructure.ObjectInterfaces.ICollidable i_OtherComponent)
        {
            return !(i_OtherComponent is EnemyBullet) &&
                      base.CheckForCollision(i_OtherComponent);
        }

        #endregion

        #region IShootable Members

        /// <summary>
        /// Realse a shoot in the game
        /// </summary>
        public void     Shoot()
        {
            Bullet bullet = new EnemyBullet(Game);
            bullet.Initialize();
            bullet.TintColor = Color.Blue;
            bullet.Position = new Vector2(
                                    Position.X + (Bounds.Width / 2),
                                    Position.Y - (bullet.Bounds.Height / 2));
            bullet.MotionVector = new Vector2(0, k_BulletVelocity);
        }

        #endregion

        /// <summary>
        /// Move the invader in the screen in case enough time had passed 
        /// since last move
        /// </summary>
        /// <param name="i_GameTime">Provides a snapshot of timing values.</param>
        public override void    Update(GameTime i_GameTime)
        {
            bool moveEnemy = false;

            // If the enemy was hit (changed to unvisible), we need to 
            // dispose it
            if (Visible == false)
            {
                Dispose();
            }
            else
            {
                m_TimeLeftToNextMove -= i_GameTime.ElapsedGameTime;

                // Check if enough time had passed since previous move
                if (m_TimeLeftToNextMove.TotalSeconds < 0)
                {
                    MotionVector = m_CurrMotion;
                    m_TimeLeftToNextMove = m_TimeBetweenMove;

                    moveEnemy = true;
                }
                else
                {
                    MotionVector = new Vector2(0, MotionVector.Y);
                }

                base.Update(i_GameTime);

                // If we moved the enemy this time we'll also check if the 
                // enemy is close to the screen bounds and raise an event
                if (moveEnemy)
                {
                    moveEnemy = false;

                    if (Bounds.Left <= 0 || Bounds.Right >= Game.GraphicsDevice.Viewport.Width || 
                        Bounds.Bottom >= m_EnemyMaxPositionYVal)
                    {
                        OnReachedScreenBounds();
                    }
                }
            }
        }

        /// <summary>
        /// An empty proc that simply prevents the parent method that initializes
        /// the coponent position from happening.
        /// this is done due to the fact that the invader position is set from
        /// the outside by the invaders matrix class, and there is no need to
        /// initialize it ourselves
        /// </summary>
        protected override void InitPosition()
        {
        }

        /// <summary>
        /// Change the enemy moving direction, so that in the next move the 
        /// enemy will move to the other screen side on the X axis
        /// </summary>
        public void     SwitchPosition()
        {
            m_CurrMotion *= -1;         
        }

        /// <summary>
        /// Raising the ReachedScreenBounds event that marks that the enemy
        /// reached the allowed bounds in the screen
        /// </summary>
        protected void     OnReachedScreenBounds()
        {
            if (ReachedScreenBounds != null)
            {
                ReachedScreenBounds(this);
            }
        }                
    }
}