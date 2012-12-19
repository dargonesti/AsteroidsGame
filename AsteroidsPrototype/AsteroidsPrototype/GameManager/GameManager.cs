using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using AsteroidsPrototype.GameObjects;
using ParallelTasks;
using AsteroidsPrototype.GameObjects.Weapons;
using AsteroidsPrototype.GameObjects.Walls;
using AsteroidsPrototype.GameManager.Levels;
using AsteroidsPrototype.GameObjects.Powerups;
using AsteroidsPrototype.Utilities;

using AsteroidsPrototype.Menus;

namespace AsteroidsPrototype.GameManager
{
    /// <summary>
    /// Classe gérant las cène de jeu, les updates des MovingObject et les Draws
    /// </summary>
    public class GameManager
    {
        public const int TIME_FREEZE_DURATION = 5;
        public static Boolean InfiniteWorld = true;

        /// <summary>
        /// Niveau courrement en jeu (Doit être une référence à un élément de _lstLevels)
        /// </summary>
        private ILevel _nivCourant = new Test1();
        /// <summary>
        /// Liste des niveaux 
        /// </summary>
        public List<ILevel> _lstLevels = new List<ILevel>() { new Level1(), new Level2(), new Level3(), new Level4(), new Level5(), new Level7(), new Test1(), new Test2() };

        private Radar _radar;
        private Ship _ship;
        private List<MovingObject> _enemies;
        private List<MovingObject> _alies;
        private List<Wall> _walls;
        private List<Powerup> _powerups;
        public List<Score> _scores;

        private Texture2D _background;
        public Game1 _game;
        public static int _WidthGame = 3000;
        public static int _HeightGame = 3000;
        public static Vector2 _ScreenSizeBy2 = new Vector2(0, 0);

        private long timeBetweenUpdates = 0;
        
        private int _currSec = 0;

        public int _playerScore = 0;
        public int _playerDeaths = 0;
        public int _astKilled = 0;
        public int _enemiKilled = 0;
        public int _levelsCompleted = 0;

        //Variables de caméra
        private MyCamera _cam = null;
        private int _mouseWheel = 0;
        private bool _zoomPressed = false;
        private float _zoomStrength = 1.2f;

        private bool _timeFreeze;
        private double _timeFreezeBeginning;

        private KeyboardState _lastState;

        SpriteFont txtDescription;
        Vector2 txtPos;

        #region "accessors"
        /// <summary>
        /// Setter le niv_courant et set tout les éléments du game manager
        /// </summary>
        public ILevel nivCourant
        {
            get
            {
                return _nivCourant;
            }
            set
            {
                _nivCourant = value;
                _nivCourant.InitializeWorld(ref _enemies, ref _alies, ref _walls, ref _powerups, ref _ship, ref _WidthGame, ref _HeightGame);
                _cam.Ship = _ship;
                _cam._sideScroller = value.IsSideScroller();
                _cam.Pos = _ship.Position;
                _scores = new List<Score>();
                for (int i = 0; i < 6; i++)
                {
                    _scores.Add(new Score(new Vector2(_ship.Position.X + Utilitaire._random.Next(-200, 200), _ship.Position.Y + Utilitaire._random.Next(-200, 200)), (int)_ship.Position.Y, value.GetName()));
                }
                try
                {
                    _radar = new Radar(_game, this);
                }
                catch { }
            }
        }

        public List<Powerup> Powerups
        {
            get { return _powerups; }
            set { _powerups = value; }
        }
        public Ship ship
        {
            get { return _ship; }
            set { _ship = value; }
        }

        public List<MovingObject> enemies 
        {
            get {return _enemies ;}
        }

        public Texture2D Background
        {
            get { return _background; }
            set { _background = value; }
        }

        public bool TimeFreeze
        {
            get { return _timeFreeze; }
            set
            {
                _timeFreeze = value;
                if (_timeFreeze)
                {
                    SoundManager.Play(SoundManager.TimeStart);
                }
                else
                {
                    SoundManager.Play(SoundManager.TimeStop);
                }
            }
        }

        public Matrix getCameraTransformation(GameTime gameTime)
        {
            return _cam.get_transformation(gameTime);
        }

