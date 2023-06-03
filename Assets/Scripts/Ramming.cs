using UnityEngine;

public class Ramming : MonoBehaviour
{
    [Tooltip("Danno causato dallo speronamento"), Range(1, 100)]
    public int damage = 30;

    private void OnCollisionEnter2D(Collision2D other)
    {
        IHittableInterface damageInterface = other.gameObject.GetComponent<IHittableInterface>();
        if (damageInterface != null)
            damageInterface.DealDamage(damage);
    }
}