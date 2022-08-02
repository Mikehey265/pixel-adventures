using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystemSpawner : MonoBehaviour
{
    [SerializeField] GameObject eventSystemPrefab;
    [SerializeField] public GameObject firstSelectedInventoryObject;

    private void Start() {
        EventSystem sceneEventSystem = FindObjectOfType<EventSystem>();
        if(sceneEventSystem == null){
            GameObject newEventSystem = Instantiate(eventSystemPrefab);
            newEventSystem.transform.parent = transform;
            newEventSystem.GetComponent<EventSystem>().SetSelectedGameObject(firstSelectedInventoryObject);
            FindObjectOfType<InventoryManager>().SetEventSystem(newEventSystem.GetComponent<EventSystem>());
        }
    }
}
