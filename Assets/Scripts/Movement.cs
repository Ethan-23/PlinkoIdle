using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float maxFallingSpeed = -5f;
    public float maxXSpeed = 3f;
    Dropper dropper;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dropper = GameObject.Find("Dropper").GetComponent<Dropper>();
    }

    void FixedUpdate()
    {
        if (rb.velocity.y < maxFallingSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, maxFallingSpeed);
        }

        if(rb.velocity.x < (maxXSpeed * -1))
        {
            rb.velocity = new Vector2(-maxXSpeed, rb.velocity.y);
        }
            
        if (rb.velocity.x > maxXSpeed)
        {
            rb.velocity = new Vector2(maxXSpeed, rb.velocity.y);
        }

        if (rb.velocity.y == 0 && rb.velocity.x == 0 && transform.position.y != 3.25f)
            Destroy(gameObject);

        //kills out of bounds
        if (transform.position.y < -3)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(collider.tag);
        GetComponent<Ball>().AddPathEnd(transform.position);
        dropper.AddPath(collider.tag, GetComponent<Ball>().GetPath());
        Destroy(gameObject);
    }
}
