using System;

[Serializable]
public class GameEventField {
    public int type;
    public string question;
    public string answer;
    public int time;
    public int used;

    public GameEventField (int type, string question, string answer, int time, int used) {
        this.type = type;
        this.question = question;
        this.answer = answer;
        this.time = time;
        this.used = used;
    }

    public override string ToString()
    {
        return type + question + answer + time + used;
    }

}



