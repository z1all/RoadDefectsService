using RoadDefectsService.Core.Application.DTOs.UserService;
using RoadDefectsService.Core.Application.Models;

namespace RoadDefectsService.Core.Application.Interfaces.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Если флаг showOperators равен true, то помимо инспекторов будет показываться операторы
        /// </summary>
        Task<ExecutionResult<UserPagedDTO>> GetUsersAsync(UserFilterDTO userFilter, bool showOperators);

        /// <summary>
        /// Если флаг editOperator равен false, то если редактируемый пользователь является оператором, будет выдана ошибка Forbid
        /// </summary>
        Task<ExecutionResult> EditUserAsync(EditUserDTO editUser, Guid userId, bool editOperator);

        /// <summary>
        /// Если флаг deleteOperator равен false, то если удаляемый пользователь является оператором, будет выдана ошибка Forbid
        /// </summary>
        Task<ExecutionResult> DeleteUserAsync(Guid userId, bool deleteOperator);

        /// <summary>
        /// Создает оператора с дополнительной ролью администратора
        /// </summary>
        Task<ExecutionResult> CreateAdminAsync(CreateUserDTO createAdmin);

        /// <summary>
        /// Создает оператора 
        /// </summary>
        Task<ExecutionResult> CreateOperatorAsync(CreateUserDTO createOperator);

        /// <summary>
        /// Создает дорожного инспектора 
        /// </summary>
        Task<ExecutionResult> CreateRoadInspectorAsync(CreateUserDTO createRoadInspector);
    }
}
