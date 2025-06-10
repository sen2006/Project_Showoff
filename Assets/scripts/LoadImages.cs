using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using System.Collections.Generic;

public class LoadImages : MonoBehaviour
{
    [Header("Image Size")]
    [SerializeField] float maxWidth = 192;
    [SerializeField] float maxHeight = 108;

    [Header("Displays")]
    [SerializeField] Image imageDisplay;

    private Texture2D[] allImages;

    void Start()
    {
        GaleryLoader.Load();

        allImages = GaleryLoader.getImages();
        Debug.Log("amount of images: " + allImages.Length);

        Texture2D texture = allImages[2];
        Transform imageTransform = imageDisplay.GetComponent<Transform>();
        imageTransform.localScale = new Vector2(Mathf.Clamp(texture.width, 1, maxWidth), Mathf.Clamp(texture.height, 1, maxHeight));

        imageDisplay.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }

    void Update()
    {
        
    }
}
