using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordApi.Shared.Model
{
     public interface IRecord
    {
        string LastName { get; set; }
        string FirstName { get; set; }
        string Email { get; set; }
        string FavoriteColor { get; set; }
        DateTimeOffset DateOfBirth { get; set; }

           
    }
}
