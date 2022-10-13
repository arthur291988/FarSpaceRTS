
using UnityEngine;
using UnityEngine.UI;

public class SwipeMenu : MonoBehaviour
{
    [SerializeField]
    GameObject scrollBar;
    Scrollbar scrollBarUI;

    [SerializeField]
    RawImage backgrundSpace;

    [SerializeField]
    Texture darkSpace;
    [SerializeField]
    Texture blueSpace;
    [SerializeField]
    Texture redSpace;

    //float scroll_pos = 0;

    float[] pos;
    float distance;
    int currentPosIndex;

    private void Awake()
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

    // Start is called before the first frame update
    //void Start()
    //{
    //    scrollBarUI = scrollBar.GetComponent<Scrollbar>();
    //    pos = new float[transform.childCount];
    //    distance = 1f / (pos.Length-1);
    //    for (int i = 0; i < pos.Length; i++)
    //    {
    //        pos[i] = distance * i;
    //    }
    //    currentPosIndex = 0;

    //}

    public void setSavedLevel(int lvl)
    {
        if (currentPosIndex != lvl)
        {
            scrollBarUI.value = pos[lvl]; //Mathf.Lerp(scrollBarUI.value, pos[lvl], 0.1f);
            currentPosIndex = lvl;
        }
    }


    // Update is called once per frame
    void Update()
    {

        //if (Input.GetMouseButton(0))
        //{
        //    scroll_pos = scrollBarUI.value;
        //}

        //scroll_pos = scrollBarUI.value;
        //for (int i = 0; i < pos.Length; i++)
        //{
        //    pos[i] = distance * i;
        //}
            
        //scroll_pos = scrollBarUI.value;



        //if (pos[currentPosIndex] < scroll_pos) {
        //    
        //}
        //else
        //{
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

                if (levelTab.gameObject.name == "Level1" || levelTab.gameObject.name == "Level2" ||
                    levelTab.gameObject.name == "Level3") backgrundSpace.texture = darkSpace;
                else if (levelTab.gameObject.name == "Level4" || levelTab.gameObject.name == "Level5" || levelTab.gameObject.name == "Level6" ||
                    levelTab.gameObject.name == "Level7") backgrundSpace.texture = blueSpace;
                else if (levelTab.gameObject.name == "Level8" || levelTab.gameObject.name == "Level9" || levelTab.gameObject.name == "Level01")
                    backgrundSpace.texture = redSpace;

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


        //else {
        //    for (int i = 0; i < pos.Length; i++)
        //    {
        //        if (scroll_pos < pos[i] +0.03f /*(distance / 2)*/ && scroll_pos > pos[i] - 0.03f/*(distance / 2)*/)
        //            scrollBarUI.value = Mathf.Lerp(scrollBarUI.value, pos[i], 0.1f);
        //    }
        //}
        //for (int i = 0; i < pos.Length; i++)
        //{
        //    if (scroll_pos < (pos[i]) && scroll_pos > (pos[i]))
        //    {
        //        Transform levelTab = transform.GetChild(i);

        //        if (levelTab.gameObject.name == "Level1" || levelTab.gameObject.name == "Level2" ||
        //            levelTab.gameObject.name == "Level3") backgrundSpace.texture = darkSpace;
        //        else if (levelTab.gameObject.name == "Level4" || levelTab.gameObject.name == "Level5" || levelTab.gameObject.name == "Level6" ||
        //            levelTab.gameObject.name == "Level7") backgrundSpace.texture = blueSpace;
        //        else if (levelTab.gameObject.name == "Level8" || levelTab.gameObject.name == "Level9" || levelTab.gameObject.name == "Level10")
        //            backgrundSpace.texture = redSpace;

        //        levelTab.localScale = Vector2.Lerp(levelTab.localScale, new Vector2(1f, 1f), 0.1f);
        //        for (int a = 0; a < pos.Length; a++)
        //        {
        //            if (a != i)
        //            {
        //                Transform levelTabA = transform.GetChild(a);
        //                levelTabA.localScale = Vector2.Lerp(levelTabA.localScale, new Vector2(0.3f, 0.3f), 0.1f);
        //            }
        //        }
        //    }
        //}
    }
}
