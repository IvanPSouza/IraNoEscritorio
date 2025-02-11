using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class radomBackground : MonoBehaviour
{
    [SerializeField] public GameObject paused;
    public float chance = 1;
    // Start is called before the first frame update
    void Start()
    {
        chance = Random.Range(0, 2);
        if (chance >= 1)
        {
            paused.SetActive(false);
        }
    }

    public void Update()
    {

    }
    public void Change()
    {
        if(chance == 1f)
        {
            chance = 0f;
            paused.SetActive(false);
        }
        else if (chance == 0f)
        {
            chance = 1f;
            paused.SetActive(true);
        }
    }

}
