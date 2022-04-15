using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] GameObject scene;

    private Scene sceneScript;

    private UIDocument uiDocument;
    // Start is called before the first frame update
    void Start()
    {
        sceneScript = scene.GetComponent<Scene>();
        uiDocument = GetComponent<UIDocument>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sceneScript.paused)
        {
            uiDocument.enabled = true;
            if (Input.GetButton("Jump"))
            {
                sceneScript.Resume();
                uiDocument.enabled = false;
            }
        }
        else
        {
            uiDocument.enabled = false;
        }
    }
}
