using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphabetHandView : MonoBehaviour
{
    readonly List<Image> _alphabetImages = new();

    public void Set()
    {
        foreach (var item in _alphabetImages)
        {
            item.enabled = StringManager.hasElements[(int)StringManager.Element.E21];
        }
    }

    public void Add(Image alphabetImage)
    {
        _alphabetImages.Add(alphabetImage);
    }
}
