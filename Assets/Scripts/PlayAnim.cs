using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayAnim : MonoBehaviour
{
    public int frame = 66;
    private int index;
    Renderer animRenderer;
    int indexId = Shader.PropertyToID("_Index");
    MaterialPropertyBlock block;

    void Start()
    {
        index = 0;
        animRenderer = GetComponent<Renderer>();
        block = new MaterialPropertyBlock();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(index < frame)
        {
            block.SetInt(indexId, index);
            animRenderer.SetPropertyBlock(block);
            index ++;
        }
        else
        {
            index = 0;
        }
    }

}
