using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    [field:SerializeField]
    public int TotalHealthPoints { get; protected set; }
    public int HealthPoints { get; private set; }

    public void TakeHit(int damage = 1)
    {
        if(HealthPoints <= 0 || IsProtected())
            return;
    
        HealthPoints--;
        OnTakeDamage();
        if (HealthPoints <= 0)
        {
            OnDeath();
        }
    }

    protected void InitHealth()
    {
        HealthPoints = TotalHealthPoints;
    }
    
    protected virtual void OnTakeDamage(){ }
    protected virtual void OnDeath(){ }

    protected virtual bool IsProtected()
    {
        return false;
    }
}
