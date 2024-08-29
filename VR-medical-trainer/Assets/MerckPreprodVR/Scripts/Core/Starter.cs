using MerckPreprodVR;
using UnityEngine;

public class Starter : MonoBehaviour
{
    private void Awake() 
    {
        var game = CompositionRoot.GetGame();
        var experience = CompositionRoot.GetExperience();
        experience.Start();
        experience.ExperienceFinished += () => experience.Restart();
    }
}
