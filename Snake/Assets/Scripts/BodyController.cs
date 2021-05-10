using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour
{
    PlayerController pc;

    // Start is called before the first frame update
    void Start()
    {
        pc = gameObject.GetComponentInParent<PlayerController>();
    }

    // Manage collisions with collectibles & obstacles
    void OnTriggerEnter2D(Collider2D other)
    {
        var collectible = other.gameObject.GetComponent<ICollectible>();
        if (collectible != null)
        {
            collectible.onCollected();
            return;
        }

        if (other.CompareTag("Obstacle"))
        {
            pc.Die();
        }
    }
}
