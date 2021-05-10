using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SpeedBooster : MonoBehaviour, ICollectible
{
    public PlayerController pc;
    [Range(0.01f,0.19f)]
    public float amount = 0.1f;
    public float time = 5f;

    // When collected gives player a speed boost for x amount of time
    public void onCollected()
    {
        pc.movementSpeed += amount;
        pc.Invoke("ReturnSpeed", time);
        Destroy(gameObject);
    }
}
