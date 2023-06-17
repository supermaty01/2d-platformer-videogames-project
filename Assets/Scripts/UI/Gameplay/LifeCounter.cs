using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCounter : MonoBehaviour
{
    private List<Image> _hearts = new List<Image>();
    
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
        for (var i = 0; i < _hearts.Count; i++)
            if (i < health)
                _hearts[i].color = Color.white;
            else
                _hearts[i].color = new Color(0.3f, 0.3f, 0.3f, 1);
    }
}