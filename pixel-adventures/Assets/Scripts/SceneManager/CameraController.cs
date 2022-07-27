using Cinemachine;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    #region Private Variables

    [SerializeField] Transform player;
    [SerializeField] CinemachineVirtualCamera vCam;

    #endregion

    #region Unity Methods

    protected override void Awake()
    {
        base.Awake();
        vCam = FindObjectOfType<CinemachineVirtualCamera>();
    }

    private void Start() {
        if(FindObjectOfType<PlayerController>()){
            player = PlayerController.Instance.transform;
            vCam.LookAt = player;
            vCam.Follow = player;
        }
    }

    private void Update() {
        FindPlayer();
    }
        
    #endregion

    #region Private Methods

    private void FindPlayer(){
        if(player == null){
            player = PlayerController.Instance.transform;
            vCam.LookAt = player;
            vCam.Follow = player;
        }
    }
        
    #endregion
}
