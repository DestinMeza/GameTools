using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace GameTools.Editor{
    public class TextureGenerator : ScriptableWizard{

        public string name = "texture";

        public int width = 1024;
        public int height = 1024;

        public float scale = 1;
        
        public float xOffset = 0;
        public float yOffset = 0;

        float _scale; 
        float _xOffset = 0;
        float _yOffset = 0;

        Texture2D texture;
        Color[] colors;

        [MenuItem("Assets/Texture/PerlinNoise")]
        public static void CreateWizard(){

            ScriptableWizard.DisplayWizard<TextureGenerator>("Create Texture", "Create");
        }

        void OnWizardCreate(){
            UpdateTexture();
            byte[] bytes = texture.EncodeToPNG();
            string path = EditorUtility.SaveFilePanelInProject("Save png", name, "png", "Saving File?");
            File.WriteAllBytes(path, bytes);
            AssetDatabase.Refresh();
        }

        void OnWizardUpdate(){
            if(texture == null || texture.width != width || texture.height != height || scale != _scale || xOffset != _xOffset || yOffset != _yOffset){
                UpdateTexture();
            }
        }

        void UpdateTexture(){
            _scale = scale;
            _xOffset = xOffset;
            _yOffset = yOffset;
            texture = new Texture2D(width, height);
            colors = new Color[width*height];

            for(int rows = 0; rows < height; rows++){
                for(int columns = 0; columns < width; columns++){

                    float xCoord = xOffset + (float)rows / (float)width * scale;
                    float yCoord = yOffset + (float)columns / (float)height * scale;

                    float noise = Mathf.PerlinNoise(xCoord, yCoord);
                    colors[rows * width + columns] = new Color(noise, noise, noise);
                }
            }
            texture.SetPixels(colors);
            texture.Apply();
            
        }
    }
}