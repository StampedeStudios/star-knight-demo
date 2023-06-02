using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputAction : MonoBehaviour
{
    private void Update()
    {
        // premo spazio
        if (Input.GetKeyDown("space"))
        {
            foreach (var item in GetComponents<IGunInterface>())
            {
                item.StartShoot();
            }
        };

        // rilascio spazio
        if (Input.GetKeyUp("space"))
        {
            foreach (var item in GetComponents<IGunInterface>())
            {
                item.StopShoot();
            }
        };
    }
}
