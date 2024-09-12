using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class PreviewBall : MonoBehaviour
{
    List<Vector2> path = new List<Vector2>();
    int currentIndex = 0;
    [SerializeField] float replaySpeed = 1.0f;
    PlayerManager playerManager;
    List<string> multis = new List<string> { "0.2x", "2x", "4x", "9x", "26x", "130x", "1000x" };
    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
        path = playerManager.GetRandomBallPath();
        transform.position = path[0];
        /*StartCoroutine(FollowPath());*/
    }

    /*IEnumerator FollowPath()
    {
        if (currentIndex < path.Count)
        {
            // Move ball to the next recorded position
            transform.position = Vector3.Lerp(transform.position, path[currentIndex], Time.deltaTime * replaySpeed);

            // Check if the ball is close enough to the next point, then move to the next one

            if (Vector3.Distance(transform.position, path[currentIndex]) < 0.01f)
            {
                currentIndex++;
            }
            yield return new WaitForSeconds(0f);
            StartCoroutine(FollowPath());
        }
        StopCoroutine(FollowPath());
        
    }*/

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentIndex < path.Count)
        {
            // Move ball to the next recorded position
            transform.position = Vector3.Lerp(transform.position, path[currentIndex], Time.fixedDeltaTime * replaySpeed);

            // Check if the ball is close enough to the next point, then move to the next one
            if (Vector3.Distance(transform.position, path[currentIndex]) < 0.01f)
            {
                currentIndex++;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(multis.Contains(collider.tag)) {
            Debug.Log(collider.tag);
            Destroy(gameObject);
        }
        
    }
}
