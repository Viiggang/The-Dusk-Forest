using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Custom.CustomComponent;

public class GameSceneManager : MonoBehaviour
{
    public GameObject ParentsOBJ;
    GameObject[] children; 
    //<Layer이름>
    public struct UILayerNames
    {
        public const string UI_GAME_START = "UI_GameStart";
        public const string UI_GAME_OPTION = "UI_GameOption";
        public const string UI_EXIT = "UI_exit";
        public const string TITLE_TEXT = "TitleText";
        public const string BACKGROUND_IMAGE = "BackGround_Image";
    }

    //<리소스 경로>
    public struct ResourcePaths
    {
        public const string FontPath= "Image_resources";
        public const string ButtonImagePath="Font_resources";
        public const string BackGroundImagePath = "Image_resources";
        public const string SceneDataPath = "SceneData";
    }

    string LayerName;//오브젝트Layer값을 받을 변수
    string TitleText;//오브젝트 Text에 값을 넣어줄 문자변수
    int FontSize = 50;//폰트사이즈 기본값 50

    RectTransform rectTransform;//UI 위치
    Button button;//오브젝트 버튼
    Text text;//오브젝트 Text
    Font loadedTextFont;//리소스폴더에서 폰트가져옴
    Sprite loadedImage;//리소스폴더에서 로드한 이미지 
    Image image;// 오브젝트 이미지
    
    SceneData m_Data;//스크립트오브젝트 <-- 리소스빌드함
    void Findchildren()
    {
        int i = 0;
        var FindObj = GetComponentsInChildren<ComponentEnum>();
        foreach (var obj in FindObj)
        {
            if (obj != null)
            {
                children[i++] = obj.GetComponent<GameObject>();
            }
        }
        foreach (var obj in children)
        {
            Debug.Log($"오브젝트 이름 :{obj.name}");
        }
    }
    private void Awake()
    {
        //컴포넌트 할당
        GetComponent();
        ResourceBuild();
        SetLayerName();
        ApplyUIByLayerName();
    }

    void ApplyUIByLayerName()
    {
        //현재 오브젝트 레이어가 BackGround_Image가 아니면 폰트 지정
        if (LayerName != UILayerNames.BACKGROUND_IMAGE)
        {
            text.font = loadedTextFont;//로드한 텍스트 폰트
        }

        //UI_로 시작하는 레이어는 버튼에 설정한 레이어인데 버튼이미지를 설정해줌
        if (LayerName.StartsWith("UI_"))
        {
            image.sprite = loadedImage;//버튼 이미지 설정
        }
    }
    void SetLayerName()
    {
        //씬에 설정된 Layer문자로 받는다.
        LayerName = LayerMask.LayerToName(gameObject.layer);//현재 오브젝트Layer를 문자열로 저장
    }
    void ResourceBuild()  //리소스 빌드
    {
        var TestPath = ResourcePaths.SceneDataPath+"/"+ DataName.SceneOneData.ToString();//<-enum으로 변경예정
        m_Data = Resources.Load<SceneData>(TestPath);
        if(m_Data == null)
        { 
            Debug.LogError($"{m_Data}");
            return;
        }

        var FontImagePath = ResourcePaths.FontPath + "/" + m_Data.FontImage.ToString();
        loadedImage = Resources.Load<Sprite>(FontImagePath);
        if (loadedImage == null) 
        {
            Debug.LogError($"{loadedImage}");
            return;
        }

        var ButtonImagePath = ResourcePaths.ButtonImagePath+ "/" + m_Data.ButtonImage.ToString();
        loadedTextFont = Resources.Load<Font>(ButtonImagePath);
        if (loadedTextFont == null) 
        { 
            Debug.LogError($"{loadedTextFont}");
            return;
        }

    }

    void GetComponent()
    {
        text =gameObject.SafeGetComponentInChildren<Text>("Text");
        rectTransform = gameObject.SafeGetComponent<RectTransform>("RectTransform");
        button = gameObject.SafeGetComponent<Button>("Button");
        image = gameObject.SafeGetComponent<Image>("Image");
    }

    void Start()
    {
        Initialize_Layer_Settings();
        Apply_UI_TextSetting();
    }

    public void  Initialize_Layer_Settings()
   {
        switch (LayerName)
        {
            case UILayerNames.UI_GAME_START:
                SetupLayer("게임시작", new Vector2(0, 0), Game_start);
                break;
            case UILayerNames.UI_GAME_OPTION:
                SetupLayer("옵션", new Vector2(0, -100), Game_option);
                break;
            case UILayerNames.UI_EXIT:
                SetupLayer("종료", new Vector2(0, -200), Game_Exit);
                break;
            case UILayerNames.TITLE_TEXT:
                SetupTitleText();
                break;
            case UILayerNames.BACKGROUND_IMAGE:
                SetupBackgroundImage();
                break;
            default:
                Debug.Log("Layer설정 잘못 된거 같은데 확인필요");
                break;
        }

   }
    
    void SetupLayer(string title, Vector2 position, UnityEngine.Events.UnityAction buttonAction)
    {
        TitleText = title;
        rectTransform.anchoredPosition = position;  // UI 위치 설정
        button.onClick.AddListener(buttonAction);  // 버튼 클릭 이벤트 설정
    }

    protected void Apply_UI_TextSetting( )//UI에 Text와 size를 설정한다.
    {
        if(text !=null)
        {
            text.text = TitleText;
            text.fontSize = FontSize;
        }
   
    }
    void SetupTitleText()
    {
        rectTransform.anchoredPosition = new Vector2(-536, 354);
        TitleText = "The Avarice";//메인 타이틀 제목 설정
        FontSize = 250;//타이틀 폰트 크기
    }
    void SetupBackgroundImage()
    {
        var BackGroundImagePath = ResourcePaths.BackGroundImagePath+"/" + m_Data.BackImage.ToString();
        var Load = Resources.Load<Sprite>(BackGroundImagePath);
        if (Load != null)
        {
            image.sprite = Load;//배경이미지 설정
            image.SetNativeSize();
        }
        else
        {
            Debug.LogError("GameSceneManager->SetupBackgroundImage함수 error");
        }
    }
    protected void Game_Exit()
    {
        Debug.Log("EXIT");

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 에디터에서 실행 중지
        #endif

        Application.Quit();
    }
   protected void Game_option()
   {
        Debug.Log("OPTION");
   }
   protected void Game_start()
   {
        Debug.Log("START");
        SceneManager.LoadScene("Character selection");
    }


}
