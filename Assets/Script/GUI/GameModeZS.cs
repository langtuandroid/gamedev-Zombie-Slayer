using UnityEngine;

public class GameModeZS : MonoBehaviour
{
    public bool showTestOption = false;

    [Header("ITEM PRICE")]
    public int grenadePrice = 50;
    public int rocketPrice = 100;

    [Header("FPS DISPLAY")]
    [SerializeField] private bool showInfor = true;
    [HideInInspector]  public Vector2 resolution = new Vector2(1280, 720);
    [SerializeField] private int setFPS = 60;
    private float deltaTimeT = 0.0f;

    [SerializeField] private PurchaserZS purchase;

    public void BuyItem(int id)
    {
        switch (id)
        {
            case 1:
                purchase.BuyItem1();
                break;
            case 2:
                purchase.BuyItem2();
                break;
            case 3:
                purchase.BuyItem3();
                break;
            default:
                break;
        }
    }

    public void BuyRemoveAds()
    {
        purchase.BuyRemoveAds();
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        Application.targetFrameRate = setFPS;
    }

    #region FPS DISPLAY
    private void Update()
    {
        deltaTimeT += (Time.unscaledDeltaTime - deltaTimeT) * 0.1f;

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.U))
        {
            GlobalValueZS.LevelPass = 999;
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.R))
        {
            ResetData();
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.G))
        {
            GlobalValueZS.SavedCoins += 999999;
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.A))
        {
            GlobalValueZS.LevelPass = 999999;
        }
    }

    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    private void OnGUI()
    {
        if (showInfor)
        {
            int w = Screen.width, h = Screen.height;

            GUIStyle style = new GUIStyle();

            Rect rect = new Rect(0, 0, w, h * 2 / 100);
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = h * 2 / 100;
            style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
            float msec = deltaTimeT * 1000.0f;
            float fps = 1.0f / deltaTimeT;
            string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);


            GUI.Label(rect, text, style);

            Rect rect2 = new Rect(250, 0, w, h * 2 / 100);
            GUI.Label(rect2, Screen.currentResolution.width + "x" + Screen.currentResolution.height, style);
        }
    }
    #endregion
}
