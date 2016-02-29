using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataFramework
{
    [Model]
    public class UserProfile : BaseModel
    {
        [DataField(PrimaryKey = true)]
        public int UserProfileId { get; set; }

        [DataField]
        public Guid UserId { get; set; }

        [DataField]
        public string FirstName { get; set; }

        [DataField]
        public string LastName { get; set; }

        [DataField]
        public DateTime? BirthDate { get; set; }

        [DataField(DeletedMark = true)]
        public bool IsDeleted { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        [FieldInfo(IsName = true)]
        public string FullName
        {
            get
            {
                if (string.IsNullOrEmpty(FirstName) && string.IsNullOrEmpty(LastName))
                    return UserName;
                return string.Format("{0} {1}", FirstName, LastName).Trim();
            }
        }

        public override string SqlQuery_GetAll
        {
            get
            {
                return @"
                    SELECT up.*, u.UserName, m.Email
                    FROM [UserProfile] up
                        JOIN [aspnet_Users] u ON u.UserId = up.UserId
                        JOIN [aspnet_Membership] m ON m.UserId = up.UserId
                ";
            }
        }
    }
}
