using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton : MonoBehaviour
{
    [SerializeField] GameObject gameObject;
    [SerializeField] GameObject audioManager;

    // Start is called before the first frame update
    public void DeactivePanel(bool isMainMenu)
    {
        if (isMainMenu)
            audioManager.GetComponent<SoundManager>().clickSoundPlay();
        else
            GameManager.Instance.audioManager.GetComponent<SoundManager>().clickSoundPlay();

        if (gameObject.activeInHierarchy)
            this.gameObject.SetActive(false);
    }
}
