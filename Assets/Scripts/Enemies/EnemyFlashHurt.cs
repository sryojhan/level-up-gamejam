using UnityEngine;

public class EnemyFlashHurt : MonoBehaviour
{
    public float hurtFlashDuration = 0.2f;

    public SkinnedMeshRenderer meshToFlash;

    MaterialPropertyBlock mpb;

    private void Start()
    {
        mpb = new MaterialPropertyBlock();
    }


    public void OnHurt()
    {
        meshToFlash.GetPropertyBlock(mpb);
        mpb.SetFloat("_isHurt", 1);
        meshToFlash.SetPropertyBlock(mpb);

        Invoke(nameof(RemoveHurt), hurtFlashDuration);
    }


    private void RemoveHurt()
    {
        meshToFlash.GetPropertyBlock(mpb);
        mpb.SetFloat("_isHurt", 0);
        meshToFlash.SetPropertyBlock(mpb);
    }

}
