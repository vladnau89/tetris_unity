using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GeneratorManager : MonoBehaviour
{
    [SerializeField] private Generator.Type _type;
    [SerializeField] private int _width = 1024;
    [SerializeField] private int _height = 1024;

    public Texture aTexture;


    private void OnPostRender()
    {
        GL.PushMatrix();
        GL.LoadPixelMatrix(0, Screen.width, Screen.height, 0);
        Graphics.DrawTexture(
            new Rect(0, 0, Screen.width, Screen.height),
            aTexture);
        GL.PopMatrix();
    }

    [ContextMenu("Generate")]
    void Generate()
    {
        
        Texture2D texture = new Texture2D(_width, _height, TextureFormat.Alpha8, false);
        for (int i = 0; i < _height; i++)
        {
            float[] randArray = Generator.GenerateRandomArray01(_width, _type);

            for (int y = 0;  y < _width;  y++)
            {
                texture.SetPixel(i, y, new Color(0, 0, 0, randArray[y]));
            }

        }

        texture.Apply();

        var bytes = texture.EncodeToPNG();

        string path = string.Format("{0}/random_{1}.png", Path.Combine(Application.dataPath, "texture"), _type);

        File.WriteAllBytes(path, bytes);


        Debug.LogError("OK");

    }

    //void OnValidate()
    //{
    //    string path = string.Format("{0}/random_{1}.png", Path.Combine(Application.dataPath, "textures"), _type);
    //    Debug.LogError(path);
    //}

}
