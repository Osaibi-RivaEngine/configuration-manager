using ConfigManagerApp.API.Data;
using ConfigManagerApp.API.Models;
using System.Text.RegularExpressions;

namespace ConfigManagerApp.API.Repos
{
    public class ConfigRepo : IConfigRepo
    {
        /// <summary>
        /// Validate the key value pair string and convert it to a KeyValuePairModel object
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        KeyValuePairModel ValidateKeyValuePair(string setting)
        {
            var alphaNumericPattern = new Regex("^[a-zA-Z0-9]*$");

            if (string.IsNullOrWhiteSpace(setting))
            {
                throw new ArgumentNullException(nameof(setting), "Setting cannot be null or empty.");
            }

            if (setting.IndexOf('=') == -1)
            {
                throw new FormatException("Setting must contain an equal sign '='.");
            }

            string[] parts = setting.Split('=');

            if (parts.Length != 2)
            {
                throw new FormatException("Setting must contain exactly one equal sign '='.");
            }

            string name = parts[0].Trim();
            string value = parts[1].Trim();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(value))
            {
                throw new FormatException("Name or value cannot be empty.");
            }

            if (!alphaNumericPattern.IsMatch(name) || !alphaNumericPattern.IsMatch(value))
            {
                throw new FormatException("Only alphanumeric characters are allowed for the name and value.");
            }

            int id = ApplicationDbContext.Configs != null && ApplicationDbContext.Configs.Count > 0 ? ApplicationDbContext.Configs[ApplicationDbContext.Configs.Count - 1].Id + 1 : 1;
            return new KeyValuePairModel(id, name, value);
        }

        /// <summary>
        /// Get all the key value pair models
        /// </summary>
        /// <returns></returns>
        public IEnumerable<KeyValuePairModel> GetAll()
        {
            return ApplicationDbContext.Configs;
        } 

        /// <summary>
        /// Add a new key value pair model
        /// </summary>
        /// <param name="keyValuePair"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public KeyValuePairModel Add(string keyValuePair)
        {
            try
            {
                //validate the key value pair string and convert it to a KeyValuePairModel object
                var model = ValidateKeyValuePair(keyValuePair);
                ApplicationDbContext.Configs.Add(model);
                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Validate the key value pair string and filter the key value pair list
        /// </summary>
        /// <param name="keyValuePair"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        /// <exception cref="FormatException"></exception>
        public IEnumerable<KeyValuePairModel> Filter(string keyValuePair)
        {
            try
            {
                var model = ValidateKeyValuePair(keyValuePair);
                if (model == null || string.IsNullOrEmpty(model.Key))
                {
                    throw new FormatException("Invalide filter option");
                }

                var searchKey = model.Key.ToLower();
                if (searchKey != "name" && searchKey != "value")
                {
                    throw new FormatException("Invalide filter option");

                }

                var searchTerm = model.Value.ToLower();
                if (string.IsNullOrEmpty(searchTerm))
                {
                    return ApplicationDbContext.Configs;
                }
                 
                if (searchKey == "name")
                    return ApplicationDbContext.Configs.Where(p => p.Key.ToLower().Contains(searchTerm)).ToList();
                else
                    return ApplicationDbContext.Configs.Where(p => p.Value.ToLower().Contains(searchTerm)).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Sort the key value pair models by name or value
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public IEnumerable<KeyValuePairModel> Sort(SortType type)
        {
            if (ApplicationDbContext.Configs == null || ApplicationDbContext.Configs.Count == 0)
            {
                throw new Exception( "List is empty."); 
            }

            if (type == SortType.Name)
                return ApplicationDbContext.Configs.OrderBy(p => p.Key).ToList();
            else if(type == SortType.Value)
                return ApplicationDbContext.Configs.OrderBy(p => p.Value).ToList();
            else
                throw new Exception("Invalid sort type.");
        }

        /// <summary>
        /// Delete all the key value pair from the list
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void Delete()
        {
            if (ApplicationDbContext.Configs == null)
            {
                throw new Exception("List is already empty.");
            }

            ApplicationDbContext.Configs.Clear();
        }

        public enum SortType
        {
            Name,
            Value
        }
    }
}
