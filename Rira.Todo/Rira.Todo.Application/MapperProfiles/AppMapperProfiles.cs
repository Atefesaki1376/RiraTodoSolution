using Rira.Todo.Application.Contracts.Dtos.TodoItems;

namespace Rira.Todo.Application.MapperProfiles
{
    public class AppMapperProfiles : Profile
    {
        private static readonly Type[] AllTypes = System.Reflection.Assembly.GetExecutingAssembly().GetTypes();

        public AppMapperProfiles()
        {
            //EntityBaseToDtoBaseAndReverse();

            CreateMap<AppSettings, AppSettingsDto>()
                .ReverseMap();

            CreateMap<TodoItem, TodoItemDto>()
                .ReverseMap();
        }


        /// <summary>
        /// Automates reverse mapping between EntityBase<T> and DtoBase<T>.
        /// Ensure proper adherence to naming conventions for entity and DTO types.
        /// </summary>
        private void EntityBaseToDtoBaseAndReverse()
        {
            var dtoBaseType = typeof(DtoBase<>);
            var entityBaseType = typeof(EntityBase<>);

            foreach (var dtoType in AllTypes.Where(t => t.BaseType?.IsGenericType == true &&
                                                        t.BaseType.GetGenericTypeDefinition() == dtoBaseType))
            {
                var entityType = AllTypes.FirstOrDefault(t => t.BaseType?.IsGenericType == true &&
                                                              t.BaseType.GetGenericTypeDefinition() == entityBaseType &&
                                                              t.Name == dtoType.Name.Replace("Dto", ""));
                if (entityType != null)
                {
                    CreateMap(dtoType, entityType);
                    CreateMap(entityType, dtoType);
                }
            }
        }
    }
}