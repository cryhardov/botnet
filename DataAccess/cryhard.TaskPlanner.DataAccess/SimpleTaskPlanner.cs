public class SimpleTaskPlanner
{
    private readonly IWorkItemsRepository _repository;
    private WorkItem[] _plan;

    public SimpleTaskPlanner(IWorkItemsRepository repository)
    {
        _repository = repository;
    }

    public void CreatePlan()
    {
        _plan = _repository.GetAll().Where(task => !task.IsCompleted).ToArray();
    }

    public WorkItem[] GetPlan() => _plan;  
}