using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using AsteroidsPrototype.GameObjects.Weapons;
using AsteroidsPrototype.GameObjects.Defense;
using AsteroidsPrototype.GameObjects;

    /// <summary>
    /// Fichier contenant plusieurs sous classes pour gérer l'IA
    /// </summary>
namespace AsteroidsPrototype.GameManager
{
    /// <summary>
    /// Classe de gestion des différentes actions d'UN IA
    /// </summary>
    public class ActionsIA
    {
        private List<Action> _delaiActions = new List<Action>();

        public void AddAction(Action action, int sleepTime = 0)
        {
            _delaiActions.Add(action);           
        }

        public void Update(GameTime gameTime)
        {
            List<Action > toDelete = new List<Action>();
            foreach (Action tmpDelaiAction in _delaiActions)
            {
                if (tmpDelaiAction.TimePaused > 0)
                {
                    tmpDelaiAction.TimePaused -= gameTime.ElapsedGameTime.Milliseconds;
                }
                else
                {
                    //ces 3 magnifiques lignes == delai -= tempsPassé;
                    tmpDelaiAction.Time -= gameTime.ElapsedGameTime.Milliseconds;
                
                    if (tmpDelaiAction.Time <= 0 || tmpDelaiAction.ActOnDegree)
                    {
                        if (tmpDelaiAction.ActOnDegree)
                        {
                            if (tmpDelaiAction.GoodAngle())
                            {
                                tmpDelaiAction.run();
                                tmpDelaiAction.Time = 300;
                            }
                        }
                        else
                        {
                            if (tmpDelaiAction.run())
                            {
                                tmpDelaiAction.Time = tmpDelaiAction.InitialValue;
                            }
                            else
                            {
                                toDelete.Add(tmpDelaiAction);
                            }
                        }
                    }
                }
            }
            foreach (Action action in toDelete)
            {
                _delaiActions.Remove(action);
            } 
        }
    }

    /// <summary>
    /// Classe abstraite d'une action a éxécuter par l'IA
    /// </summary>
    public abstract class Action
    {
        protected bool _recursive = false;
        protected EnnemyShip  _ship;
        protected int _initialTime = int.MinValue;
        protected int _timePaused = int.MinValue;
        protected int _time = int.MinValue;
        protected float _degreeToAct = 0;
        protected bool _actOnDegree = false;


        #region "accessors"

        public int Time
        {
            get { return _time; }
            set { _time = value;}
        }
        public int TimePaused
        {
            get { return _timePaused; }
            set { _timePaused = value; }
        }
        public bool Recursive
        {
            get { return _recursive; }
            set { _recursive = value; }
        }
        public int InitialValue
        {
            set { _initialTime = value; }
            get { return _initialTime ; }
        }

        public float DegreeToAct
        {
            get { return _degreeToAct; }
            set { _degreeToAct = value; }
        }
        public bool ActOnDegree
        {
            get { return _actOnDegree; }
            set { _actOnDegree = value; }
        }
#endregion
        abstract public bool run();
        public bool GoodAngle()
        {
            return Math.Abs(_ship.RotationInDegrees % 360 - _degreeToAct % 360) < 3;
        }
    }

    /// <summary>
    /// Classe d'IA qui tir un laser
    /// </summary>
    public class ActionShoot : Action
    {
        private Weapon _weapon;

        public ActionShoot(EnnemyShip ship, Weapon arme, int delay = 0, int sleep = 0)
        {
            _time = delay;
            _initialTime = delay;
            _timePaused = sleep; 
            _ship = ship;
            _weapon = arme;
            Recursive = true;
        }
        public override bool run()
        {
            _ship.Shoot(_weapon);
            return Recursive;
        }
    }
    /// <summary>
    /// Classe qui active un bouclier TODO
    /// </summary>
    public class ActionDefend : Action
    {
        public ActionDefend(EnnemyShip ship, DefenseBase shield)
        {

        }
        public override bool run()
        {
            //asd 
            return false;
        }
    }

    /// <summary>
    /// Classe qui bouge un movingObject
    /// </summary>
    public class ActionMouvement : Action
    {
        protected Vector2 _movePosition = Vector2.Zero;
        protected Vector2 _moveSpeed = Vector2.Zero;
        protected Vector2 _moveAccel = Vector2.Zero;
        protected float _rotationDegPerSec = 0;
        protected bool _addNotSet = true;

