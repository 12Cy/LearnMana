using LearnMonoGame.Manager;
using LearnMonoGame.PlayerComponents;
using LearnMonoGame.Spells;
using LearnMonoGame.Spells.Darkness;
using LearnMonoGame.Spells.Fire;
using LearnMonoGame.Tools;
using LearnMonoGame.Weapons;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame.Summoneds.Enemies.Monster
{
    class SkeletonMage : Character
    {
        int attackRange = 400;

        static int ID = 0;

        public SkeletonMage(Vector2 _pos) : base(SummonedsInformation.Instance.characterInformation["SkeletonMage"])
        {
            id = "SM" + ID++;
            pos = _pos;
            creatureTexture = _CM.GetTexture(_CM.TextureName.skeletonMage);
            selectedTexture = _CM.GetTexture(_CM.TextureName.damageselect);
            lifeTexture = _CM.GetTexture(_CM.TextureName.backLife);
            Initialize();
            weapon = new Weapon(new SAbility(EMoveType.Attack, EStatus.Normal, _name: "WandHit", _damage: new[] { 1, 5 }, _crit: new[] { 2f, 4f }, _critChance: 30), 1, 80);

            attributes.Alignment = EAlignment.Enemy;
            moveDestination = pos;
            animatedSprite = new AnimatedSprite(creatureTexture, _AnimationManager.GetAnimation(_AnimationManager.AnimationName.skeletonMage));
            animatedSprite.CurrentAnimation = AnimationKey.WalkRight;
            animatedSprite.Position = pos;
            spellBook.AddSpell(new SCallSkeleton(EAlignment.Enemy));

            //spellBook.AddSpell(new SFireInferno(EAlignment.Enemy));




        }

        void Fight(GameTime gameTime)
        {
            Vector2 playerDiff = PlayerManager.Instance.MyPlayer.AimPoint;
            if (spellBook.ToString().CompareTo("SCallSkeleton") == 0)
            {
                playerDiff = pos + new Vector2(SpellManager.Instance.rnd.Next(400) - 200, SpellManager.Instance.rnd.Next(400) - 200);
            }
            if (spellBook.Cast(gameTime, pos, playerDiff, this))
            {
                if (weaponStatus == EWeaponStatus.TargetFound)
                    weaponStatus = EWeaponStatus.Channel;
            }
        }


        public override void Update(GameTime gameTime)
        {
            Vector2 playerDiff = PlayerManager.Instance.MyPlayer.Pos;

            playerDiff -= pos;

            if (playerDiff.Length() < attackRange)
            {
                Move(gameTime, playerDiff);
                Fight(gameTime);
            }




            base.Update(gameTime);
        }
    }
}
