
using UnityEngine.EventSystems;
using UnityEngine;

public class PutReloadPartBack : MonoBehaviour, IPointerDownHandler
{
    private Vector3 reloadPartStartPos;
    private bool isTransforming = false;
    private void Start()
    {
        reloadPartStartPos = transform.localPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (GunShotButt.onTriggerMode) {
            isTransforming = true;
            
            
        }
    }

    private void Update()
    {
        if (isTransforming) {
            transform.localPosition = Vector3.Lerp(transform.localPosition, reloadPartStartPos, 0.3f);
            if (transform.localPosition == reloadPartStartPos)
            {
                isTransforming = false;
                GunShotButt.wholeReloadIsOver = true;
                GunShotButt.onTriggerMode = false;
            }
        }
    }
}
