using Completed;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arqueira : Enemy {

    public Flecha arrow;
    private Flecha aInstance;

    public Alvo sensor;
    private Alvo[] sInstance;

    public bool hasArrow;
    public int isPlayerUnderAim;
    public Vector3 aimLocation;

    private Queue<Alvo> inactiveList;

    // Use this for initialization
    protected override void Start () {

        this.hp = 1;
        this.male = false;
        this.id = 'A';

        aInstance = Instantiate(arrow, this.transform.position, Quaternion.identity);
        hasArrow = true;
        isPlayerUnderAim = 0;

        SetSensors();

        base.Start();
    }

    private void SetSensors()
    {
        inactiveList = new Queue<Alvo>();

        float x = transform.position.x;
        float y = transform.position.y;

        sInstance = new Alvo[12];
        sInstance[0] = Instantiate(sensor, new Vector3(x, y + 1, 0), Quaternion.identity);
        sInstance[1] = Instantiate(sensor, new Vector3(x, y + 2, 0), Quaternion.identity);
        sInstance[2] = Instantiate(sensor, new Vector3(x, y + 3, 0), Quaternion.identity);
        sInstance[3] = Instantiate(sensor, new Vector3(x + 1, y, 0), Quaternion.identity);
        sInstance[4] = Instantiate(sensor, new Vector3(x + 2, y, 0), Quaternion.identity);
        sInstance[5] = Instantiate(sensor, new Vector3(x + 3, y, 0), Quaternion.identity);
        sInstance[6] = Instantiate(sensor, new Vector3(x, y - 1, 0), Quaternion.identity);
        sInstance[7] = Instantiate(sensor, new Vector3(x, y - 2, 0), Quaternion.identity);
        sInstance[8] = Instantiate(sensor, new Vector3(x, y - 3, 0), Quaternion.identity);
        sInstance[9] = Instantiate(sensor, new Vector3(x - 1, y, 0), Quaternion.identity);
        sInstance[10] = Instantiate(sensor, new Vector3(x - 2, y, 0), Quaternion.identity);
        sInstance[11] = Instantiate(sensor, new Vector3(x - 3, y, 0), Quaternion.identity);

        foreach (Alvo s in sInstance)
        {
            s.archer = this;
        }
    }

    public void PlayerDettected(Alvo sensor, Vector3 pos)
    {
        sensor.active = false;
        inactiveList.Enqueue(sensor);
        
        aimLocation = pos;
        if (hasArrow) isPlayerUnderAim = 1;
    }

    private void Reload()
    {
        aInstance = Instantiate(arrow, this.transform.position, Quaternion.identity);
        hasArrow = true;
    }

    public override void Die()
    {
        if (hasArrow) aInstance.die();
        base.Die();
    }

    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        for (int i = 0; i < sInstance.Length; i++)
        {
            sInstance[i].Teste123();
        }

        if (inactiveList.Count > 0)
            inactiveList.Dequeue().active = true;

        if (hasArrow && isPlayerUnderAim > 0)
        {
            isPlayerUnderAim = 0;

            aInstance.Shoot(this, aimLocation);
        } else if (!hasArrow) Reload();
    }

    protected override void OnCantMove<T>(T component)
    {

    }
}
