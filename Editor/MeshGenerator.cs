using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class MeshGenerator : EditorWindow {

    public Material material;

    [MenuItem("Assets/Mesh Generator")]
    static void Init(){
        MeshGenerator window = EditorWindow.GetWindow<MeshGenerator>();
        window.Show();
    }

    void OnGUI (){
        material = EditorGUILayout.ObjectField("Material", material, typeof(Material), false) as Material;
        if(GUILayout.Button("Create")){
            CreateMesh();
        }
    }

    void CreateMesh(){
        
        Mesh mesh = new Mesh();
        Vector3[] verts = new Vector3[] {Vector3.zero, Vector3.up, Vector3.one, Vector3.right};
        Vector2[] uvs = new Vector2[] {Vector2.zero, Vector2.up, Vector2.one, Vector2.right};
        Vector3[] norms = new Vector3[] {Vector3.back, Vector3.back, Vector3.back, Vector3.back,};
        int[] triangles = new int[] {0,1,2,0,2,3};
        mesh.vertices = verts;
        mesh.uv = uvs;
        mesh.triangles = triangles;
        mesh.normals = norms;

        string s = "";
        foreach(Vector3 v in verts){
            s += "v " + v.x + " " + v.y + " " + v.z + "\n";
        }
        foreach(Vector2 u in uvs){
            s += "vt " + u.x + " " + u.y + "\n";
        }
        foreach(Vector3 n in norms){
            s += "vn " + n.x + " " + n.y + " " + n.z + "\n";
        }
        for(int i = 0; i < triangles.Length; i += 3){
            s += "f " + triangles[i] + " " + triangles[i + 1] + " " + triangles[i + 2] + "\n";
        }

        string path = EditorUtility.SaveFilePanelInProject("Save Mesh As", "quad", "obj", "Saving Mesh?");
        File.WriteAllText(path, s);
        
        AssetDatabase.Refresh();
    }
}