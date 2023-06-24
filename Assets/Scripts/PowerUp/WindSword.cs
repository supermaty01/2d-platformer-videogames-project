using UnityEngine;
using UnityEngine.Serialization;

public class WindSword : PowerUp
{
    [SerializeField] public float floatingSpeed = 2f;

    [FormerlySerializedAs("_initPos")] [Space(20)] [SerializeField]
    private Transform _initPosTransform;

    [FormerlySerializedAs("_endPos")] [SerializeField]
    private Transform _endPosTransform;

    private Vector3 _initPos, _endPos;


    private void Start()
    {
        _initPos = _initPosTransform.position;
        _endPos = _endPosTransform.position;
    }

    private void Update()
    {
        var d = (_initPos - _endPos).magnitude;
        var delta = Mathf.PingPong(Time.time * floatingSpeed, d);
        transform.position = Vector3.Lerp(_initPos, _endPos, delta / d);
    }

    protected override void ActivatePowerUp(Player player)
    {
        player.ActivatePowerUp(Player.PowerUp.WindSword);
    }
}