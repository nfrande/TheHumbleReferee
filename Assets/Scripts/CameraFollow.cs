using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]Transform Target;
    Vector2 targetLastPosition;

    [SerializeField] string TargetName;

    float FollowSpeed;
    AnimationCurve FollowSpeedCurve;
    [SerializeField] Vector2 FollowOffset;
    [SerializeField]Vector2 Treshold;
    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.Find(TargetName).transform;
        Treshold = CalculateTreshold();
        transform.position = Target.position;
        if(Target)targetLastPosition = Target.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Target)
        {

        }
        

    }

    void FixedUpdate()
    {
        if(Target)
        {
            Vector2 follow = Target.position;
            float xDifference = follow.x - transform.position.x;
            float yDifference = follow.y - transform.position.y;

            Vector2 NewPos = transform.position;
            if(xDifference <= -Treshold.x || xDifference >= Treshold.x) NewPos.x += CalculateSpeed(follow.x, targetLastPosition.x);
            if(yDifference < -Treshold.y || yDifference > Treshold.y)   NewPos.y += CalculateSpeed(follow.y, targetLastPosition.y);

            transform.position = NewPos;
            targetLastPosition = follow;
        }
        else
        {
            Target = GameObject.Find(TargetName).transform;
        }

    }

    private Vector3 CalculateTreshold()
    {
        Rect aspect = Camera.main.pixelRect;
        Vector2 t = new Vector2(Camera.main.orthographicSize * aspect.width/aspect.height, Camera.main.orthographicSize);
        t.x = t.x * FollowOffset.x;
        t.y = t.y *FollowOffset.y;
        return t;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        
        Treshold = CalculateTreshold();
        Gizmos.DrawWireCube(transform.position, Treshold * 2);

    }

    float CalculateSpeed(float current, float last)
    {
        return current - last;
    }
}
