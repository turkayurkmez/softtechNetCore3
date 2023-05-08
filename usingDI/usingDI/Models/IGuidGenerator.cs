namespace usingDI.Models
{
    public interface IGuidGenerator
    {
        Guid Guid { get; set; }
    }

    public interface ISingleton : IGuidGenerator
    {

    }

    public class Singleton : ISingleton
    {
        public Singleton()
        {
            Guid = Guid.NewGuid();
        }
        public Guid Guid { get; set; }
    }

    public interface ITransient : IGuidGenerator
    {

    }

    public class Transient : ITransient
    {
        public Transient()
        {
            Guid = Guid.NewGuid();
        }
        public Guid Guid { get; set; }
    }

    public interface IScoped : IGuidGenerator
    {

    }

    public class Scoped : IScoped
    {
        public Scoped()
        {
            Guid = Guid.NewGuid();
        }
        public Guid Guid { get; set; }
    }

    public class GuidService
    {
        public ISingleton Singleton { get; set; }
        public IScoped Scoped { get; set; }
        public ITransient Transient { get; set; }

        public GuidService(ISingleton singleton, IScoped scoped, ITransient transient)
        {
            Singleton = singleton;
            Scoped = scoped;
            Transient = transient;
        }
    }
}
