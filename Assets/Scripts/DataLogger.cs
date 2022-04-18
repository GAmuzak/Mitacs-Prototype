using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class DataLogger : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [Range(1,100)]
    [Description("per second")]
    [SerializeField] private int resolution;

    private float waitTime;
    private TextWriter tw;
    private string fileName;
    private Vector3 playerLoc;
    private Vector3 playerRot;
    private List<Array> data;
    private float[] singleFrame;

    private void Start()
    {
        waitTime = 1 / resolution;
        data = new List<Array>();
        singleFrame = new float[7];
        playerLoc = player.transform.position;
        playerRot = player.transform.eulerAngles;
        fileName = Application.dataPath + "/test.csv";
        tw = new StreamWriter(fileName, false);
        tw.WriteLine("Time, Pos.x, Pos.y, Pos.z, Rot.x, Rot.y, Rot.z");
        tw.Close();

        StartCoroutine(CallLogger(waitTime));
    }

    private IEnumerator CallLogger(float waitTime)
    {
        while (true)
        {
            LogData();
            yield return new WaitForSeconds(waitTime);
        }
    }

    public void KillLogging(InputAction.CallbackContext ctx)
    {
        tw = new StreamWriter(fileName, true);
        foreach (Array array in data)
        {
            float[] frame = (float[]) array;
            // Debug.Log(frame[0]+", "+frame[1]);
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
        Debug.Log("Called" + Time.realtimeSinceStartup);
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
