using UnityEngine;

public class EssentialsLoader : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject cameraController;
    [SerializeField] GameObject audioManager;

    private void Start() {
        if(PlayerController.Instance == null){
            PlayerController clone = Instantiate(player).GetComponent<PlayerController>();

            if(FindObjectOfType<PlayerRespawn>()){
                clone.transform.position = FindObjectOfType<PlayerRespawn>().respawnPoint.transform.position;
            }else{
                clone.transform.position = FindObjectOfType<AreaEntrance>().transform.position;
            }
        }

        if(CameraController.Instance == null){
            Instantiate(cameraController).GetComponent<CameraController>();
        }

        if(AudioManager.Instance == null){
            Instantiate(audioManager).GetComponent<AudioManager>();
        }
    }
}
