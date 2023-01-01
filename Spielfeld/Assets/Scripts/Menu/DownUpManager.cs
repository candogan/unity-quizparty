using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class DownUpManager : MonoBehaviour
{

    public void Download(){
        FileHandler.DownloadOptions();
    }

    void Upload(){
        FileHandler.UploadOptions();
    }
}
