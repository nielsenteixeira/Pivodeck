using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Pivodeck
{
    /// <summary>
    /// Helper para enums
    /// </summary>
    /// <typeparam name="T">Tipo do enum</typeparam>
    public class EnumUtil<T>
    {
        /// <summary>
        /// Retorna um dicionario com o valores do enum
        /// </summary>
        /// <typeparam name="J">tipo alternativo do enum</typeparam>
        /// <returns>Dictionary(string,  tipo definido)</returns>
        public Dictionary<string, J> ReturnAsDictionary<J>()
        {
            var dic = new Dictionary<string, J>();
            var names = Enum.GetNames(typeof(T));
            var values = Enum.GetValues(typeof(T));
            for (int i = 0; i < Enum.GetValues(typeof(T)).Length; i++)
            {
                dic.Add(names[i], (J)values.GetValue(i));
            }
            return dic;
        }

        /// <summary>
        /// Retorna um dicionario do enum que possui DescriptionAttribute no seus elementos
        /// </summary>
        /// <typeparam name="J">tipo alternativo do enum</typeparam>
        /// <returns>Dictionary(string,  tipo definido)</returns>
        public Dictionary<string, J> ReturnAsDictionaryWithDescription<J>()
        {
            var dic = new Dictionary<string, J>();
            var values = Enum.GetValues(typeof(T));
            var type = typeof(T);
            var fieldInfos = type.GetFields();
            var names = fieldInfos.Select(fieldInfo => fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true)[0].ToString());
            for (int i = 0; i < Enum.GetValues(typeof(T)).Length; i++)
                dic.Add(names.ElementAt(i), values.OfType<J>().ElementAt(i));

            return dic;
        }
    }
}
