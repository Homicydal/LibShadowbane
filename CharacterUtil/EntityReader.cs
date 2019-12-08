using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace LibShadowbane.CharacterUtil
{
    static class EntityReader
    {
        public static Dictionary<TKey, TValue> EntityMap<TKey, TValue>(string jsonFile, Func<TValue, TKey> keyGetter)
        {
            var entityMap = new Dictionary<TKey, TValue>();
            string jsonPath = Path.Combine("CharacterUtil","Data",jsonFile);
         
            using (StreamReader file = File.OpenText(jsonPath))
            {
                JsonSerializer serializer = new JsonSerializer();
                List<TValue> entities;
                entities = (List<TValue>)serializer.Deserialize(file, typeof(List<TValue>));
                
                foreach (TValue entity in entities)
                {
                    entityMap[keyGetter(entity)] = entity;
                }

                return entityMap;
            }
        }
        
        public static Dictionary<string, T> EntityNames<T>(string jsonFile)
            where T : IEntity
            => EntityMap<string, T>(jsonFile, entity => entity.Name);
    }
}