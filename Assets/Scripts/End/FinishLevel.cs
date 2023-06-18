using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        GameEvents.OnEndLevelEvent?.Invoke();
    }
}