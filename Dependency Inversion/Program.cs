/*
 * Dependency inversion states that high level modules shouldn't depend on the implementation details of lower level modules
 */

public enum Relationship
{
    Parent, Child, Sibling
}

public class Person
{
    public string Name;

}

public interface IRelationshipBrowser
{
    IEnumerable<Person> FindAllChildrenOf(string name);
}

//low-level

public class Relationships : IRelationshipBrowser
{
    private List<(Person, Relationship, Person)> relations
        = new List<(Person, Relationship, Person)> ();

    public void AddParentAndChild(Person parent, Person child)
    {
        relations.Add((parent, Relationship.Parent, child));
        relations.Add((child, Relationship.Child, parent));
    }

    public IEnumerable<Person> FindAllChildrenOf(string name)
    {
        return relations.Where(x => x.Item1.Name == name && x.Item2 == Relationship.Parent).Select(x => x.Item3);
    }

    //public List<(Person, Relationship, Person)> Relations => relations;
}

public class Research
{
    public Research(IRelationshipBrowser browser)
    {
        foreach(var p in browser.FindAllChildrenOf("John"))
        {
            Console.WriteLine($"John has a child of {p.Name}");
        }
    }
    //public Research(Relationships relationships)
    //{
    //    var relations = relationships.Relations;

    //    foreach(var r in relations.Where(x => x.Item1.Name == "John" && x.Item2 == Relationship.Parent))
    //    {
    //        Console.WriteLine($"John has a child called {r.Item3.Name}");
    //    }
    //}
    static void Main(string[] args)
    {
        var parent = new Person { Name = "John" };
        var child1 = new Person { Name = "Chris" };
        var child2 = new Person { Name = "Mary" };

        var relationships = new Relationships();

        relationships.AddParentAndChild(parent, child1);
        relationships.AddParentAndChild(parent, child2);

        new Research(relationships);
    }
}