
using UnityEngine;
//using UnityEngine.SceneManagement;


public class SceneSwitchMngr : MonoBehaviour
{
    public static void LoadMenuScene()
    {
        Loader.Load(0);
    }
    public static void LoadJourneyScene()
    {
        Loader.Load(1);
    }

    public static void LoadBattleScene() {
        Loader.Load(2);
    }

    public static void LoadDefenceScene()
    {
        Loader.Load(3);
    }

}
