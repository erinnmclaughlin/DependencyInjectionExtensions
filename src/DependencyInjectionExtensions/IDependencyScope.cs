namespace DependencyInjectionExtensions
{
    public interface IDependencyScope { }

    public interface ISingleton : IDependencyScope { }
    public interface IScoped : IDependencyScope { }
    public interface ITransient : IDependencyScope { }
}
