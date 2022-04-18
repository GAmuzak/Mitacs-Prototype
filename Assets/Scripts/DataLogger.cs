using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class DataLogger : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [Range(1,100)]
    [Description("per second")]
    [SerializeField] private int resolution;

    private Coroutine routine;
    private float waitTime;
    private TextWriter tw;
    private string fileName;
    private Vector3 playerLoc;
    private Vector3 playerRot;
    private List<Array> data;
    private float[] singleFrame;

    private void Start()
    {
        waitTime = 1.0f / resolution;
        data = new List<Array>();
        singleFrame = new float[7];
        fileName = Application.dataPath + "/test.csv";
        tw = new StreamWriter(fileName, false);
        tw.WriteLine("Time, Pos.x, Pos.y, Pos.z, Rot.x, Rot.y, Rot.z");
        tw.Close();

        routine=StartCoroutine(CallLogger(waitTime));
    }

    private void Update()
    {
        playerLoc = playerTransform.position;
        playerRot = playerTransform.eulerAngles;
    }

    private IEnumerator CallLogger(float timeToWait)
    {
        while (true)
        {
            LogData();
            yield return new WaitForSeconds(timeToWait);
        }
    }

    public void KillLogging(InputAction.CallbackContext ctx)
    {
        StopCoroutine(routine);
        tw = new StreamWriter(fileName, true);
        Debug.Log(data.Count);
        foreach (Array array in data)
        {
            float[] frame = (float[]) array;
            tw.WriteLine(frame[0]+","+frame[1]+","+frame[2]+","+frame[3]+","+frame[4]+","+frame[5]+","+frame[6]);
        }
        Debug.Log("ended logging session");
        tw.Close();
    }
    
    /// <summary>
    /// solved it!
    /// </summary>
    private void LogData()
    {
        Array.Clear(singleFrame, 0, singleFrame.Length);
        
        singleFrame[0] = Time.timeSinceLevelLoad;
        singleFrame[1] = playerLoc.x;
        singleFrame[2] = playerLoc.y;
        singleFrame[3] = playerLoc.z;
        singleFrame[4] = playerRot.x;
        singleFrame[5] = playerRot.y;
        singleFrame[6] = playerRot.z;

        data.Add(singleFrame);
    }
}
