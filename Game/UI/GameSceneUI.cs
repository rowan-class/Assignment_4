using Gum.Forms.Controls;
using Gum.Wireframe;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

namespace GameBase;

public class GameSceneUI : SceneUI
{
    public GameSceneUI(Scene s) : base(s)
    {
    }

    protected override void Init()
    {
        //all scene ui should be created here
        var txt = new Label();
        txt.Text = " press e interact. spaace to contineu dialigouge press i invenrtory";
        txt.Anchor(Anchor.BottomRight);
        Root.AddChild(txt);
    }
}
