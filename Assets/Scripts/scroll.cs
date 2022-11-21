using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scroll : MonoBehaviour
{
    public Material stars;
    public Material background;
    public float speed = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = new Vector2 (0, Time.time * speed);
        //gameObject.GetComponent<Renderer>().material.SetTextureOffset("_Maintex", offset);
        //gameObject.GetComponent<Renderer>().material.te
        stars.mainTextureOffset = offset;
        background.mainTextureOffset = offset;

    }
}
