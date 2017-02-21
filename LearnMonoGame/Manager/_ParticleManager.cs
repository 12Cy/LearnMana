using LearnMonoGame.Particle;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame.Manager
{
    public abstract class GameParticle
    {
        public bool alive;

        public abstract void Update(GameTime gTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }

    class _ParticleManager
    {
        public List<GameParticle> particles = new List<GameParticle>();


        static _ParticleManager instance;
        public static _ParticleManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new _ParticleManager();

                return instance;
            }
        }

    }
}
