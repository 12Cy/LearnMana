using LearnMonoGame.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame.Summoneds
{
    public abstract class Attributes
    {
        protected float speed = 180;
        protected float maxHealth = 100;
        protected float attackSpeed = 0.3f;
        protected int textureSize = MapStuff.Instance.size;

        public float Speed { get { return speed; } }
        public int TextureSize { get { return textureSize; } }
        public float MaxHealth { get { return maxHealth; } }
    }
}
