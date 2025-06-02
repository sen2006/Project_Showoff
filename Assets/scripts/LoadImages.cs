using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class LoadImages : MonoBehaviour
{
    [Header("Image Size")]
    [SerializeField] float maxWidth = 192;
    [SerializeField] float maxHeight = 108;

    [Header("Displays")]
    [SerializeField] Image imageDisplay;

    void Start()
    {
        GaleryLoader.Load();
        Texture2D texture = GaleryLoader.getImage(0);
        Transform imageTransform = imageDisplay.GetComponent<Transform>();
        imageTransform.localScale = new Vector2(Mathf.Clamp(texture.width, 1, maxWidth), Mathf.Clamp(texture.height, 1, maxHeight));

        imageDisplay.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }
    void Update()
    {
        
    }
}
