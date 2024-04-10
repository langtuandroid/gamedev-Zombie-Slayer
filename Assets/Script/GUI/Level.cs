/// <summary>
/// The UI Level, check the current level
/// </summary>
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public int world = 1;
    public int level = 1;
    public bool isUnlock = false;
    public Text numberTxt;
    public GameObject lockContainer, openingContainer, passedContainer;

    void Start()
    {
        
        var openLevel = isUnlock ? true : GlobalValueZS.LevelPass + 1 >= level;

        lockContainer.SetActive(false);
        openingContainer.SetActive(false);
        passedContainer.SetActive(false);

        numberTxt.text = level + "";
        if (openLevel)
        {
          
            if (GlobalValueZS.LevelPass + 1 == level)
            {
                openingContainer.SetActive(true);
                FindObjectOfType<MapControllerUI>().SetCurrentWorld(world);
            }else
                passedContainer.SetActive(true);

        }
        else
        {
            lockContainer.SetActive(true);
        }

        GetComponent<Button>().interactable = openLevel;
    }

    public void Play()
    {
        GlobalValueZS.LevelPlaying = level;
        SoundManager.Click();
        MainMenuHomeScene.Instance.ShowChooseWeapon(true);
    }
    private void OnDrawGizmosSelected()
    {
        gameObject.name = "Level " + level;
    }
}
