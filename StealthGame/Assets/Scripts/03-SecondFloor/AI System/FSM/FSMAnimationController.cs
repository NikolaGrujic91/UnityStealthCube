using UnityEngine;
using System.Collections;

public class FSMAnimationController : MonoBehaviour {

    private float elapsedTime = 0.0f;
    //Interval time between left/right animation
    public float intervalTime = 2.0f;
    public Animation animation;
    public int random;



    void Start()
    {
        animation = GetComponent<Animation>();

        if (animation == null)
            Debug.LogError("Animation not find!");

        //Assigning different layers enable animations blending
        animation["FSMJasubotLeft"].layer = 5;
        animation["FSMJasubotRight"].layer = 5;

    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= intervalTime)
        {
            elapsedTime = 0.0f;

            random = Random.Range(1, 100);

            int index = 0;

            if (random >= 1 && random <= 50)
                animation.CrossFade("FSMJasubotLeft");
            else if (random >= 51 && random <= 100)
                animation.CrossFade("FSMJasubotRight");
        }
    }
}
