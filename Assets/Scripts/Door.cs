using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    public Switch[] Switches;
    public GameObject door;

    private void Start()
    {
        foreach (var i in Switches)
        {
            i.GetComponent<Switch>();
        }
    }

    private void Update()
    {
        foreach (var i in Switches)
        {
            if (i.leverBool)
            {
                door.gameObject.SetActive(false);
            }
            else
            {
                return;
            }
        }
    }
}
