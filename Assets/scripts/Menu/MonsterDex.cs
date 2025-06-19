using System;
using UnityEngine;

public class MonsterDex : MonoBehaviour
{
    public void HideAllChilds()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
