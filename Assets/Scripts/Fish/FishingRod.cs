using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class FishingRod : MonoBehaviour
{
    public Transform firePoint;
    public Transform reelPoint;
    public GameObject bulletPrefab;
    private HookCast hook;
    public Slider slider;
    //public Animator anim;
    private Fish_Manager fishManager;
    private GameObject fish;
    private AudioManager audioManager;

    public bool canShoot;
    public bool casted;
    public bool waterHit;
    public bool fishSpawned = false;

    public bool isHooked = false;
    public float elapsedTime = 0f;
    private float fishSpawnInterval = 1f;
    private float fishTime = 0f;
    private float speed = 5f;

    private GameObject bullet;

    void Start()
    {
        hook = bulletPrefab.GetComponent<HookCast>();

        fishManager = GameObject.Find("FishManager").GetComponent<Fish_Manager>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U) && casted == false)
        {
            Charge();
        }

        if (Input.GetKeyUp(KeyCode.U) && canShoot && casted == false)
        {
            Shoot();
        }

        if (isHooked)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= fishSpawnInterval)
            {
                float spawnChance = Random.value;

                if (spawnChance <= 0.6f)
                {
                    SpawnFish();
                }


                elapsedTime = 0f;
            }
        }

        if (fishSpawned)
        {
            Reel();
        }
    }

    public void Charge()
    {
        slider.gameObject.SetActive(true);
    }

    public void Shoot()
    {
        var hookSpeed = hook.speed = slider.value / 10;

        audioManager.PlaySFX(audioManager.playerDashing);
        casted = true;
        canShoot = false;
        slider.gameObject.SetActive(false);

        bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    private void SpawnFish()
    {
        if (bullet != null)
        {
            string randomFishType = fishManager.fish[Random.Range(0, fishManager.fish.Length)].name;

            if (fishManager.spawnChances.TryGetValue(randomFishType, out float spawnChance))
            {
                if (Random.value <= spawnChance)
                {
                    fish = Instantiate(fishManager.fish[Random.Range(0, fishManager.fish.Length)], bullet.transform.position, Quaternion.identity);

                    fishManager.spawnChances[randomFishType] = 0.2f;

                    fishSpawned = true;
                }
            }
        }
    }

    public void Reel()
    {
        Fish_Reel fishReel = fish.GetComponent<Fish_Reel>();

        StartCoroutine(fishReel.FollowArc(fish.transform, fish.transform.position, reelPoint.transform.position, 1, 1));
        fishSpawned = false;
    }
}