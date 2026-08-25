using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class InteractionSystem : MonoBehaviour
{



    void Start()
    {
        gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }




}
