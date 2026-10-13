using GameBase.Dialouge;
using GameBase.Items;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GameBase;

public class GameInfoManager
{
    public Inventory playerInventory { get; } = new();
    public Dictionary<Character, int> npcDialogueProgress = new Dictionary<Character, int>();
    //public HashSet<string> collectedItems = new HashSet<string>();
    //for item picksups probs wont be used here
    public HashSet<string> interactedItems = new HashSet<string>();
    public HashSet<string> collectedItems = new HashSet<string>();
    public HashSet<string> completedDialouge = new HashSet<string>();
    public HashSet<Character> talkedToNPCs = new HashSet<Character>();
    public GameInfoManager(ContentManager content)
    {
        EventSystem.OnDialougeComplete += DialougeComplete;
        EventSystem.OnInteract += HandleInteract;
        EventSystem.OnPickup += HandlePickup;

    }
    public void InitItemList()
    {
        //populate inventorylist items
    }
    public void Reset() {
        npcDialogueProgress.Clear();
        interactedItems.Clear();
        completedDialouge.Clear();
        talkedToNPCs.Clear();
    }
    void DialougeComplete(DialougeInfo dInfo) {
        //add to completed dialouge
        //have this pass the dialouge info instead
        completedDialouge.Add(dInfo.ID);
        if (dInfo.advancesProgress) {
            AdvanceDialogue(dInfo.character);
        }
    }
    void HandleInteract(string ID) {
        interactedItems.Add(ID);
    }
    void HandlePickup(string ID) {
        interactedItems.Add(ID);
        collectedItems.Add(ID);
        
    }
    public int GetDialogueIndex(Character c)
    {
        if (npcDialogueProgress.ContainsKey(c))
        {
            return npcDialogueProgress[c];
        }
        npcDialogueProgress[c] = 0;
        return 0;
    }
    public void AdvanceDialogue(Character c)
    {

        if (npcDialogueProgress.ContainsKey(c))
        {
            npcDialogueProgress[c]++;
        }
        else
        {
            npcDialogueProgress[c] = 1;
        }
    }
}
