namespace ModelLayer.DTO
{
    public class GetUpdateGameInfo
    {
        public EditGameBoardInfo EditGameBoardInfo { get; set; }

        public List<TypeDTO> AllTypes { get; set; }

        public List<CategoryDTO> AllCategories { get; set; }
    }
}
