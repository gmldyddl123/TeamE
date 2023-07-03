using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueParser : MonoBehaviour
{
    public Dialogue[] Parse(string _CSVFileName)
    {
        List<Dialogue> dialogueList = new List<Dialogue>(); //��� ����Ʈ ����
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName); // csv���� ������

        string[] data = csvData.text.Split(new char[] { '\n' });
        
        for(int i=1; i<data.Length;)
        {
            string[] row = data[i].Split(new char[] { ',' });

            Debug.Log(row[0]);
            Debug.Log(row[1]);
            Debug.Log(row[2]);

            if(++i < data.Length)
            {
                ;
            }
        }

        return dialogueList.ToArray();
    }

    void Start()
    {
        Parse("dialogue1");
    }
}
