using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GalleryLoader {
    private static List<TextureDetails> imageBuffer = new List<TextureDetails>();
    private static string filePath = "screenshots";

    /// <summary>
    /// loads all images from the files
    /// </summary>
    public static void Load() {
        imageBuffer.Clear();
        Debug.Log("reading files in: " + Application.persistentDataPath + "/" + filePath);
        string[] files = System.IO.Directory.GetFiles(Application.persistentDataPath + "/" + filePath);
        foreach (string filename in files) {
            byte[] bytes = new byte[1024];
            bytes = System.IO.File.ReadAllBytes(filename);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(bytes);
            TextureDetails textureDetails;
            try {
                textureDetails = new(texture, int.Parse(filename.Split('_').Last().Split('.').First()));
            } catch {
                textureDetails = new(texture, 0);
            }
            Debug.Log("read Image: " + filename+", with score: "+textureDetails.getScore());
            imageBuffer.Add(textureDetails);
        
        }
    }

    /// <summary>
    /// gets an image from the loaded images at the given ID
    /// have to call .Load() first
    /// </summary>
    public static Texture2D getImage(int id) {
        if (id >= imageBuffer.Count || id < 0) return null;
        return imageBuffer[id].getTexture();
    }

    /// <summary>
    /// gets an image from the loaded images at the given ID
    /// have to call .Load() first
    /// </summary>
    public static int getScore(int id) {
        if (id >= imageBuffer.Count || id < 0) return 0;
        return imageBuffer[id].getScore();
    }

    /// <summary>
    /// gets an array of all loaded images
    /// have to call .Load() first
    /// </summary>
    public static TextureDetails[] getImages() {
        return imageBuffer.ToArray();
    }

    /// <summary>
    /// loads all images from the files
    /// then gets an array of all loaded images
    /// DOES NOT have to call .Load() first
    /// </summary>
    public static TextureDetails[] loadAndGetImages() {
        Load();
        return getImages();
    }

    /// <summary>
    /// gets the count of all loaded images
    /// have to call .Load() first
    /// </summary>
    public static int loadedImageCount() {
        return imageBuffer.Count;
    }

    /// <summary>
    /// loads all images from the files
    /// then gets the count of all loaded images
    /// DOES NOT have to call .Load() first
    /// </summary>
    public static int loadAndImageCount() {
        Load();
        return imageBuffer.Count;
    }

    public class TextureDetails {
        readonly Texture2D texture;
        readonly int score;

        public TextureDetails(Texture2D texture, int score) {
            this.texture = texture;
            this.score = score;
        }

        public Texture2D getTexture() {
            return texture;
        }
        public int getScore() {
            return score;
        }
    }
}


