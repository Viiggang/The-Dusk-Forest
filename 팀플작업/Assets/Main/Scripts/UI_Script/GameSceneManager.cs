using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    string thisLayer;//오브젝트Layer값을 받을 변수
    string TitleText;//오브젝트 Text에 값을 넣어줄 문자변수

    //<Layer이름>
    const string UI_GAME_START = "UI_GameStart";
    const string UI_GAME_OPTION = "UI_GameOption";
    const string UI_EXIT = "UI_exit";
    const string TITLE_TEXT = "TitleText";
    const string BACKGROUND_IMAGE = "BackGround_Image";
    
    int FontSize = 50;//폰트사이즈 기본값 50
   
    RectTransform thisPos;//UI 위치
    Button thisButton;//오브젝트 버튼

    Text myText;//오브젝트 Text

    Font loadedTextFont;//리소스폴더에서 폰트가져옴
    Sprite loadedImage;//리소스폴더에서 로드한 이미지 
    Image thisImage;// 오브젝트 이미지

    //<리소스 경로>
    const string FontPath = "Image_resources/aqua";
    const string button_Image_Path = "Font_resources/Maple";
    const string BackGround_Image_Path = "Image_resources/Test";
    private void Awake()
    {
        //리소스 빌드
        loadedImage = Resources.Load<Sprite>(FontPath);
        loadedTextFont = Resources.Load<Font>(button_Image_Path);

        //컴포넌트 할당
        myText = GetComponentInChildren<Text>();
        if (myText == null) { myText = GetComponent<Text>(); }
        thisPos = GetComponent<RectTransform>();
        thisButton =GetComponent<Button>();
        thisImage = GetComponent<Image>();

        //씬에 설정된 Layer문자로 받는다.
        thisLayer = LayerMask.LayerToName(this.gameObject.layer);//현재 오브젝트Layer를 문자열로 저장

        //현재 오브젝트 레이어가 BackGround_Image가 아니면 폰트 지정
        if (thisLayer != BACKGROUND_IMAGE)
        {
            myText.font = loadedTextFont;//로드한 텍스트 폰트
        }

        //UI_로 시작하는 레이어는 버튼에 설정한 레이어인데 버튼이미지를 설정해줌
        if (thisLayer.StartsWith("UI_"))
        {
            thisImage.sprite = loadedImage;//버튼 이미지 설정
        }

    }

    void Start()
    {
        Initialize_Layer_Settings();
        Apply_UI_Text_Setting();
        
      
    }



    public void  Initialize_Layer_Settings()
   {
        switch (thisLayer)
        {
            case UI_GAME_START:
                TitleText = "게임시작";
                thisPos.anchoredPosition = new Vector2(0,0);//anchoredPosition	UI에서 부모 RectTransform + 앵커 기준의 2D 위치
                thisButton.onClick.AddListener(Game_start);
                break;
            case UI_GAME_OPTION:
                TitleText = "옵션";
                thisPos.anchoredPosition = new Vector2(0, -100);
                thisButton.onClick.AddListener(Game_option);
                break;
            case UI_EXIT:
                TitleText = "종료";
                thisPos.anchoredPosition = new Vector2(0, -200);
                thisButton.onClick.AddListener(Game_Exit);
                break;
            case TITLE_TEXT:
                thisPos.anchoredPosition= new Vector2(-536, 354);
                TitleText = "The Avarice";//메인 타이틀 제목 설정
                FontSize = 250;//타이틀 폰트 크기
                break;
            case BACKGROUND_IMAGE:
                Sprite loaded = Resources.Load<Sprite>(BackGround_Image_Path);//폴더 경로 기본Resources폴더에서 시작하므로 하위폴더 경로시 알잘짝
                if (loaded != null)
                {
                    thisImage.sprite = loaded;//배경이미지 설정
                    thisImage.SetNativeSize();
                }
                break;
            default:
                Debug.Log("Layer설정 잘못 된거 같은데 확인필요");
                break;
        }

   }
    protected void Apply_UI_Text_Setting( )//UI에 Text와 size를 설정한다.
    {
        if(myText !=null)
        {
            myText.text = TitleText;
            myText.fontSize = FontSize;
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
