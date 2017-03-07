using LearnMonoGame.Manager;
using LearnMonoGame.Particle;
using LearnMonoGame.Summoneds;
using LearnMonoGame.Summoneds.Enemies;
using LearnMonoGame.Tools;
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
        public int[] randValues;

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

        public void ApplyRandAttributes(Attributes attr, Character c)
        {
            randValues = new[]{
                MyMath.CalculateRandomValue(effect.attackDamage),
                MyMath.CalculateRandomValue(effect.attackSpeed),
                MyMath.CalculateRandomValue(effect.defense),
                MyMath.CalculateRandomValue(effect.speed)
            };

            if (randValues[0] > 0)
                _ParticleManager.Instance.particles.Add(new PopUpText(c.Pos + new Vector2(c.Width / 2, c.Height / 2) - new Vector2(10, 20), 2f, randValues[0].ToString(), Color.OrangeRed));
            else if (randValues[0] < 0)
                _ParticleManager.Instance.particles.Add(new PopUpText(c.Pos + new Vector2(c.Width / 2, c.Height / 2) - new Vector2(10, 20), 2f, randValues[0].ToString(), Color.Orchid));

            if (randValues[1] > 0)
                _ParticleManager.Instance.particles.Add(new PopUpText(c.Pos + new Vector2(c.Width / 2, c.Height / 2) - new Vector2(10, 20), 2f, randValues[1].ToString(), Color.Gray));
            else if (randValues[1] < 0)
                _ParticleManager.Instance.particles.Add(new PopUpText(c.Pos + new Vector2(c.Width / 2, c.Height / 2) - new Vector2(10, 20), 2f, randValues[1].ToString(), Color.DarkSeaGreen));

            if (randValues[3] > 0)
                _ParticleManager.Instance.particles.Add(new PopUpText(c.Pos + new Vector2(c.Width / 2, c.Height / 2) - new Vector2(10, 20), 2f, randValues[3].ToString(), Color.Yellow));
            else if (randValues[3] < 0)
                _ParticleManager.Instance.particles.Add(new PopUpText(c.Pos + new Vector2(c.Width / 2, c.Height / 2) - new Vector2(10, 20), 2f, randValues[3].ToString(), Color.BlueViolet));

            if (randValues[2] > 0)
                _ParticleManager.Instance.particles.Add(new PopUpText(c.Pos + new Vector2(c.Width / 2, c.Height / 2) - new Vector2(10, 20), 2f, randValues[2].ToString(), Color.BlanchedAlmond));
            else if (randValues[2] < 0)
                _ParticleManager.Instance.particles.Add(new PopUpText(c.Pos + new Vector2(c.Width / 2, c.Height / 2) - new Vector2(10, 20), 2f, randValues[2].ToString(), Color.AliceBlue));


            attr.AttackDamage += randValues[0];
            attr.AttackSpeed += randValues[1];
            attr.Defense += randValues[2];
            attr.Speed += randValues[3];
        }

        public void ReRollAttributes(Attributes attr, Character c)
        {
            if (randValues[0] < 0)
                _ParticleManager.Instance.particles.Add(new PopUpText(c.Pos + new Vector2(c.Width / 2, c.Height / 2) - new Vector2(10, 20), 2f, (-randValues[0]).ToString(), Color.OrangeRed));
            else if (randValues[0] > 0)
                _ParticleManager.Instance.particles.Add(new PopUpText(c.Pos + new Vector2(c.Width / 2, c.Height / 2) - new Vector2(10, 20), 2f, (-randValues[0]).ToString(), Color.Orchid));

            if (randValues[1] < 0)
                _ParticleManager.Instance.particles.Add(new PopUpText(c.Pos + new Vector2(c.Width / 2, c.Height / 2) - new Vector2(10, 20), 2f, (-randValues[1]).ToString(), Color.Gray));
            else if (randValues[1] > 0)
                _ParticleManager.Instance.particles.Add(new PopUpText(c.Pos + new Vector2(c.Width / 2, c.Height / 2) - new Vector2(10, 20), 2f, (-randValues[1]).ToString(), Color.DarkSeaGreen));

            if (randValues[3] < 0)
                _ParticleManager.Instance.particles.Add(new PopUpText(c.Pos + new Vector2(c.Width / 2, c.Height / 2) - new Vector2(10, 20), 2f, (-randValues[3]).ToString(), Color.Yellow));
            else if (randValues[3] > 0)
                _ParticleManager.Instance.particles.Add(new PopUpText(c.Pos + new Vector2(c.Width / 2, c.Height / 2) - new Vector2(10, 20), 2f, (-randValues[3]).ToString(), Color.BlueViolet));

            if (randValues[2] < 0)
                _ParticleManager.Instance.particles.Add(new PopUpText(c.Pos + new Vector2(c.Width / 2, c.Height / 2) - new Vector2(10, 20), 2f, (-randValues[2]).ToString(), Color.BlanchedAlmond));
            else if (randValues[2] > 0)
                _ParticleManager.Instance.particles.Add(new PopUpText(c.Pos + new Vector2(c.Width / 2, c.Height / 2) - new Vector2(10, 20), 2f, (-randValues[2]).ToString(), Color.AliceBlue));


            if (randValues != null)
            {
                attr.AttackDamage -= randValues[0];
                attr.AttackSpeed -= randValues[1];
                attr.Defense -= randValues[2];
                attr.Speed -= randValues[3];
            }
        }

        public bool Update(GameTime gTime)
        {
            timerDuration -= gTime.ElapsedGameTime.TotalSeconds;
            if (timerDuration > 0) return false;
            return true;
        }
    }
}
