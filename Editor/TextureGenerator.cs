using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace GameTools.Editor{
    public class TextureGenerator : ScriptableWizard{

        public int width = 1024;
        public int height = 1024;
        public int scale = 1;
        public float xOffset = 0;
        public float yOffset = 0;
        Texture2D texture;
        Color[] colors;

        [MenuItem("Assets/Texture/PerlinNoise")]
        public static void CreateWizard(){
            ScriptableWizard.DisplayWizard<TextureGenerator>("Create Texture", "Create");
        }

        void OnWizardCreate(){
            byte[] bytes = texture.EncodeToPNG();
            string path = EditorUtility.SaveFilePanelInProject("Save png", texture.name + "png", "png", "Saving File");
            if (bytes != null)
            {
                File.WriteAllBytes(path, bytes);
                AssetDatabase.Refresh();
            }
        }

        void OnWizardUpdate(){
            if(texture == null || texture.height != height || texture.width != width){
                texture = new Texture2D(width, height);
                colors = new Color[width*height];
                UpdateTexture();
            }
        }

        void UpdateTexture(){
            for(int rows = 0; rows < height; rows++){
                for(int columns = 0; columns < width; columns++){

                    float xCoord = xOffset + rows / texture.width * scale;
                    float yCoord = yOffset + columns / texture.height * scale;

                    float noise = Mathf.PerlinNoise(xCoord, yCoord);
                    colors[rows * width + columns] = new Color(noise, noise, noise);
                    Debug.Log(noise);
                }
            }
            texture.SetPixels(colors);
            texture.Apply();
        }
    }
}