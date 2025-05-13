using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
 
public enum character
{
    one=1,
    two,
    three,
    four,
}

public class CharacterSelectionController : Singleton<CharacterSelectionController>
{
  
    public character CharacterSelection;
    private void Awake()
    {
        base.Awake();
     
    }

    public override void ThisObjectDestroy()//캐릭터 생성되면 파괴
    {
       base.ThisObjectDestroy();
    }
   
}
