using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class referee : MonoBehaviour
{
    InputAction Move, Fire;

    [Header("Movement variables")]
    [SerializeField]float MaxSpeed;
    [SerializeField]float Acceleration;
    [SerializeField]float WalkAnimationTreshold = 0.1f;
    Vector2 Direction;

    [Header("Components")]
    [SerializeField]Rigidbody2D rb;
    [SerializeField]Animator animator;
    [SerializeField]SpriteRenderer spriteRenderer;

    Timer<float> RedCardTimer;
    uint RedCardCooldownFrames;

    public AudioSource audioUse;

    public AudioClip penalize;

    // Start is called before the first frame update
    void Start()
    {
        void ControlInit()
        {
            Move = GameManager.instance.input.Player.FindAction("Move");
            Move.performed += UpdateDirection;
            Move.canceled += UpdateDirection;
            Move.started += UpdateDirection;
            Fire = GameManager.instance.input.Player.FindAction("Fire");
            RedCardTimer = new FloatTimer(0);
        }

        ControlInit();
        
        audioUse = gameObject.GetComponent<AudioSource>();
    }

    
    // Update is called once per frame
    void Update()
    {
        void DebugUpdate()
        {
            //Debug.DrawLine(transform.position, transform.position + (Vector3)Direction, Color.cyan, 0.01f);

        }
        void AnimatorUpdate()
        {
            bool moving = rb.velocity.magnitude > WalkAnimationTreshold;
            if(moving)
            {

                spriteRenderer.flipX = rb.velocity.x > 0;
            }
            animator.SetBool("Moving", moving);
        }
        void RedCard()
        {
            animator.SetTrigger("RedCard");
            RedCardTimer = new FloatTimer(0.25f);
            GiveRedCard();
        }

        if(RedCardTimer.CountDown(Time.deltaTime))
        {
            if(Fire.WasPressedThisFrame())RedCard();
        }
        
        DebugUpdate();
        AnimatorUpdate();

    }

    void FixedUpdate()
    {
        void Movement()
        {
            void ApplyDirection()
            {
                rb.velocity += Direction * Acceleration * Time.fixedDeltaTime;
                rb.velocity = Vector2.ClampMagnitude(rb.velocity, MaxSpeed);
            }
            ApplyDirection();
        }

        if(RedCardTimer.timeLeft <= 0)Movement();
    }

    void UpdateDirection(InputAction.CallbackContext context)
    {
        Direction = context.ReadValue<Vector2>();
    }

    void GiveRedCard()
    {
        Ray ray = new Ray(transform.position, Direction);
        Debug.DrawLine(ray.origin,ray.origin + (Vector3)Direction, Color.yellow, 0.2f);
        RaycastHit2D hit = Physics2D.CircleCast(ray.origin,0.5f,ray.direction,1, Physics.AllLayers ^ LayerMask.GetMask("Referee"));
        if(hit)
        {
            FootballPlayer player;
            if(hit.transform.TryGetComponent<FootballPlayer>(out player))
            {
                if(player.state != FootballPlayerState.Penalized)
                {
                    GameManager.instance.uiManager.addScore(5);
                    player.SetState(FootballPlayerState.Penalized);
                    audioUse.PlayOneShot(penalize, 0.4f);
                }
            }
        }
        Debug.LogWarning(hit.point);

    }
}

public abstract class Timer<T>
{
    public T timeLeft {get; protected set;}

    public abstract bool CountDown(T increment);
    public abstract void AddTime(T amount);

    
}

public class FloatTimer : Timer<float>
{
    public FloatTimer(float initialTime)
    {
        timeLeft = initialTime;
    }

    public override bool CountDown(float increment)
    {
        if(timeLeft - increment > 0)
        timeLeft = timeLeft - increment;
        else timeLeft = 0;
        Debug.Log(timeLeft);
        return timeLeft >= 0;
    }

    public override void AddTime(float amount)
    {
        if(amount < 0)
        timeLeft =+ amount;
    }

    
}

