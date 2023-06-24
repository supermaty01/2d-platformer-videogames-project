public class Boots : PowerUp
{
    protected override void ActivatePowerUp(Player player)
    {
        player.ActivatePowerUp(Player.PowerUp.Boots);
    }
}