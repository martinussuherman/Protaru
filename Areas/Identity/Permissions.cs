using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using Itm.Misc;

namespace Protaru.Identity
{
    public static class Permissions
    {
        public const string All = "Permissions.All";

        public const string CustomClaimTypes = "ProtaruClaims";

        public static class Users
        {
            public const string All = "Permissions.Users.All";

            public const string List = "Permissions.Users.List";

            public const string Create = "Permissions.Users.Create";
            public const string Edit = "Permissions.Users.Edit";
            public const string Delete = "Permissions.Users.Delete";
        }

        public static class RtrwT50
        {
            public const string Create = "Permissions.RtrwT50.Create";
            public const string Edit = "Permissions.RtrwT50.Edit";
            public const string Delete = "Permissions.RtrwT50.Delete";
        }

        public static class RtrwT51
        {
            public const string Create = "Permissions.RtrwT51.Create";
            public const string Edit = "Permissions.RtrwT51.Edit";
            public const string Delete = "Permissions.RtrwT51.Delete";
        }

        public static class RtrwT52
        {
            public const string Create = "Permissions.RtrwT52.Create";
            public const string Edit = "Permissions.RtrwT52.Edit";
            public const string Delete = "Permissions.RtrwT52.Delete";
        }

        public static class RdtrT51
        {
            public const string Create = "Permissions.RdtrT51.Create";
            public const string Edit = "Permissions.RdtrT51.Edit";
            public const string Delete = "Permissions.RdtrT51.Delete";
        }

        public static class RdtrT52
        {
            public const string Create = "Permissions.RdtrT52.Create";
            public const string Edit = "Permissions.RdtrT52.Edit";
            public const string Delete = "Permissions.RdtrT52.Delete";
        }

        public static class RtrwnT51
        {
            public const string Create = "Permissions.RtrwnT51.Create";
            public const string Edit = "Permissions.RtrwnT51.Edit";
            public const string Delete = "Permissions.RtrwnT51.Delete";
        }

        public static class RtrwnT52
        {
            public const string Create = "Permissions.RtrwnT52.Create";
            public const string Edit = "Permissions.RtrwnT52.Edit";
            public const string Delete = "Permissions.RtrwnT52.Delete";
        }

        public static class RtrPulauT51
        {
            public const string Create = "Permissions.RtrPulauT51.Create";
            public const string Edit = "Permissions.RtrPulauT51.Edit";
            public const string Delete = "Permissions.RtrPulauT51.Delete";
        }

        public static class RtrPulauT52
        {
            public const string Create = "Permissions.RtrPulauT52.Create";
            public const string Edit = "Permissions.RtrPulauT52.Edit";
            public const string Delete = "Permissions.RtrPulauT52.Delete";
        }

        public static class RtrKsnT51
        {
            public const string Create = "Permissions.RtrKsnT51.Create";
            public const string Edit = "Permissions.RtrKsnT51.Edit";
            public const string Delete = "Permissions.RtrKsnT51.Delete";
        }

        public static class RtrKsnT52
        {
            public const string Create = "Permissions.RtrKsnT52.Create";
            public const string Edit = "Permissions.RtrKsnT52.Edit";
            public const string Delete = "Permissions.RtrKsnT52.Delete";
        }

        public static class RdtrKpnT51
        {
            public const string Create = "Permissions.RdtrKpnT51.Create";
            public const string Edit = "Permissions.RdtrKpnT51.Edit";
            public const string Delete = "Permissions.RdtrKpnT51.Delete";
        }

        public static class RdtrKpnT52
        {
            public const string Create = "Permissions.RdtrKpnT52.Create";
            public const string Edit = "Permissions.RdtrKpnT52.Edit";
            public const string Delete = "Permissions.RdtrKpnT52.Delete";
        }
    }

    public class PermissionListFactory
    {
        public static List<PermissionGroupInfo> ViewList { get; set; }

        public static List<PermissionInfo> LookupList { get; set; }

