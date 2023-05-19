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

    public AudioSource audioUse;

    public AudioClip kickBall;

    public AudioClip ballBounce;

    [Header("Visuals")]
    [SerializeField]AnimationCurve airKickArc;
    [SerializeField]Transform graphicTransform;
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == playerTag)
        {
            rb.AddForce((transform.position - other.transform.position).normalized*kickForce, ForceMode2D.Impulse);
        }
        if(other.gameObject.tag == "Referee")
        {
            StartCoroutine(AirKick(transform.position * -1f, 1.5f));
            audioUse.PlayOneShot(kickBall, 1f);
        }
         if(other.gameObject.tag == fenceTag)
        {
           
            audioUse.PlayOneShot(ballBounce, 1f);
        }
    }

    IEnumerator AirKick(Vector2 force, float time)
    {
        gameObject.layer = 10;
        rb.velocity = force * 0.5f;
        float inversedivider = 1/time;
        for(float elapsed = 0; elapsed <= time; elapsed += Time.deltaTime)
        {
            Vector2 graphicposition = graphicTransform.localPosition;
            graphicposition.y = airKickArc.Evaluate(elapsed*inversedivider);
            graphicTransform.localPosition = graphicposition;
            yield return null;
        }
        gameObject.layer = 8;

    }
    // Start is called before the first frame update
    void Start()
    {
        audioUse = gameObject.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
