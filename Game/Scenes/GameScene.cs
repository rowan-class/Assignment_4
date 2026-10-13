using BaseTemplate.Game.GameObjects;
using GameBase.Dialogue;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GameBase;

public class GameScene : Scene
{
    public Tilemap ground { get; }
    public IReadOnlyList<SpawnInfo> Spawns { get; }
    List<ICollidable> tileColliders = new();
    Vector2 playerSpawn;
    private readonly Player player;
    GameObject house;
    Vector2 houseEntrance;
    bool enteredBefore;
    

    // references to a tilemap & player if we using either
    public GameScene(ContentManager content) : base(content)
    {
        LevelData level = Tilemap.LoadLevel(content, "Maps/GrassMap.xml");
        ground = level.Map;
        Spawns = level.spawns;
        ground.Scale = new Vector2(3, 3);
        sceneUI = new GameSceneUI(this);



        player = SpawnLevelObjects(content, ground, Spawns);
        if (player != null)
        {
            playerSpawn = player.transform.position;
        }
        //resettiilecolliders
        ResetTileColliders();
        house = new House(content, "HouseAtlas.house", "House", this);
        house.UpdateSize(144, 144);
        house.UpdateLocation(9 * ground.TileWidth, 4 * ground.TileHeight);
        AddObject(house);
        house.SetActive(isActive);
        var doorPosition = house.transform.position + new Vector2(84, 120);
        var doorInfo = new TileInfo {
            Region = house.sprite.Region,
            IsTrigger = true,
            layer = Layer.Exit,
            CollisionBounds = new Rectangle(0, 0, 36, 24)
        };
        AddCollider(new TileCollider(doorInfo, new Transform(doorPosition, 0f, Vector2.One)));
        houseEntrance = doorPosition + new Vector2(2, 32);
    }
     Player SpawnLevelObjects(ContentManager content, Tilemap map, IReadOnlyList<SpawnInfo> spawns) {
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
        //this should loop through all interactables and make thewm check if they r still 
        base.EnterScene();
        if (enteredBefore && player != null) {
            player.transform.position = houseEntrance;
        }
        enteredBefore = true;
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

    public void ResetLevel()
    {
        if (player != null)
            player.transform.position = playerSpawn;
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
