using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{

    public bool xMove;
    public bool yMove;
    public new Transform camera;
    public float parallaxXSpeed = 0.001F;
    public float parallaxYSpeed = 0.001F;
    public Image image;
    public SpriteRenderer spriteRenderer;
    private Vector2 startCamPos;
    //private Vector2 offset;
    //private float horizontal = 0;
    //private float vertical = 0;
    float x=0;
    float y=0;
    float parallaxX;
    float parallaxY;
    // Use this for initialization
    private void OnEnable()
    {
        image = GetComponent<Image>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        camera = Camera.main.transform;
       startCamPos = (Vector2)camera.position;
        
    }


    // Update is called once per frame
    void Update()
    {
        parallaxX = -(startCamPos.x - camera.position.x) * parallaxXSpeed;
        parallaxY = -(startCamPos.y - camera.position.y) * parallaxYSpeed;

        if (xMove)
            x = parallaxX;
        else
            x = 0;
        if (yMove)
            y = parallaxY;
        else
            y = 0;
        image.material.mainTextureOffset= new Vector2(x, y);
        //spriteRenderer.material.mainTextureOffset= new Vector2(x, y);
        //Debug.Log(x);

       // previousCamPos = (Vector2)camera.position;
    }
}
