using System.Collections.Generic;
using UnityEngine;

public class GaleryLoader : MonoBehaviour
{
    List<Texture2D> imageBuffer = new List<Texture2D>();
    [SerializeField] string filePath = "screenshots";

    public void Load()
    {
        imageBuffer.Clear();
        Debug.Log("reading files in: " + Application.persistentDataPath + "/" + filePath);
        string[] files = System.IO.Directory.GetFiles(Application.persistentDataPath +"/"+ filePath);
        foreach (string filename in files) {
            byte[] bytes = new byte[1024];
            bytes = System.IO.File.ReadAllBytes(filePath);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(bytes);
            imageBuffer.Add(texture);
        }
    }

    public Texture2D getImage(int id)
    {
        if (id>=imageBuffer.Count || id<0) return null;
        return imageBuffer[id];
    }
}
