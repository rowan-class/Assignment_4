using GameBase.Assets;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBase;

public class ContentLoader
{
    public ContentLoader(ContentManager content, SpriteLibrary sprites)
    {
        //initalize all textures here. 
        Texture2D grass = content.Load<Texture2D>("Textures/Grass");
        TextureAtlas tilemap = new TextureAtlas(grass);
        tilemap.AddRegion("Grass", 0, 0, grass.Width, grass.Height);
        sprites.AddAtlas("TilemapAtlas", tilemap);

        Texture2D usagi = content.Load<Texture2D>("unaunauna");
        TextureAtlas atlas = new TextureAtlas(usagi);
        atlas.AddRegion("Player", 0, 0, usagi.Width, usagi.Height);
        sprites.AddAtlas("PlayerAtlas", atlas);

        Texture2D poro = content.Load<Texture2D>("poro");
        TextureAtlas patlas = new TextureAtlas(poro);
        patlas.AddRegion("Poro", 0, 0, poro.Width, poro.Height);
        sprites.AddAtlas("PoroAtlas", patlas);

        Texture2D pw = content.Load<Texture2D>("chud2");
        TextureAtlas pwatlas = new TextureAtlas(pw);
        pwatlas.AddRegion("Wright", 0, 0, pw.Width, pw.Height);
        sprites.AddAtlas("WrightAtlas", pwatlas);

        Texture2D chud = content.Load<Texture2D>("chud");
        TextureAtlas catlas = new TextureAtlas(chud);
        catlas.AddRegion("Chud", 0, 0, chud.Width, chud.Height);
        sprites.AddAtlas("ChudAtlas", catlas);

        Texture2D meow = content.Load<Texture2D>("meow");
        TextureAtlas matlas = new TextureAtlas(meow);
        matlas.AddRegion("meow", 0, 0, meow.Width, meow.Height);
        sprites.AddAtlas("MeowAtlas", matlas);

        Texture2D house = content.Load<Texture2D>("RedH");
        TextureAtlas hatlas = new TextureAtlas(house);
        hatlas.AddRegion("house", 0, 0, house.Width, house.Height);
        sprites.AddAtlas("HouseAtlas", hatlas);
    }
}
