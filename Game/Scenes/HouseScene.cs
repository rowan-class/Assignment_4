using GameBase.Dialogue;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GameBase;

public class HouseScene : Scene
{
    public Tilemap ground { get; }
    public IReadOnlyList<SpawnInfo> Spawns { get; }
    List<ICollidable> tileColliders = new();

    private readonly Player player;
    //public IReadOnlyList<SpawnInfo> Spawns { get; }
    // references to a tilemap & player if we using either
    Vector2 playerSpawn;
    public HouseScene(ContentManager content) : base(content)
    {
        LevelData level = Tilemap.LoadLevel(content, "Maps/HouseMap.xml");
        ground = level.Map;
        Spawns = level.spawns;
        ground.Scale = new Vector2(3, 3);
        sceneUI = new GameSceneUI(this);

        player = SpawnLevelObjects(content, ground, Spawns);
        if (player != null) {
            playerSpawn = player.transform.position;
        }
        ResetTileColliders();
    }
    private Player SpawnLevelObjects(ContentManager content, Tilemap map, IReadOnlyList<SpawnInfo> spawns) {
        Player player = null;
        foreach (SpawnInfo spawn in spawns)
        {
            GameObject obj;
            switch (spawn.Type)
            {
                case "Player":
                    player = new Player(content, "PlayerAtlas.Player", spawn.ID, this);
                    obj = player;
                    obj.UpdateSize(32, 32);
                    break;
                case "NPC":
                    Character character = (Character)Enum.Parse(typeof(Character), spawn.Definition);
                    NPCInfo npcInfo = Services.GameInfo.npcByCharacter[character];
                    obj = new NPC(content, npcInfo.spriteName, spawn.ID, this, npcInfo);
                    obj.UpdateSize(64, 64);
                    break;
                case "Pickup":
                    if (Services.GameInfo.collectedItems.Contains(spawn.ID))
                        continue;
                    ItemInfo itemInfo = Inventory.itemList[spawn.Definition];
                    obj = new PickupItem(content, itemInfo, spawn.ID, this);
                    obj.UpdateSize(64, 64);
                    break;
                default:
                    throw new InvalidOperationException("eoror");
            }

            obj.transform.position = new Vector2(spawn.Column * map.TileWidth, spawn.Row * map.TileHeight);
            AddObject(obj);
            obj.SetActive(isActive);
        }

        return player;
    }

    public override void EnterScene()
    {
        base.EnterScene();
        if (player != null) {
            player.transform.position = playerSpawn;
        }
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }
    public override void ExitScene()
    {
        base.ExitScene();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        ground.Draw(spriteBatch);
        base.Draw(spriteBatch);
    }

    public void ResetLevel() { 
    //if level requires a hard reset
    }
    public void ResetTileColliders()
    {
        foreach (ICollidable collider in tileColliders)
        {
            RemoveCollider(collider);
        }
        tileColliders.Clear();
        for (int i = 0; i < ground.Rows; i++)
        {
            for (int j = 0; j < ground.Columns; j++)
            {
                var tileInfo = ground.GetTile(j, i);
                if (tileInfo != null && (tileInfo.IsSolid || tileInfo.IsTrigger))
                {
                    Transform t = new Transform(new Vector2(j * ground.TileWidth, i * ground.TileHeight), ground.Scale);
                    TileCollider collider = new TileCollider(tileInfo, t);
                    tileColliders.Add(collider);
                    AddCollider(collider);
                }
            }
        }

    }
}
