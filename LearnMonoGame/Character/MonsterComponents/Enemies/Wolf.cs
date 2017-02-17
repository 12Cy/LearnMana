using LearnMonoGame.Components;
using LearnMonoGame.Manager;
using LearnMonoGame.PlayerComponents;
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
    class Wolf : Character
    {
        protected int smell_distance = 320;
        protected int time_till_charge_sec = 5;

        protected int time_to_reach;
        private bool chaseModeTrigger = false; // Player recognised 
        private bool aggroTrigger = false; // Player didnt leave Wolf alone
        private bool runTrigger = false;
       // private bool 

        public Wolf(Vector2 _pos) : base(SummonedsInformation.Instance.characterInformation["Wolf"])
        {
            pos = _pos;
            creatureTexture = _CM.GetTexture(_CM.TextureName.wolf);
            selectedTexture = _CM.GetTexture(_CM.TextureName.damageselect);
            lifeTexture = _CM.GetTexture(_CM.TextureName.backLife);
            Initialize();
            weapon = new Weapon(new SAbility(EMoveType.Attack, EStatus.Normal, _name: "Bite", _damage: new[] { 1, 3 }, _crit: new[] { 2f, 4f }, _critChance: 60), 0.5f, 70);

        }
        protected override void Initialize()
        {
            characterTyp = ECharacterTyp.enemy;
            moveDestination = pos;
            // --- Animation ---
            animatedSprite = new AnimatedSprite(creatureTexture, _AnimationManager.GetAnimation(_AnimationManager.AnimationName.dummy));
            animatedSprite.CurrentAnimation = AnimationKey.WalkRight;
            animatedSprite.Position = pos;
        }
        public override void Update(GameTime gameTime)
        {
            chaseModeTrigger = smell();
            if (chaseModeTrigger && !aggroTrigger)
            {
                time_to_reach = gameTime.TotalGameTime.Seconds + time_till_charge_sec;
                aggroTrigger = true;
            } 
            else if (aggroTrigger && !runTrigger)
            {
                if(gameTime.TotalGameTime.Seconds >= time_to_reach)
                {
                    runTrigger = true;
                }
                
            } else if (runTrigger)
            {
                Vector2 dif = PlayerManager.Instance.MyPlayer.Pos - pos;
                Move(gameTime, dif);
            } else 
            {
                aggroTrigger = false;
                chaseModeTrigger = false;
            }
            base.Update(gameTime);
        }

        protected override void WeaponUpdate(GameTime gameTime, EAlignment alignment)
        {

            if (weaponStatus == EWeaponStatus.TargetFound)
                weaponStatus = EWeaponStatus.Channel;

            base.WeaponUpdate(gameTime, alignment);
        }

        // Riecht, ob der Player sich in der Nähe aufhält.
        private Boolean smell()                
        {
            Vector2 dif = PlayerManager.Instance.MyPlayer.Pos -  pos;
            // Um negative Zahlen zu eliminieren
            int true_dif = (int)(Math.Sqrt(dif.X * dif.X)+Math.Sqrt(dif.Y * dif.Y));
            

            if (true_dif<smell_distance)
            {
                return true;
            }
            else
            {
                aggroTrigger = false;
                runTrigger = false;
                return false;
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (chaseModeTrigger && !runTrigger)
            {
                Vector2 scale = new Vector2(-25,-20);
                spriteBatch.DrawString(_CM.GetFont(_CM.FontName.Arial), "GRRRRRRRR", Pos + scale, Color.Red);               
            } else if (runTrigger)
            {
                Vector2 scale = new Vector2(-30, -20);
                spriteBatch.DrawString(_CM.GetFont(_CM.FontName.Arial), "WOOOOOOOOO", Pos + scale, Color.Red);
            }
                
            base.Draw(spriteBatch);
        }







    }
}
