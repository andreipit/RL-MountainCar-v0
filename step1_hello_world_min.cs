using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GymLibrary;


public class step1_hello_world_min : MonoBehaviour
{
    Envir env;
    const float TIME_LIMIT = 250000;

    private void Start()
    {
        env = Gym.Make();
        StartCoroutine(Stepper4());
    }

    IEnumerator Stepper4()
    {
        var s = env.Reset(); // observation 0
        Dictionary<string, int> actions = new Dictionary<string, int>() { { "left", 0 }, { "stop", 1 }, { "right", 2 } };
        var chosen_action = -1;

        for (int t = 0; t < TIME_LIMIT; t++)
        {
            if (s.Velocity.x <= 0)
                chosen_action = actions["left"];
            else
                chosen_action = actions["right"];
                
            env.stepStart(chosen_action);

            yield return new WaitForFixedUpdate();

            Tuple<BoxObservation, float, bool, float> newobs_Rew_Isdone_Info = env.stepEnd();
            s = newobs_Rew_Isdone_Info.Item1;

            if (newobs_Rew_Isdone_Info.Item3)
            {
                GameObject.Find("Text").GetComponent<Text>().text = "Well done!";
                yield break;
            }
        }
    }
}
