using UnityEngine;
using System.Collections;
using System.IO;
using SimpleFileBrowser;
using System.IO.Compression;
using System;
using System.Diagnostics;
using TMPro;
using UnityEngine.SceneManagement;

public class DownUpManager : MonoBehaviour
{

	public GameObject dialog;
	public TMP_Text text;

	public void ShowDialog(int i, String path){
		dialog.SetActive(true);
		if (i == 0){
			text.GetComponent <TMP_Text> ().text = "Die Frageliste wurde auf Ihrem Desktop unter " + path + " gespeichert";
		}else{
			text.GetComponent <TMP_Text> ().text = "Die Frageliste wurde erfolgreich importiert";
		}
	}

	public void DeactivateUI(){
		dialog.SetActive(false);
		text.GetComponent <TMP_Text> ().text = "";
	}

	public void confirmButton(){
		dialog.SetActive(false);
		text.GetComponent <TMP_Text> ().text = "";
	}


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

			string destinationPath = Application.dataPath + "/Resources/" +  "RessourceOrdner.zip";

			DeleteResources();

			FileBrowserHelpers.CopyFile( FileBrowser.Result[0], destinationPath );
			
			UnpackResources();

			File.Delete(Application.dataPath + "/Resources/" +  "RessourceOrdner.zip");

			ShowDialog(1, "");

		}
	}

    public void SaveFile()
    {
		var timestamp = DateTime.Now.ToFileTime();
        string startPath = Application.dataPath + "/Resources/";
        string zipPath =  Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Resources" + timestamp + ".zip";
		ShowDialog(0, zipPath);
        ZipFile.CreateFromDirectory(startPath, zipPath);
    }

	public void UnpackResources()
	{
		ZipFile.ExtractToDirectory(Application.dataPath + "/Resources/" +  "RessourceOrdner.zip", Application.dataPath + "/Resources/");
	}

	public void DeleteResources()
	{
		System.IO.DirectoryInfo di = new DirectoryInfo(Application.dataPath + "/Resources/");
		foreach (FileInfo file in di.GetFiles())
		{
			if (file.Name == "unity default resources" || file.Name == "unity_builtin_extra")
			{
				return;
			} 
			else
			{
				file.Delete(); 
			}
		}
	}
}
