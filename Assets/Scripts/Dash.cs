using UnityEngine;

class Dash : Ability
{
    public float dashSpeed;
    float originalMaxSpeed;

    public AudioClip mySoundClip;

    public override void Activate()
    {
        originalMaxSpeed = player.maxSpeed;
        player.maxSpeed = dashSpeed;
        Invoke(nameof(DelayedApplyForce), 0.025f);
    }

    public override void Deactivate()
    {
        player.maxSpeed = originalMaxSpeed;
    }

    void DelayedApplyForce()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(moveHorizontal, 0f, moveVertical);
        rb.AddForce(direction.normalized * dashSpeed, ForceMode.Impulse);
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = mySoundClip;
        audioSource.Play();
    }
}
