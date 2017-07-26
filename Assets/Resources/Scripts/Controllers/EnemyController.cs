using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : DinamicObjectController {

    private Rigidbody[] enemiesRigidbody = new Rigidbody[Game.countOfEnemies];

    public float Speed { get; set; }

    private System.Random RandomValue { get; set; }


    public EnemyController(GameObject enemyObject, float speed)
    {
        GameObject[] allEnemiesObject = GameObject.FindGameObjectsWithTag(enemyObject.tag);

        for (int i = 0; i < allEnemiesObject.Length; i++)
        {
            this.enemiesRigidbody[i] = allEnemiesObject[i].GetComponent<Rigidbody>();
        }

        this.Speed = speed;
        this.RandomValue = new System.Random();
    }

    public override void Move()
    {
        foreach(var enemy in enemiesRigidbody)
        {
            int diretion = RandomValue.Next(0, 4);

            switch (diretion)
            {
                // up
                case 0:
                    enemy.transform.rotation = Quaternion.Euler(0, 0, 0);
                    enemy.transform.position += new Vector3(0, 0, 1) * Time.deltaTime * this.Speed;
                    break;
                // down
                case 1:
                    enemy.transform.rotation = Quaternion.Euler(0, 180, 0);
                    enemy.transform.position += new Vector3(0, 0, -1) * Time.deltaTime * this.Speed;
                    break;
                // right
                case 2:
                    enemy.transform.rotation = Quaternion.Euler(0, 90, 0);
                    enemy.transform.position += new Vector3(1, 0, 0) * Time.deltaTime * this.Speed;
                    break;
                // left
                case 3:
                    enemy.transform.rotation = Quaternion.Euler(0, 270, 0);
                    enemy.transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * this.Speed;
                    break;
            }
        }

    }
}
