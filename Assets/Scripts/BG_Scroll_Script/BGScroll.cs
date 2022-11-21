using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroll : MonoBehaviour
{
    public float scrollSpeed = 0.2f;
    public MeshRenderer meshRenderer;
    private float yScroll;
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Scroll();
    }

    private void Scroll()
    {
        yScroll = Time.time * scrollSpeed;
        Vector2 offset = new Vector2(0f, yScroll);
        meshRenderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}
