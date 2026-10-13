using BaseTemplate.Game.UI;
using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBase.Dialouge;

public class DialougeManager
{
    public static DialougeUI dialougeUI;
    private DialougeInfo dinfo;
    bool isTyping;
    string temptxt;
    float SecPerChar = 0.05f;
    float typingTimer;
    int visChars;


    public DialougeManager(InputManager inputManager) {
        //idk init here
        dialougeUI = new();
        inputManager.ActionPressed += SkipDialouge;
        dialougeUI.SetActive(false);
    }
    public void DialougeSetup(DialougeInfo info) {
        dinfo = info;
        dinfo.ResetIndex();
        dialougeUI.SetActive(true);
        Services.Input.UpdateActionMap(InputActionMap.Dialogue);
        UpdateDialouge();
        
    }

    public void UpdateDialouge() {
        if (!dialougeUI.activeSelf || dinfo == null)
        {
            return;
        }
        temptxt = dinfo.GetDialouge();
        if (temptxt == null)
        {
            dialougeUI.SetActive(false);
            EventSystem.DialougeComplete(dinfo);
            Services.Input.UpdateActionMap(InputActionMap.Gameplay);
            return;
        }
        else
        {
            StartTypewriter(dinfo.GetDialouge());
            dialougeUI.UpdateDialougeLine(dinfo.GetDialougeLine());
        }
    }
    void StartTypewriter(string text) {
        temptxt = text ?? "";
        typingTimer = 0;
        visChars = 0;

        dialougeUI.SetDialouge(temptxt);
        dialougeUI.SetVisibleCharacters(0);

        isTyping = temptxt.Length > 0;
    } 
    public void Update(GameTime gameTime)
    {
        if (!dialougeUI.activeSelf || !isTyping)
        {
            return;
        }

        typingTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        int previousCount = visChars;

        if (typingTimer >= SecPerChar && visChars < temptxt.Length)
        {
            typingTimer -= SecPerChar;
            visChars++;
        }

        if (visChars != previousCount)
            dialougeUI.SetVisibleCharacters(visChars);

        isTyping = visChars < temptxt.Length;
    }
    public void FinishTyping()
    {
        //end type early
        if (!isTyping)
            return;

        visChars = temptxt.Length;
        dialougeUI.SetVisibleCharacters(visChars);

        typingTimer = 0;
        isTyping = false;
    }
    public void SkipDialouge(GameAction action) {
        if (!dialougeUI.activeSelf || Services.Input.inputActionMap != InputActionMap.Dialogue) {
            return;
        }
        if (action == GameAction.ProgressDialouge) {
            if (isTyping)
            {
                FinishTyping();
            }
            else {
                dinfo.IncrementIndex();
                UpdateDialouge();
            }
        } 
    }
}
