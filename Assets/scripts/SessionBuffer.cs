using System.Collections.Generic;
using UnityEngine;

public static class SessionBuffer
{
    static Dictionary<Texture2D, int> takenPhotos = new Dictionary<Texture2D, int>();

    public static void Reset() {
        foreach (Texture2D texture in takenPhotos.Keys) {
            // since texture are stored in memory a little differen destroy them
            Object.Destroy(texture);
        }
        takenPhotos.Clear();
    }

    public static int GetScoreBuffer() {
        int toReturn = 0;
        foreach (int score in takenPhotos.Values) {
            toReturn += score;
        }
        return toReturn;
    }

    public static void SaveImageToBuffer(Texture2D texture, int score) {
        takenPhotos.Add(texture, score);
    }

    internal static Dictionary<Texture2D, int> getImageBuffer() {
        return takenPhotos;
    }
}
