using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg_Collision : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    private Animator anim;
    [SerializeField] private ParticleSystem particles;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            anim = GetComponent<Animator>();
            anim.SetTrigger("Destroyed");
            particles.Play();

            if (enemy != null)
            {
                enemy.SetActive(true);
            }

        }
    }
}