        #endregion
        /// <summary>
        /// Constructeur de base de gameManager
        /// </summary>
        /// <param name="pgame"></param>
        public GameManager(Game1 pgame)
        {
            _lstLevels = new List<ILevel>() { new Level1(), new Level2(), new Level3(), new Level4(), new Level5(), new Level6(), new Level7() };
            _cam = new MyCamera();
            _currSec = DateTime.Now.Second;
            nivCourant = _lstLevels[0];
            _cam.Pos = _ship.Position;

            _powerups = new List<Powerup>();
            _scores = new List<Score>();
            txtPos = new Vector2(121, 123);
            txtDescription = Game1.txtDescription;
            _game = pgame;

            _radar = new Radar(pgame,this);

            _ScreenSizeBy2 = new Vector2(ViewPortManager.Width / 2, ViewPortManager.Height / 2);

            _timeFreeze = false;
        }


        public void HandlePlayerInput(KeyboardState keyboardState, MouseState mouseState)
        {
            updateCamera(keyboardState, mouseState, GamePad.GetState(PlayerIndex.One));
            _ship.HandlePlayerInput(keyboardState, mouseState, GamePad.GetState(PlayerIndex.One));
        }

        /// <summary>
        /// update la caméra, le niveau de zoom et la position
        /// </summary>
        /// <param name="keyboardState"></param>
        /// <param name="mouseState"></param>
        /// <param name="gamePadState"></param>
        private void updateCamera(KeyboardState keyboardState, MouseState mouseState, GamePadState gamePadState)
        {
            if (mouseState.ScrollWheelValue != _mouseWheel)
            {
                _cam.Zoom = (float)(_cam.Zoom * Math.Pow(_zoomStrength, (mouseState.ScrollWheelValue - _mouseWheel)/150f));
                _mouseWheel = mouseState.ScrollWheelValue;
            }
            if (keyboardState.IsKeyDown(Keys.OemMinus) || gamePadState.DPad.Down == ButtonState.Pressed)
            {
                if (_zoomPressed == false)
                {
                    _zoomPressed = true;
                    _cam.Zoom = _cam.Zoom / _zoomStrength;
                }
            }
            else if (keyboardState.IsKeyDown(Keys.OemPlus) || gamePadState.DPad.Up == ButtonState.Pressed)
            {
                if (_zoomPressed == false)
                {
                    _zoomPressed = true;
                    _cam.Zoom = _cam.Zoom * _zoomStrength;
                }
            }
            else
            {
                _zoomPressed = false;
            }

            if (keyboardState.IsKeyDown(Keys.L) && _lastState.IsKeyUp(Keys.L))
            {
                int iNivCourant = _lstLevels.IndexOf(nivCourant) + 1;
                if (iNivCourant >= _lstLevels.Count)
                {
                    iNivCourant = 0;
                }
                nivCourant = _lstLevels[iNivCourant];
            }

            _lastState = keyboardState;
        }

