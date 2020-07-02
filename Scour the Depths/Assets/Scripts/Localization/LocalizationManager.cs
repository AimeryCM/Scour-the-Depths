using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LocalizationManager : MonoBehaviour
{
	public static LocalizationManager instance = null;

	[SerializeField] private string missingText = "Localized Text not found";
	private Dictionary<string, string> localizedText = null;
	private bool isReady = false;

	void Awake()
	{
		if(instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
		
		DontDestroyOnLoad(gameObject);
	}

	public void LoadLocalizedText(string fileName)
	{
		localizedText = new Dictionary<string, string>();
		string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
		if(File.Exists(filePath))
		{
			string dataAsJson = File.ReadAllText(filePath);
			LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);
			for(int x = 0; x < loadedData.items.Length; x++)
			{
				localizedText.Add(loadedData.items[x].key, loadedData.items[x].value);
			}
		} else
		{
			Debug.LogError("Localization file requested not found");
		}
		isReady = true;
	}

	public string GetLocalizedValue(string key)
	{
		return localizedText.ContainsKey(key) ? localizedText[key] : missingText;
	}

	public bool GetIsReady()
	{
		return isReady;
	}
}
