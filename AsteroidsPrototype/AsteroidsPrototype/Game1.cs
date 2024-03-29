using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using AsteroidsPrototype.GameObjects;
using AsteroidsPrototype.GameManager;
using DisplaySpring;
using AsteroidsPrototype.Menus;

namespace AsteroidsPrototype
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    /// 

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameManager.GameManager gameManager;
        public static SpriteFont txtDescription;
        Vector2 txtPos;
        UpdateEvent updates;
        public Menu mainMenu;

        KeyboardState oldKeyboardState = Keyboard.GetState();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Window.AllowUserResizing = true;
            Content.RootDirectory = "Content";
            txtPos = new Vector2(5, 5);
            Window.ClientSizeChanged += new EventHandler<EventArgs>(Window_ClientSizeChanged);
            graphics.PreferredBackBufferHeight = 1000;
            graphics.PreferredBackBufferWidth = 1800;
            this.Components.Add(new GamerServicesComponent(this));
            updates = new UpdateEvent();
        }

        void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            ViewPortManager.Load(GraphicsDevice.Viewport);
            ViewPortManager.graphicsDevice = GraphicsDevice;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            App.Initialize(graphics, updates);
            base.Initialize();
            txtDescription = Content.Load<SpriteFont>("asd");
            gameManager = new GameManager.GameManager(this);
            gameManager.Background = Content.Load<Texture2D>("background");
            mainMenu = MenuCreator.createMainMenu(App.PrimaryController, App.Controllers, App.TitleSafeArea, gameManager);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            SpriteManager.LoadSpritesFromContent(Content);
            ViewPortManager.Load(GraphicsDevice.Viewport);
            ViewPortManager.graphicsDevice = GraphicsDevice;
            SoundManager.Load(Content);
            // Create a new SpriteBatch, which can be used to draw textures.
            App.LoadContent(Content, GraphicsDevice);
            Menu.LoadContent(GraphicsDevice, App.MenuFont);

            SoundManager.Play(SoundManager.LoopSong, 0.5f);
        }


        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            KeyboardState keyboardState = Keyboard.GetState();


            if (mainMenu.IsAlive)
            {
                updates.Update(gameTime);
                mainMenu.Update(gameTime);
            }
            // Allows the game to pause and exit
            if (!mainMenu.IsAlive && (GamePad.GetState(PlayerIndex.One).Buttons.Start == Microsoft.Xna.Framework.Input.ButtonState.Pressed ||
                keyboardState.IsKeyDown(Keys.Escape) && !oldKeyboardState.IsKeyDown(Keys.Escape)))
            {
                //mainMenu = MenuCreator.createMainMenu(App.PrimaryController, App.Controllers, App.TitleSafeArea, gameManager, true);
                mainMenu = MenuCreator.createPauseMenu(App.PrimaryController, App.Controllers, App.TitleSafeArea, gameManager);
            }
            // Allows the game to exit
            if (!mainMenu.IsAlive)
            {
                //this.Exit();

                gameManager.HandlePlayerInput(keyboardState, Mouse.GetState());
                gameManager.UpdateScene(gameTime);

            }
                oldKeyboardState = keyboardState;
            //base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
           
            Vector2 origin = new Vector2(0,0);

            //premier beginDraw pour un background fixe
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend); // ancien, avec camera Handmade
            spriteBatch.Draw(SpriteManager.background, new Rectangle(0, 0, ViewPortManager.Width, ViewPortManager.Height), Color.White);          
            spriteBatch.End();
                //2 iem begin Draw pour toute la sc�ne
                spriteBatch.Begin(SpriteSortMode.BackToFront,
                            BlendState.AlphaBlend,
                            null,
                            null,
                            null,
                            null,
                           gameManager.getCameraTransformation(gameTime));

                gameManager.DrawScene(spriteBatch);

                spriteBatch.End();
                base.Draw(gameTime);

                if (mainMenu.IsAlive)
                {
                    //                GraphicsDevice.Clear(Color.CornflowerBlue);
                    spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

                    spriteBatch.Draw(SpriteManager.colorBackgroundFaded, new Rectangle(0, 0, ViewPortManager.Width, ViewPortManager.Height), new Color(255, 255, 255, 10));// asd asd asd 

                    spriteBatch.End();

                    spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                    mainMenu.Draw(gameTime, spriteBatch);
                    spriteBatch.End();
                }
                else
                {

                    //premier beginDraw pour un background fixe
                    spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend); // ancien, avec camera Handmade

                    gameManager.DrawInterface(spriteBatch);

                    spriteBatch.End();
                }

        }
    }
}