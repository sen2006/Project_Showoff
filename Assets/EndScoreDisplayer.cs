using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.Rendering;

public class EndScoreDisplayer : MonoBehaviour {
    [Header("Displays")]
    [SerializeField] private List<Image> images;
    [SerializeField] private List<TMP_Text> scoreText;

    void Start() {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        Debug.Assert(images.Count == 3, "image list length needs to be 3");
        Debug.Assert(scoreText.Count == 3, "scoreText list length needs to be 3");

        Tuple<Texture2D, int> nrOne = Tuple.Create((Texture2D)null, 0);
        Tuple<Texture2D, int> nrTwo = Tuple.Create((Texture2D)null, 0);
        Tuple<Texture2D, int> nrThree = Tuple.Create((Texture2D)null, 0);
        Dictionary<Texture2D, int> takenPhotos = SessionBuffer.getImageBuffer();
        Debug.Log("total photos taken in session:" + takenPhotos.Count);
        foreach (Texture2D image in takenPhotos.Keys) {
            int score = takenPhotos[image];

            if (score > nrOne.Item2) {
                nrThree = Tuple.Create(nrTwo.Item1, nrTwo.Item2);
                nrTwo = Tuple.Create(nrOne.Item1, nrOne.Item2);
                nrOne = Tuple.Create(image, score);
            } else if (score > nrTwo.Item2) {
                nrThree = Tuple.Create(nrTwo.Item1, nrTwo.Item2);
                nrTwo = Tuple.Create(image, score);
            } else if (score > nrThree.Item2) {
                nrThree = Tuple.Create(image, score);
            }
        }
        foreach (Image image in images) {
            image.gameObject.SetActive(false);
        }
        foreach (TMP_Text text in scoreText) {
            text.gameObject.SetActive(false);
        }

        if (nrOne.Item1 != null) {
            images[0].gameObject.SetActive(true);
            images[0].preserveAspect = true;
            images[0].sprite = Sprite.Create(nrOne.Item1, new Rect(0, 0, nrOne.Item1.width, nrOne.Item1.height), new Vector2(0.5f, 0.5f));
            scoreText[0].gameObject.SetActive(true);
            scoreText[0].text = "SCORE: " + nrOne.Item2;
        }

        if (nrTwo.Item1 != null) {
            images[1].gameObject.SetActive(true);
            images[1].preserveAspect = true;
            images[1].sprite = Sprite.Create(nrTwo.Item1, new Rect(0, 0, nrTwo.Item1.width, nrTwo.Item1.height), new Vector2(0.5f, 0.5f));
            scoreText[1].gameObject.SetActive(true);
            scoreText[1].text = "SCORE: " + nrTwo.Item2;
        }

        if (nrThree.Item1 != null) {
            images[2].gameObject.SetActive(true);
            images[2].preserveAspect = true;
            images[2].sprite = Sprite.Create(nrThree.Item1, new Rect(0, 0, nrThree.Item1.width, nrThree.Item1.height), new Vector2(0.5f, 0.5f));
            scoreText[2].gameObject.SetActive(true);
            scoreText[2].text = "SCORE: " + nrThree.Item2;
        }
    }
}
