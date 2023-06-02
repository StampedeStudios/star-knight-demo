using UnityEngine;

public class HealthHandler : MonoBehaviour, IHittableInterface
{
    [Tooltip("Vita del possessore")]
    public int Health = 100;

    public void DealDamage(int damage)
    {
        // sottraggo il danno subito
        Health -= damage;

        // controllo se la vita è finita
        if (Health <= 0)
            Die();
    }

    private void Die()
    {
        foreach (var item in GetComponents<IDeathIterface>())
        {
            item.OnDeathEvent();
        }
        // recupero l' animatore ed eseguo l' animazione dedicata 
        Animator anim = GetComponent<Animator>();
        if (anim)
            anim.SetBool("isDead", true);
        else
            Destroy(gameObject);
    }

}
