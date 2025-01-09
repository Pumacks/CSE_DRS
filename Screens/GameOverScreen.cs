using GameStateManagement;
using GameStateManagementSample.Models;
using GameStateManagementSample.Models.Entities;
using GameStateManagementSample.Models.Items;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameStateManagementSample.Screens
{
    internal class GameOverScreen : MenuScreen
    {
        #region Initialization

    

        public GameOverScreen(PlayerGameStatus status)
            : base(status == PlayerGameStatus.DEAD ? "Game  over, you  died!" : "Congratulations, You  won! \nTotal  score:  " + Player.totalScore)
        {
            

            // Create our menu entries.

            MenuEntry quitGameMenuEntry = new MenuEntry("Quit");

            // Hook up menu event handlers.

            quitGameMenuEntry.Selected += ConfirmQuitMessageBoxAccepted;

            // Add entries to the menu.

            MenuEntries.Add(quitGameMenuEntry);
        }


        #endregion Initialization

        #region Handle Input

        /// <summary>
        /// Event handler for when the Quit Game menu entry is selected.
        /// </summary>


        /// <summary>
        /// Event handler for when the user selects ok on the "are you sure
        /// you want to quit" message box. This uses the loading screen to
        /// transition from the game back to the main menu screen.
        /// </summary>
        private void ConfirmQuitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(),
                                                           new MainMenuScreen());
        }

        public override void HandleInput(InputState input)
        {
            // Move to the previous menu entry?
            if (input.IsMenuUp(ControllingPlayer))
            {
                selectedEntry--;

                if (selectedEntry < 0)
                {
                    selectedEntry = menuEntries.Count - 1;
                }
            }

            // Move to the next menu entry?
            if (input.IsMenuDown(ControllingPlayer))
            {
                selectedEntry++;

                if (selectedEntry >= menuEntries.Count)
                {
                    selectedEntry = 0;
                }
            }

            // Accept or cancel the menu? We pass in our ControllingPlayer, which may
            // either be null (to accept input from any player) or a specific index.
            // If we pass a null controlling player, the InputState helper returns to
            // us which player actually provided the input. We pass that through to
            // OnSelectEntry and OnCancel, so they can tell which player triggered them.
            PlayerIndex playerIndex;

            if (input.IsMenuSelect(ControllingPlayer, out playerIndex))
            {
                OnSelectEntry(selectedEntry, playerIndex);
            }
            //else if (input.IsMenuCancel(ControllingPlayer, out playerIndex))
            //{
            //    OnCancel(playerIndex);
            //}
        }

        #endregion Handle Input


    }
}
