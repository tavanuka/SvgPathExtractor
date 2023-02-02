using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SvgPathExtractor
{
    public class RegexService
    {
        private readonly Regex pattern;
        public RegexService()
        {
            pattern = new Regex(@"[<path]*\sd=(?:[""'])([\s\S]*?)(?:[""']\S*)|(?:[""']/>)", RegexOptions.IgnoreCase | RegexOptions.Multiline);
        }

        /// <summary>
        /// Matches the regex pattern to extract the value of the <c>path d</c> element.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Match group value of <c>d</c> tag.</returns>
        public string GetSvgPath(string input)
        {
            var result = pattern.Match(input);
            return result.Groups[1].Value;
        }

        /// <summary>
        /// Matches the regex to extract the <c>path</c> element.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Match value element <c>path</c></returns>
        public string GetPathElementAsString(string input)
        {
            var result = pattern.Match(input);
            return result.Value;
        }
    }
}
