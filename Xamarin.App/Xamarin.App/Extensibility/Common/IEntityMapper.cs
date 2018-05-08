namespace Xamarin.App.Extensibility.Common
{
    public interface IEntityMapper
    {
        TDestination Map<TSource, TDestination>(TSource src);
    }
}