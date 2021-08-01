using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordApi.Shared.Model
{
    public class Record : IRecord
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string FavoriteColor { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
    }
}
