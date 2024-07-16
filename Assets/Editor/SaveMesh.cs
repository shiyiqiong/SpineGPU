using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

public class SaveMesh 
{
    [MenuItem("GameObject/SaveMesh")]
    public static void ToSaveMesh()
    {
        var meshFilter = Selection.activeGameObject.GetComponent<MeshFilter>();
        var mesh = meshFilter.mesh;
        Debug.Log(mesh.boneWeights.Length);
        // Debug.Log($"{mesh.boneWeights[0].boneIndex0}, {mesh.boneWeights[0].weight0}");
        var path = "Assets/mesh.asset";
        Debug.Log(path);
        AssetDatabase.CreateAsset(mesh, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

          
    }
}
