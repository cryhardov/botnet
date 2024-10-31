using System;
using System.Linq;
using Moq;
using Xunit;
using cryhard.TaskPlanner.DataAccess.Abstractions;
using cryhard.TaskPlanner.DataAccess;

public class SimpleTaskPlannerTests
{
    [Fact]
    public void CreatePlan_ShouldIncludeOnlyIncompleteTasks()
    {
        var mockRepository = new Mock<IWorkItemsRepository>();
        mockRepository.Setup(repo => repo.GetAll()).Returns(new[]
        {
            new WorkItem { Id = Guid.NewGuid(), Name = "Task 1", IsCompleted = false },
            new WorkItem { Id = Guid.NewGuid(), Name = "Task 2", IsCompleted = true }
        });

        var planner = new SimpleTaskPlanner(mockRepository.Object);

        planner.CreatePlan();
        var result = planner.GetPlan(); 

        Assert.Single(result);
        Assert.Equal("Task 1", result.First().Name);
    }
}