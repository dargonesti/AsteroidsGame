using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace AsteroidsPrototype
{
    static class SoundManager
    {
        public static Song LoopSong;
        public static SoundEffect LaserHeavy;
        public static SoundEffect LaserLight;
        public static SoundEffect LaserShorter;
        public static SoundEffect ShotGun;
        public static SoundEffect Explosion;
        public static SoundEffect TimeStart;
        public static SoundEffect TimeStop;
        
        public static float _volumeEffect = 1;

        public static void Load(ContentManager content)
        {
            LoopSong = content.Load<Song>("36secLoopMusic");
            LaserHeavy = content.Load<SoundEffect>("Laser_Shoot34");
            LaserLight = content.Load<SoundEffect>("Laser_Shoot33");
            LaserShorter = content.Load<SoundEffect>("Laser_Shorter");
            ShotGun = content.Load<SoundEffect>("ShotGun");
            Explosion = content.Load<SoundEffect>("Explosion8");
            TimeStart = content.Load<SoundEffect>("time-start");
            TimeStop = content.Load<SoundEffect>("time-stop");

            MediaPlayer.Volume = 0.5f;
        }

        public static void Play(SoundEffect soundEffect, float vol = 1)
        {
            soundEffect.Play(vol, 1, 0);
        }

        public static void Play(Song song, float vol = 1)
        {
            MediaPlayer.Play(song);
            MediaPlayer.Volume = vol;
            MediaPlayer.IsRepeating = true;
        }

    }
}
