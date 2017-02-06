using LearnMonoGame.Tools;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame.Manager
{
    class _AnimationManager
    {

        private static Dictionary<AnimationName, Dictionary<AnimationKey, Animation>> AnimationDictionary = new Dictionary<AnimationName, Dictionary< AnimationKey, Animation>>();
        static ContentManager Content;


        static void LoadAnimation()
        {
            #region Character
            //LoadPlayer
            Dictionary<AnimationKey, Animation> playerDic = new Dictionary<AnimationKey, Animation>();

            Animation animation = new Animation(3, 64, 64, 0, 0);
            playerDic.Add(AnimationKey.WalkDown, animation);

            animation = new Animation(3, 64, 64, 0, 64);
            playerDic.Add(AnimationKey.WalkLeft, animation);

            animation = new Animation(3, 64, 64, 0, 128);
            playerDic.Add(AnimationKey.WalkRight, animation);

            animation = new Animation(3, 64, 64, 0, 192);
            playerDic.Add(AnimationKey.WalkUp, animation);

            AnimationDictionary.Add(AnimationName.player, playerDic);

            //LoadDummy

            Dictionary<AnimationKey, Animation> dummyDic = new Dictionary<AnimationKey, Animation>();

            animation = new Animation(3, 64, 64, 0, 0);
            dummyDic.Add(AnimationKey.WalkDown, animation);

            animation = new Animation(3, 64, 64, 0, 64);
            dummyDic.Add(AnimationKey.WalkLeft, animation);

            animation = new Animation(3, 64, 64, 0, 128);
            dummyDic.Add(AnimationKey.WalkRight, animation);

            animation = new Animation(3, 64, 64, 0, 192);
            dummyDic.Add(AnimationKey.WalkUp, animation);

            AnimationDictionary.Add(AnimationName.dummy, dummyDic);

            // Load Wolf

            Dictionary<AnimationKey, Animation> wolfDic = new Dictionary<AnimationKey, Animation>();

            animation = new Animation(4, 32, 32, 0, 0);
            wolfDic.Add(AnimationKey.WalkDown, animation);

            animation = new Animation(4, 32, 32, 0, 32);
            wolfDic.Add(AnimationKey.WalkLeft, animation);

            animation = new Animation(4, 32, 32, 0, 64);
            wolfDic.Add(AnimationKey.WalkRight, animation);

            animation = new Animation(4, 32, 32, 0, 96);
            wolfDic.Add(AnimationKey.WalkUp, animation);

            AnimationDictionary.Add(AnimationName.wolf, wolfDic);

            //SkelettDic
            Dictionary<AnimationKey, Animation> skelettDic = new Dictionary<AnimationKey, Animation>();


            animation = new Animation(6, 64, 64, 0, 0);
            skelettDic.Add(AnimationKey.WalkDown, animation);

            animation = new Animation(6, 64, 64, 0, 64);
            skelettDic.Add(AnimationKey.WalkLeft, animation);

            animation = new Animation(6, 64, 64, 0, 128);
            skelettDic.Add(AnimationKey.WalkRight, animation);

            animation = new Animation(6, 64, 64, 0, 192);
            skelettDic.Add(AnimationKey.WalkUp, animation);

            AnimationDictionary.Add(AnimationName.skelett, skelettDic);

            //ZombieDIc
            Dictionary<AnimationKey, Animation> zombieDic = new Dictionary<AnimationKey, Animation>();


            animation = new Animation(6, 64, 64, 0, 0);
            zombieDic.Add(AnimationKey.WalkDown, animation);

            animation = new Animation(6, 64, 64, 0, 64);
            zombieDic.Add(AnimationKey.WalkLeft, animation);

            animation = new Animation(6, 64, 64, 0, 128);
            zombieDic.Add(AnimationKey.WalkRight, animation);

            animation = new Animation(6, 64, 64, 0, 192);
            zombieDic.Add(AnimationKey.WalkUp, animation);

            AnimationDictionary.Add(AnimationName.zombie, zombieDic);

            #endregion

            #region effects
            //Effects

            Dictionary<AnimationKey, Animation> effectsDic = new Dictionary<AnimationKey, Animation>();

            animation = new Animation(10, 128, 128, 0, 0);
            effectsDic.Add(AnimationKey.heal, animation);
 

            animation = new Animation(5, 128, 128, 0, 0);
            effectsDic.Add(AnimationKey.burn, animation);

            animation = new Animation(1, 32, 35, 0, 0);
            effectsDic.Add(AnimationKey.tornado, animation);

            animation = new Animation(4, 96, 96, 0, 0);
            effectsDic.Add(AnimationKey.manaSource, animation);

            AnimationDictionary.Add(AnimationName.effects, effectsDic);

            #endregion


            Dictionary<AnimationKey, Animation> clickAnimation = new Dictionary<AnimationKey, Animation>();

            animation = new Animation(6, 32, 32, 0, 0);
            clickAnimation.Add(AnimationKey.moveClick, animation);

            AnimationDictionary.Add(AnimationName.move, clickAnimation);








        }
        public static Dictionary <AnimationKey, Animation> GetAnimation (AnimationName animationName)
        {

            if (AnimationDictionary.Count == 0)
            {
                LoadAnimation();
            }

            return AnimationDictionary[animationName];
        }


        public _AnimationManager(ContentManager _content)
        {
            Content = _content;

        }

        public enum AnimationName
        {
            player,
            dummy,
            wolf,
            effects,
            skelett,
            zombie,
            move

        }
    }
}
