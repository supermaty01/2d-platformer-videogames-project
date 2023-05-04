using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private float _startingPos, _lengthOfSprite;
    public float amountOfParallax; 
    public Camera mainCamera;

    private void Start()
    {
        _startingPos = transform.position.x;
        _lengthOfSprite = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        var position = mainCamera.transform.position;
        var temp = position.x * (1 - amountOfParallax);
        var distance = position.x * amountOfParallax;

        var newPosition = new Vector3(_startingPos + distance, transform.position.y, transform.position.z);

        transform.position = newPosition;

        if (temp > _startingPos + _lengthOfSprite / 2)
        {
            _startingPos += _lengthOfSprite;
        }
        else if (temp < _startingPos - _lengthOfSprite / 2)
        {
            _startingPos -= _lengthOfSprite;
        }
    }
}