        public ActionMouvement(EnnemyShip ship, int delay = 0, int sleep = 0)
        {
            _time = delay;
            _timePaused = sleep;
            _initialTime = delay;
            _ship = ship;
        }
        //TROIS fonctions, sois on SET sois on ADD la valeure losr de l'Action
        public void setPosition(Vector2 pSet, bool addNotSet = true)
        {
            _movePosition = pSet;
            _addNotSet = addNotSet;
        }
        public void setSpeed(Vector2 pSet, bool addNotSet = true)
        {
            _moveSpeed = pSet;
            _addNotSet = addNotSet;
        }
        public void setAccel(Vector2 pSet, bool addNotSet = true)
        {
            _moveAccel = pSet;
            _addNotSet = addNotSet;
        }
        public void setRotation(float pSet, bool addNotSet = true)
        {
            _rotationDegPerSec = pSet;
            _addNotSet = addNotSet;
        }

        public override bool run()
        {
            if (_addNotSet)
            {
                _ship.RotationInDegrees += _rotationDegPerSec;
                _ship.changeAngleAccel();
                _ship.setSpeedWithDirection();
                _ship.Position = _ship.Position + _movePosition;
                _ship.Speed = _ship.Speed + _moveSpeed;
                _ship.Acceleration = _ship.Acceleration + _moveAccel;
            }
            else
            {
                _ship.RotationInDegrees = _rotationDegPerSec;
                _ship.changeAngleAccel();
                _ship.setSpeedWithDirection();
                _ship.Position =_movePosition;
                _ship.Speed = _moveSpeed;
                _ship.Acceleration = _moveAccel;
            }
            return Recursive;
        }
    }
    /// <summary>
    /// Classe qui fait suivre le joueur par l'ennemi
    /// </summary>
    public class ActionFollowPlayer : Action
    {
        protected Vector2 _movePosition = Vector2.Zero;
        protected Vector2 _moveSpeed = Vector2.Zero;
        protected Vector2 _moveAccel = Vector2.Zero;
        protected float _rotationDegPerSec = 0;
        protected bool _addNotSet = true;
        protected float _laserSpeed = Laser.LASER_SPEED;
        protected Ship _playerShip = null;
        protected Vector2 _randMiss = Vector2.Zero;

        public ActionFollowPlayer(EnnemyShip ship,Ship playerShip, float laserSpeed = Laser.LASER_SPEED, int minMaxError = 0,int delay = 0, int sleep = 0)
        {
            _time = delay;
            _timePaused = sleep;
            _initialTime = delay;
            _ship = ship;
            _playerShip = playerShip;
            _laserSpeed = laserSpeed + _ship.Speed.Length();
            _randMiss = new Vector2(Utilities.Utilitaire._random.Next(-minMaxError, minMaxError), Utilities.Utilitaire._random.Next(-minMaxError, minMaxError));
        }
        //TROIS fonctions, sois on SET sois on ADD la valeure losr de l'Action
        public void setPosition(Vector2 pSet, bool addNotSet = true)
        {
            _movePosition = pSet;
            _addNotSet = addNotSet;
        }
        public void setSpeed(Vector2 pSet, bool addNotSet = true)
        {
            _moveSpeed = pSet;
            _addNotSet = addNotSet;
        }
        public void setAccel(Vector2 pSet, bool addNotSet = true)
        {
            _moveAccel = pSet;
            _addNotSet = addNotSet;
        }
        public void setRotation(float pSet, bool addNotSet = true)
        {
            _rotationDegPerSec = pSet;
            _addNotSet = addNotSet;
        }
        public void setLaserSpeed(float lsp)
        {
            _laserSpeed = lsp;
        }
        public override bool run()
        {

            _ship.RotationInRadians = Utilities.Utilitaire.RotToAimRad(_ship.Position, _ship.Speed, _playerShip.Position + _randMiss, _playerShip.Speed, _laserSpeed);
            _ship.changeAngleAccel();
            _ship.setSpeedWithDirection();
               
            return Recursive;
        }
    }
    public class ActionSpawnChild : Action
    {

        public override bool run()
        {
            //asd 
            return false;
        }
    }
}
