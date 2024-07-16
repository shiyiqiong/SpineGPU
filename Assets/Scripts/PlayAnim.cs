using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayAnim : MonoBehaviour
{
    public Material material;
    public int frame = 66;
    private int index;
    void Start()
    {
        index = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(index < frame)
        {
            material.SetInt("_Index", index);
            index ++;
        }
        else
        {
            index = 0;
        }
    }

    void OnDestroy()
    {
        material.SetInt("_Index", 0);
    }
}
