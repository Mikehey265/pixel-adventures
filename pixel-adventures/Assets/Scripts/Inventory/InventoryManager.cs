using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryManager : Singleton<InventoryManager>
{
    #region Public Variables

    public enum CurrentEquippedItem {Boomerang, Bomb};
    public CurrentEquippedItem currentEquippedItem;
    public GameObject itemEquippedInventory;
    public GameObject currentSelectedItem;
    public GameObject inventoryContainer;
        
    #endregion

    #region Private Variables

    [SerializeField] GameObject selectionBorder;
    [SerializeField] EventSystem eventSystem;
    [SerializeField] Image activeSpriteUI;
    PlayerControls playerControls;
    const string boomerang = "Boomerang";
    const string bomb = "Bomb";
        
    #endregion

    #region Unity Methods

    protected override void Awake(){
        base.Awake();
        playerControls = new PlayerControls();
    }

    private void Start() {
        playerControls.Inventory.OpenInventory.performed += _ => OpenInventoryContainer();
        DetectIfItemChange();
    }

    private void Update() {
        if(inventoryContainer.activeInHierarchy == true){
            DetectIfItemChange();
        }
    }

    private void OnEnable() {
        playerControls.Enable();
    }

    private void OnDisable() {
        if(playerControls != null){
            playerControls.Disable();
        }
    }
        
    #endregion

    #region Public Methods

    public void SetEventSystem(EventSystem newEventSystem){
        eventSystem = newEventSystem;
    }
        
    #endregion

    #region Private Methods

    private void OpenInventoryContainer(){
        if(inventoryContainer.gameObject.activeInHierarchy == false){
            inventoryContainer.gameObject.SetActive(true);
            PlayerController.Instance.PauseGame();
        }

        else if(inventoryContainer.gameObject.activeInHierarchy == true){
            inventoryContainer.gameObject.SetActive(false);
            PlayerController.Instance.ResumeGame();
        }
    }

    private void DetectIfItemChange(){
        if(currentSelectedItem != eventSystem.currentSelectedGameObject || currentSelectedItem == null){
            currentSelectedItem = eventSystem.currentSelectedGameObject;
            selectionBorder.transform.position = currentSelectedItem.transform.position;
            activeSpriteUI.sprite = currentSelectedItem.GetComponent<Image>().sprite;
            ChangeCurrentEquippedItem();
        }
    }

    private void UpdateSelectedBorder(){
        selectionBorder.transform.position = currentSelectedItem.transform.position;
    }

    private void ChangeCurrentEquippedItem(){
        ItemDisplay thisItem = currentSelectedItem.GetComponent<ItemDisplay>();

        if(thisItem){
            if(thisItem.item.itemType == boomerang){
                currentEquippedItem = CurrentEquippedItem.Boomerang;
            }else if(thisItem.item.itemType == bomb){
                currentEquippedItem = CurrentEquippedItem.Bomb;
            }
            itemEquippedInventory = thisItem.item.useItemPrefab;
        }else {
            itemEquippedInventory = null;
        }
    }
        
    #endregion
}
