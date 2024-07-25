using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class monsterController : MonoBehaviour
{
    public static monsterController instance { get; private set; }

    public float changeTime = 2.0f;
    public float speed = 0.4f;
    bool left;

    Rigidbody2D rb;
    float timer;
    int direction = 1;

    private SpriteRenderer sprite;
    private Animator animator;

    bool isDead;

/*    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
        }
    }*/

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        timer = changeTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }

        if (direction == -1)
        {
            sprite.flipX = false;
        }
        else
        {
            sprite.flipX = true;
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = rb.position;

        if (left)
        {
            position.x = position.x + Time.deltaTime * speed * direction;
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
        }

        if (!isDead)
        {
            rb.MovePosition(position);
        }

    }

    public void setTriggerToDie()
    {
        animator.SetTrigger("GhostDie");
        isDead = true;
    }

    void OnFireEndAnimationComplete()
    {
        Destroy(gameObject);
    }
}
