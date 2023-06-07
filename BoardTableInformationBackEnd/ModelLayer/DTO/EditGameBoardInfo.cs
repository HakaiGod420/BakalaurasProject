namespace ModelLayer.DTO
{
    public class EditGameBoardInfo
    {
        public int GameBoardId { get; set; }
        public string Title { get; set; }
        public int PlayerCount { get; set; }
        public int? PlayingTime { get; set; }
        public int PlayableAge { get; set; }
        public string Description { get; set; }
        public bool IsBlocked { get; set; }
        public string? Rules { get; set; }
        public List<TypeDTO> SelectedTypes { get; set; }
        public List<CategoryDTO> SelectedCategories { get; set; }
    }
}
