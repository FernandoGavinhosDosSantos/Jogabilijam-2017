using Completed;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IaraSummonArea : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Player.selecionado == false)
        {
            Destroy(gameObject);
        }
        
    }
}
