namespace School_CRUD.Model
{
    public class ClassRoom
    {
        public int Id { get; set; }
        public string Name { get; set; }  
        public string Section { get; set; }
        public int TeacherId { get; set; } 
        public Teacher Teacher { get; set; }
        public ICollection<Student> Students { get; set; }
    }

}
