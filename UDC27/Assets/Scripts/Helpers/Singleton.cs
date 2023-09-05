using UnityEngine;

/// <summary>
/// Generic Singleton
/// </summary>
/// <typeparam name="T">The type for the Singleton</typeparam>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
   /// <summary>
   /// The instance
   /// </summary>
   private static T _instance;

   /// <summary>
   /// The instance property
   /// </summary>
   public static T Instance => _instance;

   /// <summary>
   /// Cast this to the instance
   /// </summary>
   public void Awake()
   {
      _instance = (T) (object) this;
   }
}
