using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnMonoGame.Summoneds;
using Microsoft.Xna.Framework;
using LearnMonoGame.Weapons;

namespace LearnMonoGame.AI
{
    public class FollowAttackScript : AIScript
    {
        public FollowAttackScript(EAlignment alignment) : base(alignment)
        {

        }

        public override bool DoScript(GameTime gTime, Character c)
        {
            Character target = MonsterManager.Instance.CheckNearestCharacter(alignment, c.HitBox);

            float dist = MonsterManager.Instance.DistanceBetweenTwoCharacters(target, c);

            if (dist > c.attributes.AttackRange)
            {
                ResetMove(c);
                return true;
            }
            UpdateMovement(gTime, c, target);
            UpdateWeapon(gTime, c);
            return false;
        }

        void ResetMove(Character c)
        {
            c.PosDestination = c.Pos;
        }

        void UpdateMovement(GameTime gTime, Character c, Character target)
        {
            c.PosDestination = target.Pos;
        }

        void UpdateWeapon(GameTime gTime, Character c)
        {
            if (c.weaponStatus == EWeaponStatus.TargetFound)
                c.weaponStatus = EWeaponStatus.Channel;
        }
    }
}
