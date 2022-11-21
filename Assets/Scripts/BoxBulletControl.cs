using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBulletControl : MonoBehaviour
{
    // Start is called before the first frame update
    private float maxTime = 2f;
    private float time;
    void Start()
    {
        time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        time +=Time.deltaTime;
        if (time>maxTime){
            Destroy(gameObject);
        }
    }
}
