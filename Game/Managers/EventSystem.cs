using GameBase.Dialouge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBase;

public static class EventSystem
{
    public static event Action<string> OnInteract;
    public static event Action<string> OnPickup;
    public static event Action<DialougeInfo> OnDialougeComplete;
    //global events that the GameInfoManager can like subscribe to, so a reference isnt needed.
    public static void Interact(string ID) {
        OnInteract?.Invoke(ID);
    }
    public static void Pickup(string ID)
    {
        OnPickup?.Invoke(ID);
    }
    public static void DialougeComplete(DialougeInfo dialougeInfo)
    {
        OnDialougeComplete?.Invoke(dialougeInfo);
    }
}
