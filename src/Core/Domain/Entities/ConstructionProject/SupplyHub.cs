using System.Xml.Linq;

namespace Domain.Entities.ConstructionProject;

public sealed class SupplyHub
{
    private SupplyHub() { }

    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;

    public BuildingObject BuildingObject { get; private set; } = null!;
    public Guid BuildingObjectId { get; private set; }

    public Guid? ParentId { get; private set; }
    public SupplyHub? Parent { get; private set; } = null!;

    private readonly List<SupplyHub> _children = [];
    public IReadOnlyCollection<SupplyHub> Children => _children.AsReadOnly();

    public static SupplyHub Create(string name, string description, SupplyHub? parent = null)
    {
        SupplyHub supplyHub = new()
        {
            Name = name,
            Description = description,
        };

        if (parent != null)
        {
            supplyHub.Parent = parent;
            supplyHub.ParentId = parent.Id;
            parent._children.Add(supplyHub);
        }

        return supplyHub;
    }

    public void Update(string name, string description) 
    {
        if (!string.IsNullOrWhiteSpace(name))
            Name = name;
        if (!string.IsNullOrWhiteSpace(description))
            Description = description;
    }

    public void ChangeParent(SupplyHub? newParent)
    {
        if (newParent != null && WouldCreateCycle(this, newParent))
            throw new ArgumentException("Затычка ошибки");

        if (Parent != null)
            Parent._children.Remove(this);

        Parent = newParent;
        ParentId = newParent?.Id;

        if (newParent != null)
            newParent._children.Add(this);
    }

    public static bool WouldCreateCycle(SupplyHub node, SupplyHub potentialParent)
    {
        SupplyHub? current = potentialParent;
        while (current != null)
        {
            if (current.Id == node.Id)
                return true;
            current = current.Parent;
        }
        return false;
    }

    public void AttachTo(BuildingObject buildingObject)
    {
        if (buildingObject == null)
            throw new ArgumentNullException(nameof(buildingObject));

        BuildingObject = buildingObject;
        BuildingObjectId = buildingObject.Id;
    }
}
