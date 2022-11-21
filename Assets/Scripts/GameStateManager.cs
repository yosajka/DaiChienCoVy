using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;
    private void Awake(){
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    [System.Serializable] public class ShipItem{
        public Sprite Image;
		public int Price;
		public bool IsPurchased = false;
    }
    [System.Serializable] public class ShipAvatar
	{
		public Sprite Image;
	}
    public List<ShipItem> ShipList;
    public int Coins;
    public int selectedShipIndex=0;
    public List<ShipAvatar> ShipAvatarList= new List<ShipAvatar>();
    public void Start(){
        SceneManager.LoadScene("Menu");
    }
    public void AddShip(Sprite img){
        ShipAvatar av = new ShipAvatar (){ Image = img };
	    ShipAvatarList.Add (av);
    }
    public int GetSelectedSkinIndex(){
        return selectedShipIndex;
    }
}