        /// <summary>
        /// Update la position de tous les movingObject de la scène
        /// </summary>
        /// <param name="gameTime"></param>
        public void UpdateScene(GameTime gameTime)
        {
            timeBetweenUpdates = DateTime.Now.Ticks;
            List<MovingObject> toDelete = new List<MovingObject>();
            List<MovingObject> enemiesToAdd = new List<MovingObject>();

            _ship.Update(gameTime);
            _alies.AddRange(_ship.spawnedChilds());
            try
            {
                if (_timeFreeze)
                {
                    if (gameTime.TotalGameTime.TotalSeconds - _timeFreezeBeginning >= TIME_FREEZE_DURATION)
                    {
                        this.TimeFreeze = false;
                    }
                }
                else
                {
                    _timeFreezeBeginning = gameTime.TotalGameTime.TotalSeconds;
                }

                foreach (Wall tmpWall in _walls)
                {
                    _ship.Shield.TouchWall(tmpWall);
                }

                //foreach multithread
                ParallelTasks.Parallel.ForEach<MovingObject>(_enemies, enemy =>
                {
                    if (enemy != null && !_timeFreeze)
                    {
                        enemy.Update(gameTime);
                        if (ship.FadeTime <= 0 &&
                            Math.Abs(enemy.Position.X - _ship.Position.X) < enemy.Sprite.Width * enemy.scale / 2 + ship.Sprite.Width * ship.scale / 3 &&
                            Math.Abs(enemy.Position.Y - _ship.Position.Y) < enemy.Sprite.Height * enemy.scale / 2 + ship.Sprite.Height * ship.scale / 3)
                        {
                            _ship.Shield.Touch();
                            enemy.Shield.Touch();
                            _ship.FadeTime = 2000;//2 sec
                            if (_playerScore > 0)
                                _playerScore += _ship._value;
                            _scores.Add(new Score(_ship.Position, (int)_ship.Position.Y, _ship._value));
                        }

                        foreach (Wall tmpWall in _walls)
                        {
                            enemy.Shield.TouchWall(tmpWall);
                        }
                    }
                });

                foreach (Powerup pwTmp in _powerups)
                {
                    pwTmp.Update(gameTime);
                    if (pwTmp.IsActive)
                    {
                        if (Math.Abs(pwTmp.Position.X - _ship.Position.X) < pwTmp.Sprite.Width * pwTmp.scale / 2 + ship.Sprite.Width * ship.scale / 3 &&
                            Math.Abs(pwTmp.Position.Y - _ship.Position.Y) < pwTmp.Sprite.Height * pwTmp.scale / 2 + ship.Sprite.Height * ship.scale / 3)
                        {
                            if (pwTmp.IsAppliedToWorld)
                            {
                                pwTmp.ApplyTo(this);
                            }
                            else
                            {
                                pwTmp.ApplyTo(_ship);
                            }
                            pwTmp.IsActive = false;
                            if (!pwTmp.CanRespawn)
                            {
                                toDelete.Add(pwTmp);
                            }

                        }
                    }

                }
                foreach (Powerup pwTmp in toDelete)
                {
                    _powerups.Remove(pwTmp);
                }

                Parallel.ForEach<MovingObject>(_alies, laser =>
                {
                    laser.Update(gameTime);
                    foreach (MovingObject ast in _enemies.Where(FindNotFade))
                    {
                        if (Math.Abs(ast.Position.X - laser.Position.X) < ast.Sprite.Width * ast.scale / 2 &&
                            Math.Abs(ast.Position.Y - laser.Position.Y) < ast.Sprite.Height * ast.scale / 2)
                        {
                            if (laser.IsAlive)
                            {
                                laser.Shield.Touch();
                                ast.Shield.Touch();

                                if (ast.IsAlive == false)
                                {
                                    _scores.Add(new Score(ast.Position, (int)ast.Position.Y, ast._value));
                                    _playerScore += ast._value;

                                    if (ast._value == TypeScore.Asteroid)
                                        _astKilled += 1;
                                    else
                                        _enemiKilled += 1;
                                }
                            }
                        }
                    }

                    foreach (Wall tmpWall in _walls)
                    {
                        laser.Shield.TouchWall(tmpWall);
                    }
                });


                foreach (MovingObject enemy in _enemies)
                {
                    enemiesToAdd.AddRange(enemy.spawnedChilds());
                }
                _enemies.AddRange(enemiesToAdd);

                enemiesToAdd.Clear();

                foreach (Laser laser in _alies)
                {
                    enemiesToAdd.AddRange(laser.spawnedChilds());
                }

                foreach (Score score in _scores)
                {
                    if (score != null)
                    {
                        score._pos.Y -= 1;
                        score._alpha -= 4;
                        if ((int)score._alpha <= 0)
                            score._display = false;
                    }
                }

                //doit enlever les éléments morts de la scène ici pour ne pas faire d'erreures dans le multithread
                _alies.AddRange(enemiesToAdd);
                _alies.RemoveAll(FindNulls);
                _enemies.RemoveAll(FindNulls);
                _alies.RemoveAll(FindDeads);
                _enemies.RemoveAll(FindDeads);
                _scores.RemoveAll(FindObsoleteScore);

                _alies.RemoveAll(FindDeads);
            }
            catch { 
            }
        
            if (!ship.IsAlive)
            {
                _playerDeaths += 1;
                PlayerDead();
            }
            if (_enemies.Count == 0 || (nivCourant.IsSideScroller() && _ship.XPosition >= _WidthGame - 500))
            {
                _levelsCompleted += 1;
                LevelSuccess();
            }
            _cam.ApplyShake();
        }

