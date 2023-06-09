﻿using Shhmoney.Models;
using Shhmoney.Data;

namespace Shhmoney.Services
{
    public class AuthorizationService
    {
        private readonly RoleRepository _roleRepository;

        public AuthorizationService()
        {
            _roleRepository = new RoleRepository();
        }

        public bool IsUserInRole(User user, string roleName)
        {
            Role role = _roleRepository.GetRoleByName(roleName);
            if (role != null)
            {
                return _roleRepository.GetRoleById(user.RoleId) == role;
            }
            return false;
        }
    }
}
