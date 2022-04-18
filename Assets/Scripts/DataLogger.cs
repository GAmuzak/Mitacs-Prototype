using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class DataLogger : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private TextWriter tw;
    private string fileName;
    private Vector3 playerLoc;
    private Vector3 playerRot;
    private List<Array> data;
    private float[] singleFrame;

    private void Start()
    {
        data = new List<Array>();
        singleFrame = new float[7];
        playerLoc = player.transform.position;
        playerRot = player.transform.eulerAngles;
        fileName = Application.dataPath + "/test.csv";
        tw = new StreamWriter(fileName, false);
        tw.WriteLine("Time, Pos.x, Pos.y, Pos.z, Rot.x, Rot.y, Rot.z");
        tw.Close();
    }
    
    private void FixedUpdate()
    {
        LogData();
    }

    public void KillLogging(InputAction.CallbackContext ctx)
    {
        tw = new StreamWriter(fileName, true);
        
        foreach (Array array in data)
        {
            float[] frame = (float[]) array;
            Debug.Log(frame[0]+", "+frame[1]);
        }
        
        Debug.Log("ended logging session");
        tw.Close();
    }
    
    /// <summary>
    /// This is currently running every possible game engine tick,
    /// need to find a way to optimise it based on interpolation
    /// or reduce it to a resolution needed by prof
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
