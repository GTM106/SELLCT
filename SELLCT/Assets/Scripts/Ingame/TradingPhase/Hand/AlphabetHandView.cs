using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphabetHandView : MonoBehaviour
{
    [SerializeField] List<Image> _alphabetImages;

    private void Awake()
    {
        Set();
    }

    public void Set()
    {
        foreach (var item in _alphabetImages)
        {
            item.enabled = StringManager.hasElements[4];
        }
    }
}