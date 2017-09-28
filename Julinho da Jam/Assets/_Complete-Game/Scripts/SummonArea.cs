using Completed;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonArea : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Player.selecionando == false)
        {
            Destroy(gameObject);
        }
    }
}
