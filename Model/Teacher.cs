namespace School_CRUD.Model
{
    public class Teacher
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Experience { get; set; }

        public string Qualifications { get; set; }

        public string ContactNo { get; set; }

        public bool isClassTeacher { get; set; }

        public string Subject { get; set; }
    }
}
