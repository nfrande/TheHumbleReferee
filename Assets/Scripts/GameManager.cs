using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Input")]
    public InputStateManager input;
    public string PlayerActionMapName = "Player", UIActionMapName = "UI";

    [SerializeField]
    public PlayerManager playerManager;

    public UIManager uiManager;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            
            input.Initialize(PlayerActionMapName, UIActionMapName);
            input.SetState(InputStateManager.InputState.game);
        }
    }



    // Update is called once per frame
    void Update()
    {

    }
}
