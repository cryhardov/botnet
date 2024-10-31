using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using cryhard.TaskPlanner.DataAccess.Abstractions; 
public class FileWorkItemsRepository : IWorkItemsRepository
{
    private const string FileName = "work-items.json";
    private readonly Dictionary<Guid, WorkItem> _workItems;

    public FileWorkItemsRepository()
    {
        _workItems = LoadWorkItems();
    }

    private Dictionary<Guid, WorkItem> LoadWorkItems()
    {
        if (!File.Exists(FileName))
            return new Dictionary<Guid, WorkItem>();

        var json = File.ReadAllText(FileName);
        return JsonConvert.DeserializeObject<Dictionary<Guid, WorkItem>>(json) 
               ?? new Dictionary<Guid, WorkItem>();
    }

    public Guid Add(WorkItem workItem)
    {
        var id = Guid.NewGuid();
        workItem.Id = id;
        _workItems[id] = workItem;
        return id;
    }

    public WorkItem Get(Guid id) => _workItems.GetValueOrDefault(id);

    public WorkItem[] GetAll() => _workItems.Values.ToArray();

    public bool Update(WorkItem workItem)
    {
        if (!_workItems.ContainsKey(workItem.Id))
            return false;

        _workItems[workItem.Id] = workItem;
        return true;
    }

    public bool Remove(Guid id) => _workItems.Remove(id);

    public void SaveChanges()
    {
        var json = JsonConvert.SerializeObject(_workItems.Values);
        File.WriteAllText(FileName, json);
    }
}