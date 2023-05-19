using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreGoal : MonoBehaviour
{
    [SerializeField] string ballTag;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == ballTag)
        {
            Debug.Log("Game Over");
            GameManager.instance.GameOver();
        }

    }
}
