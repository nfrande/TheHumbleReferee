using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class CharacterDestination : VersionedMonoBehaviour
{
    [SerializeField] float distance;
    [SerializeField] BallBehaviour ball;
    [SerializeField] Vector2 GoalPosition;

    IAstarAI ai;

    [SerializeField] Team team;


    // Start is called before the first frame update
    void OnEnable()
    {
        switch (team)
        {
            case Team.team1:
                GoalPosition = new Vector2(5, 0);
                break;
            case Team.team2:
                GoalPosition = new Vector2(-5, 0);
                break;
        }
        ai = GetComponent<IAstarAI>();
        if (ai != null) ai.onSearchPath += Update;
    }

    void OnDisable()
    {
        if (ai != null) ai.onSearchPath -= Update;
    }

    // Update is called once per frame
    void Update()
    {

        ai.destination = CalculateDestination();
        //Debug.DrawLine(ball.transform.position, CalculateDestination(),Color.magenta,0.01f);
    }

    Vector2 CalculateDestination()
    {
        return (Vector2)ball.transform.position + ((Vector2)ball.transform.position - GoalPosition).normalized * distance;
    }

}

public enum Team
{
    team1,
    team2
}
