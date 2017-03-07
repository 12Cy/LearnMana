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

namespace LearnMonoGame.Summoneds.Enemies.Monster
{
    class Zombie : Character
    {
        private object xin;
        private int StepsToWalk = 0;
        private int StepsWalked = 0;
        int updaterate = 1500;

        static int ID = 0;


        public Zombie(Vector2 _pos) : base(SummonedsInformation.Instance.characterInformation["Skelett"])
        {
            id = "ZM" + ID++;
            pos = _pos;
            creatureTexture = _CM.GetTexture(_CM.TextureName.zombie);
            selectedTexture = _CM.GetTexture(_CM.TextureName.damageselect);
            lifeTexture = _CM.GetTexture(_CM.TextureName.backLife);
            Initialize();
            weapon = new Weapon(new SAbility(EMoveType.Attack, EStatus.Normal,_name:"SwordAttack", _damage: new[] { 1, 5 },_crit: new[] { 2f, 4f }, _critChance: 30),1,100);
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
            if (weaponStatus != EWeaponStatus.Channel)
                MoveRandom(gameTime);


            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {

            base.Draw(spriteBatch);
        }

        protected override void WeaponUpdate(GameTime gameTime, EAlignment alignment)
        {

            if (weaponStatus == EWeaponStatus.TargetFound)
                weaponStatus = EWeaponStatus.Channel;

            base.WeaponUpdate(gameTime, alignment);
        }


        protected void MoveRandom(GameTime gameTime)
        {

            int seed = Guid.NewGuid().GetHashCode();
            Random random = new Random(seed);


            if ((int)gameTime.TotalGameTime.TotalMilliseconds % updaterate == 0)
            {
                StepsWalked = 0;
                StepsToWalk = (int)(random.NextDouble() * 7 + 1);
                moveDestination = new Vector2((int)(pos.X + random.NextDouble() * 80 - 40), (int)(pos.Y + random.NextDouble() * 80 - 40));                

            }
            Vector2 dif = moveDestination - pos; //VerbindungsVektor

            Move(gameTime, dif);
        }




    }
}
