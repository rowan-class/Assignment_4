using GameBase.Dialouge;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBase;

public class NPC : GameObject, ICollidable, IInteractable
{
    //other interfaces like iinteractable can also be implemented here

    public Rectangle BoxCollider =>  new Rectangle((int)transform.position.X, (int)transform.position.Y, (int)transform.scale.X, (int)transform.scale.Y);

    public bool IsTrigger { get; protected set; } = false;

    public bool ColliderIsActive { get; protected set; } = true;

    Layer ICollidable.layer { get;} = Layer.Interactable;

    public bool CanInteract { get; protected set; } = true;

    public string ID => name;

    public Character character = Character.NPC1;
    readonly List<DialougeInfo> dialougeInfoList;


    public NPC(ContentManager content, string spriteName, string name, Scene owner) : base(content, spriteName, name, owner)
    {
        //update sprites, animated sprites and then size in here
        Services.Dialouge.OnDialogueComplete += DialougeComplete;

    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    public void DialougeComplete(Character c) {
        if (character != c) { return; }

    }

    public void OnCollision(ICollidable other)
    {

    }

    public void OnCollisionEnter(ICollidable other)
    {
        throw new NotImplementedException();
    }

    public void OnCollisionExit(ICollidable other)
    {
        throw new NotImplementedException();
    }

    public void Interact(GameObject player)
    {
        
    }
}
