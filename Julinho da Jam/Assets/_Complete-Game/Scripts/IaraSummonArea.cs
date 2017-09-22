using Completed;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IaraSummonArea : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0 || Input.GetKeyDown(KeyCode.Alpha1))
        {
            Destroy(gameObject);
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject == gameObject)
                {
                    GameObject iara = GameManager.instance.boardScript.Summon(4, transform.position);
                    GameManager.instance.IarasCharm(5, iara);
                }
            }

            Destroy(gameObject);
        }
    }
}
