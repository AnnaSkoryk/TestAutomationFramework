using Newtonsoft.Json.Linq;

namespace UITests
{
    public static class TestDataLoader
    {
        public static IEnumerable<TestCaseData> LoadTestData(string path)
        {
            var json = File.ReadAllText(path);
            var dataArray = JArray.Parse(json);

            foreach (var item in dataArray)
            {
                yield return new TestCaseData(item);
            }
        }
    }
}
