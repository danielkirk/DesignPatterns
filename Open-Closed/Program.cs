public class Demo
{
    /*
     * States that parts of a system/sub-systems should be open for extension, but closed for modification
     * 
     * For example, ISpecification, IFilter
     * 
     * These interfaces allow for us to abstract implementations instead of modifying the source code that
     * may already be shipped to a customer.
     */
    public enum Color
    {
        Red, Green, Blue
    }

    public enum Size
    {
        Small, Medium, Large, Yuge
    }

    public class Product
    {
        public string Name;
        public Color Color;
        public Size Size;

        public Product(string name, Color color, Size size)
        {
            if(name == null)
            {
                throw new ArgumentNullException(paramName: nameof(name));
            }

            Name = name;
            Color = color;
            Size = size;
        }
    }

    public class ProductFilter
    {
        public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
        {
            foreach(var p in products)
            {
                if (p.Size == size)
                    yield return p;
            }
        }

        public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
        {
            foreach (var p in products)
            {
                if (p.Color == color)
                    yield return p;
            }
        }
    }

    public interface ISpecification<T>
    {
        bool IsSatisfied(T t);
    }

    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
    }

    public class ColorSpecification : ISpecification<Product>
    {
        private Color color;

        public ColorSpecification(Color color)
        {
            this.color = color;
        }
        public bool IsSatisfied(Product t)
        {
            return t.Color == color;
        }
    }

    public class SizeSpecification : ISpecification<Product>
    {
        private Size size;

        public SizeSpecification(Size size)
        {
             this.size = size;
        }

        public bool IsSatisfied(Product t)
        {
            return t.Size == size;
        }
    }

    public class AndSpecification<T> : ISpecification<T>
    {
        ISpecification<T> first, second;

        public AndSpecification(ISpecification<T> first, ISpecification<T> second)
        {
            this.first = first;
            this.second = second;
        }

        public bool IsSatisfied(T t)
        {
            return first.IsSatisfied(t) && second.IsSatisfied(t);
        }
    }

    public class BetterFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
        {
            foreach(var i in items)
            {
                if(spec.IsSatisfied(i)) yield return i;
            }
        }
    }

    static void Main(string[] args)
    {
        var apple = new Product("Apple", Color.Green, Size.Small);
        var tree = new Product("Tree", Color.Green, Size.Large);
        var house = new Product("House", Color.Blue, Size.Large);

        Product[] products = {apple, tree, house};

        var pf = new ProductFilter();

        Console.WriteLine("Green Products (old): ");

        foreach(var p in pf.FilterByColor(products, Color.Green))
        {
            Console.WriteLine($"- {p.Name} is green");
        }

        var bf = new BetterFilter();
        Console.WriteLine("Green Product(new):");
        foreach(var p in bf.Filter(products, new ColorSpecification(Color.Green)))
        {
            Console.WriteLine($" - {p.Name} is green");
        }

        Console.WriteLine("Large Product(new):");
        foreach (var p in bf.Filter(products, new SizeSpecification(Size.Large)))
        {
            Console.WriteLine($" - {p.Name} is LARGE");
        }

        Console.WriteLine("Large Blue Product(new):");
        foreach (var p in bf.Filter(products, new AndSpecification<Product>(
            new SizeSpecification(Size.Large)
            , new ColorSpecification(Color.Blue))))
        {
            Console.WriteLine($" - {p.Name} is Large AND Blue");
        }

    }
}