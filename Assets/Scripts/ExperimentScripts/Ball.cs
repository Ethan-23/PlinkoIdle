using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] List<Vector2> path;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        path = new List<Vector2>
        {
            transform.position
        };
    }

    void FixedUpdate()
    {
        // Only record the path if the ball is still moving
        if (rb.velocity.y != 0f) // Adjust threshold as needed
        {
            path.Add(transform.position);
        }
    }

    public List<Vector2> GetPath()
    {
        return path;
    }

    public void AddPathEnd(Vector2 end)
    {
        path.Add(end);
    }
}
