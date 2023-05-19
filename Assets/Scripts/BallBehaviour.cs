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

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == playerTag)
        {
            rb.AddForceAtPosition((transform.position - other.transform.position).normalized*kickForce, other.transform.position, ForceMode2D.Impulse);
            audioUse.PlayOneShot(kickBall, 1f);
        }
         if(other.gameObject.tag == fenceTag)
        {
           
            audioUse.PlayOneShot(ballBounce, 1f);
        }
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