        public void BuildViewList()
        {
            if (ViewList != null)
            {
                return;
            }

            permissionCounter = 0;
            groupCounter = 0;

            ViewList = new List<PermissionGroupInfo>
            {
                CreateGroupInfo(
                    "RTRW T5-0",
                    Permissions.RtrwT50.Create,
                    Permissions.RtrwT50.Edit,
                    Permissions.RtrwT50.Delete),
                CreateGroupInfo(
                    "RTRW T5-1",
                    Permissions.RtrwT51.Create,
                    Permissions.RtrwT51.Edit,
                    Permissions.RtrwT51.Delete),
                CreateGroupInfo(
                    "RTRW T5-2",
                    Permissions.RtrwT52.Create,
                    Permissions.RtrwT52.Edit,
                    Permissions.RtrwT52.Delete),
                CreateGroupInfo(
                    "RDTR T5-1",
                    Permissions.RdtrT51.Create,
                    Permissions.RdtrT51.Edit,
                    Permissions.RdtrT51.Delete),
                CreateGroupInfo(
                    "RDTR T5-2",
                    Permissions.RdtrT52.Create,
                    Permissions.RdtrT52.Edit,
                    Permissions.RdtrT52.Delete),
                CreateGroupInfo(
                    "RTRWN T5-1",
                    Permissions.RtrwnT51.Create,
                    Permissions.RtrwnT51.Edit,
                    Permissions.RtrwnT51.Delete),
                CreateGroupInfo(
                    "RTRWN T5-2",
                    Permissions.RtrwnT52.Create,
                    Permissions.RtrwnT52.Edit,
                    Permissions.RtrwnT52.Delete),
                CreateGroupInfo(
                    "RTR PULAU/KEP T5-1",
                    Permissions.RtrPulauT51.Create,
                    Permissions.RtrPulauT51.Edit,
                    Permissions.RtrPulauT51.Delete),
                CreateGroupInfo(
                    "RTR PULAU/KEP T5-2",
                    Permissions.RtrPulauT52.Create,
                    Permissions.RtrPulauT52.Edit,
                    Permissions.RtrPulauT52.Delete),
                CreateGroupInfo(
                    "RTR KSN T5-1",
                    Permissions.RtrKsnT51.Create,
                    Permissions.RtrKsnT51.Edit,
                    Permissions.RtrKsnT51.Delete),
                CreateGroupInfo(
                    "RTR KSN T5-2",
                    Permissions.RtrKsnT52.Create,
                    Permissions.RtrKsnT52.Edit,
                    Permissions.RtrKsnT52.Delete),
                CreateGroupInfo(
                    "RDTR KPN T5-1",
                    Permissions.RdtrKpnT51.Create,
                    Permissions.RdtrKpnT51.Edit,
                    Permissions.RdtrKpnT51.Delete),
                CreateGroupInfo(
                    "RDTR KPN T5-2",
                    Permissions.RdtrKpnT52.Create,
                    Permissions.RdtrKpnT52.Edit,
                    Permissions.RdtrKpnT52.Delete)
            };
        }

        public void BuildLookupList()
        {
            if (ViewList == null || LookupList != null)
            {
                return;
            }

            LookupList = new List<PermissionInfo>();

            foreach (PermissionGroupInfo group in ViewList)
            {
                LookupList.AddRange(group.PermissionList);
            }
        }

        public List<bool> BuildUserSavedPermissionList(IList<Claim> claimList)
        {
            IEnumerable<Claim> userClaim = claimList
                .Where(e => e.Type == Permissions.CustomClaimTypes);

            List<bool> result = new List<bool>(LookupList.Count);

            for (int count = 0; count < LookupList.Count; count++)
            {
                result.Add(false);
            }

            foreach (Claim claim in userClaim)
            {
                int index = LookupList.FindIndex(e => e.Name == claim.Value);

                if (index != -1)
                {
                    result[index] = true;
                }
            }

            return result;
        }

        private PermissionGroupInfo CreateGroupInfo(
            string caption,
            string createName,
            string editName,
            string deleteName)
        {
            PermissionGroupInfo result = new PermissionGroupInfo
            {
                Caption = caption,
                Sequence = groupCounter++,
                PermissionList = new List<PermissionInfo>
                {
                    CreatePermission(createName),
                    EditPermission(editName),
                    DeletePermission(deleteName)
                }
            };

            return result;
        }

        private PermissionInfo CreatePermission(string createName)
        {
            return new PermissionInfo
            {
                Caption = "Tambah",
                Sequence = 1,
                Code = permissionCounter++,
                Name = createName
            };
        }
        private PermissionInfo EditPermission(string editName)
        {
            return new PermissionInfo
            {
                Caption = "Ubah",
                Sequence = 2,
                Code = permissionCounter++,
                Name = editName
            };
        }
        private PermissionInfo DeletePermission(string deleteName)
        {
            return new PermissionInfo
            {
                Caption = "Hapus",
                Sequence = 3,
                Code = permissionCounter++,
                Name = deleteName
            };
        }

        private static int groupCounter;
        private static int permissionCounter;
    }

    public class PermissionGroupInfo
    {
        public string Caption { get; set; }

        public int Sequence { get; set; }

        public List<PermissionInfo> PermissionList { get; set; }
    }

    public class PermissionInfo
    {
        public string Caption { get; set; }

        public int Sequence { get; set; }

        public int Code { get; set; }

        public string Name { get; set; }
    }
}