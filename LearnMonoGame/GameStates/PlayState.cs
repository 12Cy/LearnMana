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

namespace LearnMonoGame.GameStates
{
    class PlayState : IGameState
    {
#region Field Region
        
        Player player;
        Game1 gameref;
        SelectBar selectBar; //Selektiert die Player/Monsters
        Dictionary<AnimationKey, Animation> playerAnimations = new Dictionary<AnimationKey, Animation>(); //Speichert alle Animationen des Spielers

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

            Console.WriteLine("PLAYSTATE");

        }
#endregion

#region Methoden Region

        public void Initialize()
        {
            //Erstellt im Singleton die Instanz der Map
            MapStuff.Instance.map = new Tilemap(new Texture2D[] { _CM.GetTexture(_CM.TextureName.grassTile), _CM.GetTexture(_CM.TextureName.stoneTile), _CM.GetTexture(_CM.TextureName.waterTile) }, _CM.GetTexture(_CM.TextureName.map), new Point(64,74/4));
            MapStuff.Instance.camera = new Camera();
            MapStuff.Instance.camera.Zoom = 1f;
 

            selectBar = new SelectBar();
            player = new Player(gameref, new Vector2(750, 250),_CM.GetTexture(_CM.TextureName.player));
            MonsterManager.Instance.enemyList.Add(new Skelett(new Vector2(200, 200)));
            MonsterManager.Instance.enemyList.Add(new Skelett(new Vector2(600, 400)));
            MonsterManager.Instance.enemyList.Add(new Skelett(new Vector2(350, 260)));
            MonsterManager.Instance.enemyList.Add(new Skelett(new Vector2(270, 450)));

        }


        public EGameState Update(GameTime gTime)
        {
            MapStuff.Instance.map.Update(gTime);
            player.Update(gTime);
            foreach (Summoned a in MonsterManager.Instance.mySummoned)
            {
                a.Update(gTime);
            }
            foreach (Enemy a in MonsterManager.Instance.enemyList)
            {
                a.Update(gTime);
                
            }
            for (int i = 0; i < BulletManager.Instance.bullets.Count; ++i)
            {
                BulletManager.Instance.bullets[i].Update(gTime);
            }
            CollisionTestDebugZweckeWirdNochGeaendertKeineAngst();

            if (xIn.CheckKeyReleased(Keys.NumPad1))
                MapStuff.Instance.camera.Zoom += 0.1f;
            if(xIn.CheckKeyReleased(Keys.NumPad3))
                MapStuff.Instance.camera.Zoom -= 0.1f;
            if (xIn.CheckKeyReleased(Keys.NumPad2))
                MapStuff.Instance.camera.ResetZoom();

            selectBar.Update(player);
            selectBar.CheckSelected(player);


            return EGameState.PlayState;
        }
        public void CollisionTestDebugZweckeWirdNochGeaendertKeineAngst()
        {
            foreach (Enemy enemy in MonsterManager.Instance.enemyList)
            {
                foreach (Fireball aFireball in BulletManager.Instance.bullets)
                {
                    if (aFireball.Bounds.Intersects(enemy.Bounds) && aFireball.Visible)
                    {
                        enemy.CalculateHealth(-20);
                        aFireball.Visible = false;
                    }
                }

            }
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

            Texture2D rectangle = new Texture2D(MapStuff.Instance.graphics, 200, 70);
            Color[] data = new Color[200 * 80];
            for (int i = 0; i < data.Length; i++) data[i] = Color.Chocolate;
            rectangle.SetData(data);

            spriteBatch.Draw(rectangle, new Vector2(5, 5), Color.White);
            spriteBatch.Draw(rectangle, new Vector2(210, 5), Color.White);
            

            spriteBatch.DrawString(_CM.GetFont(_CM.FontName.Arial), "Debug Information \nZoom: " + MapStuff.Instance.camera.Zoom + " Num1 & Num3\nReset Zoom: Num2", new Vector2(10, 10), Color.Bisque);
            spriteBatch.DrawString(_CM.GetFont(_CM.FontName.Arial), "Debug Information \nHealth +/- => L, K \nMana  +/- => O, I ", new Vector2(215, 10), Color.Bisque);

            spriteBatch.End();
        }


        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Begin(transformMatrix: MapStuff.Instance.camera.GetViewMatrix());
            MapStuff.Instance.map.Draw(spriteBatch);

            player.Draw(spriteBatch);
            foreach (Summoned a in MonsterManager.Instance.mySummoned)
            {
                a.Draw(spriteBatch);
            }
            foreach (Enemy a in MonsterManager.Instance.enemyList)
            {
                a.Draw(spriteBatch);
            }
            foreach(Bullet b in BulletManager.Instance.bullets)
                b.Draw(spriteBatch);

            selectBar.Draw(spriteBatch);

            spriteBatch.End();


        }

        public void UnloadContent()
        {
          
        }
#endregion

    }
}
