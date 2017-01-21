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
            MapStuff.Instance.map = new Tilemap(new Texture2D[] { _CM.GetTexture(_CM.TextureName.grassTile), _CM.GetTexture(_CM.TextureName.stoneTile), _CM.GetTexture(_CM.TextureName.waterTile) }, _CM.GetTexture(_CM.TextureName.map), 16);
            MapStuff.Instance.camera = new Camera();
            selectBar = new SelectBar();
            player = new Player(gameref, new Vector2(200, 200),_CM.GetTexture(_CM.TextureName.player));
            player.Initialize();

        }

        public void LoadContent(ContentManager content)
        {
            player.LoadContent(content);
        }

        public EGameState Update(GameTime gTime)
        {
            MapStuff.Instance.map.Update(gTime);
            player.Update(gTime);
            selectBar.Update(player);

            foreach (Summoned a in PlayerManager.Instance.mySummoned)
            {
                a.Update(gTime);
            }
            selectBar.CheckSelected(player);


            return EGameState.PlayState;
        }


        public void DrawGUI(SpriteBatch spriteBatch)
        {

        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(transformMatrix: MapStuff.Instance.camera.GetViewMatrix());
            MapStuff.Instance.map.Draw(spriteBatch);

            player.Draw(spriteBatch);
            foreach (Summoned a in PlayerManager.Instance.mySummoned)
            {
                a.Draw(spriteBatch);
            }

            selectBar.Draw(spriteBatch);

            spriteBatch.End();
        }

        public void UnloadContent()
        {
          
        }
#endregion


    }
}
