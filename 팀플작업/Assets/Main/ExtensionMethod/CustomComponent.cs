
using UnityEngine;
namespace Custom.CustomComponent
{
    public static class CustomComponent
    {
        public static T SafeGetComponent<T>(this GameObject gameObject, string ErrorMessage) where T : Component
        {
            T component = gameObject.GetComponent<T>();
            if (component == null)
            {
                Debug.LogError($"{gameObject.name}+{ErrorMessage} 컴포넌트를 찾을 수 없습니다.");
            }
            return component;
        }
        public static T SafeGetComponentInChildren<T>(this GameObject gameObject, string ErrorMessage) where T : Component
        {
            T componentChildren = gameObject.GetComponentInChildren<T>();
            if (componentChildren == null)
            {
                T component = gameObject.SafeGetComponent<T>(ErrorMessage);
                return component;
            }
            return componentChildren;
        }
        
       
    }

}

