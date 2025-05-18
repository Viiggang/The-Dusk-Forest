using Custom.CustomComponent;
using Custom.CustomResource;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
 
using static GameSceneManager;
using static Unity.VisualScripting.Metadata;

namespace Leein
{
   
    //<리소스 경로>
    public static class ResourcePaths
    {
        public const string FontPath = FolderPaths.Imagepaths;
        public const string ButtonImagePath = FolderPaths.Imagepaths;
        public const string BackGroundImagePath = FolderPaths.Imagepaths;
        public const string SceneDataPath = FolderPaths.ScriptableObjectPath;
        public const string AudioPath = FolderPaths.AudioPath;
    }

    public class FindAllChildren : MonoBehaviour//<< 테스트 코드
    {
       
        public GameObject sceneAllObj;
        const int SIZE = 50;//폰트사이즈

        SceneData ResourceName;
        Font loadedTextFont;
        Sprite loadedImage;
        Dictionary<GameLayer, Action<GameObject>> LayerActions;
        void Start()
        {
            ResourceBuild();
            CreateLayerActions();
            ApplyLayerActions();
        }


        void CreateLayerActions()
        {
            LayerActions = new Dictionary<GameLayer, Action<GameObject>>
            {
                { GameLayer.BackGround,  BackGround },
                { GameLayer.Title,       Title },
                { GameLayer.GameStart,   GameStart },
                { GameLayer.GameOption,  GameOption },
                { GameLayer.Exit,        Exit }
             };
        }
        void ApplyLayerActions()
        {
            var allTransforms = FindObjectsOfType<ComponentEnum>();

            foreach (var obj in allTransforms)
            {
                if (LayerActions.TryGetValue(obj.gameLayer, out var action))
                {
                    action(obj.gameObject); // 해당 GameObject를 기반으로 처리
                }
                //Debug.Log(obj.name);
            }
        }
        void BackGround(GameObject ChildObject)
        {
            Debug.Log("BackGroundBackGround");
            var Image = ChildObject.SafeGetComponentInChildren<Image>("image");
            var BackGroundImagePath = ResourcePaths.BackGroundImagePath + "/" + ResourceName.BackImage.ToString();
            var Load = Resources.Load<Sprite>(BackGroundImagePath);
            if (Load != null)
            {
                Image.sprite = Load;//배경이미지 설정
                Image.SetNativeSize();
            }
            else
            {
                Debug.LogError($"에러 오브젝트 이름:{ChildObject.name}");
            }

        }

        void Title(GameObject ChildObject)
        {
           
            Debug.Log("TitleTitle");
            var Text = ChildObject.SafeGetComponentInChildren<Text>("Text");
            var rectTransform = ChildObject.SafeGetComponent<RectTransform>("RectTransform");
            rectTransform.anchoredPosition = new Vector2(-536, 354);
            Text.text = "The Avarice";//메인 타이틀 제목 설정
            Text.fontSize = 250;
            Text.font = loadedTextFont;
        }

        void GameStart(GameObject ChildObject)
        {
            Debug.Log("GameStartGameStart");
            var Image = ChildObject.SafeGetComponent<Image>("image");
            var rectTransform = ChildObject.SafeGetComponent<RectTransform>("RectTransform");
            var text = ChildObject.SafeGetComponentInChildren<Text>("Text");
            var button = ChildObject.SafeGetComponent<Button>("Button");

            text.text = "게임시작";
            text.fontSize = SIZE;
            text.font = loadedTextFont;
            Image.sprite = loadedImage;
            button.onClick.AddListener(Gamestart);
            rectTransform.anchoredPosition = new Vector2(-27, -286);
        }


        void GameOption(GameObject ChildObject)
        {
            Debug.Log("GameOptionGameOption");
            var Image1 = ChildObject.SafeGetComponent<Image>("image");
            var rectTransform1 = ChildObject.SafeGetComponent<RectTransform>("RectTransform");
            var text1 = ChildObject.SafeGetComponentInChildren<Text>("Text");
            var button1 = ChildObject.SafeGetComponent<Button>("Button");

            text1.text = "옵션";
            text1.fontSize = SIZE;
            text1.font = loadedTextFont;
            Image1.sprite = loadedImage;
            button1.onClick.AddListener(ShowGameoption);
            rectTransform1.anchoredPosition = new Vector2(-27, -386);
        }


        void Exit(GameObject ChildObject)
        {
            Debug.Log("ExitExit");
            var Image = ChildObject.SafeGetComponent<Image>("image");
            var rectTransform = ChildObject.SafeGetComponent<RectTransform>("RectTransform");
            var text = ChildObject.SafeGetComponentInChildren<Text>("Text");
            var button = ChildObject.SafeGetComponent<Button>("Button");

            text.text = "종료";
            text.fontSize = SIZE;
            text.font = loadedTextFont;
            Image.sprite = loadedImage;
            button.onClick.AddListener(GameExit);
            rectTransform.anchoredPosition = new Vector2(-27, -486);
        }


        void ResourceBuild()
        {
            var LoadData = ResourcePaths.SceneDataPath + "/" + DataName.SceneOneData.ToString();
            ResourceName = Resources.Load<SceneData>(LoadData);
            if (ResourceName == null)
            {
                Debug.LogError($"{ResourceName}");
                return;
            }

            var FontImagePath = ResourcePaths.FontPath + "/" + ResourceName.FontImage.ToString();
            loadedImage = Resources.Load<Sprite>(FontImagePath);
            if (loadedImage == null)
            {
                Debug.LogError($"{loadedImage}");
                return;
            }

            var ButtonImagePath = ResourcePaths.ButtonImagePath + "/" + ResourceName.ButtonImage.ToString();
            loadedTextFont = Resources.Load<Font>(ButtonImagePath);
            if (loadedTextFont == null)
            {
                Debug.LogError($"{loadedTextFont}");
                return;
            }
        }

        protected void Gamestart()
        {
            Debug.Log("START");
            SceneManager.LoadScene("Character selection");
        }

        protected void GameExit()
        {
            Debug.Log("EXIT");

            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // 에디터에서 실행 중지
            #endif

            Application.Quit();
        }

        protected void ShowGameoption()
        {
            Debug.Log("OPTION");
        }
    }

}
