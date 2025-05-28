using System.Collections.Generic;
using UnityEngine;

public static class GaleryLoader {
    static List<Texture2D> imageBuffer = new List<Texture2D>();
    static string filePath = "screenshots";

    /// <summary>
    /// loads all images from the files
    /// </summary>
    public static void Load() {
        imageBuffer.Clear();
        Debug.Log("reading files in: " + Application.persistentDataPath + "/" + filePath);
        string[] files = System.IO.Directory.GetFiles(Application.persistentDataPath + "/" + filePath);
        foreach (string filename in files) {
            byte[] bytes = new byte[1024];
            bytes = System.IO.File.ReadAllBytes(filePath);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(bytes);
            imageBuffer.Add(texture);
        }
    }

    /// <summary>
    /// gets an images from the loaded images at the given ID
    /// have to call .Load() first
    /// </summary>
    public static Texture2D getImage(int id) {
        if (id >= imageBuffer.Count || id < 0) return null;
        return imageBuffer[id];
    }

    /// <summary>
    /// gets an array of all loaded images
    /// have to call .Load() first
    /// </summary>
    public static Texture2D[] getImages() {
        return imageBuffer.ToArray();
    }

    /// <summary>
    /// loads all images from the files
    /// then gets an array of all loaded images
    /// DOES NOT have to call .Load() first
    /// </summary>
    public static Texture2D[] loadAndGetImages() {
        Load();
        return getImages();
    }
}
