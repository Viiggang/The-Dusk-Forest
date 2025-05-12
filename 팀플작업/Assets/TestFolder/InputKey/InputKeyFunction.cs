using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class InputKeyFunction : MonoBehaviour
{
    private static Queue<Image> ImageQue = new Queue<Image>();
    public GameObject ObjTemp;

    public void ESC()
    { 
      foreach (Image img in ImageQue)
      {
       // 1. 중복 확인 + 현재 활성화 상태만 추가
       bool IsEnable = img.enabled;
       bool IsDuplication = IsAlreadyInQueue(img);

       if(IsEnable && !IsDuplication)
       {
           ImageQue.Enqueue(img);
       }
      }
        
      //  큐에서 하나 꺼내서 비활성화
      if (ImageQue.Count > 0)
      {
          var toDisable = ImageQue.Dequeue();
          toDisable.enabled = false;
         
      }
    }

    // 중복 확인 함수
    private bool IsAlreadyInQueue(Image img)
    {
        foreach (var queued in ImageQue)
        {
            if (queued == img)
            {
                return true;
            }
        }
        return false;
    }

    void InventoryUI() => EnableImageByName("Image (1)");
    void MapUI() => EnableImageByName("inven");

    private void EnableImageByName(string imageName)
    {

        Debug.Log($"1 : {imageName}, {ObjTemp.name}");
        var images = ObjTemp.GetComponentsInChildren<Image>(true);

        foreach (var img in images)
        {
            Debug.Log($"2 : {img.name}, {img.gameObject.name}");
            if (img.gameObject.name == imageName)
            {
                img.gameObject.SetActive(true);
                ImageQue.Enqueue(img);
                break;
            }
        }
    }
}
