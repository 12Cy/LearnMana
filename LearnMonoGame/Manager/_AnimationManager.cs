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
            //LoadPlayer
            Dictionary<AnimationKey, Animation> playerDic = new Dictionary<AnimationKey, Animation>();

            Animation animation = new Animation(3, 32, 32, 0, 0);
            playerDic.Add(AnimationKey.WalkDown, animation);

            animation = new Animation(3, 32, 32, 0, 32);
            playerDic.Add(AnimationKey.WalkLeft, animation);

            animation = new Animation(3, 32, 32, 0, 64);
            playerDic.Add(AnimationKey.WalkRight, animation);

            animation = new Animation(3, 32, 32, 0, 96);
            playerDic.Add(AnimationKey.WalkUp, animation);

            AnimationDictionary.Add(AnimationName.player, playerDic);

            //LoadDummy

            Dictionary<AnimationKey, Animation> dummyDic = new Dictionary<AnimationKey, Animation>();

            Animation animation1 = new Animation(3, 32, 32, 0, 0);
            dummyDic.Add(AnimationKey.WalkDown, animation1);

            animation1 = new Animation(3, 32, 32, 0, 32);
            dummyDic.Add(AnimationKey.WalkLeft, animation1);

            animation1 = new Animation(3, 32, 32, 0, 64);
            dummyDic.Add(AnimationKey.WalkRight, animation1);

            animation1 = new Animation(3, 32, 32, 0, 96);
            dummyDic.Add(AnimationKey.WalkUp, animation1);

            AnimationDictionary.Add(AnimationName.dummy, dummyDic);

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

        }
    }
}
