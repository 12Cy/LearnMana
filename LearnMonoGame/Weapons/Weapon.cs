using LearnMonoGame.PlayerComponents;
using LearnMonoGame.Summoneds;
using LearnMonoGame.Summoneds.Enemies;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame.Weapons
{
    public enum EAlignment
    {
        Enemy,
        Player,
        All
    }
    /// <summary>
    /// Eine Vorlage für NAHKAMPFWaffen.
    /// </summary>
    public class Weapon
    {
        SAbility attackEffect;
        float timer, attackTime;
        float range;
        int x;

        public Weapon(SAbility effect, float attackSpeed, float attackRange)
        {
            x = 0;
            timer = 0;
            attackTime = attackSpeed;
            attackEffect = effect;
            range = attackRange;
        }

        //Überprüft ob der Character überhaupt in Reichweite ist
        public bool CheckAttack(Character c, EAlignment alignment)
        {
            if ((c.Bounds.Location.ToVector2() - PlayerManager.Instance.MyPlayer.Bounds.Location.ToVector2()).Length() < range)
                return true;
            return false;
        }

        /// <summary>
        /// Der Character kanalisiert mit seiner Waffe - er holt aus - und gibt true zurück, wenn er zuschlägt.
        /// Dabei ruft er Attack() auf
        /// </summary>
        /// <param name="gTime"></param>
        /// <returns></returns>
        public bool Channel(Character c, GameTime gTime, EAlignment alignment)
        {
            timer += (float)gTime.ElapsedGameTime.TotalSeconds;

            

            if (timer >= attackTime)
            {
                x = 0;
                timer = 0;
                Attack(c, alignment);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Die Waffe führt die Attack aus
        /// </summary>
        /// <param name="c"></param>
        public void Attack(Character c, EAlignment alignment)
        {
            if ((c.Bounds.Location.ToVector2() - PlayerManager.Instance.MyPlayer.Bounds.Location.ToVector2()).Length() < range)
                PlayerManager.Instance.MyPlayer.ApplyEffect(attackEffect);
        }


    }
}
