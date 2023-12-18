using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] public int damage;
    //public PostProcessVolume post;
    //private Vignette vig;

    //private void Start()
    //{
    //    post.profile.TryGetSettings(out vig);
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMovement>().Damage(damage);    
            //DmgShake.Instance.ShakeCamera(5f, .1f);
            //StartCoroutine(Effect());
        }
    }

    //IEnumerator Effect()
    //{
    //    vig.intensity.value = 0.56f;
    //    yield return new WaitForSeconds(0.5f);
    //    vig.intensity.value = 0.365f;
    //}
}
