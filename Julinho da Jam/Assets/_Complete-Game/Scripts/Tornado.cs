using Completed;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    private bool center = false;

    private float xDir;
    private float yDir;
    private float speed = 10f;

    private Enemy victim;

    // Use this for initialization
    void Start()
    {
        if (GameManager.instance.activeSummons[GameManager.BOITATA])
        {
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            sprite.enabled = false;
            GameManager.instance.boitataIni.x = -1;
            speed = 5f;
        }

        xDir = Mathf.Round(transform.position.x - GameManager.instance.player.transform.position.x) * speed;
        yDir = Mathf.Round(transform.position.y - GameManager.instance.player.transform.position.y) * speed;

        int x = (int)Mathf.Round(transform.position.x);
        int y = (int)Mathf.Round(transform.position.y);

        if (GameManager.instance.levelSettings[x, y] != 'W')
            GameManager.instance.levelSettings[x, y] = '_';
        GameManager.instance.waitAnimation = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(xDir * Time.deltaTime, yDir * Time.deltaTime, 0);
        if (victim != null)
        {
            if (!center && transform.position.x - victim.transform.position.x < 0.01f && transform.position.y - victim.transform.position.y < 0.01f) center = true;
            if (center) victim.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }

    private void printPosition(Enemy enemy, char id)
    {
        int x = (int)Mathf.Round(enemy.transform.position.x);
        int y = (int)Mathf.Round(enemy.transform.position.y);
        
        GameManager.instance.levelSettings[x, y] = id;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            if (victim != null)
            {
                victim.Die();
            }
            victim = null;
            GameManager.instance.waitAnimation = false;
            Destroy(gameObject);
        }
        if (collision.tag == "Enemy")
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy.id == 'T')
            {
                if (victim != null)
                {
                    victim.Die();
                    enemy.Damage(1, false);
                }
                victim = null;
                GameManager.instance.waitAnimation = false;
                Destroy(gameObject);
            }
            else
            {
                if (victim == null)
                {
                    victim = enemy;
                    printPosition(enemy, '_');
                }
                else
                {
                    victim.Die();
                    enemy.Die();
                    GameManager.instance.waitAnimation = false;
                    Destroy(gameObject);
                }
            }
        }
    }
}