using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class LoadGame : MonoBehaviour
{
    public InputStateManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager.Initialize("Player", "UI");
        manager.Player.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if(manager.Player.FindAction("Fire").WasPressedThisFrame())
        {
           SceneManager.LoadScene("SampleScene 1");
           if(GameManager.instance)GameManager.instance.NewGame();
        }
       /* if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Application.Quit();
        }
        */
    }
}
