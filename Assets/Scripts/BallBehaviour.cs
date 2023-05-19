using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] string playerTag;
    [SerializeField] float kickForce;
    [Space]
    [Header("Components")]
    public Rigidbody2D rb;


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
        }
    }

    IEnumerator AirKick(Vector2 force, float time)
    {
        gameObject.layer = 10;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
