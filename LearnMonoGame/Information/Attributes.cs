using LearnMonoGame.Manager;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame.Summoneds
{
    public class Attributes
    {
        protected float speed = 180;
        protected int maxHealth = 100;
        protected float attackSpeed = 0.3f;
        protected float defense = 1;
        protected float maxMana = 100;


        protected int width = _MapStuff.Instance.size;
        protected int height = _MapStuff.Instance.size;


        public float Speed { get { return speed; } }
        public int Width { get { return width; } }
        public int Height { get { return height; } }
        public int MaxHealth { get { return maxHealth; } }
        public float Defense { get { return defense; } }
        public float MaxMana { get { return maxMana; } }


        public Attributes(float _speed = 180, int _maxHealth = 100, float _attackSpeed = 0.3f, float _defense = 1, float _maxMana = 100, Point _size = new Point())
        {
            width = _size.X;
            height = _size.Y;
            speed = _speed;
            maxHealth = _maxHealth;
            attackSpeed = _attackSpeed;
            defense = _defense;
            maxMana = _maxHealth;
        }
    }
}
