using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.Mathematics;

public class LoadImages : MonoBehaviour
{
    [Header("Image Size")]
    [SerializeField] float maxWidth = 192;
    [SerializeField] float maxHeight = 108;

    [Header("Displays")]
    [SerializeField] private List<Image> images;

    [SerializeField, ReadOnly] private int pageNumber = 0;

    void Start()
    {
        GalleryLoader.Load();

        Debug.Log("amount of images: " + GalleryLoader.getImages().Length);

        UpdatePage();

        //Transform imageTransform = imageDisplay.GetComponent<Transform>();
        //Image image = imageTransform.GetComponent<Image>();
        //image.preserveAspect = true;



        //imageTransform.localScale = new Vector2(Mathf.Clamp(texture.width, 1, maxWidth), Mathf.Clamp(texture.height, 1, maxHeight));

    }

    public void PreviousPage()
    {
        pageNumber--;
        if (pageNumber < 0) pageNumber = 0;
        UpdatePage();
    }

    public void NextPage()
    {
        pageNumber = math.min(pageNumber + 1, GalleryLoader.loadedImageCount() / images.Count);
        UpdatePage();
    }

    private void UpdatePage()
    {
        int i = pageNumber * images.Count;

        foreach (Image image in images)
        {
            Texture2D texture = GalleryLoader.getImage(i);
            if (texture != null)
            {
                image.gameObject.SetActive(true);
                image.preserveAspect = true;
                image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            }
            else image.gameObject.SetActive(false);
            i++;
        }
    }
}
