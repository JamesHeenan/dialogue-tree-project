using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TestDirectionTrigger : DialogueReader
{
    [SerializeField] TextMeshProUGUI TextBox;
    public Dialogue dialogue;
    [SerializeField] GameObject LocationObject;
    [SerializeField] GameObject CharacterObject;
    List<GameObject> CharacterObjects;
    [SerializeField] Vector2[] CharacterPositions;

    float continuePressed;

    void Start()
    {
        CharacterObjects = new List<GameObject>();
        StartDialogue(dialogue);

    }
    void Update() 
    {
        TextBox.text = outputText; 
    }

    public override void InstantiateScene()
    {
        Instantiate(LocationObject);
        for (int i = 0; i < 4; i++)
        {
            GameObject tmp = Instantiate(CharacterObject);
            tmp.name = i.ToString();
            CharacterObjects.Add(tmp);
        }       
    }
    public override void RefreshScene()
    {
        LocationObject.GetComponent<SpriteRenderer>().sprite = location.Sprite;
        LocationObject.GetComponent<AudioSource>();
        for (int i = 0; i < 4; i++)
        {
            if(i < charactersInScene.Length)
            {
                if(charactersInScene[i].Talking == true)
                {
                    LocationObject.GetComponent<AudioSource>().clip = charactersInScene[i].Character.SoundFont;
                    outputName = charactersInScene[i].Name;
                    baseTextSpeed = charactersInScene[i].Character.TalkSpeed;
                    textSpeed = baseTextSpeed;
                }
                CharacterObjects[i].GetComponent<Transform>().position = CharacterPositions[i];
                CharacterObjects[i].SetActive(true);
                CharacterObjects[i].GetComponent<SpriteRenderer>().sprite = charactersInScene[i].GetEmotion();
            }
            else CharacterObjects[i].SetActive(false);
        }
    }

    public override void PlaySoundFont()
    {
        LocationObject.GetComponent<AudioSource>().Play();
    }

    public override IEnumerator SkipTextAnimation()
    {
        if(continuePressed > 0)
        {
            textSpeed = 0.25f * baseTextSpeed;
        }
        else
        {
            textSpeed = baseTextSpeed;
        }
        yield return null;
    }
    public override IEnumerator Continue()
    {
        while(continuePressed < 0)
        {        
            yield return null;
        }   
        StopCoroutine(Continue());
    }
}
