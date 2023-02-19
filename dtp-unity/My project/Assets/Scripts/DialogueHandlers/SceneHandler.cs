using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SceneHandler : MonoBehaviour
{
    public Color notTalking;
    public GameObject locationObject;
    public GameObject overlay;
    public GameObject characterObjectPrefab;
    List<GameObject> characterObjects;
    public TextMeshProUGUI textObject;
    public string log;
    public Scrollbar scrollbar;
    public void RefreshScene(DirectionBase direction)
    {
        GameObject tmp = null;
        locationObject.GetComponent<SpriteRenderer>().sprite = direction.Location.Sprite;
        locationObject.GetComponent<AudioSource>().clip = direction.GetTalking().Character.SoundFont;
        for (int i = 0; i < direction.Characters.Length; i++)
        {
            if(i+1 > characterObjects.Count)
            {
                tmp = Instantiate(characterObjectPrefab);
                characterObjects.Add(tmp);
            }
            if(!direction.Characters[i].Talking) 
            {
                characterObjects[i].GetComponent<SpriteRenderer>().color = notTalking;
                characterObjects[i].GetComponent<Transform>().localScale = new Vector3(0.9f,0.9f,0);
            }
            else 
            {
                characterObjects[i].GetComponent<SpriteRenderer>().color = Color.white;
                characterObjects[i].GetComponent<Transform>().localScale = new Vector3(1f,1f,0);
            }
            if(direction.Characters[i].Flipped) characterObjects[i].GetComponent<SpriteRenderer>().flipX = true;
            else characterObjects[i].GetComponent<SpriteRenderer>().flipX = false;
            characterObjects[i].GetComponent<SpriteRenderer>().sprite = direction.Characters[i].GetEmotion();
            characterObjects[i].GetComponent<Transform>().position = direction.Characters[i].Position;

        }
        for (int i = direction.Characters.Length; i < characterObjects.Count; i++)
        {
            tmp = characterObjects[i];
            characterObjects.Remove(tmp);
            Destroy(tmp);
        }

    }
    public void RefreshText(string input)
    {
        textObject.text = input;
        textObject.text += "\n\n";
        MoveScrollbarTo(0);
    }
    public void MoveScrollbarTo(int input)
    {
        scrollbar.interactable = false;
        scrollbar.value = input;
        scrollbar.interactable = true;
    }
    public void ResetScene()
    {
        locationObject.GetComponent<SpriteRenderer>().sprite = null;
        locationObject.GetComponent<AudioSource>().clip = null;
        characterObjects = new List<GameObject>();
    }
    public void PlaySoundFont()
    {
        locationObject.GetComponent<AudioSource>().Play();
    }
    public void AppendToLog(string input)
    {
        log += input;
    }

    public IEnumerator FadeToBlack(float duration, float DurationDelay, Action action)
    {
        Color tmp = overlay.GetComponent<SpriteRenderer>().color;
        tmp.a = 0; //set aplha to 0
        overlay.GetComponent<SpriteRenderer>().color = tmp;
        overlay.SetActive(true);
        for (int i = 0; i <= duration*100; i++)
        {
            //increase alpha by 1/duration*10
            tmp.a += 1/(duration*100f);
            overlay.GetComponent<SpriteRenderer>().color = tmp;
            yield return new WaitForSecondsRealtime(0.01f);   
        }
        yield return new WaitForSecondsRealtime(DurationDelay);
        action();
    }
    public IEnumerator FadeInToScene(float DurationOfTransition, float DurationDelay, Action action)
    {
        Color tmp = overlay.GetComponent<SpriteRenderer>().color;
        tmp.a = 1; //set aplha to 0
        overlay.GetComponent<SpriteRenderer>().color = tmp;
        for (int i = 0; i <= DurationOfTransition*100; i++)
        {
            //increase alpha by 1/duration*10
            tmp.a -= 1/(DurationOfTransition*100f);
            overlay.GetComponent<SpriteRenderer>().color = tmp;
            yield return new WaitForSecondsRealtime(0.01f);   
        }
        overlay.SetActive(false);
        yield return new WaitForSecondsRealtime(DurationDelay);
        action();       
    }
}
