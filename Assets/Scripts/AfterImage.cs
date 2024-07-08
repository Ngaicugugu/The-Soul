using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImage : MonoBehaviour
{
    [SerializeField]
    private float activeTime = 0.1f;
    private float timeActived;
    private float alpha;
    [SerializeField]
    private float alphaSet = 0.8f;
    private float alphaMutiplier = 0.85f;

    private Transform player;

    private SpriteRenderer sr;
    private SpriteRenderer playerSr;

    private Color color;

    private void OnEnable()
    {
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerSr = player.GetComponent<SpriteRenderer>();

        alpha = alphaSet;
        sr.sprite = playerSr.sprite;
        transform.position = player.position;
        transform.rotation = player.rotation;
        timeActived = Time.time;
    }



    // Update is called once per frame
    void Update()
    {
        alpha *= alphaMutiplier; 
        color = new Color(1f,1f, 1f, alpha);
        sr.color = color;

        if(Time.time >=  (timeActived + activeTime)  ) 
        {
            AfterPool.Instance.AddToPool(gameObject);
        }
    }
}
