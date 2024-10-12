namespace TaggerApi.Services.Interfaces;

using TaggerApi.DTOs;
using TaggerApi.Models;
using TaggerApi.Services;
using TaggerApi.Extensions;
using TaggerApi.Pagination;

public interface IPermissionService
{  
  Task<string> CreatePermission(PermissionDTO perDTO,  string userUid);

  Task<PermissionDTO> GetPermissions(int idvideo);

  Task<PermissionDTO> DelPermissions(string token);

}