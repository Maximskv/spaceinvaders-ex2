using System;
using System.Collections.Generic;
using System.Text;
using XnaGamesInfrastructure.ObjectModel.Screens;
using XnaGamesInfrastructure.ObjectModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceInvadersGame.ObjectModel.Screens.Menus
{
    /// <summary>
    /// A parent to all the menu screens in the game
    /// </summary>
    public abstract class MenuTypeScreen : SpaceInvadersScreenAbstract
    {
        protected List<MenuItem> m_MenuItems = new List<MenuItem>();
        protected SpriteFontComponent m_Title;
        private int m_CurrentMenuItem = -1;

        public MenuTypeScreen(Game i_Game, String i_Title)
            : base(i_Game)
        {
            m_Title = new SpriteFontComponent(i_Game, @"Fonts\David40", i_Title);
            m_Title.TintColor = Color.SlateBlue;
            Add(m_Title);
        }

        /// <summary>
        /// Adds a new component to the screen. 
        /// In case it's a MenuItem, we'll save it in a dedicated list in
        /// addition to the parent list
        /// </summary>
        /// <param name="i_Component">The component we want to add to the screen</param>
        public override void    Add(IGameComponent i_Component)
        {
            base.Add(i_Component);

            if (i_Component is MenuItem)
            {
                m_MenuItems.Add((MenuItem)i_Component);
                SetItemPosition(m_MenuItems.Count - 1);

                if (m_MenuItems.Count == 1)
                {
                    m_MenuItems[0].IsSelected = true;
                    m_CurrentMenuItem = 0;
                }
            }
        }

        /// <summary>
        /// Initialize the menu by setting the position of all the menu items
        /// in the screen
        /// </summary>
        public override void    Initialize()
        {
            base.Initialize();

            m_Title.PositionOfOrigin = new Vector2(m_Title.ViewPortCenter.X, m_Title.HeightAfterScale);
            m_Title.PositionOrigin = m_Title.SpriteCenter;

            for (int i = 0; i < m_MenuItems.Count; ++i )
            {
                SetItemPosition(i);
            }
        }

        private void    SetItemPosition(int i_ItemIndex)
        {
            Vector2 position;

            if (i_ItemIndex == 0)
            {
                position = m_Title.PositionOfOrigin;
                position.Y += m_Title.HeightAfterScale * 1.2f;
            }
            else
            {
                MenuItem prevItem = m_MenuItems[i_ItemIndex - 1];
                position = prevItem.PositionOfOrigin;
                position.Y += prevItem.HeightAfterScale * 1.2f;
            }

            MenuItem item = m_MenuItems[i_ItemIndex];
            item.PositionOfOrigin = position;
            m_MenuItems[i_ItemIndex].PositionOrigin = item.SpriteCenter;
        }

        /// <summary>
        /// Updates the current selected menu item according to the player input
        /// </summary>
        /// <param name="i_GameTime">A snapshot of the current game time</param>
        public override void    Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);

            if (InputManager.KeyPressed(Keys.Down) || 
                InputManager.ScrollWheelDelta < 0)
            {
                changeCurrentItem(true);
            }
            else if (InputManager.KeyPressed(Keys.Up) ||
                     InputManager.ScrollWheelDelta > 0)
            {
                changeCurrentItem(false);
            }
        }

        /// <summary>
        /// Changes the current menu item according to the player input
        /// </summary>
        /// <param name="i_MoveDown">Mark if we want to get the next item
        /// or the previous one (true - get the next menu item, false - get
        /// the previous menu item)</param>
        private void    changeCurrentItem(bool i_MoveDown)
        {
            int itemsToMove = i_MoveDown ? 1 : -1;
            int newCurrentItem = m_CurrentMenuItem + itemsToMove;

            // If we moved up from the first menu item, or if we moved down 
            // from the last menu item, we'll move the current item to the 
            // last/first item
            if (newCurrentItem < 0)
            {
                newCurrentItem = m_MenuItems.Count + newCurrentItem;
            }
            else if (newCurrentItem > (m_MenuItems.Count - 1))
            {
                newCurrentItem = newCurrentItem - (m_MenuItems.Count - 1) - 1;
            }

            if (newCurrentItem != m_CurrentMenuItem)
            {
                m_MenuItems[m_CurrentMenuItem].IsSelected = false;
                m_MenuItems[newCurrentItem].IsSelected = true;
                m_CurrentMenuItem = newCurrentItem;

                // Play the change menu item cue
                PlayActionCue(eSoundActions.MenuItemChanged);
            }
        }
    }
}