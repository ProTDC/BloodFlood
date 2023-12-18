using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Destructable : MonoBehaviour
{
    private Tilemap tilemap;
    [SerializeField] private ParticleSystem particle;

    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            var audio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

            Bounds weaponBounds = collision.bounds;

            Vector3Int minCell = tilemap.WorldToCell(weaponBounds.min);
            Vector3Int maxCell = tilemap.WorldToCell(weaponBounds.max);

            for (int x = minCell.x; x <= maxCell.x; x++)
            {
                for (int y = minCell.y; y <= maxCell.y; y++)
                {
                    Vector3Int tilePos = new Vector3Int(x, y, 0);

                    tilemap.SetTile(tilePos, null);
                }
            }

            //var boom = Instantiate(particle, transform.position, Quaternion.identity);
            //boom.Play();
        }

    }
}
