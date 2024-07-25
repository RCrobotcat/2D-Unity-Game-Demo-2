using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        cinemachineShake.instance.shakingCamera(3.0f, 0.1f);

        Destroy(gameObject);

        if (collision.gameObject.tag == "monster")
        {
            monsterController.instance.setTriggerToDie();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.magnitude > 1000.0f)
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float force)
    {
        rb.AddForce(direction * force);
    }
}
