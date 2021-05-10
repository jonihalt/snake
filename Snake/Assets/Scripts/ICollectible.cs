using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Interface for collectible items. PlayerController uses onCollected method
// whenever picking up a collectible.
public interface ICollectible
{
    void onCollected();
}
