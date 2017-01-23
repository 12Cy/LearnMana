using LearnMonoGame.Manager;
using Microsoft.Xna.Framework;
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
        protected int maxHealth = 100;
        protected float attackSpeed = 0.3f;
        protected int damage = 1;
        protected float defense = 1;


        protected int width = MapStuff.Instance.size;
        protected int height = MapStuff.Instance.size;


        public float Speed { get { return speed; } }
        public int Width { get { return width; } }
        public int Height { get { return height; } }
        public int MaxHealth { get { return maxHealth; } }
        public int Damage { get { return damage; } }
        public float Defense { get { return defense; } }
    }
}
