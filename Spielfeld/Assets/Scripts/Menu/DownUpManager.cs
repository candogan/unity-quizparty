using UnityEngine;
using System.Collections;
using System.IO;
using SimpleFileBrowser;
using System.IO.Compression;
using System;
using System.Diagnostics;

public class DownUpManager : MonoBehaviour
{


	public void GetPath()
	{
		FileBrowser.SetFilters( false, new FileBrowser.Filter( "Zips", ".zip"));
		FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".rar", ".exe");
		FileBrowser.AddQuickLink("Users", "C:\\Users", null);

		StartCoroutine(ShowLoadDialogCoroutine());
	}

	IEnumerator ShowLoadDialogCoroutine()
	{
		
		yield return FileBrowser.WaitForLoadDialog( FileBrowser.PickMode.FilesAndFolders, true, null, null, "Load Files and Folders", "Load" );

		UnityEngine.Debug.Log( FileBrowser.Success );

		if( FileBrowser.Success )
		{
			for( int i = 0; i < FileBrowser.Result.Length; i++ )
				UnityEngine.Debug.Log( FileBrowser.Result[i] );

			byte[] bytes = FileBrowserHelpers.ReadBytesFromFile( FileBrowser.Result[0] );

			string destinationPath = Application.dataPath + "/Resources/" +  "TestOrdner.zip";
			FileBrowserHelpers.CopyFile( FileBrowser.Result[0], destinationPath );
			DeleteResources();
			UnpackResources();
		}
	}

    public static void SaveFile()
    {
		var timestamp = DateTime.Now.ToFileTime();
        string startPath = Application.dataPath + "/Resources/";
        string zipPath =  Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Resources" + timestamp + ".zip";

        ZipFile.CreateFromDirectory(startPath, zipPath);

    }

	public void UnpackResources()
	{
		ZipFile.ExtractToDirectory(Application.dataPath + "/Resources/" +  "TestOrdner.zip", Application.dataPath + "/Resources/");
	}

	public void DeleteResources()
	{
		System.IO.DirectoryInfo di = new DirectoryInfo(Application.dataPath + "/Resources/");
		foreach (FileInfo file in di.GetFiles())
		{
    		file.Delete(); 
		}
	}
}
