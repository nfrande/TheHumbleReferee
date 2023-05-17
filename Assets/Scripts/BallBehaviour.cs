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

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == playerTag)
        {
            rb.AddForceAtPosition((transform.position - other.transform.position).normalized*kickForce, other.transform.position, ForceMode2D.Impulse);
        }
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
