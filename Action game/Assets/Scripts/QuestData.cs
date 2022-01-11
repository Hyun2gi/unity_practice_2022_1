using System.Collections;
using System.Collections.Generic;


public class QuestData 
{
    //코드에서 불러서 사용해야한다.
    public string questName;
    //퀘스트와 연관되어있는 npc id를 저장하는 int배열
    public int[] npcId;

    //구조체 생성을 위해 매개변수 생성자를 작성
    public QuestData(string name, int[] npc)
    {
        questName = name;
        npcId = npc;
    }
}
