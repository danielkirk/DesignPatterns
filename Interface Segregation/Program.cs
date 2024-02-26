public class Demo
{
    /*
     * Interface segregation is the idea that a client shouldn't depend on functionality that it doesn't use.
     * --Basically, if you have too much functionality within one interface, break it down into separate ones.
     */
    public class Document
    {

    }

    public interface IMachine
    {
        void Print(Document document);
        void Scan(Document document);
        void Fax(Document document);
    }

    // Bad practice
    public class MultiFunctionPrinter : IMachine
    {
        public void Fax(Document document)
        {
            throw new NotImplementedException();
        }

        public void Print(Document document)
        {
            throw new NotImplementedException();
        }

        public void Scan(Document document)
        {
            throw new NotImplementedException();
        }
    }

    public class OldFashionPrinter : IMachine
    {
        public void Fax(Document document)
        {
            throw new NotImplementedException();
        }

        public void Print(Document document)
        {
            throw new NotImplementedException();
        }

        public void Scan(Document document)
        {
            throw new NotImplementedException();
        }
    }

    //Best practice
    public interface IPrinter
    {
        void Print(Document document);
    }

    public interface IScanner
    {
        void Scan(Document document);
    }

    public class PhotoCopier : IPrinter, IScanner
    {
        public void Print(Document document)
        {
            throw new NotImplementedException();
        }

        public void Scan(Document document)
        {
            throw new NotImplementedException();
        }
    }

    public interface IMultiFunctionDevice : IScanner, IPrinter
    {

    }

    /// <summary>
    /// Decorator pattern implementation to break up interfaces into smaller interfaces
    /// </summary>
    public class MultiFunctionMachine : IMultiFunctionDevice
    {
        private readonly IScanner _scanner;
        private readonly IPrinter _printer;
        public MultiFunctionMachine()
        {

        }
        public MultiFunctionMachine(IPrinter printer, IScanner scanner)
        {
            _printer = printer;
            _scanner = scanner;
        }

        public void Print(Document document)
        {
            _printer.Print(document);
        }

        public void Scan(Document document)
        {
            _scanner.Scan(document);
        }
        
    }
    static void Main(string[] args)
    {

    }
}