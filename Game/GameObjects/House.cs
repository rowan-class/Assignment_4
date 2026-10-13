using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseTemplate.Game.GameObjects
{
    public class House : GameObject, ICollidable {
        public House(ContentManager content, string spriteName, string name, Scene owner) : base(content, spriteName, name, owner)
        {
            layer = Layer.Obstacle;
        }

        public bool IsTrigger => false;

        public bool ColliderIsActive => true;

        public Rectangle BoxCollider => new Rectangle(
            (int)(transform.position.X + transform.scale.X / 6),
            (int)(transform.position.Y + transform.scale.Y / 3),
            (int)(transform.scale.X * 2 / 3),
            (int)(transform.scale.Y / 2));
       
        public void OnCollision(ICollidable other)
        {
            
        }

        public void OnCollisionEnter(ICollidable other)
        {
            
        }

        public void OnCollisionExit(ICollidable other)
        {
          
        }
    }
}
