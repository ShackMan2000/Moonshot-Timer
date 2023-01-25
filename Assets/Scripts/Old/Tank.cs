using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;

public class Tank : MonoBehaviour
 {
//     [SerializeField] SpriteRenderer tankFillPlaceHolder;
//
//     List<SpriteRenderer> tankFillSprites;
//
//     [SerializeField] TextMeshProUGUI fillDistractedText;
//     [SerializeField] TextMeshProUGUI fillFocusedText;
//
//     [SerializeField] GlobalSettings settings;
//
//  
//
//     public SaveData data;
//
//     List<MineData> minesInTank;
//
//     Vector3 rocketStartPosition;
//
//     bool isFlying;
//
//
//     public static event Action OnRocketLaunched = delegate { };
//     public static event Action OnRocketDone = delegate { };
//
//     [SerializeField] float maxRocketSpeed;
//     [SerializeField] float timeTillMaxSpeed;
//
//     float currentSpeed;
//
//
//     [SerializeField] GameObject fire;
//
//     [SerializeField] Curve curve;
//
//
//     void OnEnable()
//     {
//         SaveManager.OnSaveDataReady += x => SetUpTank();
//         Mine.OnAnyProgressMade += AdjustTankFillSprites;
//         Mine.OnAnyColorChanged += CreateSprites;
//         fire.SetActive(false);
//
//         rocketStartPosition = transform.position;
//     }
//
//
//     void SetUpTank()
//     {
//         data = SaveManager.Data;
//        // UpdateMinesInTank();
//       //  CreateSprites();
//     }
//
//
//     void UpdateMinesInTank()
//     {
//         // minesInTank = new List<MineData>();
//         //
//         // foreach (var mine in SaveManager.Data.activeMinesData.Where(m => m.secondsInTank > 0.1f))
//         // {
//         //     minesInTank.Add(mine);
//         // }
//     }
//
//
//     // void CheckToUpdateMinesInTank()
//     // {
//     //     int minesInTankBefore = minesInTank.Count;
//     //     int minesInTankNow = 0;
//     //
//     //     foreach (var mine in SaveManager.Data.activeMinesData.Where(m => m.secondsInTank > 0.1f))
//     //     {
//     //         minesInTankNow++;
//     //     }
//     //
//     //     if (minesInTankNow != minesInTankBefore)
//     //     {
//     //         UpdateMinesInTank();
//     //         CreateSprites();
//     //     }
//     // }
//
//
//     void CreateSprites()
//     {
//         if (tankFillSprites == null)
//             tankFillSprites = new List<SpriteRenderer>();
//
//
//         while (tankFillSprites.Count < minesInTank.Count)
//         {
//             var newSprite = Instantiate(tankFillPlaceHolder, transform);
//             tankFillSprites.Add(newSprite);
//             newSprite.gameObject.SetActive(true);
//         }
//
//         for (int i = 0; i < minesInTank.Count; i++)
//         {
//             tankFillSprites[i].color = settings.GetColorPackByName(data.activeMinesData[i].colorName).mainColor;
//         }
//
//         tankFillPlaceHolder.gameObject.SetActive(false);
//
//         AdjustTankFillSprites();
//     }
//
//
//     void AdjustTankFillSprites()
//     {
//       //  CheckToUpdateMinesInTank();
//
//         //pivot at bottom
//         // float yOffset = 0f;
//         // float originalScale = tankFillPlaceHolder.transform.localScale.y;
//         // Vector2 originPosition = tankFillPlaceHolder.transform.localPosition;
//         // float blocksInTank = GetTotalBlocksInTank();
//         //
//         // float adjustedTankSizeInBlocks = settings.blocksForFullTank;
//         // if (blocksInTank > adjustedTankSizeInBlocks)
//         //     adjustedTankSizeInBlocks = blocksInTank;
//         //
//         //
//         // for (int i = 0; i < minesInTank.Count; i++)
//         // {
//         //     float percentOfTank = minesInTank[i].secondsInTank / settings.secondsPerBlock / adjustedTankSizeInBlocks;
//         //
//         //     percentOfTank = Mathf.Clamp01(percentOfTank);
//         //
//         //     float newScale = percentOfTank * originalScale;
//         //
//         //     tankFillSprites[i].transform.localScale = new Vector2(1f, newScale);
//         //
//         //     tankFillSprites[i].transform.localPosition = new Vector2(originPosition.x, originPosition.y + yOffset);
//         //
//         //     yOffset += percentOfTank * tankFillPlaceHolder.bounds.size.y * (1f / tankFillPlaceHolder.transform.lossyScale.x);
//         // }
//
//       //  fillAmountText.text = blocksInTank.ToString("F2");
//     }
//
//
//     // public float GetTotalBlocksInTank()
//     // {
//     //     float secondsInTank = 0f;
//     //
//     //     foreach (var m in minesInTank)
//     //     {
//     //         secondsInTank += m.secondsInTank;
//     //     }
//     //
//     //     return secondsInTank / settings.secondsPerBlock;
//     // }
//
//     // void EmptyTank()
//     // {
//     //     foreach (var m in minesInTank)
//     //     {
//     //         m.secondsInTank = 0f;
//     //     }
//     //
//     //     AdjustTankFillSprites();
//     // }
//
//
//     public void Click()
//     {
//         panel.OpenPanel();
//         panel.Initialize(StartLaunchRoutine);
//     }
//
//
//     public void StartLaunchRoutine()
//     {
//      //   StartCoroutine(LaunchRoutine());
//     }
//
//     // IEnumerator LaunchRoutine()
//     // {      
//     //     // lock camera, music, lock mines, maybe lock rocket too.
//     //     // maybe just lock all clicks
//     //     OnRocketLaunched();
//     //
//     //     isFlying = true;
//     //     fire.SetActive(true);
//     //
//     //     float flightTime = 0f;
//     //     currentSpeed = 0f;
//     //     curve.TotalTime = timeTillMaxSpeed;
//     //
//     //
//     //     while (flightTime < timeTillMaxSpeed)
//     //     {
//     //         currentSpeed = curve.ValueInTime(flightTime) * maxRocketSpeed;
//     //         flightTime += Time.deltaTime;
//     //         yield return null;
//     //     }
//     //
//     //
//     //     while (flightTime < settings.rocketFlightTime)
//     //     {   
//     //         flightTime += Time.deltaTime;
//     //         yield return null;
//     //     }
//     //
//     //   //  EmptyTank();
//     //     fire.SetActive(false);
//     //     ResetRocket();
//     //
//     // }
//
//     public void ResetRocket()
//     {
//         isFlying = false;
//         OnRocketDone();
//         transform.position = rocketStartPosition;
//
//     }
//
//
//     void Update()
//     {
//         if(isFlying)
//         {
//             transform.position += Vector3.up * currentSpeed * Time.deltaTime;
//         }
//     }
//
//     void OnDisable()
//     {
//       //  Mine.OnAnyProgressMade -= CheckToUpdateMinesInTank;
//       //  Mine.OnAnyProgressMade -= AdjustTankFillSprites;
//         SaveManager.OnSaveDataReady -= _ => SetUpTank();
//     }
}






