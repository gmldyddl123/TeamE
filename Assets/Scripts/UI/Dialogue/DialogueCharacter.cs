using UnityEngine;

namespace HeneGames.DialogueSystem
{
    [CreateAssetMenu(fileName = "New Character", menuName = "Dialogue System/New Dialogue Character", order = 1)]

    public class DialogueCharacter : ScriptableObject
    {
        public int NpcId;
        public string characterName;
    }
}