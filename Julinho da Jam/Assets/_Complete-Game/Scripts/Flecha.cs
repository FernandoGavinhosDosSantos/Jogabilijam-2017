using Completed;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flecha : MonoBehaviour {

    private const float ASCEND = 10f, DESCEND = -10f;
    private Vector3 flyingDirection;

    private bool shoot;
    private float shootStage;
    private Vector3 target;
    private Arqueira archer;

    public void Shoot(Arqueira archer, Vector3 target)
    {
        GameManager.instance.waitArrow = true;
        this.shoot = true;
        this.target = target;
        this.archer = archer;

        this.shootStage = ASCEND;
        this.flyingDirection = new Vector3(0, shootStage, 0);
    }

    public void Update()
    {
        if (shoot)
        {
            transform.Translate(0, flyingDirection.y * Time.deltaTime, 0);

            if (shootStage == ASCEND && transform.position.y > archer.transform.position.y + 3)
            {
                transform.position = new Vector3(target.x, target.y + 3, 0);
                transform.Rotate(new Vector3(180, 0, 0));
                shootStage = DESCEND;
            }

            if (shootStage == DESCEND && transform.position.y <= target.y)
            {
                archer.hasArrow = false;

                float playerX = Mathf.Round(GameManager.instance.player.transform.position.x);
                float playerY = Mathf.Round(GameManager.instance.player.transform.position.y);
                float arrowX = Mathf.Round(transform.position.x);
                float arrowY = Mathf.Round(transform.position.y);

                if (playerX == arrowX && playerY == arrowY) GameManager.instance.player.LoseFood(1);
                GameManager.instance.waitArrow = false;
                Destroy(gameObject);
            }
        }
    }
}