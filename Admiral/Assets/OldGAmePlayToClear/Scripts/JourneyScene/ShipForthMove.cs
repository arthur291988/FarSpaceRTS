 
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShipForthMove : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    //public ParticleSystem starsSecond;
    private GameObject cameraMain;
    Color startColor;

    private void Start()
    {
        //startColor = GetComponent<RawImage>().color;
        //cameraMain = Camera.main.gameObject;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ShipController.isMovePushed = true;
        //SpaceCtrlr.Instance.EngineSound.Play();
        //GetComponent<RawImage>().color = new Color(0, 1, 1, 0.3f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ShipController.isMovePushed = false;
        //SpaceCtrlr.Instance.EngineSound.Stop();
        //GetComponent<RawImage>().color = startColor;
    }

    //private void Update()
    //{
    //    //if (cameraMain) starsSecond.transform.position = cameraMain.transform.position;


        
    //}

}
