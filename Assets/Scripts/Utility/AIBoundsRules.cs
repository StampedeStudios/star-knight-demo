using UnityEngine;

public class AIBoundsRules : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Gun gun = other.gameObject.GetComponent<Gun>();
        if (gun)
            gun.enabled = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        Destroy(other.gameObject);
    }
}
