using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alvo : MonoBehaviour {

    public Arqueira archer;
    public bool active = true;

    public void Teste123()
    {
        if (active)
        {
            if (Mathf.Round(archer.target.position.x) == Mathf.Round(transform.position.x) && Mathf.Round(archer.target.position.y) == Mathf.Round(transform.position.y))
            {
                archer.PlayerDettected(this, new Vector3(transform.position.x, transform.position.y, 0));
            }
        }
    }
}
