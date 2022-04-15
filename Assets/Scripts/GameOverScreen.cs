using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] GameObject player;
    private PlayerScript playerScript;
    private UIDocument uiDocument;
    // Start is called before the first frame update
    void Start()
    {
        uiDocument = GetComponent<UIDocument>();
        uiDocument.enabled = false;
        playerScript = player.GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.dead)
        {
            uiDocument.enabled = true;
            if (Input.GetButton("Jump"))
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
