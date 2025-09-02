using System.Text.Json.Serialization;

namespace School_CRUD.Model
{
    public class Student
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Class { get; set; }

        public DateOnly BirthDate { get; set; }

        public string Address { get; set; }

        public string ParentContactNo { get; set; }

        public int TeacherId { get; set; }
        [JsonIgnore]
        public Teacher Teacher { get; set; }
    }
}
