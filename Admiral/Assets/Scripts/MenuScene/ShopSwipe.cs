
using UnityEngine;
using UnityEngine.UI;

public class ShopSwipe : MonoBehaviour
{
    [SerializeField]
    GameObject scrollBar;
    [SerializeField]
    GameObject ShopPanel;
    Scrollbar scrollBarUI;

    float scroll_pos = 0;

    float[] pos;
    float distance;
    int currentPosIndex;

    // Start is called before the first frame update
    void Start()
    {
        scrollBarUI = scrollBar.GetComponent<Scrollbar>();
        pos = new float[transform.childCount];
        distance = 1f / (pos.Length - 1);
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }
        currentPosIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (ShopPanel.transform.localPosition.x != -30000) //this code will operate only if shop panel is active
        {
            for (int i = 0; i < pos.Length; i++)
            {
                //pos[i] = distance * i;
                if (scrollBarUI.value < (pos[i] + 0.03f) && scrollBarUI.value > (pos[i] - 0.03f))
                {
                    if (!Input.GetMouseButton(0))
                    {
                        if (currentPosIndex != i)
                        {
                            scrollBarUI.value = Mathf.Lerp(scrollBarUI.value, pos[i], 0.1f);
                            currentPosIndex = i;
                        }
                        else
                        {
                            scrollBarUI.value = Mathf.Lerp(scrollBarUI.value, pos[currentPosIndex], 0.1f);
                        }
                    }
                    Transform levelTab = transform.GetChild(i);
                    

                    levelTab.localScale = Vector2.Lerp(levelTab.localScale, new Vector2(1f, 1f), 0.1f);
                    for (int a = 0; a < pos.Length; a++)
                    {
                        if (a != i)
                        {
                            Transform levelTabA = transform.GetChild(a);
                            levelTabA.localScale = Vector2.Lerp(levelTabA.localScale, new Vector2(0.3f, 0.3f), 0.1f);
                        }
                    }
                }


            }

            //for (int i = 0; i < pos.Length; i++)
            //{
            //    pos[i] = distance * i;
            //}
            //if (Input.GetMouseButton(0))
            //{
            //    scroll_pos = scrollBarUI.value;
            //}
            //else
            //{
            //    for (int i = 0; i < pos.Length; i++)
            //    {
            //        if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
            //            scrollBarUI.value = Mathf.Lerp(scrollBarUI.value, pos[i], 0.1f);
            //    }
            //}
            //for (int i = 0; i < pos.Length; i++)
            //{
            //    if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
            //    {
            //        Transform levelTab = transform.GetChild(i);
            //        levelTab.localScale = Vector2.Lerp(levelTab.localScale, new Vector2(1f, 1f), 0.1f);
            //        for (int a = 0; a < pos.Length; a++)
            //        {
            //            if (a != i)
            //            {
            //                Transform levelTabA = transform.GetChild(a);
            //                levelTabA.localScale = Vector2.Lerp(levelTabA.localScale, new Vector2(0.5f, 0.5f), 0.1f);
            //            }
            //        }
            //    }
            //}
        }
    }
}
