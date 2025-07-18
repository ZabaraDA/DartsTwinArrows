using UnityEngine;

public class DataStorage : MonoBehaviour
{
    private const string SAVED_URL_KEY = "gray_module_saved_url";
    private const string ORGANIC_KEY = "gray_module_is_organic";
    private const string BASE_URL_KEY = "gray_module_base_url";
    private const string COMPANY_KEY = "gray_module_company_key";
   

    public string LoadUrl()
    {
        return PlayerPrefs.GetString(SAVED_URL_KEY, "");
    }

    public bool LoadOrganicStatus()
    {
        return PlayerPrefs.GetInt(ORGANIC_KEY, 1) == 1;
    }

    public (string baseUrl, string companyKey) LoadBaseConfig()
    {
        return (
            PlayerPrefs.GetString(BASE_URL_KEY, ""),
            PlayerPrefs.GetString(COMPANY_KEY, "")
        );
    }

    public void SaveUrl(string url, bool isOrganic)
    {
        PlayerPrefs.SetString(SAVED_URL_KEY, url);
        PlayerPrefs.SetInt(ORGANIC_KEY, isOrganic ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void SaveBaseConfig(string baseUrl, string companyKey)
    {
        PlayerPrefs.SetString(BASE_URL_KEY, baseUrl);
        PlayerPrefs.SetString(COMPANY_KEY, companyKey);
        PlayerPrefs.Save();
    }
}