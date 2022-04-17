using System;
using System.Collections;
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

    private void Start()
    {
        playerLoc = player.transform.position;
        playerRot = player.transform.eulerAngles;
        fileName = Application.dataPath + "/test.csv";
        tw = new StreamWriter(fileName, false);
        tw.WriteLine("Time, Pos.x, Pos.y, Pos.z, Rot.x, Rot.y, Rot.z");
    }
    
    private void Update()
    {
        WriteCsv();
    }

    public void KillLogging(InputAction.CallbackContext ctx)
    {
        tw.Close();
    }
    
    private void WriteCsv()
    {
        tw.WriteLine(Time.timeSinceLevelLoad+","+playerLoc.x+","+playerLoc.y+","+playerLoc.z+","+playerRot.x+","+playerRot.y+","+playerRot.z+",");
    }
}
