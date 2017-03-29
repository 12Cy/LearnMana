using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using LearnMonoGame.Tools;
using LearnMonoGame.PlayerComponents;
using LearnMonoGame.Summoneds;
using LearnMonoGame.Map;
using LearnMonoGame.Manager;
using Microsoft.Xna.Framework.Input;
using LearnMonoGame.Components;

using LearnMonoGame.Summoneds.Enemies;
using LearnMonoGame.Summoneds.Enemies.Monster;
using LearnMonoGame.Bullets;
using LearnMonoGame.Spells;
using LearnMonoGame.Particle;
using LearnMonoGame.Tools.Logger;

namespace LearnMonoGame.GameStates
{
    class PlayState : IGameState
    {
#region Field Region
        
        Player player;
        Game1 gameref;
        SelectBar selectBar; //Selektiert die Player/Monsters
        Dictionary<AnimationKey, Animation> playerAnimations = new Dictionary<AnimationKey, Animation>(); //Speichert alle Animationen des Spielers
        CheatConsole cheatConsole;

#endregion

#region Properties Region

        public Dictionary<AnimationKey, Animation> PlayerAnimations
        {
            get { return playerAnimations; }
        }

#endregion

#region Constructor Region
        public PlayState(Game1 _gameref)
        {
            this.gameref = _gameref;

            LogHelper.Instance.Log(Logtarget.Contructor, "PlayState");
            Console.WriteLine("PLAYSTATE");

        }
#endregion

#region Methoden Region

        public void Initialize()
        {

            cheatConsole = new CheatConsole();
            SpellManager.Instance.LoadInformation();
            SummonedsInformation.Instance.LoadInformation();
            //Erstellt im Singleton die Instanz der Map
            /*
            _MapStuff.Instance.map = new Tilemap(new Texture2D[] 
            {
                _CM.GetTexture(_CM.TextureName.stoneTile), //0
                _CM.GetTexture(_CM.TextureName._flower1),   //1
                _CM.GetTexture(_CM.TextureName._flower2),   //2
                _CM.GetTexture(_CM.TextureName._grass1),    //3
                _CM.GetTexture(_CM.TextureName._grass2),    //4
                _CM.GetTexture(_CM.TextureName._grass3),    //5
                _CM.GetTexture(_CM.TextureName._cheast),    //6
                _CM.GetTexture(_CM.TextureName._water),     //7
                _CM.GetTexture(_CM.TextureName._wood),      //8
                _CM.GetTexture(_CM.TextureName.manaSource), //9
                _CM.GetTexture(_CM.TextureName._stone)      //10

            }, 

             
             _CM.GetTexture(_CM.TextureName.map), new Point(64,74/4));
                         */

            _MapStuff.Instance.map = Tilemap.ParseJson("Assets/Level01.json");
            _MapStuff.Instance.camera = new Camera();
            _MapStuff.Instance.camera.Zoom = 1f;
 

            selectBar = new SelectBar();
            PlayerManager.Instance.MyPlayer = new Player(new Vector2(750, 250),_CM.GetTexture(_CM.TextureName.malePlayer));
            MonsterManager.Instance.enemyList.Add(new Skelett(new Vector2(200, 200)));
            MonsterManager.Instance.enemyList.Add(new Skelett(new Vector2(600, 400)));
            MonsterManager.Instance.enemyList.Add(new Skelett(new Vector2(350, 260)));
            MonsterManager.Instance.enemyList.Add(new Skelett(new Vector2(270, 450)));

            MonsterManager.Instance.enemyList.Add(new Zombie(new Vector2(980, 575)));
            MonsterManager.Instance.enemyList.Add(new Zombie(new Vector2(950, 520)));
            MonsterManager.Instance.enemyList.Add(new Zombie(new Vector2(850, 490)));

            MonsterManager.Instance.enemyList.Add(new Wolf(new Vector2(1500, 450)));
            MonsterManager.Instance.enemyList.Add(new Wolf(new Vector2(1630, 550)));
            MonsterManager.Instance.enemyList.Add(new Wolf(new Vector2(1690, 150)));

            MonsterManager.Instance.enemyList.Add(new SkeletonMage(new Vector2(500, 1000)));


        }


