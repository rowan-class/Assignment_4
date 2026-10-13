using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBase;

public class Player : GameObject, IPixelCollidable
{
    Vector2 direction = Vector2.Zero;
    float speed = 100f;
    public bool IsTrigger => false;

    public bool ColliderIsActive => true;
    private int interactRadius = 30;

    public Rectangle BoxCollider => new Rectangle((int)transform.position.X, (int)transform.position.Y, (int)transform.scale.X, (int)transform.scale.Y);
    public Player(ContentManager content, string spriteName, string name, Scene owner) : base(content, spriteName, name, owner)
    {
        Services.Input.OnInputMapChange += OnActionMapChanged;
        Services.Input.ActionKeyDown += OnActionKeyDown;
        Services.Input.ActionReleased += OnActionKeyReleased;
        Services.Input.ActionPressed += OnActionKeyPressed;
        layer = Layer.Player;

    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (!IsActive) { return; }
        CollisionManager.Instance.Move(speed * direction * (float)gameTime.ElapsedGameTime.TotalSeconds, this);
    }

    public void OnCollision(ICollidable other)
    {}

    public void OnCollisionEnter(ICollidable other)
    {
    }

    public void OnCollisionExit(ICollidable other)
    {
    }
    void OnActionKeyDown(GameAction action) {
        if (!IsActive) { return; }
        if (Services.Input.inputActionMap == InputActionMap.Gameplay)
        {
            switch (action)
            {
                case GameAction.MoveDown:
                    direction.Y = 1;
                    break;
                case GameAction.MoveUp:
                    direction.Y = -1;
                    break;
                case GameAction.MoveLeft:
                    direction.X = -1;
                    break;
                case GameAction.MoveRight:
                    direction.X = 1;
                    break;
            }
            if (direction != Vector2.Zero)
            {
                direction.Normalize();
            }
        }
    }
    private void OnActionKeyReleased(GameAction action)
    {
        if (!IsActive) { return; }
        if (Services.Input.inputActionMap == InputActionMap.Gameplay) {
            switch (action)
            {
                case GameAction.Interact:
                    // Handle interact release
                    List<ICollidable> collidables = CollisionManager.Instance.CircleCast(interactRadius, this);
                    foreach (ICollidable col in collidables)
                    {
                        if (col is IInteractable i)
                        {
                            //i should yoink my old code and make it interact w whats  closest to the player still
                            i.Interact(this);
                            if (Services.Input.inputActionMap != InputActionMap.Gameplay) { return; }
                        }
                    }
                    break;
            }
        }
    }

    private void OnActionKeyPressed(GameAction action)
    {
    }
    public void OnActionMapChanged(InputActionMap map) {
        if (map != InputActionMap.Gameplay)
        {
            // disable player movement
            direction = Vector2.Zero;
            speed = 0f;
        }
        else {
            speed = 100f;
        }
    }
}
