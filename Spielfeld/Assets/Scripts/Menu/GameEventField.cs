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

    public int GetFieldType(){
        return type;
    }

    public void SetFieldType(int newType){
        type = newType;
    }

    public string GetQuestion(){
        return question;
    }

    public void SetQuestion(string newQuestion){
        question = newQuestion;
    }

    public string GetAnswer(){
        return answer;
    }

    public void SetAnswer(string newAnswer){
        answer = newAnswer;
    }

    public int GetTime(){
        return time;
    }

    public void SetTime(int newTime){
        time = newTime;
    }

    public int GetUsed(){
        return used;
    }

    public void SetUsed(int newUsed){
        used = newUsed;
    }

    public override string ToString()
    {
        return type + question + answer + time + used;
    }

}