        public EGameState Update(GameTime gTime)
        {
            cheatConsole.Update(gTime);
            _MapStuff.Instance.map.Update(gTime);
            PlayerManager.Instance.MyPlayer.Update(gTime);
            foreach (Character a in MonsterManager.Instance.mySummoned)
            {
                a.Update(gTime);
            }
            foreach (Character a in MonsterManager.Instance.enemyList)
            {
                a.Update(gTime);
                
            }
            for (int i = 0; i < _BulletManager.Instance.bullets.Count; ++i)
            {
                if (_BulletManager.Instance.bullets[i].alive)
                    _BulletManager.Instance.bullets[i].Update(gTime);
                else
                    _BulletManager.Instance.bullets.RemoveAt(i--);
            }
            CollisionTestDebugZweckeWirdNochGeaendertKeineAngst();

            if (xIn.CheckKeyReleased(Keys.NumPad1))
                _MapStuff.Instance.camera.Zoom += 0.1f;
            if(xIn.CheckKeyReleased(Keys.NumPad3))
                _MapStuff.Instance.camera.Zoom -= 0.1f;
            if (xIn.CheckKeyReleased(Keys.NumPad2))
                _MapStuff.Instance.camera.ResetZoom();

            selectBar.Update();
            selectBar.CheckSelected();

            for(int i = 0; i < _ParticleManager.Instance.particles.Count; ++i)
            {
                if (_ParticleManager.Instance.particles[i].alive)
                    _ParticleManager.Instance.particles[i].Update(gTime);
                else
                    _ParticleManager.Instance.particles.RemoveAt(i--);
            }
            foreach (ManaSource a in _MapStuff.Instance.manaSourceList)
            {
                a.Update(gTime);
            }

            MonsterManager.Instance.SpawnCharacterInQueue();


            return EGameState.PlayState;
        }
        public void CollisionTestDebugZweckeWirdNochGeaendertKeineAngst()
        {
            for (int i = 0; i < MonsterManager.Instance.enemyList.Count; i++)
            {

                if (!MonsterManager.Instance.enemyList[i].IsAlive)
                {
                    MonsterManager.Instance.enemyList.RemoveAt(i);
                    i--;
                }
            }


        }


        public void DrawGUI(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            cheatConsole.Draw(spriteBatch);

            Texture2D rectangle = new Texture2D(_MapStuff.Instance.graphics, 200, 90);
            Color[] data = new Color[200 * 90];
            for (int i = 0; i < data.Length; i++) data[i] = Color.Chocolate;
            rectangle.SetData(data);

            spriteBatch.Draw(rectangle, new Vector2(5, 5), Color.White);
            spriteBatch.Draw(rectangle, new Vector2(210, 5), Color.White);
            spriteBatch.Draw(rectangle, new Vector2(415, 5), Color.White);


            spriteBatch.DrawString(_CM.GetFont(_CM.FontName.Arial), "Debug Information \nZoom: " + _MapStuff.Instance.camera.Zoom + " Num1 & Num3\nReset Zoom: Num2", new Vector2(10, 10), Color.Bisque);
            spriteBatch.DrawString(_CM.GetFont(_CM.FontName.Arial), "Debug Information \nHealth +/- => L, K \nMana  +/- => O, I ", new Vector2(215, 10), Color.Bisque);
            spriteBatch.DrawString(_CM.GetFont(_CM.FontName.Arial), "Player Attack     \nAktueller Spell: "+ PlayerManager.Instance.MyPlayer.Spellbook.ToString(), new Vector2(415, 10), Color.Bisque);

            spriteBatch.End();


        }


        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Begin(transformMatrix: _MapStuff.Instance.camera.GetViewMatrix(),blendState: BlendState.NonPremultiplied);
            _MapStuff.Instance.map.Draw(spriteBatch);

            PlayerManager.Instance.MyPlayer.Draw(spriteBatch);
            foreach (Character a in MonsterManager.Instance.mySummoned)
            {
                a.Draw(spriteBatch);
            }
            foreach (Character a in MonsterManager.Instance.enemyList)
            {
                a.Draw(spriteBatch);
            }
            foreach(Bullet b in _BulletManager.Instance.bullets)
                b.Draw(spriteBatch);


            foreach (GameParticle p in _ParticleManager.Instance.particles)
                p.Draw(spriteBatch);

            selectBar.Draw(spriteBatch);

            foreach (ManaSource a in _MapStuff.Instance.manaSourceList)
            {
                a.Draw(spriteBatch);
            }

            spriteBatch.End();


        }

        public void UnloadContent()
        {
          
        }
#endregion

    }
}
