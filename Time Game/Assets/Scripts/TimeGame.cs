using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeGame : MonoBehaviour {

    float roundStartDelayTime = 3; // Initializing delay before each playthrough  

    float roundStartTime; // Starts from 0 if there is no initialisation
    int waitTime; // Declaring waitTime here so we can access it outside SetNewRandomTime
    bool roundStarted; // False by default if there is no initialisation

    // Use this for initialization
    void Start () {
        print("Press the spacebar once you think the allotted time is up.");
        Invoke("SetNewRandomTime", roundStartDelayTime); // Start first round by calling the method from Start (as a string, because of invoke) and delaying for 3 seconds.

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && roundStarted) { // If space is pressed AND round is started call InputReceived
            InputReceived();
        }
    }

    void InputReceived() { 
        roundStarted = false; // End of the round. Space is already pressed.
        float playerWaitTime = Time.time - roundStartTime; // How much time have passed since start
        float error = Mathf.Abs(waitTime - playerWaitTime); // How far off the player was from the correct answer in absolute value (positive number)
        
        print("You waited for " + playerWaitTime + " seconds. That's " + error + " seconds off. " + GenerateMessage(error));
        Invoke("SetNewRandomTime", roundStartDelayTime); // Start new round by calling the method from Update (as a string, because of invoke) and delaying for 3 seconds.
    }

    string GenerateMessage(float error) { // Generates and returns message based on the error from InputReceived 
        string message = "";
        if (error < .5f) {
            message = "Outstanding! You're Like A Swiss Watch!";
        } else if (error < 1.5f) {
            message = "Close But Not Close Enough!";
        } else if (error < 2f) {
            message = "Come On You Can Do Better!";
        } else if (error < 3f) {
            message = "Not Even Close!";
        } else {
            message = "Are You Paying Any Attention At All?";
        }
        return message;
    }

    void SetNewRandomTime() { // Creating method which will set random number between min and max
        waitTime = Random.Range(5, 21);
        roundStartTime = Time.time; // Start time is equal to the current time
        roundStarted = true; // Round is started
        print(waitTime + " seconds.");
    }
}
