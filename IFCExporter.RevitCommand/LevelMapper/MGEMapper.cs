using System.Text.RegularExpressions;

namespace IFCExporter.RevitCommand
{
    public class MGEMapper : ILevelMapper
    {
        private readonly string regexPattern = @"^((?<section>[^_]+)_)?((?<number>[^_]+)_)?((?<elevation>[^_]+)_)(?<name>[^_]+)$";
        //private readonly string inputTemplate = "section_number_elevation_name";
        private readonly string outputTemplate = "section_number_name_baseInfo";

        public string Map(string inputLevelName)
        {
            if (string.IsNullOrEmpty(inputLevelName))
                throw new ArgumentException($"\"{nameof(inputLevelName)}\" null or empty", nameof(inputLevelName));

            var levelInfoDict = Parse(inputLevelName);
            var outputLevelName = CreateOut(levelInfoDict);

            return outputLevelName;
        }


        private Dictionary<string, string> Parse(string inputString)
        {
            var match = Regex.Match(inputString, regexPattern);
            if (!match.Success)
                throw new Exception("match not found");

            var resultDict = match.Groups
                .Cast<Group>()
                .Where(g => !string.IsNullOrEmpty(g.Name) && g.Name != "0")
                .ToDictionary(g => g.Name, g => g.Value.Trim());

            return resultDict;
        }
        private string CreateOut(Dictionary<string, string> levelInfoDict)
        {
            AddNewField(levelInfoDict);

            return FillOutputTemplate(levelInfoDict);
        }

        private void AddNewField(Dictionary<string, string> levelInfoDict)
        {
            if (IsBaseLevel(levelInfoDict.Values.Last()))
                levelInfoDict.Add("baseInfo", "Основной");
            //если уровень не основной, убираем лишнее в шаблоне
            else
                levelInfoDict.Add("_baseInfo", "");
        }

        private bool IsBaseLevel(string nameLavel)
        {
            return Regex.IsMatch(nameLavel, @"этаж\w*\b", RegexOptions.IgnoreCase) &&
               !Regex.IsMatch(nameLavel, @"тех\w+\b", RegexOptions.IgnoreCase);
        }

        private string FillOutputTemplate(Dictionary<string, string> levelInfoDict)
        {
            var result = outputTemplate;
            foreach (var kv in levelInfoDict)
                result = ReplaceByKey(result, kv);
            return result;
        }

        private string ReplaceByKey(string replacedString,KeyValuePair<string, string> kv)
        {
            //если выше мы уже подготовились и добавили "_"
            if (kv.Value.Any() || kv.Key.Contains('_'))
                return replacedString.Replace(kv.Key, kv.Value);
            //если нет значения убираем лишнии текст из шаблона 
            else
                return replacedString.Replace(kv.Key + "_", "");
        }
    }
}
