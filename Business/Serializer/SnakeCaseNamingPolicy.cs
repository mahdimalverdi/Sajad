using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Business.Serializer
{
    public class SnakeCaseNamingPolicy : JsonNamingPolicy
    {
        private readonly SnakeCaseNamingStrategy _newtonsoftSnakeCaseNamingStrategy
            = new SnakeCaseNamingStrategy();

        public static SnakeCaseNamingPolicy Instance { get; } = new SnakeCaseNamingPolicy();

        public override string ConvertName(string name)
        {
            /* A conversion to snake case implementation goes here. */

            return _newtonsoftSnakeCaseNamingStrategy.GetPropertyName(name, false);
        }
    }
}
