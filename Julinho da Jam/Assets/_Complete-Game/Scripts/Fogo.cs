using Completed;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fogo : MonoBehaviour
{

    public int turns, x, y, xDir, yDir;
    public bool last;
    SpriteRenderer sprite;

    public void readyToBurn(Enemy victim)
    {
        if (victim.id == 'T')
        {
            victim.Damage(1, false);
            GameManager.instance.waitAnimation = false;
        }
        else
        {
            victim.transform.position = new Vector3(x + xDir, y + yDir, 0);

            Instantiate(GameManager.instance.boardScript.SummonTiles[GameManager.SACI], new Vector3(x, y, 0), Quaternion.identity);
        }
    }

    public void initialize()
    {
        /*
        if (victim == null)
        {
            GameManager.instance.boitataIni = transform.position;
            GameManager.instance.fireOrigin = new Vector3(GameManager.instance.player.transform.position.x, GameManager.instance.player.transform.position.y, 0);
            GameManager.instance.initialFlame = this;
        }
        */

        GameManager.instance.waitAnimation = true;
        sprite = GetComponent<SpriteRenderer>();
        GameManager.instance.initialFlame = this;
        GameManager.instance.boitataIni = transform.position;
        sprite.enabled = true;

        x = (int)Mathf.Round(transform.position.x);
        y = (int)Mathf.Round(transform.position.y);

        xDir = (int)Mathf.Round(x - GameManager.instance.player.transform.position.x);
        yDir = (int)Mathf.Round(y - GameManager.instance.player.transform.position.y);

        if (xDir > 0)
            for (int i = x + 1; i < GameManager.instance.levelSettings.GetLength(0); i++)
                Instantiate(GameManager.instance.boardScript.SummonTiles[GameManager.BOITATA], new Vector3(i, y, 0), Quaternion.identity);

        if (xDir < 0)
            for (int i = x - 1; i > 0; i--)
                Instantiate(GameManager.instance.boardScript.SummonTiles[GameManager.BOITATA], new Vector3(i, y, 0), Quaternion.identity);

        if (yDir > 0)
            for (int i = y + 1; i < GameManager.instance.levelSettings.GetLength(1); i++)
                Instantiate(GameManager.instance.boardScript.SummonTiles[GameManager.BOITATA], new Vector3(x, i, 0), Quaternion.identity);

        if (yDir < 0)
            for (int i = y - 1; i > 0; i--)
                Instantiate(GameManager.instance.boardScript.SummonTiles[GameManager.BOITATA], new Vector3(x, i, 0), Quaternion.identity);
    }

    // Use this for initialization
    void Start()
    {

        x = (int)Mathf.Round(transform.position.x);
        y = (int)Mathf.Round(transform.position.y);

        GameManager.instance.levelSettings[x, y] = 'W';

        sprite = GetComponent<SpriteRenderer>();
        GameManager.instance.player.fire.Enqueue(this);
        turns = 6;
    }

    // Update is called once per frame
    void Update()
    {
        x = (int)Mathf.Round(transform.position.x);
        y = (int)Mathf.Round(transform.position.y);

        GameManager.instance.levelSettings[x, y] = 'W';

        if (turns <= 0)
        {
            GameManager.instance.activeSummons[GameManager.BOITATA] = false;
            GameManager.instance.levelSettings[x, y] = '_';

            GameManager.instance.player.fire.Dequeue();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ally") sprite.enabled = true;
    }
}