namespace IFCExporter.RevitCommand
{
    public class LevelReaderMock : ILevelReader
    {
        public List<LevelDTO> ReadAll()
        {
            var listLevels = new List<LevelDTO>();
            //for (int i = 1; i < 15; i++)
            //{
            //    var voidDto = new LevelDTO("C1_" + i.ToString() + $"_+{i},000" + "_этаж", i);
            //    listLevels.Add(voidDto);
            //}
            listLevels.Add(new LevelDTO("C1_" + 16.ToString() + $"_+16,000" + "_ТехЭтаж", 16));
            listLevels.Add(new LevelDTO($"+19,000" + "_Крыша", 17));
            listLevels.Add(new LevelDTO("C1"  + $"_+20,000" + "_Архитектурная высота", 18));

            return listLevels;
        }
    }
}
