using AutoMapper;
using System.Reflection;

namespace SocialMedia.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void ApplyMappingFromAssembly(Assembly assembly)
        {
            var exportedTypes = assembly.GetExportedTypes();

            var mapFromTypes = exportedTypes
                                .Where(type => type.GetInterfaces().Any(@interface =>
                                    @interface.IsGenericType &&
                                    @interface.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                                .ToList();

            mapFromTypes.ForEach(type =>
            {
                object? instance = Activator.CreateInstance(type);
                MethodInfo? mapMethod = type.GetMethod("Mapping") ?? type.GetInterface("IMapFrom`1").GetMethod("Mapping");
                mapMethod?.Invoke(instance, new object[] { this });
            });
        }
    }
}
