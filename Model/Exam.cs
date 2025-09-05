namespace School_CRUD.Model
{
    public class Exam
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ClassRoomId { get; set; }
        public ClassRoom ClassRoom { get; set; }
        public DateTime Date { get; set; }
    }
}
