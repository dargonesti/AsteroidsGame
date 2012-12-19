namespace AsteroidsPrototype.Menus
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Audio;
    using Microsoft.Xna.Framework.Graphics;
    using DisplaySpring;
    using Layout = DisplaySpring.Frame.LayoutType;
    using Microsoft.Xna.Framework.Input;
    using AsteroidsPrototype.GameManager;

    /// <summary>
    /// Cactory du menu
    /// </summary>
    public static class MenuCreator
    {

        /// <summary>
        /// Creates a Main Menu
        /// </summary>
        public static Menu createMainMenu(MultiController controllers, List<Controller> allControllers, Rectangle bounds, GameManager pmanager, Boolean recommencer = false)
        {
            Menu MainMenu = new Menu(controllers, bounds);
            MainMenu.BaseFrame.Layout = Layout.VerticalShared;

            new Label(MainMenu.BaseFrame, "Asteroids") { Scale = new Vector2(2, 2), FontColor = Color.White };

            ScrollList sl = new ScrollList(MainMenu.BaseFrame);
            sl.Focus = true;
            sl.LayoutStretch = 4;
            sl.Scale = new Vector2(2, 2);

            Label lbl = new Label(sl, (recommencer ? "Restart" : "Start"));
            lbl.OnA = delegate() { MainMenu.Close(); };

            lbl = new Label(sl, "Choose Level");
            lbl.OnA = delegate()
            {
                MainMenu.ActiveSubMenu = new ChooseLevelMenu(controllers, allControllers, bounds, pmanager);
                MainMenu.ActiveSubMenu.OnClosing = delegate() { MainMenu.Close(); };
            };

            /*lbl = new Label(sl, "Options");
            lbl.OnA = delegate()
            {
                //MainMenu.ActiveSubMenu = new OptionButtonMenu(controllers, allControllers, bounds); 
            };*/

            lbl = new Label(sl, "Exit");
            lbl.OnA = delegate() { MainMenu.Close(); pmanager._game.Exit(); };
            return MainMenu;
        }

        /// <summary>
        /// Creates a Death Menu
        /// </summary>
        public static Menu createDeathMenu(MultiController controllers, List<Controller> allControllers, Rectangle bounds, GameManager pmanager)
        {
            Menu MainMenu = new Menu(controllers, bounds);
            MainMenu.BaseFrame.Layout = Layout.VerticalShared;

            new Label(MainMenu.BaseFrame, "You D.I.E.D.!!!!!") { Scale = new Vector2(2, 2), FontColor = new Color(200,0,0), Animation = AnimateType.Size };
            new Label(MainMenu.BaseFrame, "Score Level '" + pmanager.nivCourant.GetName() + "': " + pmanager._playerScore.ToString()) { Scale = new Vector2(2, 2), FontColor = Color.White };
            
            ScrollList sl = new ScrollList(MainMenu.BaseFrame);
            sl.Focus = true;
            sl.LayoutStretch = 4;
            sl.Scale = new Vector2(2, 2);

            Label lbl = new Label(sl, "Try Again!");
            lbl.OnA = delegate() { MainMenu.Close(); };
            lbl.FontColor = Color.DarkBlue;
            lbl.FontFocusColor = Color.DarkRed;

            lbl = new Label(sl, "Choose Level");
            lbl.OnA = delegate()
            {
                MainMenu.ActiveSubMenu = new ChooseLevelMenu(controllers, allControllers, bounds, pmanager);
                MainMenu.ActiveSubMenu.OnClosing = delegate() { MainMenu.Close(); };
            };
            lbl.FontColor = Color.DarkBlue;
            lbl.FontFocusColor = Color.DarkRed;

            lbl = new Label(sl, "Quit");
            lbl.OnA = delegate() { MainMenu.Close(); pmanager._game.Exit(); };
            lbl.FontColor = Color.DarkBlue;
            lbl.FontFocusColor = Color.DarkRed;
            return MainMenu;
        }

        /// <summary>
        /// Creates a Win Menu
        /// </summary>
        public static Menu createWinMenu(MultiController controllers, List<Controller> allControllers, Rectangle bounds, GameManager pmanager)
        {
            Menu MainMenu = new Menu(controllers, bounds);
            MainMenu.BaseFrame.Layout = Layout.VerticalShared;

            new Label(MainMenu.BaseFrame, "Level succes!") { Scale = new Vector2(2, 2), FontColor = Color.White };
            new Label(MainMenu.BaseFrame, "Score Level '" + pmanager.nivCourant.GetName() + "': " + pmanager._playerScore.ToString()) { Scale = new Vector2(2, 2), FontColor = Color.White };
            
            ScrollList sl = new ScrollList(MainMenu.BaseFrame);
            sl.Focus = true;
            sl.LayoutStretch = 4;
            sl.Scale = new Vector2(2, 2);

            Label lbl = new Label(sl, "Next Level");
            lbl.OnA = delegate() { MainMenu.Close(); };

            lbl = new Label(sl, "Choose Level");
            lbl.OnA = delegate()
            {
                MainMenu.ActiveSubMenu = new ChooseLevelMenu(controllers, allControllers, bounds, pmanager);
                MainMenu.ActiveSubMenu.OnClosing = delegate() { MainMenu.Close(); };
            };

            lbl = new Label(sl, "Exit");
            lbl.OnA = delegate() { MainMenu.Close(); pmanager._game.Exit(); };
            return MainMenu;
        }

        /// <summary>
        /// Creates a Pause Menu
        /// </summary>
        public static Menu createPauseMenu(MultiController controllers, List<Controller> allControllers, Rectangle bounds, GameManager pmanager)
        {
            Menu MainMenu = new Menu(controllers, bounds);
            MainMenu.BaseFrame.Layout = Layout.VerticalShared;

            new Label(MainMenu.BaseFrame, "Score Level '" + pmanager.nivCourant.GetName() + "': " + pmanager._playerScore.ToString()) { Scale = new Vector2(2, 2), FontColor = Color.White };
            new Label(MainMenu.BaseFrame, "Levels Completed : " + pmanager._levelsCompleted.ToString()) { Scale = new Vector2(2, 2), FontColor = Color.White };
            new Label(MainMenu.BaseFrame, "Lives Lost : " + pmanager._playerDeaths.ToString()) { Scale = new Vector2(2, 2), FontColor = Color.White };
            new Label(MainMenu.BaseFrame, "Asteroids Destroyed : " + pmanager._astKilled.ToString()) { Scale = new Vector2(2, 2), FontColor = Color.White };
            new Label(MainMenu.BaseFrame, "Ennemy Ships Destroyed : " + pmanager._enemiKilled.ToString()) { Scale = new Vector2(2, 2), FontColor = Color.White };

            ScrollList sl = new ScrollList(MainMenu.BaseFrame);
            sl.Focus = true;
            sl.LayoutStretch = 4;
            sl.Scale = new Vector2(2, 2);

            Label lbl = new Label(sl, "Resume");
            lbl.OnA = delegate() { MainMenu.Close(); };
            lbl.FontColor = Color.DarkBlue;
            lbl.FontFocusColor = Color.DarkRed;

            lbl = new Label(sl, "Quit");
            lbl.OnA = delegate() { MainMenu.Close(); pmanager._game.Exit(); };
            lbl.FontColor = Color.DarkBlue;
            lbl.FontFocusColor = Color.DarkRed;
            return MainMenu;
        }
    }
}
