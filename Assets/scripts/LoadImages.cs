using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.Mathematics;
using TMPro;

public class LoadImages : MonoBehaviour
{
    [Header("Displays")]
    [SerializeField] private List<Image> images;
    [SerializeField] private List<TMP_Text> scoreText;

    [Header("Page Number")]
    [SerializeField, ReadOnly] private int pageNumber = 0;
    [SerializeField] private TextMeshProUGUI pageNumberText;

    void Start()
    {
        GalleryLoader.Load();

        Debug.Assert(images.Count == scoreText.Count, "Image display amount does not match with score display amount");

        Debug.Log("amount of images: " + GalleryLoader.getImages().Length);

        UpdatePage();
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

        pageNumberText.text = (pageNumber + 1).ToString();
        for (int j = 0; j < images.Count; j++)
        {
            Texture2D texture = GalleryLoader.getImage(i);
            if (texture != null)
            {
                images[j].gameObject.SetActive(true);
                images[j].preserveAspect = true;
                images[j].sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

                scoreText[j].gameObject.SetActive(true);
                scoreText[j].text = "SCORE: " + GalleryLoader.getScore(i);
            }
            else
            {
                images[j].gameObject.SetActive(false);
                scoreText[j].gameObject.SetActive(false);
            }
            i++;
        }
    }
}
