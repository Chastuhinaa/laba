namespace laba
{
    public class StudentInfo
    {
        public string Name { get; set; }            
        public string Faculty { get; set; }          
        public string EducationLevel { get; set; }   
        public string Course { get; set; }           
        public double AverageRating { get; set; }    
        public List<SubjectInfo> Subjects { get; set; } = new List<SubjectInfo>(); 
    }
}
