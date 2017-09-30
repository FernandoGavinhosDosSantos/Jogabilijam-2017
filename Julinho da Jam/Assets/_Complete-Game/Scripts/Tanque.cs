using Completed;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tanque : Enemy {

    // Use this for initialization
    protected override void Start()
    {
        this.hp = 2;
        this.male = true;
        this.id = 'T';

        base.Start();
    }

    public override void Damage(int dmg, bool reaction)
    {
        if (trapped) return;

        if (reaction)
        {
            float pX = Mathf.Round(GameManager.instance.player.transform.position.x);
            float pY = Mathf.Round(GameManager.instance.player.transform.position.y);
            float tX = Mathf.Round(transform.position.x);
            float tY = Mathf.Round(transform.position.y);

            if (pX == tX && (pY == tY + 1 || pY == tY - 1)) GameManager.instance.player.LoseFood(1);
            if (leftTurned && pY == tY && pX == tX - 1) GameManager.instance.player.LoseFood(1);
            if (!leftTurned && pY == tY && pX == tX + 1) GameManager.instance.player.LoseFood(1);
        }

        base.Damage(dmg, reaction);
    }
}
