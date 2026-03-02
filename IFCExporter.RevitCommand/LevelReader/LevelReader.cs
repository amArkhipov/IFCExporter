using Autodesk.Revit.DB;

namespace IFCExporter.RevitCommand
{
    public class LevelReader : ILevelReader
    {
        private readonly Document document;

        public LevelReader(Document document)
        {
            this.document = document;
        }

        public List<LevelDTO> ReadAll()
        {
            List<Level> levelInstances = ReadModel();
            List<LevelDTO> levelDTO = MapAllToDTO(levelInstances);
            return levelDTO;
        }

        private List<Level> ReadModel()
        {
            using (var collector = new FilteredElementCollector(document))
            {
                return collector
                    .OfCategory(BuiltInCategory.OST_Levels)
                    .WhereElementIsNotElementType()
                    .OfClass(typeof(Level))
                    .OfType<Level>()
                    .ToList();
            }
        }
        private List<LevelDTO> MapAllToDTO(List<Level> levelInstances)
        {
            if (!levelInstances.Any())
                throw new Exception("no level instances");

            List<LevelDTO> levelDTO = levelInstances.Select(v => Map(v)).OfType<LevelDTO>().ToList();

            if (levelDTO is null)
                throw new Exception("list levelDTO is null");

            return levelDTO;
        }

        private LevelDTO Map(Level v)
        {
            return new LevelDTO(v.Name, v.Id.IntegerValue);
        }
    }
}
