using System;
using System.Collections.Generic;
using System.Text;
using XnaGamesInfrastructure.ServiceInterfaces;
using XnaGamesInfrastructure.Services;
using XnaGamesInfrastructure.ObjectModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XnaGamesInfrastructure.ObjectInterfaces;
using A09_Ex02_Koby_021766944_Inbar_015267479.ObjectModel;

namespace A09_Ex02_Koby_021766944_Inbar_015267479
{
    /// <summary>
    /// The class represents the player's component in the game (the game's
    /// SpaceShip)
    /// </summary>
    public class SpaceShip : Sprite ,IShootable
    {
        private const string k_AssetName = @"Sprites\Ship01_32x32";

        private const int k_AllowedBulletsNum = 3;
        private const int k_Velocity = 200;
        private const int k_BulletVelocity = 200;
        private const int k_LivesNum = 3;

        private int m_RemainingLivesLeft;

        private readonly TimeSpan r_ShootingCoolingOff = TimeSpan.FromSeconds(0.5f);
        private TimeSpan m_PreviousShootingTime;

        private List<Bullet> m_Bullets;
        private IInputManager m_InputManager;

        private int m_PlayerScore = 0;

        #region CTOR's

        public SpaceShip(Game i_Game) : this(k_AssetName, i_Game, 
                                             Int32.MaxValue, Int32.MaxValue)
        {            
        }

        public SpaceShip(string k_AssetName, Game i_Game, int i_UpdateOrder,
                         int i_DrawOrder) : base(k_AssetName, i_Game, 
                                                 i_UpdateOrder, i_DrawOrder)
        {
            m_Bullets = new List<Bullet>();
            m_PreviousShootingTime = r_ShootingCoolingOff;
            m_RemainingLivesLeft = k_LivesNum;
        }

        #endregion

        /// <summary>
        /// A property to the player's score
        /// </summary>
        public int Score
        {
            get
            {
                return m_PlayerScore;
            }
        }

        #region IShootable Members

        /// <summary>
        /// Release a SpaceShip shoot
        /// </summary>
        public void     Shoot()
        {
            // If we didn't reach the maximum bullets allowed, we'll create 
            // a new one and add it to the game components
            if (m_Bullets.Count < k_AllowedBulletsNum)
            {
                Bullet bullet = new SpaceShipBullet(this.Game);                
                bullet.Initialize();
                bullet.TintColor = Color.Red;
                bullet.Position = new Vector2(m_Position.X + Bounds.Width / 2,
                m_Position.Y - bullet.Bounds.Height / 2);                                
                bullet.MotionVector = new Vector2(0, -k_BulletVelocity);
                bullet.ReachedScreenBounds += new SpriteReachedScreenBoundsDelegate(bullet_ReachedScreenBounds);

                m_Bullets.Add(bullet);                
            }
        }

        #endregion

        /// <summary>
        /// Initialize the SpaceShip position
        /// </summary>
        protected override void     InitPosition()
        {
            base.InitPosition();    

            float x = (float)GraphicsDevice.Viewport.Width / 2;
            float y = ((float)GraphicsDevice.Viewport.Height) - 40;            

            Position = new Vector2(x - (this.Texture.Width / 2), y);
        }

        /// <summary>
        /// Initialize the SpaceShip bounds
        /// </summary>
        protected override void     InitBounds()
        {            
            int x = (int)GraphicsDevice.Viewport.Width / 2;
            int y = ((int)GraphicsDevice.Viewport.Height) - 40;

            Position = new Vector2(x, y);
        }

        /// <summary>
        /// Updates the SpaceShip according to the player choise (move the ship
        /// or release a shoot)
        /// </summary>
        /// <param name="i_GameTime">The time passed from the previous update call</param>
        public override void    Update(GameTime i_GameTime)
        {
            Vector2 newPosition = this.MotionVector;

            m_PreviousShootingTime -= i_GameTime.ElapsedGameTime;

            if (m_InputManager.KeyboardState.IsKeyDown(Keys.Left))
            {
                newPosition.X = k_Velocity * -1;
            }
            else if (m_InputManager.KeyboardState.IsKeyDown(Keys.Right))
            {
                newPosition.X = k_Velocity;
            }
            else
            {
                newPosition.X = 0;
            }

            MotionVector = newPosition;

            // Shoot only if the player press the space key and it passed enough time from the previous shoot
            if (m_InputManager.KeyPressed(Keys.Space) && m_PreviousShootingTime.TotalSeconds < 0)
            {
                m_PreviousShootingTime = r_ShootingCoolingOff;

                Shoot();
            }

            base.Update(i_GameTime);
        }

        /// <summary>
        /// Initialize the space ship by getting the game's input manager
        /// </summary>
        public override void    Initialize()
        {
            m_InputManager = Game.Services.GetService(typeof(InputManager)) as IInputManager;
            base.Initialize();
        }

        public void     bullet_ReachedScreenBounds(Sprite i_Sprite)
        {
            if (i_Sprite is Bullet)
            {
                if (m_Bullets != null && m_Bullets.Contains((Bullet)i_Sprite))
                {
                    m_Bullets.Remove((Bullet)i_Sprite);
                }
            }
        }

        public override bool CheckForCollision(ICollidable i_OtherComponent)
        {
            return !m_Bullets.Contains(i_OtherComponent as Bullet) &&
                    (i_OtherComponent is Bullet ||
                     i_OtherComponent is Enemy) && 
                   base.CheckForCollision(i_OtherComponent);
        }

        public override void    Collided(ICollidable i_OtherComponent)
        {
            if (!(i_OtherComponent is SpaceShipBullet))
            {
                base.Collided(i_OtherComponent);
            }
        }

    }
}