using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCounter : MonoBehaviour
{
    private readonly List<Image> _hearts = new();

    private void Start()
    {
        GameEvents.OnPlayerHealthChangeEvent += UpdateHearts;

        var childCount = transform.childCount;

        for (var i = 0; i < childCount; i++)
        {
            var child = transform.GetChild(i);
            _hearts.Add(child.GetComponent<Image>());
        }
    }

    private void OnDestroy()
    {
        GameEvents.OnPlayerHealthChangeEvent -= UpdateHearts;
    }

    private void UpdateHearts(int health)
    {
        // When the player is hit, the color of one of the hearts is changed to gray
        for (var i = 0; i < _hearts.Count; i++)
            if (i < health)
                _hearts[i].color = Color.white;
            else
                _hearts[i].color = new Color(0.3f, 0.3f, 0.3f, 1);
    }
}