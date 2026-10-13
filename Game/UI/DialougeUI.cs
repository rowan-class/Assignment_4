using GameBase.Dialouge;
using Gum.Forms.Controls;
using Gum.GueDeriving;
using Gum.Wireframe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseTemplate.Game.UI
{
    public class DialougeUI 
    {
        private Panel Root = new();
        SpriteRuntime backgroundPanel;
        RectangleRuntime dialougePanel;
        SpriteRuntime spritePanel;
        RectangleRuntime namePanel;
        Label name;
        Label dialouge;
        public bool activeSelf { get; private set; } = false;

        public DialougeUI() {
            //init here
            Init();
        }
        //this shouldn't count as scene UI imo, any scene that has dialouge should contain one of these?

        public void Init() {
            Root.Dock(Dock.Fill);

            backgroundPanel = new SpriteRuntime();
            backgroundPanel.Dock(Dock.Fill);
            Root.AddChild(backgroundPanel);

            dialougePanel = new RectangleRuntime();
            dialougePanel.IsFilled = true;
            dialougePanel.FillAlpha = 230;

            dialougePanel.HeightUnits = Gum.DataTypes.DimensionUnitType.PercentageOfParent;
            dialougePanel.Dock(Dock.Bottom);
            dialougePanel.Height = 30;
            Root.AddChild(dialougePanel);

            Panel dialougeBox = new Panel();
            dialougePanel.AddChild(dialougeBox);
            dialougeBox.Dock(Dock.Fill);
            dialougeBox.Height = -16;
            dialougeBox.Width = -16;


            dialouge = new Label();
            dialougeBox.AddChild(dialouge);
            dialouge.Anchor(Anchor.TopLeft);
            dialouge.Visual.WidthUnits = Gum.DataTypes.DimensionUnitType.PercentageOfParent;
            dialouge.Width = 100;
            dialouge.Text = "Dialouge goes here";

            spritePanel = new SpriteRuntime();
            dialougePanel.AddChild(spritePanel);
            spritePanel.Anchor(Anchor.TopRight);
            spritePanel.YOrigin = RenderingLibrary.Graphics.VerticalAlignment.Bottom;
            spritePanel.X = -16; 
            spritePanel.Y = 0;
            spritePanel.WidthUnits = Gum.DataTypes.DimensionUnitType.MaintainFileAspectRatio;
            spritePanel.HeightUnits = Gum.DataTypes.DimensionUnitType.PercentageOfParent;
            spritePanel.Height = 80;
            spritePanel.Width = 100;


            namePanel = new RectangleRuntime();
            dialougePanel.AddChild(namePanel);
            namePanel.IsFilled = true;
            namePanel.Anchor(Anchor.TopLeft);
            namePanel.YOrigin = RenderingLibrary.Graphics.VerticalAlignment.Bottom;
            namePanel.Y = 20;
            namePanel.X = -16;
            namePanel.WidthUnits = Gum.DataTypes.DimensionUnitType.RelativeToChildren;
            namePanel.HeightUnits = Gum.DataTypes.DimensionUnitType.RelativeToChildren;
            namePanel.Width = 30;  
            namePanel.Height = 10;

            name = new Label();
            namePanel.AddChild(name);

            name.Visual.WidthUnits = Gum.DataTypes.DimensionUnitType.RelativeToChildren;
            name.Visual.HeightUnits = Gum.DataTypes.DimensionUnitType.RelativeToChildren;
            name.Width = 0;
            name.Height = 0;

            name.Anchor(Anchor.Center);
            name.Text = "Name";


        }
        public void SetName(string n) {
            name.Text = n;
        }
        public void SetDialouge(string d) {
            dialouge.Text = d;
        }
        public void SetBackground( Sprite bg) {
            backgroundPanel.Texture = bg.Region.Texture;
        }
        public void ToggleBackgroundOn(bool state) {
            backgroundPanel.Visible = state;
        }

        public void SetSprite(Sprite s) {
            spritePanel.Texture = s.Region.Texture;
            //this only holds unanimated stuff rn work in laater?
        }

        public void UpdateDialougeLine(DialougeLine d) {
            if (d.background == null)
            {
                ToggleBackgroundOn(false);
            }
            else {
                SetBackground(d.background);
                ToggleBackgroundOn(true);
            }
            SetDialouge(d.dialouge);
            SetSprite(d.sprite);
            SetName(d.Name);
        }
        public void SetActive(bool state) {
            if (state)
            {
                Root.AddToRoot();
                activeSelf = true;
            }
            else {
                Root.RemoveFromRoot();
                activeSelf = false;
            }
        }

        public void SetVisibleCharacters(int count)
        {
            TextRuntime text = (TextRuntime)dialouge.TextComponent;
            text.MaxLettersToShow = count;
        }
    }
}