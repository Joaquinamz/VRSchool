using UnityEngine;

/// <summary>
/// Estructura que almacena los resultados de un minijuego
/// </summary>
public class CourseResults
{
    public int score;
    public int maxScore;
    public float timeUsed;
    public float maxTime;
    public float timeElapsed; // Alias para timeUsed
    public int itemsCollected;
    public int itemsNeeded;
    public int successCount;
    public int failureCount;
    public bool passed;
    public bool isPassed; // Alias para passed
    public string module;
    public string difficulty;

    public CourseResults()
    {
        score = 0;
        maxScore = 0;
        timeUsed = 0f;
        timeElapsed = 0f;
        maxTime = 0f;
        itemsCollected = 0;
        itemsNeeded = 0;
        successCount = 0;
        failureCount = 0;
        passed = false;
        isPassed = false;
        module = "Unknown";
        difficulty = "Unknown";
    }

    public CourseResults(int score, int maxScore, float timeUsed, float maxTime, 
                         int itemsCollected, int itemsNeeded, bool passed, 
                         string module, string difficulty)
    {
        this.score = score;
        this.maxScore = maxScore;
        this.timeUsed = timeUsed;
        this.timeElapsed = timeUsed;
        this.maxTime = maxTime;
        this.itemsCollected = itemsCollected;
        this.itemsNeeded = itemsNeeded;
        this.successCount = itemsCollected;
        this.failureCount = itemsNeeded - itemsCollected;
        this.passed = passed;
        this.isPassed = passed;
        this.module = module;
        this.difficulty = difficulty;
    }

    public override string ToString()
    {
        return $"Módulo: {module} | Dificultad: {difficulty} | Puntuación: {score}/{maxScore} | Tiempo: {timeUsed:F1}s/{maxTime:F1}s | Pasó: {passed}";
    }
}
