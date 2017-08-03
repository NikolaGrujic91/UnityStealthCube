using UnityEngine;
using System.Collections;

/// <summary>
/// The Sense class is the interface of our sensory system that the other custom senses
/// can implement. It defines two virtual methods, Initialize and UpdateSense,
/// which will be implemented in custom senses, and are executed from the Start and
/// Update methods, respectively
/// 
/// Basic properties include its detection rate to execute the sensing operation as well as
/// the name of the aspect it should look for. This script will not be attached to any of
/// our objects.
/// </summary>
public class Sense : MonoBehaviour {
    public bool bDebug = true;
    public Aspect.aspect aspectName = Aspect.aspect.Enemy;
    public float detectionRate = 1.0f;

    protected float elapsedTime = 0.0f;

    protected virtual void Initialize() {}
    protected virtual void UpdateSense() {}

	// Use this for initialization
	void Start ()
    {
        elapsedTime = 0.0f;
        Initialize();
	}
	
	// Update is called once per frame
	void Update () 
    {
        UpdateSense();
	}
}
