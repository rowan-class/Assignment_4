using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameBase;

namespace GameBase;

public enum SceneID
{
    //StartScene,
    GameScene,
    HouseScene
    //EndScene
}
public class SceneManager
{
    public static SceneManager Instance { get; private set; }
    public StateMachine SceneM = new StateMachine();
    Dictionary<SceneID, Scene> SceneDict = new Dictionary<SceneID, Scene>();

    //inits scenes, in scene manager and sets the functions for enter update and exiting. 

    public SceneManager(ContentManager content)
    {
        Instance = this;
        SceneM.SetNumOfStates(2);
        //SceneDict.Add(SceneID.StartScene, new StartScene(content));
        SceneDict.Add(SceneID.GameScene, new GameScene(content));
        SceneDict.Add(SceneID.HouseScene, new HouseScene(content));
        //SceneDict.Add(SceneID.EndScene, new EndScene(content));
        //SceneM.SetStateFunctions(0, SceneDict[SceneID.StartScene].EnterScene, SceneDict[SceneID.StartScene].Update, SceneDict[SceneID.StartScene].ExitScene);
        SceneM.SetStateFunctions(0, SceneDict[SceneID.GameScene].EnterScene, SceneDict[SceneID.GameScene].Update, SceneDict[SceneID.GameScene].ExitScene);
        SceneM.SetStateFunctions(1, SceneDict[SceneID.HouseScene].EnterScene, SceneDict[SceneID.HouseScene].Update, SceneDict[SceneID.HouseScene].ExitScene);
        //SceneM.SetStateFunctions(3, SceneDict[SceneID.EndScene].EnterScene, SceneDict[SceneID.EndScene].Update, SceneDict[SceneID.EndScene].ExitScene);
        //init stateMachine
        SceneM.SetState((int)SceneID.GameScene);
    }
    public void Update(GameTime gameTime)
    {
        SceneM.UpdateState(gameTime);
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        if (SceneM.CurrentState == -1) { return; }
        SceneDict[(SceneID)SceneM.CurrentState].Draw(spriteBatch);
        //update shouldnt know draw spritebatchinfo
    }
}
