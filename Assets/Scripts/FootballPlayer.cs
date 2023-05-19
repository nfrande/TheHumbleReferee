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
    
    public Team team;
    public Role role;
    [SerializeField]Targets targets;
    public FootballPlayerState state = FootballPlayerState.StandingStill;

    [Header("Components")]
    public Animator animator;

    const float ballDistance = 0.5f;

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
            switch(role)
            {
                case Role.Attacking:
                path.destination = positionToKickBallToGoal();
                path.canMove = true;
                break;
                case Role.Defending:
                path.destination = BetweenBallAndGoal();
                path.canMove = true;
                break;
            }
            
            break;
            case FootballPlayerState.Penalized:
            path.destination = PenaltyBenchPoint();
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

    Vector3 positionToKickBallToGoal()
    {
        Vector3 BallPosition = FieldInfo.instance.BallPosition.position;
        switch(team)
        {
            case Team.team1:
            return BallPosition + (BallPosition-(Vector3)FieldInfo.instance.team2Goal).normalized*ballDistance;
            case Team.team2:
            return BallPosition + (BallPosition-(Vector3)FieldInfo.instance.team1Goal).normalized*ballDistance;
        }
        return Vector3.zero;
    }

    Vector3 BetweenBallAndGoal()
    {
        Vector3 BallPosition = FieldInfo.instance.BallPosition.position;
        switch(team)
        {
            case Team.team1:
            return (Vector3)FieldInfo.instance.team1Goal - ((Vector3)FieldInfo.instance.team1Goal -BallPosition)*0.5f;
            case Team.team2:
            return (Vector3)FieldInfo.instance.team2Goal - ((Vector3)FieldInfo.instance.team2Goal -BallPosition)*0.5f;
        }
        return Vector3.zero;
    }

    Vector3 PenaltyBenchPoint()
    {
        switch(team)
        {
            case Team.team1:
            return FieldInfo.instance.team1Bench;
            case Team.team2:
            return FieldInfo.instance.team2Bench;
        }
        return Vector3.zero;
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

public enum Role
{
    Attacking, Defending
}
