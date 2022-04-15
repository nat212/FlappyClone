using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Scene : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float moveSpeedIncreaseFactor = 0.01f;
    [SerializeField] GameObject pipePrefab;
    [SerializeField] float pipeSpawnInterval = 20f;
    [SerializeField] float pipeMinY = -1f;
    [SerializeField] float pipeMaxY = 1f;
    [SerializeField] GameObject player;
    private float screenWidth;
    private List<GameObject> pipes;

    public bool paused;

    private Vector2 savedPlayerVelocity;
    // Start is called before the first frame update
    void Start()
    {
        paused = true;
        pipes = new List<GameObject>();
        screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
        for (float f = pipeSpawnInterval; f <= screenWidth * 4; f += pipeSpawnInterval)
        {
            CreatePipe(f);
        }
    }

    public void Resume()
    {
        this.paused = false;
        player.GetComponent<PlayerScript>().Resume();
    }

    public void Pause()
    {
        this.paused = true;
        player.GetComponent<PlayerScript>().Pause();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Quit")) {
            Application.Quit();
        }
        if (Input.GetButton("Cancel"))
        {
            this.Pause();
        }
        List<GameObject> pipesToRemove = new List<GameObject>();
        if (!player.GetComponent<PlayerScript>().dead && !paused)
        {
            foreach (GameObject pipe in pipes)
            {
                pipe.transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
                if ((pipe.transform.position.x + pipe.GetComponentInChildren<SpriteRenderer>().size.x) <= -screenWidth)
                {
                    pipesToRemove.Add(pipe);
                }
            }
            if (pipesToRemove.Count > 0)
            {
                AddPipeToEnd();
            }
            foreach (GameObject pipe in pipesToRemove)
            {
                pipes.Remove(pipe);
                Destroy(pipe);
            }
            moveSpeed += moveSpeedIncreaseFactor * Time.deltaTime;
        }

    }

    void AddPipeToEnd()
    {
        float pipeX = GetLastPipe().transform.position.x + pipeSpawnInterval;
        CreatePipe(pipeX);
    }

    GameObject GetLastPipe()
    {
        GameObject pipe = null;
        foreach (GameObject p in pipes)
        {
            if (!pipe)
            {
                pipe = p;
            }
            else
            {
                if (p.transform.position.x > pipe.transform.position.x)
                {
                    pipe = p;
                }
            }
        }
        return pipe;
    }

    GameObject CreatePipe(float xPos)
    {
        float yPos = Random.Range(pipeMinY, pipeMaxY);
        GameObject pipe = (GameObject)Instantiate(pipePrefab, new Vector2(xPos, yPos), Quaternion.identity);
        pipe.transform.parent = transform;
        pipes.Add(pipe);
        return pipe;
    }
}
