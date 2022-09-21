using System.Collections;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosePanel : MonoBehaviour
{
    private string uri = "http://localhost/gamedevDB/finish.php";

    private void OnEnable()
    {
        GameManager.Instance.audioManager.GetComponent<SoundManager>().loseSoundPlay();
    }

    public void ToMainMenu()
    {
        DBManager.remaining_coins = 0;
        DBManager.isWin = false;
        StartCoroutine(PostLose());
    }

    IEnumerator PostLose()
    {
        WWWForm form = new();

        form.AddField("team_name", DBManager.team_name);
        form.AddField("account_id", DBManager.account_id);
        form.AddField("player_id", DBManager.player_id);
        form.AddField("remaining_coins", DBManager.remaining_coins);
        form.AddField("remaining_hours", DBManager.remaining_hours.ToString());
        form.AddField("discardCardsCount", DBManager.discardCardsCount);
        form.AddField("scores", DBManager.scores);
        form.AddField("mapID", DBManager.mapID);
        if (DBManager.isWin)
            form.AddField("isWin", 1);
        else
            form.AddField("isWin", 0);
        string ownedCards = "";
        form.AddField("ownedCards", ownedCards);

        using (UnityWebRequest webRequest = UnityWebRequest.Post(uri, form))
        {
            webRequest.SetRequestHeader("Access-Control-Allow-Origin", "*");
            yield return webRequest.SendWebRequest();
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                webRequest.Dispose();
                SceneManager.LoadScene("Main Menu");
            }
        }
    }
}
