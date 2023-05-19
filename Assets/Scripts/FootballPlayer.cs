using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Pathfinding;

public class FootballPlayer : MonoBehaviour
{
    public IAstarAI path;

    [Serializable]
    public struct Targets
    {
        public Transform BallPosition, PenaltyBench;       
    }
    
    [SerializeField]Targets targets;
    public FootballPlayerState state = FootballPlayerState.StandingStill;

    [Header("Components")]
    public Animator animator;

    public SpriteRenderer spriteRenderer;

    public void SetState(FootballPlayerState state)
    {
        this.state = state;
    }


    void UpdateTarget()
    {
        switch(state)
        {
            case FootballPlayerState.Attacking:
            path.destination = targets.BallPosition.position;
            path.canMove = true;
            break;
            case FootballPlayerState.Penalized:
            path.destination = targets.PenaltyBench.position;
            path.canMove = !path.reachedDestination;
            StartCoroutine(PenaltyTimer(10));
            break;
            case FootballPlayerState.StandingStill:
            path.SetPath(null);
            path.canMove = false;
            break;
        }
    }

    void Update()
    {
        spriteRenderer.flipX = path.steeringTarget.x > path.position.x;
        UpdateTarget();
    }



    void OnEnable()
    {
        path = GetComponent<IAstarAI>();
        PlayerManager manager = GameManager.instance.playerManager;
        manager.players.Add(this);
        path.onSearchPath += UpdateTarget;
        if(!path.hasPath)UpdateTarget();

    }

    void OnDisable()
    {
        PlayerManager manager = GameManager.instance.playerManager;
        manager.players.Add(this);
        path.onSearchPath -= UpdateTarget;
    }

    IEnumerator PenaltyTimer(float time)
    {
        yield return new WaitForSeconds(time);
        SetState(FootballPlayerState.Attacking);
    }

}
