using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HUDScript : MonoBehaviour
{
    [SerializeField] GameObject player;

    private VisualElement hud;
    private Label scoreLabel;
    // Start is called before the first frame update
    void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();
        scoreLabel = uiDocument.rootVisualElement.Q<Label>("score");
    }

    // Update is called once per frame
    void Update()
    {
        scoreLabel.text = player.GetComponent<PlayerScript>().score.ToString();
    }
}
