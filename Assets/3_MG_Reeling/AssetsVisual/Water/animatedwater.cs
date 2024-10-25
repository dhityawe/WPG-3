using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float speedX = 0.1f;
    public float speedY = 0.1f;
    private float curX;
    private float curY;

    // Start is called before the first frame update
    void Start()
    {
        curX = GetComponent<Renderer>().material.mainTextureOffset.x;
        curY = GetComponent<Renderer>().material.mainTextureOffset.y;  // Fixed: Assign to curY
    }

    // Update is called once per frame
    void Update()
    {
        curX += Time.deltaTime * speedX;  // Fixed: deltaTime with capital T
        curY += Time.deltaTime * speedY;  // Fixed: deltaTime with capital T
        
        // Fixed: Use curY (not CurY) - it's case-sensitive.
        GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(curX, curY));
    }
}