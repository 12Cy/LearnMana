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

namespace LearnMonoGame.Summoneds.Enemies.Monster
{
    class Skelett : Character
    {
        private object xin;

        public Skelett(Vector2 _pos) : base(SummonedsInformation.Instance.skelettInformation)
        {
            pos = _pos;
            creatureTexture = _CM.GetTexture(_CM.TextureName.dummy);
            selectedTexture = _CM.GetTexture(_CM.TextureName.damageselect);
            lifeTexture = _CM.GetTexture(_CM.TextureName.backLife);
            Initialize();
        }
        protected override void Initialize()
        {
            moveDestination = pos;
            currentHealth = 10;
            // --- Animation ---
            animatedSprite = new AnimatedSprite(creatureTexture, _AnimationManager.GetAnimation(_AnimationManager.AnimationName.dummy));
            animatedSprite.CurrentAnimation = AnimationKey.WalkRight;
            animatedSprite.Position = pos;
        }
        public override void Update(GameTime gameTime)
        {
            //if (xIn.CheckKeyReleased(Microsoft.Xna.Framework.Input.Keys.M))
            //{
            //    base.ResolveMove(MoveManager.GetMove("Heal"));
            //}
            //if (xIn.CheckKeyReleased(Microsoft.Xna.Framework.Input.Keys.N))
            //{
            //    base.ResolveMove(MoveManager.GetMove("Hot"));
            //}


            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }



    }
}
