using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Apple : MonoBehaviour, ICollectible
{
    public int scoreValue = 1;

    // Add score if player eats this apple
    public void onCollected()
    {
        GameManager.Instance.AddScore(scoreValue);
        Destroy(gameObject);
    }
}