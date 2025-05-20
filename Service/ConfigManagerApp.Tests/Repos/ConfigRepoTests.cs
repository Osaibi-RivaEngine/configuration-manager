using ConfigManagerApp.API.Data;
using ConfigManagerApp.API.Repos;

namespace ConfigManagerApp.Tests.Repos
{
    public class ConfigRepoTests
    {
        private readonly ConfigRepo _configRepo;

        public ConfigRepoTests()
        {
            _configRepo = new ConfigRepo();
        }

        [Fact]
        public void GetAll_ShouldReturnEmptyList_WhenNoConfigsExist()
        {
            _configRepo.Delete();

            var result = _configRepo.GetAll();

            Assert.Empty(result);
        }

        [Fact]
        public void Add_ShouldAddNewConfig_WhenValidKeyValuePairProvided()
        {
            string validConfig = "testKey=testValue";

            var result = _configRepo.Add(validConfig);

            Assert.NotNull(result);
            Assert.Equal("testKey", result.Key);
            Assert.Equal("testValue", result.Value);
        }

        [Fact]
        public void Add_ShouldThrowFormatException_WhenInvalidFormatProvided()
        {
            string invalidKeyValuePair = "invalidFormat";

            Assert.Throws<FormatException>(() => _configRepo.Add(invalidKeyValuePair));
        }

        [Fact]
        public void Add_ShouldThrowFormatException_WhenNonAlphanumericCharactersProvided()
        {
            string invalidKeyValuePair = "test/key=test.value";

            Assert.Throws<FormatException>(() => _configRepo.Add(invalidKeyValuePair));
        }

        [Fact]
        public void Filter_ShouldReturnMatchingConfigs_WhenFilteringByName()
        {
            _configRepo.Delete();
            _configRepo.Add("test1=value1");
            _configRepo.Add("test2=value2");
            _configRepo.Add("randomNname=value3");

            var result = _configRepo.Filter("name=test");

            Assert.Equal(2, result.Count());
            Assert.All(result, item => Assert.Contains("test", item.Key.ToLower()));
        }

        [Fact]
        public void Filter_ShouldReturnMatchingConfigs_WhenFilteringByValue()
        {
            _configRepo.Delete();
            _configRepo.Add("key1=test1");
            _configRepo.Add("key2=test2");
            _configRepo.Add("key3=randomValue");

            var result = _configRepo.Filter("value=test");

            Assert.Equal(2, result.Count());
            Assert.All(result, item => Assert.Contains("test", item.Value.ToLower()));
        }

        [Fact]
        public void Filter_ShouldThrowFormatException_WhenInvalidFilterOptionProvided()
        {
            string invalidFilter = "invalid=test";

            Assert.Throws<FormatException>(() => _configRepo.Filter(invalidFilter));
        }

        [Fact]
        public void Sort_ShouldSortByName_WhenSortTypeIsName()
        {
            _configRepo.Delete();
            _configRepo.Add("z=value1");
            _configRepo.Add("a=value2");
            _configRepo.Add("1=value3");

            var result = _configRepo.Sort(ConfigRepo.SortType.Name).ToList();

            Assert.Equal("1", result[0].Key);
            Assert.Equal("a", result[1].Key);
            Assert.Equal("z", result[2].Key);
        }

        [Fact]
        public void Sort_ShouldSortByValue_WhenSortTypeIsValue()
        {
            _configRepo.Delete();
            _configRepo.Add("key1=z");
            _configRepo.Add("key2=a");
            _configRepo.Add("key3=1");

            var result = _configRepo.Sort(ConfigRepo.SortType.Value).ToList();

            Assert.Equal("1", result[0].Value);
            Assert.Equal("a", result[1].Value);
            Assert.Equal("z", result[2].Value);
        }

        [Fact]
        public void Sort_ShouldThrowException_WhenListIsEmpty()
        {
            _configRepo.Delete();

            Assert.Throws<Exception>(() => _configRepo.Sort(ConfigRepo.SortType.Name));
        }

        [Fact]
        public void Delete_ShouldClearAllConfigs()
        {
            _configRepo.Add("test1=value1");
            _configRepo.Add("test2=value2");

            _configRepo.Delete();

            Assert.Empty(_configRepo.GetAll());
        }
    }
} 