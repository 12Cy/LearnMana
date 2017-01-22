using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame.Manager
{
    //MyConntentManager
    public class _CM
    {
        private static Dictionary<TextureName, Texture2D> textureDictionary = new Dictionary<TextureName, Texture2D>();
        private static Dictionary<FontName, SpriteFont> fontDictionary = new Dictionary<FontName, SpriteFont>();
        static ContentManager Content;

        public _CM(ContentManager _content)
        {
            Content = _content;

        }
        public static Texture2D GetTexture(TextureName textureName)
        {
            if (textureDictionary.Count == 0)
            {
                LoadTexture();
            }
            return textureDictionary[textureName];
        }
        public static SpriteFont GetFont(FontName FontName)
        {

            if (fontDictionary.Count == 0)
            {

                LoadFont();
            }
            return fontDictionary[FontName];
        }

        static void LoadTexture()
        {
            textureDictionary.Add(TextureName.player, Content.Load<Texture2D>("Player//femalePlayer"));
            textureDictionary.Add(TextureName.select, Content.Load<Texture2D>("Select//DottedLine3"));
            textureDictionary.Add(TextureName.selectCircle, Content.Load<Texture2D>("Select//Circle"));
            textureDictionary.Add(TextureName.selected, Content.Load<Texture2D>("Select//selected"));
            textureDictionary.Add(TextureName.damageselect, Content.Load<Texture2D>("Select//Damageselected"));
            textureDictionary.Add(TextureName.fireballCursor, Content.Load<Texture2D>("Select//Cursor//fireballCursor"));

            //Weapons
            textureDictionary.Add(TextureName.fireball, Content.Load<Texture2D>("Player//Weapons//Fireball"));
            //HealthBar
            textureDictionary.Add(TextureName.backLife, Content.Load<Texture2D>("HealthBar/BackLifeGUI"));
            textureDictionary.Add(TextureName.frontLife, Content.Load<Texture2D>("HealthBar/FrontLifeGUI"));
            //Map
            textureDictionary.Add(TextureName.map, Content.Load<Texture2D>("Map//Map1"));

            //Tiles
            textureDictionary.Add(TextureName.grassTile, Content.Load<Texture2D>("Map//Tiles//GrassIso"));
            textureDictionary.Add(TextureName.stoneTile, Content.Load<Texture2D>("Map//Tiles//StoneIso"));
            textureDictionary.Add(TextureName.waterTile, Content.Load<Texture2D>("Map//Tiles//WaterIso"));

            //Monster
            textureDictionary.Add(TextureName.dummy, Content.Load<Texture2D>("Monster//Dummy"));

        }
        static void LoadFont()
        {
            fontDictionary.Add(FontName.Arial, Content.Load<SpriteFont>("Font//Arial"));
        }


        public enum TextureName
        {
            player,
            map,
            grassTile,
            stoneTile,
            waterTile,
            select,
            selected,
            damageselect,
            selectCircle,
            backLife,
            frontLife,
            fireball,
            dummy,
            fireballCursor,
                
        }
        public enum FontName
        {
            Arial,

        }

    }
}