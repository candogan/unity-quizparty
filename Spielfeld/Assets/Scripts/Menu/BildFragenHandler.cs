using UnityEngine;
using System.Collections;
using System.IO;
using SimpleFileBrowser;
using System.IO.Compression;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class BildFragenHandler : MonoBehaviour
{

    [SerializeField] TMP_InputField answer;
    [SerializeField] string filename;
    public GameObject uploadButton;
    public GameObject addButton;


    private List<GameEventField> entries = new List<GameEventField> ();

    private void Start () {
        entries = FileHandler.ReadListFromJSON<GameEventField> (filename);
    }

    private void Update (){
        if (answer.text == ""){
            uploadButton.GetComponent <Button>().interactable = false;
            addButton.GetComponent <Button>().interactable = false;
        } else {
            uploadButton.GetComponent <Button>().interactable = true;
        }
    }

    public void AddFieldToList () {
        entries = FileHandler.ReadListFromJSON<GameEventField> (filename);
   
        entries.Add (new GameEventField (3, answer.text + ".png", answer.text, 60, 0));
       
        answer.text = "";
        FileHandler.SaveToJSON<GameEventField> (entries, filename);
    }

    public void GetPath()
	{
		FileBrowser.SetFilters( false, new FileBrowser.Filter( "Bilder", ".png", ".jpg"));
		FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".rar", ".exe", ".zip", ".pdf");
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

			string destinationPath = Application.dataPath + "/Resources/" + answer.text + ".png";

			FileBrowserHelpers.CopyFile( FileBrowser.Result[0], destinationPath );
			
            UnityEngine.Debug.Log(FileBrowser.Result[0]);

            addButton.GetComponent <Button>().interactable = true;

		}
	}
}
