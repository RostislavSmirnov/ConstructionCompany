namespace Domain.Entities.ConstructionProject;

public sealed class BuildingObject
{
    private BuildingObject() { }

    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;

    private readonly List<SupplyHub> _supplyHubs  = [];
    public IReadOnlyCollection<SupplyHub> SupplyHubs => _supplyHubs.AsReadOnly();

    public static BuildingObject Create(string name, string description)
    {
        BuildingObject buildingObject = new()
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description
        };
        return buildingObject;
    }

    public void Update(string name, string description)
    {
        if (!string.IsNullOrWhiteSpace(name))
            Name = name;
        if (!string.IsNullOrWhiteSpace(description))
            Description = description;
    }

    public void AddSupplyHub(SupplyHub supplyHub)
    {
        if (supplyHub == null)
            throw new ArgumentNullException("Временная затычка ошибки");

        _supplyHubs.Add(supplyHub);
    }

    public void RemoveSupplyHub(SupplyHub supplyHub)
    {
        if (supplyHub == null)
            throw new ArgumentNullException("Временная затычка ошибки");
        _supplyHubs.Remove(supplyHub);
    }
}

