using System;
using UnityEngine;

public class HideMonsterInformation : MonoBehaviour
{
    public void ResetMonsterView()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
