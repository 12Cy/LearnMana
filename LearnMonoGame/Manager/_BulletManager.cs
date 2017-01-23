using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnMonoGame.Spells;

namespace LearnMonoGame.Manager
{
    class _BulletManager
    {
        public List<Bullet> bullets = new List<Bullet>();


        static _BulletManager instance;
        public static _BulletManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new _BulletManager();

                return instance;
            }
        }
    }
}
