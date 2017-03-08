using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnMonoGame.Summoneds;
using LearnMonoGame.Weapons;
using Microsoft.Xna.Framework;
using LearnMonoGame.Summoneds.Enemies;

namespace LearnMonoGame.Spells.Darkness
{
    class SCallSkeleton : Spell
    {
        public SCallSkeleton(EAlignment _alignment) : base(SpellManager.Instance.spellInformation["SCallSkeleton"], _alignment)
        {
        }

        public override void Cast(Vector2 position, Vector2 _direction, Character me)
        {
            if (CastAble(me, position, _direction))
            {
                timer = 0;
                channelTimer = 0;
                MonsterManager.Instance.SpawnCharacter("skelett", _direction);
                me.ApplyEffect(new SAbility(EMoveType.Attack, EStatus.Normal, _mana: new[] { -manaCost, -manaCost }));
            }
        }

        public override string ToString()
        {
            return "SCallSkeleton";
        }
    }
}
