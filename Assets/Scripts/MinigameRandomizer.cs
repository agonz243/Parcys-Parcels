using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameRandomizer : MonoBehaviour
{
    public static MinigameRandomizer minigameRandomizer;
    private List<string> minigames;
    private Dictionary<string, string> minigameScenes;
    private string currentMinigame;
    private int minigameCount;
    private static bool initiated = false;

    public MinigameRandomizer() {
        minigameRandomizer = this;
        minigames = new List<string>();
        minigames.Add("Dog");
        minigames.Add("Sprinkler");
        minigames.Add("Mailbag");

        minigameScenes = new Dictionary<string, string>();
        minigameScenes.Add("Dog", "DogChase");
        minigameScenes.Add("Sprinkler", "Sprinkler-2");
        minigameScenes.Add("Mailbag", "Mailbag");

        currentMinigame = "None";

        minigameCount = 0;
    }

    public string getCurrentMinigame() {
        return currentMinigame;
    }

    public Dictionary<string, string> getMinigameScenes() {
        return minigameScenes;
    }

    public string randomizeMinigame() {
        int index = Random.Range(0, minigames.Count);
        currentMinigame = minigames[index];
        minigames.RemoveAt(index);
        minigameCount++;
        return currentMinigame;
    }
    
    public int getMinigameCount() {
        return minigameCount;
    }

    public static bool getInitiated() {
        return initiated;
    }

    public static void setInitiated(bool state) {
        initiated = state;
    }


}