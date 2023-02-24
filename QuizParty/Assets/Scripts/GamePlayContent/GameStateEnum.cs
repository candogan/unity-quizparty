using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateEnum : MonoBehaviour
{
    public static int INITIALIZING = -1;
    public static int SWITCHING_ACTIVE_TEAM = 0;
    public static int ROLLING_DICE = 1;
    public static int WAITING_FOR_DICE = 2;
    public static int WAITING_FOR_MOVING_CHARACTER = 3;
    public static int QUESTION_MODE = 4;
    public static int PREPARING_NEXT_ROUND = 5;
    public static int ROUND_ENDED_AND_ESTIMATION_TASK = 6;
    public static int GAME_FINISHED = 7;
}
