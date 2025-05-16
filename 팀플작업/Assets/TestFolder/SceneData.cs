using UnityEngine;

[CreateAssetMenu(menuName = "Data/SceneData")]
public class SceneData : ScriptableObject
{
    //public SoundType soundType;  // 여기서 드롭다운이 보기 좋은 이름으로 보임
    
    [Header("버튼이미지")]public buttonImagePath ButtonImage;
    [Header("배경이미지")]public BackGroundImagePath BackImage;
    [Header("폰트이미지")]public FontPath FontImage;
    // public AudioClip clip;
}
