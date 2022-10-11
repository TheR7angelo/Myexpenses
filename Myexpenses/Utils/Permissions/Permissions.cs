using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MyExpenses.Utils
{
    public class Permission
    {
        public static bool Camera() => Ask<Permissions.Camera>().Result;
        public static bool Location() => Ask<Permissions.LocationWhenInUse>().Result;

        private static async Task<bool> Ask<TPermission>() where TPermission : Permissions.BasePermission, new()
        {
            try
            {
                var permissionStatus = await Permissions.CheckStatusAsync<TPermission>();

                if (!permissionStatus.Equals(PermissionStatus.Granted))
                {
                    permissionStatus = await Permissions.RequestAsync<TPermission>();
                }
                return permissionStatus.Equals(PermissionStatus.Granted);
            }
            catch (Exception)
            {
                /* pass */
            }

            return false;
        }
    }
}