using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

[Serializable]
public class InputStateManager
{
    [SerializeField] InputActionAsset actions;

    [HideInInspector]public InputActionMap Player, UI;

    public void Initialize(string PlayerActionMapName, string UIActionMapName)
    {
        if(actions != null)
        {
            Player  = actions.FindActionMap(PlayerActionMapName);
            UI      = actions.FindActionMap(UIActionMapName);
        }
    }

    public enum InputState
    {
        game, menu, none
    }
    public void SetState(InputState state)
    {
        switch(state)
        {
            case InputState.game:
            UI.Disable();
            Player.Enable();
            break;
            case InputState.menu:
            UI.Enable();
            Player.Disable();
            break;
            case InputState.none:
            UI.Disable();
            Player.Disable();
            break;
        }
        Debug.Log($"Switched to state {state}");
    }
}

