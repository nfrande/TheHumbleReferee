using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] string playerTag;
    [SerializeField] string fenceTag;
    [SerializeField] float kickForce;
    [Space]
    [Header("Components")]
    public Rigidbody2D rb;
    public Collider2D coll;

    public AudioSource audioUse;

    public AudioClip kickBall;

    public AudioClip ballBounce;

    [Header("Visuals")]
    [SerializeField]AnimationCurve airKickArc;
    [SerializeField]Transform graphicTransform;

    void Update()
    {
        if(Mathf.Abs(rb.position.x) > FieldInfo.instance.fieldSize.x * 0.5f)
        {
            rb.position = Vector2.zero;
            rb.velocity = Vector2.zero;
        }
    }   
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == playerTag)
        {
            rb.AddForce((transform.position - other.transform.position).normalized*kickForce, ForceMode2D.Impulse);
            if(kickBall)audioUse.PlayOneShot(kickBall, 1f);
        }
        if(other.gameObject.tag == "Referee")
        {
            StartCoroutine(AirKick(transform.position * -1f, 1.5f));
            if(kickBall)audioUse.PlayOneShot(kickBall, 1f);
        }
         if(other.gameObject.tag == fenceTag)
        {
           
            if(ballBounce)audioUse.PlayOneShot(ballBounce, 1f);
        }
    }

    IEnumerator AirKick(Vector2 force, float time)
    {
        gameObject.layer = 9;
        rb.velocity = force * 0.5f;
        float inversedivider = 1/time;
        
        Vector2 graphicposition;
        for(float elapsed = 0; elapsed <= time; elapsed += Time.deltaTime)
        {
            graphicposition = graphicTransform.localPosition;
            graphicposition.y = airKickArc.Evaluate(elapsed*inversedivider) * rb.velocity.magnitude;
            graphicTransform.localPosition = graphicposition;
            yield return null;
        }
        graphicposition = graphicTransform.localPosition;
        graphicposition.y = 0;
        graphicTransform.localPosition = graphicposition;
        gameObject.layer = 8;

    }
    // Start is called before the first frame update
    void Start()
    {
        audioUse = gameObject.GetComponent<AudioSource>();

    }
}
