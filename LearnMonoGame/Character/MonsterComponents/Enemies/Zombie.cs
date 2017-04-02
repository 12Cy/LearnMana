using LearnMonoGame.Components;
using LearnMonoGame.Manager;
using LearnMonoGame.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnMonoGame.Weapons;
using LearnMonoGame.AI;
using LearnMonoGame.Spells.Light;

namespace LearnMonoGame.Summoneds.Enemies.Monster
{
    class Zombie : Character
    {

        static int ID = 0;

        AIScript homeScript;
        AIScript followerScript;
        AIScript healScript;

        public Zombie(Vector2 _pos) : base(SummonedsInformation.Instance.characterInformation["Skelett"])
        {
            id = "ZM" + ID++;
            pos = _pos;
            creatureTexture = _CM.GetTexture(_CM.TextureName.zombie);
            selectedTexture = _CM.GetTexture(_CM.TextureName.damageselect);
            lifeTexture = _CM.GetTexture(_CM.TextureName.backLife);
            Initialize();
            weapon = new Weapon(new SAbility(EMoveType.Attack, EStatus.Normal, _name: "SwordAttack", _damage: new[] { 1, 5 }, _crit: new[] { 2f, 4f }, _critChance: 30), 1, 100);
            followerScript = new FollowAttackScript(attributes.Alignment);
            homeScript = new MoveBackToHomeLocation(attributes.Alignment, pos);
            aiScript = followerScript;
            spellBook.AddSpell(new SHolyLight(EAlignment.Player));
            healScript = new HealSelfScript(EAlignment.Enemy);
        }
        protected override void Initialize()
        {
            attributes.Alignment = EAlignment.Enemy;
            moveDestination = pos;
            //currentHealth = 200000;
            // --- Animation ---
            animatedSprite = new AnimatedSprite(creatureTexture, _AnimationManager.GetAnimation(_AnimationManager.AnimationName.skelett));
            animatedSprite.CurrentAnimation = AnimationKey.WalkRight;
            animatedSprite.Position = pos;
        }
        public override void Update(GameTime gameTime)
        {
            if (followerScript.DoScript(gameTime, this))
                if (homeScript.DoScript(gameTime, this))
                    healScript.DoScript(gameTime, this);

            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        protected override void WeaponUpdate(GameTime gameTime, EAlignment alignment)
        {
            base.WeaponUpdate(gameTime, alignment);
        }




    }
}
