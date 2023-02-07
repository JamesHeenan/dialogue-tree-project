using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TestDirectionTrigger : MonoBehaviour
{
    DialogueReader d;
    string text;
    public TextMeshProUGUI textBox;
    public Direction direction;
    GameObject location;
    public GameObject character;

    // Start is called before the first frame update
    void Start()
    {
        d = GetComponent<DialogueReader>();
        location = GameObject.Find("Location");
        d.PlayDirection(direction);
    }
    void Update() 
    {
        textBox.text = d.outputText; 

        //Check on flags
        location.GetComponent<SpriteRenderer>().sprite = d.location.Sprite;
        foreach(ActiveCharacter character in d.charactersInScene)
        {
            GameObject tmp = Instantiate(this.character);
            tmp.name = character.Name;
            tmp.GetComponent<SpriteRenderer>().sprite = character.GetEmotion();
        }
    }
}
