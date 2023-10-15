using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1
{
    public class PermissionsUtilities
    {
        private static PermissionStatus cameraStatus = PermissionStatus.Unknown;
        private static PermissionStatus storageStatusRead = PermissionStatus.Unknown;
        private static PermissionStatus storageStatusWrite = PermissionStatus.Unknown;


        public static async Task RequestPermissions()
        {
            if (cameraStatus != PermissionStatus.Granted)
            {
                cameraStatus = await Permissions.CheckStatusAsync<Permissions.Camera>();

                if (cameraStatus != PermissionStatus.Granted)
                {
                    cameraStatus = await Permissions.RequestAsync<Permissions.Camera>();
                }
            }

            if (storageStatusRead != PermissionStatus.Granted)
            {
                storageStatusRead = await Permissions.CheckStatusAsync<Permissions.StorageRead>();

                if (storageStatusRead != PermissionStatus.Granted)
                {
                    storageStatusRead = await Permissions.RequestAsync<Permissions.StorageRead>();
                }
            }


            if (storageStatusWrite != PermissionStatus.Granted)
            {
                storageStatusWrite = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();

                if (storageStatusWrite != PermissionStatus.Granted)
                {
                    storageStatusWrite = await Permissions.RequestAsync<Permissions.StorageWrite>();
                }
            }
        }
    }
}
