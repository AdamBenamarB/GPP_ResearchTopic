using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public SpriteRenderer renderer;
    public float lifeSpan = 2f;
    private float elapsedTime;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<SpriteRenderer>();
        renderer=gameObject.GetComponent<SpriteRenderer>();
        renderer.sprite = Resources.Load<Sprite>("TargetHD");

    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if(elapsedTime>=lifeSpan)
        {

        Destroy(gameObject);
        }
        
    }
}
