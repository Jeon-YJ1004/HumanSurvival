using UnityEngine;
using Enums;
public class SelectCharacter : MonoBehaviour
{
    public ECharacterType Charname;
    public void OnClickCharacter()
    {
        DataManager.instance.CurrentCharcter=Charname;
    }
}