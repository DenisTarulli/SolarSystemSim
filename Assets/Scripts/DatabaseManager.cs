using UnityEngine;
using Proyecto26;
using TMPro;
using UnityEditor;
using System.Collections.Generic;
using System.Collections;

public class DatabaseManager : MonoBehaviour
{
    [SerializeField] private string databaseURL;

    private void LogMessage(string title, string message)
    {
#if UNITY_EDITOR
        EditorUtility.DisplayDialog(title, message, "Ok");
#else
		Debug.Log(message);
#endif
    }

    public void SaveData(CelestialStats stats)
    {
        RestClient.Put(databaseURL + "/" + stats.Name + ".json", stats);
        Debug.Log($"Planet {stats.Name} values have been saved succesfully");
    }
    
    //public void ReadData()
    //{
    //    RestClient.Get<CelestialStats>(databaseURL + "/")
    //    if (index.text != "")
    //    {
    //        RestClient.Get<ShotInfoToDisplay>(databaseURL + "/Shot%20" + index.text + ".json").Then(response =>
    //        {
    //            shotToRead = response;
    //            this.LogMessage($"Shot values", $"Index: {index.text}\n" +
    //                $"Force: {shotToRead.Force}\n" +
    //                $"X Angle: {shotToRead.X_Angle}\n" +
    //                $"Y Angle: {shotToRead.Y_Angle}\n" +
    //                $"Impact force: {shotToRead.ImpactForce}");
    //        });
    //    }
    //    else
    //        StartCoroutine(ReadAllData());
    //}

    //public IEnumerator ReadAllData()
    //{
    //    float lastShotIndex = shotInfo.ShotIndex;

    //    for (int i = 0; i < lastShotIndex; i++)
    //    {
    //        RestClient.Get<ShotInfoToDisplay>(databaseURL + "/Shot%20" + i + ".json").Then(response =>
    //        {
    //            shotsToDisplayList.Add(response);
    //        });

    //        yield return new WaitForSeconds(0.7f);
    //    }

    //    if (shotsToDisplayList.Count != shotsAmount)
    //    {
    //        DeleteAllShotsContainers();

    //        int shotCount = 0;

    //        foreach (ShotInfoToDisplay shot in shotsToDisplayList)
    //        {
    //            GameObject newShot = Instantiate(newShotContainerPrefab, newShotParent);
    //            newShot.GetComponent<ShotStatsContainer>().SetValues(shot, shotCount);
    //            shotCount++;
    //        }

    //        shotsAmount = shotsToDisplayList.Count;
    //    }

    //    shotsToDisplayList.Clear();
    //}

    //public void DeleteData()
    //{
    //    RestClient.Delete(databaseURL + ".json", (err, res) =>
    //    {
    //        if (err != null)
    //        {
    //            this.LogMessage("Error", err.Message);
    //            DeleteAllShotsContainers();
    //        }
    //        else
    //        {
    //            this.LogMessage("Success", "Status: " + res.StatusCode.ToString() + "\nData cleared");
    //            shotInfo.ResetIndex();
    //            shotsToDisplayList.Clear();
    //        }
    //    });
    //}
}
