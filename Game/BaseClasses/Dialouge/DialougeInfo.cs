using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBase.Dialouge;

public class DialougeInfo
{
    public Character character;
    List<DialougeLine> dialouge = new();
    public string ID;
    public bool advancesProgress;
    int indx = 0;
    public DialougeInfo(List<DialougeLine> d, string id, bool advanceafter) {
        dialouge = d;
        ID = id;
        advancesProgress = advanceafter;
    }
    public DialougeLine GetDialougeLine() {
        return dialouge[indx];
    }
    public string GetDialouge() {
        string dia = indx < dialouge.Count ? dialouge[indx].dialouge : null;
        return dia;
    }
    public string GetName()
    {
        string name = indx < dialouge.Count ? dialouge[indx].Name : null;
        return name;
    }
    public Sprite GetIcon()
    {
        Sprite icon = indx < dialouge.Count ? dialouge[indx].sprite : null;
        return icon;
    }
    public Sprite GetBackground()
    {
        Sprite bg = indx < dialouge.Count ? dialouge[indx].background : null;
        return bg;
    }
    public void IncrementIndex() {
        indx++;
    }
    public void ResetIndex()
    {
        indx = 0;
    }
}
