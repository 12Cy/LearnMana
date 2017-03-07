using LearnMonoGame.Summoneds.Enemies;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame
{
    //TODO: In eine extra Klassen-Datei
    public class TimerMove
    {
        public SAbility effect;
        double timerDuration;
        double timerTrigger;
        public bool applyStatus;

        public TimerMove(SAbility _effect, float duration)
        {
            applyStatus = true;
            timerTrigger = 0;
            effect = _effect;
            timerDuration = duration;
        }

        public bool Trigger(GameTime gTime)
        {
            timerTrigger += gTime.ElapsedGameTime.TotalSeconds;
            if (timerTrigger >= effect.trigger)
            {
                timerTrigger = 0;
                return true;
            }
            return false;
        }

        public bool Update(GameTime gTime)
        {
            timerDuration -= gTime.ElapsedGameTime.TotalSeconds;
            if (timerDuration > 0) return false;
            return true;
        }
    }
}
