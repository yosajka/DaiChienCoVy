using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
	GameObject g;
	[SerializeField] Transform ShopScrollView;
	Button buyBtn;
	[SerializeField] Animator NoCoinsAnim;
    public Text CoinText;
    [SerializeField] GameObject ShipTemplate;
    [SerializeField] GameObject ShipUITemplate;
	[SerializeField] Transform ShipScrollView;
    int newSelectedIndex, previousSelectedIndex;
	[SerializeField] Color ActiveShipColor;
	[SerializeField] Color DefaultShipColor;
	[SerializeField] Image CurrentShip;
    public void Start(){
		GameStateManager.Instance.ShipAvatarList.Clear();
        CoinText.text = GameStateManager.Instance.Coins.ToString();
        int len = GameStateManager.Instance.ShipList.Count;
        for (int i = 0; i < len; i++)
		{
            g = Instantiate(ShipTemplate, ShopScrollView);
			g.transform.GetChild(0).GetComponent<Image>().sprite = GameStateManager.Instance.ShipList[i].Image;
			g.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = GameStateManager.Instance.ShipList[i].Price.ToString();
			buyBtn = g.transform.GetChild(2).GetComponent<Button>();
			if (GameStateManager.Instance.ShipList[i].IsPurchased)
			{
				DisableBuyButton();
			}
			buyBtn.AddEventListener(i, OnShopItemBtnClicked);
        }
		newSelectedIndex = GameStateManager.Instance.selectedShipIndex;
        previousSelectedIndex = GameStateManager.Instance.selectedShipIndex;
        GetAvailableAvatars();
		
    }
    void GetAvailableAvatars ()
	{
		for (int i = 0; i < GameStateManager.Instance.ShipList.Count; i++) {
			if ( GameStateManager.Instance.ShipList[i].IsPurchased) {
				//add all purchased avatars to AvatarsList
				AddShip(GameStateManager.Instance.ShipList[i].Image, true);
                
			}
		}
		SelectShip(newSelectedIndex);
	}
    public void AddShip(Sprite img, bool isRestartShop)
	{
		GameStateManager.Instance.AddShip(img);
		//add avatar in the UI scroll view
		g = Instantiate (ShipUITemplate, ShipScrollView);
		g.transform.GetChild (0).GetComponent <Image> ().sprite = img;
		//add click event
		g.transform.GetComponent <Button> ().AddEventListener (GameStateManager.Instance.ShipAvatarList.Count - 1, OnShipClick);
		
	}
    void OnShipClick (int ShipIndex)
	{
		SelectShip (ShipIndex);
	}

	void SelectShip (int AvatarIndex)
	{
		previousSelectedIndex = newSelectedIndex;
		newSelectedIndex = AvatarIndex;
        
		ShipScrollView.GetChild (previousSelectedIndex).GetComponent <Image> ().color = DefaultShipColor;
		ShipScrollView.GetChild (newSelectedIndex).GetComponent <Image> ().color = ActiveShipColor;

		CurrentShip.sprite = GameStateManager.Instance.ShipAvatarList[newSelectedIndex].Image;
	}


    void DisableBuyButton()
	{
		buyBtn.interactable = false;
		buyBtn.transform.GetChild(0).GetComponent<Text>().text = "PURCHASED";
	}
	void OnShopItemBtnClicked(int itemIndex)
	{
		if (HasEnoughCoins(GameStateManager.Instance.ShipList[itemIndex].Price))
		{
			UseCoins(GameStateManager.Instance.ShipList[itemIndex].Price);
			//purchase Item
			GameStateManager.Instance.ShipList[itemIndex].IsPurchased = true;

			//disable the button
			buyBtn = ShopScrollView.GetChild(itemIndex).GetChild(2).GetComponent<Button>();
			DisableBuyButton();
			//change UI text: coins
			CoinText.text = GameStateManager.Instance.Coins.ToString();
            AddShip(GameStateManager.Instance.ShipList[itemIndex].Image, false);
		}
		else
		{
			NoCoinsAnim.SetTrigger("NotEnoughCoinTrigger");
		}
	}
    bool HasEnoughCoins(int price){
        if (GameStateManager.Instance.Coins>= price)
            return true;
        return false;
    }
    void UseCoins(int price){
        GameStateManager.Instance.Coins -= price;
    }

    public void CloseShop(){
		GameStateManager.Instance.selectedShipIndex = newSelectedIndex;
        SceneManager.LoadScene("Menu");
    }
}
