using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FootballPlayerStateDebugger : MonoBehaviour
{
    [SerializeField]InputAction SetStandStill , SetPenalized, SetAttacking; 

    [SerializeField]FootballPlayer player;    
    // Start is called before the first frame update
    void Start()
    {
        SetStandStill.Enable();
        SetPenalized.Enable();
        SetAttacking.Enable();
    }


    // Update is called once per frame
    void Update()
    {
        if(SetStandStill.WasPressedThisFrame())player.SetState(FootballPlayerState.Attacking);
        if(SetPenalized.WasPressedThisFrame())player.SetState(FootballPlayerState.Penalized);
        if(SetAttacking.WasPressedThisFrame())player.SetState(FootballPlayerState.StandingStill);
    }
}
