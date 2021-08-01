namespace RecordApi.Shared.Model
{
    public class RecordDto : IRecord
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string FavoriteColor { get; set; }

        public string DateOfBirth { get; set; }
    }
}