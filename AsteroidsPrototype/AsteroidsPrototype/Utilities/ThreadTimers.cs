#region File Description
//-----------------------------------------------------------------------------
// Author: JCBDigger
// URL: http://Games.DiscoverThat.co.uk
//-----------------------------------------------------------------------------
// A set of timers intended for use on threads that do not have access 
// to GameTime. 
// The GameTime used by the main thread cannot be accessed by other threads:
// http://forums.create.msdn.com/forums/t/10587.aspx
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
#endregion

namespace Engine
{
    /// <summary>
    /// A simple stopwatch timer to keep track of the time since it was started.
    /// </summary>
    public class ThreadTimer
    {
        protected Stopwatch threadTime;

        /// <summary>
        /// Create and start the timer.
        /// </summary>
        public ThreadTimer()
        {
            threadTime = Stopwatch.StartNew();
        }
        /// <summary>
        /// The time since the timer was started.
        /// </summary>
        public TimeSpan CurrentTime
        {
            get { return threadTime.Elapsed; }
        }
        /// <summary>
        /// Calculate the future time after a number of seconds has been added 
        /// to the current time.
        /// </summary>
        public TimeSpan FutureTimeAfter(float howManySeconds)
        {
            TimeSpan result = CurrentTime + TimeSpan.FromSeconds(howManySeconds);
            //System.Diagnostics.Debug.WriteLine("End after time: " + result.ToString());
            return result;
        }
    }

    /*/// <summary>
    /// Creates Garbage!
    /// A timer containing a GameTime object as a replacement for GameTime.
    /// This must be updated to record total and elapsed time since the last update.
    /// </summary>
    public class UpdateTimer : ThreadTimer
    {
        /// <summary>
        /// The game time for use in classes.
        /// </summary>
        //public UpdateGameTime GameTime = new UpdateGameTime();
        public GameTime GameTime;
        /// <summary>
        /// Required to calculate the elapsed time.
        /// </summary>
        private TimeSpan previousTime = TimeSpan.Zero;

        public UpdateTimer()
        {
            threadTime = Stopwatch.StartNew();
        }

        /// <summary>
        /// Must be called at the start of every update to calculate the elapsed
        /// time since the previous update was called.
        /// </summary>
        public void Update()
        {
            //GameTime.Update(threadTime.Elapsed);

            //totalUpdateTime = threadTime.Elapsed;
            //elapsedUpdateTime = totalUpdateTime - previousTime;
            //previousTime = totalUpdateTime;

            GameTime = new GameTime(threadTime.Elapsed, threadTime.Elapsed - previousTime);
            previousTime = GameTime.TotalGameTime;
        }
    }*/

    /// <summary>
    /// Garbage free GameTime replacement.
    /// </summary>
    public class UpdateGameTime : ThreadTimer
    {
        private TimeSpan elapsedGameTime = TimeSpan.Zero;
        private TimeSpan totalGameTime = TimeSpan.Zero;
        /// <summary>
        /// Required to calculate the elapsed time.
        /// </summary>
        private TimeSpan previousTime = TimeSpan.Zero;

        /// <summary>
        /// The amount of elapsed game time since the last update.
        /// </summary>
        public TimeSpan ElapsedGameTime { get { return elapsedGameTime; } }
        /// <summary>
        /// The amount of game time since the start of the game.
        /// </summary>
        public TimeSpan TotalGameTime { get { return totalGameTime; } }

        /// <summary>
        /// Set the point in time.
        /// </summary>
        public void Update()
        {
            totalGameTime = threadTime.Elapsed;
            elapsedGameTime = totalGameTime - previousTime;
            previousTime = totalGameTime;
        }
    }

    /// <summary>
    /// Base class for classes with the same properties as GameTime.
    /// This needs to be updated externally with the current times.
    /// 
    /// It is intended to be used so that the same timer can be used
    /// by threads that have access to the original game time and
    /// classes that inherit from this can provide a time for classes
    /// in other threads that do not have access to GameTime.
    /// 
    /// The original GameTime class has a different elapsed time for the 
    /// draw and update methods.
    /// </summary>
    public class GameTimeMirror
    {
        protected TimeSpan elapsedGameTime = TimeSpan.Zero;
        protected TimeSpan totalGameTime = TimeSpan.Zero;
        /// <summary>
        /// The amount of elapsed game time since the last update.
        /// </summary>
        public TimeSpan ElapsedGameTime { get { return elapsedGameTime; } }
        /// <summary>
        /// The amount of game time since the start of the game.
        /// </summary>
        public TimeSpan TotalGameTime { get { return totalGameTime; } }
        /// <summary>
        /// Set the times from an external timer.  
        /// Typically this will be from GameTime.
        /// </summary>
        public void Update(TimeSpan totalTime, TimeSpan elapsedTime)
        {
            totalGameTime = totalTime;
            elapsedGameTime = elapsedTime;
        }
    }

    /// <summary>
    /// This has the same properties as GameTime and and uses it's own 
    /// built in timer to update the values.
    /// </summary>
    public class GameSelfTimer : GameTimeMirror
    {
        /// <summary>
        /// The built in timer starts running as soon as the class is instantiated.
        /// </summary>
        protected Stopwatch threadTime;
        /// <summary>
        /// Required to calculate the elapsed time.
        /// </summary>
        protected TimeSpan previousTime = TimeSpan.Zero;
        /// <summary>
        /// Create and start the timer.
        /// </summary>
        public GameSelfTimer()
        {
            threadTime = Stopwatch.StartNew();
        }
        /// <summary>
        /// Store the point in time from the built in timer.
        /// </summary>
        public void Update()
        {
            totalGameTime = threadTime.Elapsed;
            elapsedGameTime = totalGameTime - previousTime;
            previousTime = totalGameTime;
        }
        /// <summary>
        /// Freeze the action while the game is paused.
        /// </summary>
        public void PauseElapsedTime()
        {
            previousTime = threadTime.Elapsed;
        }
        /// <summary>
        /// Returns the elasped time since the last update without updating
        /// the game times.
        /// This is used to deliberately slow down the frame rate.
        /// </summary>
        public TimeSpan ElapsedUpdateTime
        {
            get
            {
                return threadTime.Elapsed - previousTime;
            }
        }
    }

}
