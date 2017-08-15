using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class PlayerController : DinamicObjectController
{
    public GameObject bombPrefab;
    public int maxCountOfBombs = 1;
    public int countOfExplosions = 1;
    public bool wallHack = false;

    private Rigidbody PlayerRigidbody { get; set; }
    private Animator animator;
    private Text messageText;
    private List<GameObject> playersBomb = new List<GameObject>();

    public override void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        float moveX = 0;
        float moveZ = 0;

        if(Mathf.Abs(x) > Mathf.Abs(z))
            moveX = x;
        else
            moveZ = z;

        if (moveX == 0 && moveZ == 0)
        {
            this.animator.Play("Stand");
        }
        else
        {
            if(this.Speed > Game.DinamicObjectSpeed)
            {
                this.animator.Play("Run");
            }
            else
            {
                this.animator.Play("Walk");
            }
        }

        this.SetMove(this.PlayerRigidbody, moveX, this.RotationByY(moveX, moveZ), moveZ);
    }

    private void Start()
    {
        this.Speed = Game.DinamicObjectSpeed;
        this.PlayerRigidbody = gameObject.GetComponent<Rigidbody>();

        this.animator = GetComponent<Animator>();
        this.messageText = Game.GUI.GetComponentInChildren<Text>();
    }

    private void FixedUpdate()
    {
        this.Move();

        this.RemoveExplodedBombs();

        if (Input.GetKeyDown("space"))
        {
            float x = Mathf.Round(gameObject.transform.position.x);
            float z = Mathf.Round(gameObject.transform.position.z);

            if (this.CanPutBomb(x,z))
            {
                this.PutBomb(x,z);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.tag)
        {
            case "Bomb":
                other.isTrigger = false;
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        int row = (int)other.gameObject.transform.position.z;
        int col = (int)other.gameObject.transform.position.x;

        if (Game.MatrixMap[row, col] != (int)ObjectType.BreakWall)
        {
            switch (other.gameObject.tag)
            {
                case "BombPowerUp":
                    this.maxCountOfBombs++;
                    this.PickUpPowerUp(other.gameObject, "BOMBS", this.maxCountOfBombs.ToString());
                    break;
                case "ExplosionPowerUp":
                    this.countOfExplosions++;
                    this.PickUpPowerUp(other.gameObject, "EXPLOSION", this.countOfExplosions.ToString());
                    break;
                case "SpeedPowerUp":
                    this.Speed++;
                    this.PickUpPowerUp(other.gameObject, "SPEED", this.Speed.ToString());
                    break;
                case "WallHackPowerUp":
                    if (!this.wallHack)
                    {
                        foreach (GameObject breakWall in GameObject.FindGameObjectsWithTag("Break Wall"))
                        {
                            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), breakWall.GetComponent<Collider>());
                        }
                        this.wallHack = true;
                    }
                    this.PickUpPowerUp(other.gameObject, "WALL HACK", this.wallHack.ToString());
                    break;
            }
        }
    }

    private void RemoveExplodedBombs()
    {
        foreach (var bomb in this.playersBomb.ToArray())
        {
            if (bomb == null)
            {
                this.playersBomb.Remove(bomb);
            }
        }
    }

    private void PutBomb(float x, float z)
    {
        GameObject bomb = Game.AddObjectToMap(this.bombPrefab, new Vector3(x, 0.5f, z), ObjectType.Bomb);
        BombController bombController = bomb.GetComponent<BombController>();
        bombController.countOfExplosions = this.countOfExplosions;

        this.playersBomb.Add(bomb);
    }

    private bool CanPutBomb(float x, float z)
    {
        return Game.MatrixMap[(int)z, (int)x] != (int)ObjectType.BreakWall
            && Game.MatrixMap[(int)z, (int)x] != (int)ObjectType.Bomb
            && this.playersBomb.Count < this.maxCountOfBombs;
    }

    private IEnumerator ClearMessage()
    {
        yield return new WaitForSeconds(2f);
        this.messageText.text = "";
    }

    private void PickUpPowerUp(GameObject gameObject, string name, string levelPowerUp)
    {
        //StopCoroutine(ClearMessage());
        AudioSource audioEffect = gameObject.GetComponent<AudioSource>();
        audioEffect.Play();

        this.messageText.text = String.Format("Pick up {0} power up ({1})", name, levelPowerUp);
        this.Animate(gameObject, 0, 3, 0);
        StartCoroutine(ClearMessage());
    }

    private void Animate(GameObject gameObject, float x, float y, float z)
    {
        Vector3 start = gameObject.transform.position;
        Vector3 end = gameObject.transform.position + new Vector3(x, y, z);
        StartCoroutine(GetStep(gameObject, start, end));
    }

    private IEnumerator GetStep(GameObject gameObject, Vector3 start, Vector3 end)
    {
        Vector3 diff = end - start;
        for (int i = 0; i < Game.DinamicObjectSmooth; i++)
        {
            gameObject.transform.position += (diff / Game.DinamicObjectSmooth);
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }

}
