using System;

[Serializable]
public class GameEventField 
{
    public int type;
    public string content;
    public string answer;
    public int time;
    public int difficulty;
    public int used;

    public GameEventField (int type, string content, string answer, int time, int difficulty, int used) 
    {
        this.type = type;
        this.content = content;
        this.answer = answer;
        this.time = time;
        this.difficulty = difficulty;
        this.used = used;
    }

    public int GetFieldType()
    {
        return type;
    }

    public void SetFieldType(int newType)
    {
        type = newType;
    }

    public string GetContent()
    {
        return content;
    }

    public void SetContent(string newContent)
    {
        content = newContent;
    }

    public string GetAnswer()
    {
        return answer;
    }

    public void SetAnswer(string newAnswer)
    {
        answer = newAnswer;
    }

    public int GetTime()
    {
        return time;
    }

    public void SetTime(int newTime)
    {
        time = newTime;
    }

    public int GetDifficulty()
    {
        return difficulty;
    }

    public void SetDifficulty(int newDifficulty)
    {
        difficulty = newDifficulty;
    }

    public int GetUsed()
    {
        return used;
    }

    public void SetUsed(int newUsed)
    {
        used = newUsed;
    }

    public override string ToString()
    {
        return type + content + answer + time + difficulty + used;
    }

}



