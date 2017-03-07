using LearnMonoGame.Spells;
using LearnMonoGame.Summoneds.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame.Tools
{
    public static class MyMath
    {
        public static int CalculateRandomValue(int[] ary)
        {
            int[] myAry = new int[ary.Length];
            Array.Copy(ary, myAry, ary.Length);
            int negative = 0;
            if (myAry[0] == myAry[1])
                return myAry[0];
            if (myAry[0] < 0)
            {
                negative = -myAry[0];
                myAry[0] += negative;
                myAry[1] += negative;
                if (myAry[1] < 0)
                {
                    int h = myAry[0];
                    myAry[0] = myAry[1];
                    myAry[1] = h;
                }
            }

            int diff = myAry[1] - myAry[0];

            int result = SpellManager.Instance.rnd.Next(diff + 1) + myAry[0];
            return result - negative;
        }

        public static float CalculateCritValue(SAbility effect)
        {
            int diff = (int)effect.crit[1] * 100 - (int)effect.crit[0] * 100;

            float value = SpellManager.Instance.rnd.Next(diff + 1) + effect.crit[0] * 100;

            if (SpellManager.Instance.rnd.Next(101) < effect.critChance)
                return value / 100f;
            else
                return 1;

        }
    }
}
