using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sparks : MonoBehaviour
{

    [SerializeField] protected ParticleSystem sys;
    [SerializeField] ParticleSystemRenderer render;

    protected ParticleSystem.EmissionModule sysEmit;
    protected ParticleSystem.MainModule sysMain;


    [SerializeField] Dude dude;

    [SerializeField] Mine mine;

    private void OnEnable()
    {
        mine.OnColorChanged += SetMaterialColor;
        dude.OnAxeHitsStone += EmitSparks;
    }

    protected virtual void Awake()
    {
        sysEmit = sys.emission;
        sysMain = sys.main;
    }





    void SetMaterialColor(ColorPack colorPack)
    {
        render.material.SetColor("_Color", colorPack.gemBaseColor);
    }

    public void EmitSparks()
    {
        sys.Play();
    }



    private void OnDisable()
    {
        mine.OnColorChanged -= SetMaterialColor;
        dude.OnAxeHitsStone -= EmitSparks;

    }
}

