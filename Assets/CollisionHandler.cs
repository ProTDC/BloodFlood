using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public GameObject rainObject;

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("RainParticle"))
        {
            if (rainObject != null)
            {
                ParticleCollisionEvent[] collisionEvents = new ParticleCollisionEvent[3]; // Adjust the array size as needed

                // Get collision events
                int numCollisionEvents = other.GetComponent<ParticleSystem>().GetCollisionEvents(gameObject, collisionEvents);

                // Check if there are collision events
                if (numCollisionEvents > 0)
                {
                    // Use the first collision event position for instantiation
                    Vector3 collisionPoint = collisionEvents[0].intersection;

                    Instantiate(rainObject, collisionPoint, Quaternion.identity);
                }
            }
        }
    }
}
