using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public static class GameController {

    #region Score Calculating Variables
    //static varaible for enemies killed
    static int enemiesKilled;
    //static in for score
    static float score;
    //static float for speed increment
    static float speedInc = 0.05f;
    //static float for final speed multiplier (used to calculate final score)
    static float finalSpeedMultiplier;
    //static float for time player lasted
    static float time;
    #endregion

    //Score to apply per enemy killed
    static int scorePerEnemy = 10;


    /// <summary>
    /// Called by start method of player script
    /// Resets variables used to calculate score on score screen
    /// </summary>
    static void ResetVaraiables()
    {
        enemiesKilled = 0;
        score = 0;
        time = 0;
        finalSpeedMultiplier = 0;
    }

    /// <summary>
    /// Calculate score using score based variables
    /// </summary>
    /// <returns> Returns casted int of score variable after calculation </returns>
    public static int CalculateScore()
    {
        //Calculate Score using time, enemies killed and final speed multiplier
        //Calculation = (time + (enemies killed times enemies multiplier)) * speed multiplier
        score = ((int)time + (enemiesKilled*scorePerEnemy)) * finalSpeedMultiplier;
        //cast result to int for displaying
        return (int)score;
    }

    /// <summary>
    /// Sets final speed multiplier reached by player
    /// Called by Die() method of PlayerScript
    /// Used to calculate final score
    /// Also resets time scale to Default
    /// </summary>
    public static void SetFinalMultiplier()
    {
        finalSpeedMultiplier = Time.timeScale;
        Time.timeScale = 1.0f;
    }

    /// <summary>
    /// Method to update time player survived
    /// Called by player script die method
    /// </summary>
    /// <param name="playTime">Time player survived</param>
    public static void SetTime(float playTime)
    {
        time = playTime;
    }

    /// <summary>
    /// Set number of enemies killed by player
    /// Called by PlayerScript die method
    /// </summary>
    /// <param name="enemiesPlayerKilled"></param>
    public static void SetEnemiesKilled(int enemiesPlayerKilled)
    {
        enemiesKilled = enemiesPlayerKilled;
    }
    
    /// <summary>
    /// Increments the number of enemies killed by the player
    /// Called by Die Method of EnemyScript
    /// </summary>
    public static void IncrementEnemiesKilled()
    {
        enemiesKilled++;
    }

    /// <summary>
    /// Get speed increment 
    /// </summary>
    /// <returns>returns speed increment value</returns>
    public static float GetSpeedInc()
    {
        return speedInc;
    }


}
