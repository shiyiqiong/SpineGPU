using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Unity.VisualScripting;
using Spine;
using System.IO;

public class SpineManager : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public MeshFilter meshFilter;
    public Mesh defaultMesh;
    public string animPath = "anim.png";
    public int frameRate = 60;
    public Vector2Int size = new Vector2Int(128, 128);
    bool _isBaking;
    Texture2D texture;
    int index;
    int frameIndex;
    List<Vector3> defaultVertices = new List<Vector3>();

    void Start()
    {
        Application.targetFrameRate = frameRate;
        skeletonAnimation.AnimationState.Complete += OnComplete; 
        _isBaking = false;
        texture = new Texture2D(size.x, size.y, TextureFormat.RGBAFloat, 1, false);
        animPath = $"{Application.dataPath}/{animPath}";
        defaultMesh.GetVertices(defaultVertices);
    }
    
    public void Play()
    {
        skeletonAnimation.AnimationState.SetAnimation(0, "Move", false);
        _isBaking = true;
        index = 0;
        frameIndex = 0;
    }

    void OnComplete(TrackEntry trackEntry)
    {
        _isBaking = false;
        var bytes = texture.EncodeToPNG();
        BinaryWriter bw = new BinaryWriter(new FileStream(animPath, FileMode.Create));
        bw.BaseStream.Write(bytes);
        bw.Close();
        Debug.Log($"动画帧数:{frameIndex}");
        Debug.Log($"成功导出动画纹理:{animPath}");
        
    }

    void FixedUpdate()
    {
        if(_isBaking)
        {
            Mesh mesh = meshFilter.mesh;
            List<Vector3> vertices = new List<Vector3>();
            mesh.GetVertices(vertices);
            var count = vertices.Count;
            for (int i = 0; i < count; i++)
            {
                var pos = vertices[i] - defaultVertices[i];
                pos = new Vector3(pos.x+0.5f , pos.y+0.5f, pos.z);
                var color = new Color(Mathf.Round(pos.x*100)/100f, Mathf.Round(pos.y*100)/100f, pos.z);
                int x = index%size.x;
                int y = index/size.x;
                index ++;
                texture.SetPixel(x, y, color);
                Debug.Log($"{pos},{texture.GetPixel(x, y)}");
            }
            
            frameIndex++;

        }
    }
}
