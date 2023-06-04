using UnityEngine;

public class InputAction : MonoBehaviour
{
    private void Update()
    {
        // premo il tasto SPAZIO
        if (Input.GetKeyDown("space"))
        {
            foreach (var item in GetComponents<IGunInterface>())
            {
                item.StartShoot();
            }
        };

        // rilascio il tasto SPAZIO
        if (Input.GetKeyUp("space"))
        {
            foreach (var item in GetComponents<IGunInterface>())
            {
                item.StopShoot();
            }
        };

        // premo il tasto R
        if (Input.GetKeyDown("r"))
        {
            foreach (var item in GetComponents<IGunInterface>())
            {
                item.StartReleoading();
            }
        }
    }
}
