﻿using Completed;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lanceiro : Enemy
{
    // Use this for initialization
    protected override void Start()
    {
        this.hp = 1;
        this.male = true;
        this.id = 'L';

        base.Start();
    }

    public override void Damage(int dmg)
    {
        if (trapped) return;

        float pX = Mathf.Round(GameManager.instance.player.transform.position.x);
        float pY = Mathf.Round(GameManager.instance.player.transform.position.y);
        float lX = Mathf.Round(transform.position.x);
        float lY = Mathf.Round(transform.position.y);

        if (leftTurned && pY == lY && pX == lX - 1) GameManager.instance.player.LoseFood(1);
        if (!leftTurned && pY == lY && pX == lX + 1) GameManager.instance.player.LoseFood(1);

        base.Damage(dmg);
    }
}
