/// <summary>
/// The UI Level, check the current level
/// </summary>

using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace Script.GUI
{
    public class LevelZS : MonoBehaviour
    {
        [FormerlySerializedAs("world")] [SerializeField] private int worldD = 1;
        [FormerlySerializedAs("level")] [SerializeField] private int levelL = 1;
        [FormerlySerializedAs("isUnlock")] [SerializeField] private bool isUnlockK = false;
        [FormerlySerializedAs("numberTxt")] [SerializeField] private Text numberTxtT;
        [SerializeField] private GameObject lockContainer, openingContainer, passedContainer;

        [Inject] private MainMenuHomeSceneZS mainMenuHomeSceneZs;
        
        private void Start()
        {
            var openLevel = isUnlockK || GlobalValueZS.LevelPass + 1 >= levelL;

            lockContainer.SetActive(false);
            openingContainer.SetActive(false);
            passedContainer.SetActive(false);

            numberTxtT.text = levelL + "";
            if (openLevel)
            {
          
                if (GlobalValueZS.LevelPass + 1 == levelL)
                {
                    openingContainer.SetActive(true);
                    FindObjectOfType<MapControllerUI>().SetCurrentWorld(worldD);
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
            GlobalValueZS.LevelPlaying = levelL;
            SoundManagerZS.Click();
            mainMenuHomeSceneZs.ShowChooseWeapon(true);
        }
        private void OnDrawGizmosSelected()
        {
            gameObject.name = "Level " + levelL;
        }
    }
}
