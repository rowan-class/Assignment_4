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
    public static event Action<Character> OnDialogueComplete;
    private DialougeInfo dinfo;
    bool isTyping;
    string temptxt;
    float SecPerChar = 0.05f;
    float typingTimer;
    int visChars;


    public DialougeManager() {
        //idk init here
        dialougeUI = new();
        //inputActions.Dialouge.Click.performed += OnClick;
        //inputActions.Dialouge.ProgressDialouge.performed += OnClick;
        //inputActions.Dialouge.Exit.performed += OnDialougeExit;

    }
    public void DialougeSetup(DialougeInfo info) {
        dinfo = info;
        dinfo.ResetIndex();
        UpdateDialouge();
        dialougeUI.SetActive(true);
    }

    public void UpdateDialouge() {
        if (!dialougeUI.activeSelf || dinfo == null)
        {
            return;
        }
        dialougeUI.UpdateDialougeLine(dinfo.GetDialougeLine());
        temptxt = dinfo.GetDialouge();
        if (temptxt == null)
        {
            dialougeUI.SetActive(false);
            OnDialogueComplete?.Invoke(dinfo.character);
            return;
        }
        else
        {
            StartTypewriter(dinfo.GetDialouge());
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
}
