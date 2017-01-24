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

namespace LearnMonoGame.Summoneds.Enemies.Monster
{
    class Wolf : Character
    {
        private object xin;
        private int smell_distance = 100;
        private bool chaseMode = false;

        public Wolf(Vector2 _pos) : base(SummonedsInformation.Instance.wolfInformation)
        {
            pos = _pos;
            creatureTexture = _CM.GetTexture(_CM.TextureName.dummy);
            selectedTexture = _CM.GetTexture(_CM.TextureName.damageselect);
            lifeTexture = _CM.GetTexture(_CM.TextureName.backLife);
            Initialize();
        }
        protected override void Initialize()
        {
            characterTyp = ECharacterTyp.enemy;
            moveDestination = pos;
            currentHealth = 10;
            // --- Animation ---
            animatedSprite = new AnimatedSprite(creatureTexture, _AnimationManager.GetAnimation(_AnimationManager.AnimationName.dummy));
            animatedSprite.CurrentAnimation = AnimationKey.WalkRight;
            animatedSprite.Position = pos;
        }
        public override void Update(GameTime gameTime)
        {
            chaseMode = smell();
            if (chaseMode)
            {
                Vector2 dif = PlayerManager.Instance.MyPlayer.Pos - pos;
                Move(gameTime, dif);
            }
            base.Update(gameTime);
        }

        // Riecht, ob der Player sich in der Nähe aufhält.
        private Boolean smell()                
        {
            Vector2 dif = PlayerManager.Instance.MyPlayer.Pos -  pos;
            // Um negative Zahlen zu eliminieren
            long true_dif = (long)(Math.Sqrt(dif.X * dif.X)+Math.Sqrt(dif.X * dif.Y));

            //Console.WriteLine(true_dif);

            if (true_dif<smell_distance)
            {
                //Console.WriteLine("Smelled him haha!");
                return true;
            }
            else
            {
                return false;
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {

            base.Draw(spriteBatch);
        }







    }
}
