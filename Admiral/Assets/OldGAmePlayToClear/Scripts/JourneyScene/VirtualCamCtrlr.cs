
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class VirtualCamCtrlr : MonoBehaviour
{
    CinemachineVirtualCamera playerCamera;

    //virtual camera that used as default camera on scene of defence and using cinemachine features to translate to action virtual camera after the gun is assigned
    CinemachineVirtualCamera defaultCamera;

    //virtual camera that used as second higher camera on journey scene to show to player whole map of scene from the highs
    CinemachineVirtualCamera highCamera;

    //virtual camera that used as default camera on scene of defence and using cinemachine features to translate to action virtual camera after the gun is assigned
    public GameObject virtualCamera2;

    //virtual camera that used as second higher camera on journey scene to show to player whole map of scene from the highs
    public GameObject virtualCamera3;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = GetComponent<CinemachineVirtualCamera>();
        /*if (SceneManager.GetActiveScene().buildIndex==3) */defaultCamera = virtualCamera2.GetComponent<CinemachineVirtualCamera>();
        /*if (SceneManager.GetActiveScene().buildIndex == 0)*/ highCamera = virtualCamera3.GetComponent<CinemachineVirtualCamera>();
    }

    //this method is used to set a journey cruiser of player to follow for this virtual camera from SpaceCrtlr class while instantiating a ship on scene
    //public void getThePlayerInstaceToFollow(LaunchingObjcts playerShip) {
    //    playerCamera.Follow = playerShip.transform;
    //    playerCamera.LookAt = playerShip.transform;

    //    //highCamera.Follow = playerShip.transform;
    //    //highCamera.LookAt = playerShip.transform;
    //}

    //this method is used to change the view of camera on journey scene player cruiser (higher or lower)
    //public void changeTheCameraViewOnJourneyCruiser() {
    //    if (playerCamera.Priority == 2)
    //    {
    //        playerCamera.Priority = 1;
    //        highCamera.Priority = 2;
    //    }
    //    else
    //    {
    //        playerCamera.Priority = 2;
    //        highCamera.Priority = 1;
    //    }
    //}

    //this method is used to set a defence gun barrel of player to follow for this virtual camera from LauncheManager class of defence scene while instantiating a 
    //def barrel on scene
    public void getTheGunBarrelInstaceToFollow(GameObject gunBarrel)
    {
        //playerCamera.Follow = gunBarrel.transform;
        playerCamera.Follow = gunBarrel.GetComponent<DefBarrelCtrlr>().aimSprite.transform;
        playerCamera.LookAt = gunBarrel.GetComponent<DefBarrelCtrlr>().aimSprite.transform;


        highCamera.Follow = gunBarrel.GetComponent<DefBarrelCtrlr>().aimSprite.transform;
        highCamera.LookAt = gunBarrel.GetComponent<DefBarrelCtrlr>().aimSprite.transform;
        

        playerCamera.Priority = 2; 
        defaultCamera.Priority = 0;
        highCamera.Priority = 1;
    }

    //this method is used to change the view of camera on defence scene gun (higher or lower)
    public void changeTheCameraViewOnDefenceScene()
    {
        if (playerCamera.Priority == 1)
        {
            playerCamera.Priority = 2;
            highCamera.Priority = 1;
        }
        else
        {
            playerCamera.Priority = 1;
            highCamera.Priority = 2;
        }
    }

    //this method is used to change the view of camera on defence scene gun (to make it closer to gun)
    public void changeTheCameraViewOnDefenceSceneWhileReloadingGun()
    {
        playerCamera.Priority = 2;
        highCamera.Priority = 1;
    }
}
