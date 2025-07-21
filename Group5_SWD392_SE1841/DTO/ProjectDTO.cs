namespace Group5_SWD392_SE1841.DTO
{
    public class ProjectDTO
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; } = string.Empty;
        public string ProjectStatus { get; set; } = string.Empty;
        public DateTime CreatedTime { get; set; }

        public ProjectDTO()
        {
        }

    }
}
