using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    public GameObject spineObj;
    public GameObject animObj;
    public int count = 10000;
    public int frameRate = 60;
    private List<GameObject> list = new List<GameObject>();

    void Start()
    {
        Application.targetFrameRate = frameRate;
    }

    public void PlayAnim()
    {
        CreateObj(animObj);
    }

    public void PlaySpine()
    {
        CreateObj(spineObj);
    }

    public void Clear()
    {
        foreach (var item in list)
        {
            Destroy(item);
        }
        list.Clear();
    }

    void CreateObj(GameObject prefab)
    {
        Clear();
        for (int i = 0; i < count; i++)
        {
            var obj = Instantiate(prefab, transform);
            obj.transform.position = new Vector3(Random.Range(-3f, 3f), Random.Range(-5f, 5f), 0);
            obj.GetComponent<Renderer>().sortingOrder = i;
            list.Add(obj);
        }
    }
}
