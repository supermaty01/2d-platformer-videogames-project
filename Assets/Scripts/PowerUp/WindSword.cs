using UnityEngine;

public class WindSword : PowerUp
{
    
    protected override void ActivatePowerUp(Player player)
    {
        player.ActivatePowerUp(Player.PowerUp.WindSword);
    }


}