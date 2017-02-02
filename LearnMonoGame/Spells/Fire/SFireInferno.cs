using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using LearnMonoGame.Manager;
using LearnMonoGame.PlayerComponents;
using LearnMonoGame.Summoneds.Enemies;

namespace LearnMonoGame.Spells.Fire
{
    class SFireInferno : Spell
    {
        public SFireInferno() : base(SpellManager.Instance.spellInformation[ESpell.SFireInferno])
        {
        }

        public override void Cast(Vector2 bounds, Vector2 _target)
        {
            if (CastAble())
            {
                timer = 0;
                channelTimer = 0;
            }
        }

        public override void OnChannel(Vector2 bounds, Vector2 _target)
        {
            Vector2 dir = _target - bounds;
            float angle = SpellManager.Instance.rnd.Next(-10, 10);
            dir = rotate(dir, MathHelper.ToRadians(angle));
            PlayerManager.Instance.MyPlayer.ApplyEffect(new SAbility(EMoveType.Attack, EStatus.Normal, _mana: new[] { -manaCost, -manaCost }));
            _BulletManager.Instance.bullets.Add(new Bullets.SimpleBullet(new Rectangle(bounds.ToPoint(), Point.Zero), dir, _CM.GetTexture(_CM.TextureName.fireball), EBullet.FireBall));
        }

        Vector2 rotate(Vector2 vec, float angle)
        {
            return new Vector2((float)Math.Cos(angle) * vec.X - (float)Math.Sin(angle) * vec.Y,
                (float)Math.Sin(angle) * vec.X + (float)Math.Cos(angle) * vec.Y);
        }

        public override string ToString()
        {
            return "FireInferno!";
        }
    }
}
