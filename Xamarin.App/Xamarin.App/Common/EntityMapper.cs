using Xamarin.App.Extensibility.Common;

namespace Xamarin.App.Common
{
    public class EntityMapper : IEntityMapper
    {
        public TDestination Map<TSource, TDestination>(TSource src) => AutoMapper.Mapper.Map<TSource, TDestination>(src);
    }
}