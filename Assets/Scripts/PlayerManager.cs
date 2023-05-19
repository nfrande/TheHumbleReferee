using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public List<FootballPlayer> players;
    
    FootballPlayer GetClosestPlayerToPoint(Vector3 point)
    {
        FootballPlayer bestTarget = null;
        float lastClosest = Mathf.Infinity;
        foreach(FootballPlayer player in players)
        {
            Vector3 directionToTarget = player.transform.position - point;
            //the direction's square magnitude
            float dsqr = directionToTarget.sqrMagnitude;

            if(dsqr < lastClosest)
            {
                bestTarget = player;
                lastClosest = dsqr;
            }
        }
        return bestTarget;
    }
    
    void SetPlayers(FootballPlayerState state)
    {
        foreach(FootballPlayer player in players)
        {
            player.SetState(state);
        }
    }

}

public enum FootballPlayerState
{
    Penalized,
    StandingStill,
    Attacking
}
