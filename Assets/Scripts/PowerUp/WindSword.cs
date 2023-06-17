using UnityEngine;

public class WindSword : PowerUp
{
    
    private Rigidbody2D _rb;

    protected override void ActivatePowerUp(Player player)
    {
        player.ActivatePowerUp(Player.PowerUp.WindSword);
    }


}