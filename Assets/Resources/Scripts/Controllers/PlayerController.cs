using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Audio;


public class PlayerController : DinamicObjectController
{
    public AudioClip soundOfPutBomb;
    public GameObject bombPrefab;
    public int maxCountOfBombs = 1;
    public int countOfExplosions = 1;

    private bool wallHack = false;
    private Animator animator;
    private Text messageText;
    private List<GameObject> playersBomb = new List<GameObject>();

    [Command]
    public override void CmdMove()
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
            this.animator.SetFloat("Speed",0);
        }
        else
        {
            if (this.Speed > Game.DinamicObjectSpeed)
            {
                this.animator.SetFloat("Speed",2);
            }
            else
            {
                this.animator.SetFloat("Speed",1);
            }
        }

        this.SetMove(moveX, this.RotationByY(moveX, moveZ), moveZ);
    }

    public override void OnStartServer()
    {
        this.Generate();
    }

    private void PlayPutBombSound()
    {
        audioEffect.clip = soundOfPutBomb;
        audioEffect.Play();
    }

    private void Start()
    {
        this.Speed = Game.DinamicObjectSpeed;
        this.Rigidbody = gameObject.GetComponent<Rigidbody>();
        this.audioEffect = gameObject.GetComponentInChildren<AudioSource>();
        this.animator = GetComponent<Animator>();
        this.messageText = Game.GUI.GetComponentInChildren<Text>();
    }

    private void FixedUpdate()
    {
        if (isLocalPlayer)
        {
            if (this.canMove)
                this.CmdMove();

            this.RemoveExplodedBombs();

            if (Input.GetKeyDown("space"))
            {
                float x = Mathf.Round(gameObject.transform.position.x);
                float z = Mathf.Round(gameObject.transform.position.z);

                if (this.CanPutBomb(x, z))
                {
                    this.PutBomb(x, z);
                    //StartCoroutine(this.PutBomb(x, z));
                }
            }
        }
    }

    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            CmdDeath();
        }
    }

    [Command]
    private void CmdDeath()
    {
        this.animator.SetTrigger("Killed");
        gameObject.transform.position += new Vector3(0, -0.75f, 0);
        gameObject.GetComponent<PlayerController>().enabled = false;
        gameObject.GetComponentInChildren<Collider>().enabled = false;
        Destroy(gameObject, 4);
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

        if (Game.matrixMap[row, col] != (int)ObjectType.BreakWall)
        {
            switch (other.gameObject.tag)
            {
                case "BombPowerUp":
                    this.maxCountOfBombs++;
                    this.CmdPickUpPowerUp(other.gameObject, "BOMBS", this.maxCountOfBombs.ToString());
                    break;
                case "ExplosionPowerUp":
                    this.countOfExplosions++;
                    this.CmdPickUpPowerUp(other.gameObject, "EXPLOSION", this.countOfExplosions.ToString());
                    break;
                case "SpeedPowerUp":
                    this.Speed++;
                    this.CmdPickUpPowerUp(other.gameObject, "SPEED", this.Speed.ToString());
                    break;
                case "WallHackPowerUp":
                    if (!this.wallHack)
                    {
                        foreach (GameObject breakWall in GameObject.FindGameObjectsWithTag("Break Wall"))
                        {
                            Physics.IgnoreCollision(gameObject.GetComponentInChildren<Collider>(), breakWall.GetComponent<Collider>());
                        }
                        this.wallHack = true;
                    }
                    this.CmdPickUpPowerUp(other.gameObject, "WALL HACK", this.wallHack.ToString());
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
        Game.matrixMap[(int)z, (int)x] = (int)ObjectType.Bomb;

        this.animator.SetTrigger("SetBomb");

        StartCoroutine(SetMoveTimeout(1.75f));

        //yield return new WaitForSeconds(1);
        CmdCreateBomb(x, z);
    }

    [Command]
    private void CmdCreateBomb(float x, float z)
    {
        GameObject bomb = Game.AddObjectToMap(this.bombPrefab, new Vector3(x, 0.5f, z), ObjectType.Bomb);
        BombController bombController = bomb.GetComponent<BombController>();
        bombController.countOfExplosions = this.countOfExplosions;

        this.playersBomb.Add(bomb);
    }

    private bool CanPutBomb(float x, float z)
    {
        return Game.matrixMap[(int)z, (int)x] != (int)ObjectType.BreakWall
            && Game.matrixMap[(int)z, (int)x] != (int)ObjectType.Bomb
            && this.playersBomb.Count < this.maxCountOfBombs;
    }

    private IEnumerator ClearMessage()
    {
        yield return new WaitForSeconds(2f);
        RpcChangeMessageText("");
    }

    [Command]
    private void CmdPickUpPowerUp(GameObject gameObject, string name, string levelPowerUp)
    {
        RpcPowerUpEffect(gameObject);

        RpcChangeMessageText(String.Format("Pick up {0} power up ({1})", name, levelPowerUp));

        StartCoroutine(ClearMessage());
    }

    [ClientRpc]
    private void RpcPowerUpEffect(GameObject gameObject)
    {
        AudioSource audioEffect = gameObject.GetComponent<AudioSource>();
        audioEffect.Play();
        this.Animate(gameObject, 0, 3, 0);
    }

    [ClientRpc]
    private void RpcChangeMessageText(string newText)
    {
        this.messageText.text = newText;
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

    private void Generate()
    {
        System.Random randomValue = new System.Random();
        int currentCountOfObject = 0;

        while (currentCountOfObject < 1)
        {
            int row = randomValue.Next(1, Game.row);
            int col = randomValue.Next(1, Game.col);

            if (IsCellAvailable(row, col) && CanSetPlayer(row, col))
            {
                Game.matrixMap[row, col] = (int)ObjectType.Player;
                gameObject.transform.position = new Vector3(col, 0, row);
                currentCountOfObject++;
            }
        }

    }

    private bool IsCellAvailable(int row, int col)
    {
        return Game.matrixMap[row, col] == (int)ObjectType.Empty && Game.matrixMap[row, col] != (int)ObjectType.Player;
    }

    private bool CanSetPlayer(int row, int col)
    {
        bool emptyUp = Game.matrixMap[row + 1, col] == (int)ObjectType.Empty;
        bool emptyRight = Game.matrixMap[row, col + 1] == (int)ObjectType.Empty;
        bool emptyDown = Game.matrixMap[row - 1, col] == (int)ObjectType.Empty;
        bool emptyLeft = Game.matrixMap[row, col - 1] == (int)ObjectType.Empty;

        return (emptyUp && emptyRight) || (emptyRight && emptyDown) || (emptyDown && emptyLeft) || (emptyLeft && emptyUp);
    }

}
