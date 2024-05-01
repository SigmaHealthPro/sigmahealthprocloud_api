using BAL.Constant;
using BAL.Request;
using BAL.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interfaces
{
    public interface IUserService
    {
        Task<ApiResponse<UserDetailsResponse>> GetUsers(GetDataByCountRequest requestObject);
        Task<ApiResponse<PersonDetailsResponse>> GetPersons(GetDataByCountRequest requestObject);
        Task<ApiResponse<PersonDetailsResponse>> GetPersonsById(Guid id);
        Task<ApiResponse<string>> CreateUser(CreateUserRequest userRequest);
        Task<ApiResponse<string>> UpdateUser(UpdateUserRequest userRequest);
        Task<ApiResponse<string>> DeleteUser(Guid Id);
        Task<ApiResponse<string>> UpdateUserStatus(UpdateUserStatusRequest userRequest);
        Task<ApiResponse<UserDetailsResponse>> GetUserById(Guid Id);
    }
}
