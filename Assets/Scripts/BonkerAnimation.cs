using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonkerAnimation : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag.Equals("Ball"))
        {
            transform.gameObject.GetComponent<ParticleSystem>().Play();
        }
    }
}