        /// <summary>
        /// évènement de la mort du joueur
        /// </summary>
        private void PlayerDead()
        {
            _ship.IsAlive = true;
            nivCourant = nivCourant;
            _game.mainMenu = MenuCreator.createDeathMenu(App.PrimaryController, App.Controllers, App.TitleSafeArea, this);
        }

        /// <summary>
        /// évènement de fin de niveau
        /// </summary>
        private void LevelSuccess()
        {
            int iNivCourant = _lstLevels.IndexOf(nivCourant) + 1;
            if (iNivCourant >= _lstLevels.Count)
            {
                iNivCourant = 0;
            }
            nivCourant = _lstLevels[iNivCourant];
            _game.mainMenu = MenuCreator.createWinMenu(App.PrimaryController, App.Controllers, App.TitleSafeArea, this);
        }

        /// <summary>
        /// retourne la sous liste de tous les éléments nulls pour les enlever hors d'un foreach
        /// </summary>
        /// <param name="pLaser"></param>
        /// <returns></returns>
        private static bool FindNulls(MovingObject pLaser)
        {
            return pLaser == null;
        }

        /// <summary>
        /// retourne la sous liste de tous les éléments morts pour les enlever hors d'un foreach
        /// </summary>
        /// <param name="pLaser"></param>
        /// <returns></returns>
        private static bool FindDeads(MovingObject pLaser)
        {
            return  !pLaser.IsAlive;
        }

        /// <summary>
        /// retourne la sous liste de tous les éléments alive d'une liste
        /// </summary>
        /// <param name="pLaser"></param>
        /// <returns></returns>
        private static bool FindAlive(MovingObject pLaser)
        {
            return pLaser.IsAlive;
        }

        private static bool FindNotFade(MovingObject pObject)
        {
            return pObject.FadeTime <= 0;
        }
        //predicate pour un List<Score>.findAll();
        private static bool FindObsoleteScore(Score pScore)
        {
            return (pScore == null ? true : pScore._display == false);
        }

        /// <summary>
        /// Déssine les éléments fixes de la fenêtre
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void DrawInterface(SpriteBatch spriteBatch)
        {
            _radar.Draw(spriteBatch);
        
            for (int i = 0; i < _ship.LstWeapons.Count; i++)
            {
                if (_ship.WeaponsLocked)
                {
                    spriteBatch.Draw(_ship.LstWeapons[i].Icon, new Vector2(i * _ship.LstWeapons[i].Icon.Width, 0), null, Color.Red, 0, Vector2.Zero, 1, SpriteEffects.None, 0f);
                }
                else
                {
                    spriteBatch.Draw(_ship.LstWeapons[i].Icon, new Vector2(i * _ship.LstWeapons[i].Icon.Width, 0), null, (_ship.LstWeapons[i] == _ship.ActiveWeapon ? Color.Green : Color.White * 0.5f), 0, Vector2.Zero, 1, SpriteEffects.None, 0f);
                }               
            }
        }

        /// <summary>
        /// Dessine les éléments de la scène,
        ///     selon la position de la caméra (du ship)
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void DrawScene(SpriteBatch spriteBatch)
        {
            foreach (Wall wall in _walls)
            {
                wall.Draw(spriteBatch);
            }

            foreach (Powerup pwTmp in _powerups)
            {
                if (pwTmp.IsActive)
                {
                    pwTmp.Draw(spriteBatch);
                }
                
            }
            //spriteBatch.Draw(Background, screenRectangle, Color.White);
            _ship.Draw(spriteBatch);
            foreach (MovingObject enemy in _enemies)
            {
                enemy.Draw(spriteBatch);
            }
            foreach (Laser laser in _alies)
            {
                laser.Draw(spriteBatch);
            }
            foreach(Score score in _scores)
            {
                score.Draw(spriteBatch,txtDescription);
            }
        }
    }
}
