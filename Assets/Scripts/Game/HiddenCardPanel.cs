using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class HiddenCardPanel : MonoBehaviour
{
    public TMP_InputField inputText;
    public CardDetailSO cardDetail;
    public GameObject PenaltyPanel;
    public GameObject silangButton;
    public GameObject warning;
    public GameObject map;
    MapCardPanel cardPanel;
    private void Awake()
    {
        cardPanel = map.GetComponent<MapCardPanel>();
    }

    private void OnEnable()
    {
        GameManager.Instance.activePanel = ActivePanel.hidden;
    }
    public void removeCardFromHolder()
    {
        silangButton.SetActive(false);
        GameManager.Instance.selectedCardHidden = null;
        GameManager.Instance.hiddenCardImageSelected.GetComponent<Image>().sprite = GameManager.Instance.cardHolder;
    }

    public void HiddenCardSubmit()
    {
        if (GameManager.Instance.selectedCardHidden == null)
        {
            warning.SetActive(true);
            return;
        }
        else if (GameManager.Instance.selectedCardHidden.hiddenCardID == inputText.text && inputText.text != "0" && inputText.text != "")
        {
            if (CardSpawner.instance.GetCardByID(inputText.text, CardSpawner.instance.spawnRoots) != null)
            {
                Debug.Log("Udah pernah kebuka");
                return;
            }

            Debug.Log("Ketemu hiddennya");
            var generatedCard = Instantiate(GameResource.Instance.card, GameManager.Instance.cardListHolder.transform);
            generatedCard.transform.GetComponent<Card>().cardDetail = GameManager.Instance.GetCardDetailByID(GameManager.Instance.selectedCardHidden.hiddenCardID);
            generatedCard.transform.GetComponent<Image>().sprite = generatedCard.GetComponent<Card>().cardDetail.cardSprite;
            generatedCard.transform.GetComponent<Card>().panelCard = GameManager.Instance.cardDetailPanel;
            generatedCard.transform.GetComponent<Card>().imageDetail = GameManager.Instance.detailImageCard;

            generatedCard.transform.GetComponent<CardChoice>().cardDetail = GameManager.Instance.GetCardDetailByID(GameManager.Instance.selectedCardHidden.hiddenCardID);
            inputText.text = "";
            Player.instance.AddCards(generatedCard);

            if (generatedCard.GetComponent<Card>().cardDetail.cardType == CardType.map)
            {
                cardPanel.changepanel();
            }

            Destroy(generatedCard);

            //Misal terunlock, maka kartu akan hilang
            silangButton.SetActive(false);
            //Destroy(GameManager.Instance.GetCardByID(GameManager.Instance.selectedCardUnlock.cardID));
            Player.instance.DiscardCards(GameManager.Instance.selectedCardHidden.cardID);
            GameManager.Instance.selectedCardHidden = null;
            GameManager.Instance.hiddenCardImageSelected.GetComponent<Image>().sprite = GameManager.Instance.cardHolder;
        }
        else
        {
            warning.SetActive(false);
            PenaltyPanel.SetActive(true);
            Debug.Log("Salah");
            GameManager.Instance.player.getPenalty(180);
        }
    }

    public void SelectCardChoice()
    {
        GameManager.Instance.panelChoiceCard.SetActive(true);
    }

    public void OpenPanelHidden()
    {
        GameManager.Instance.CloseAllPanel();
        this.gameObject.SetActive(true);
    }

    public void OnDisable()
    {
        GameManager.Instance.warningHidden.SetActive(false);
        this.removeCardFromHolder();
        inputText.text = "";
    }
    private void Update(){
        if(GameManager.Instance.selectedCardHidden != null){
            silangButton.SetActive(true);
        }
    }
}
