public interface ISavable
{
    DataDefination GetDataID();
    void RegisterSaveData()
    {
        DataManager.instance.RegisterSaveData(this);
    }
    void UnregisterSaveData()
    {
        DataManager.instance.UnregisterSaveData(this);
    }
    void GetSaveData(Data data);
    void LoadData(Data data);
}
