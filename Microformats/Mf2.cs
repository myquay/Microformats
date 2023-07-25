using HtmlAgilityPack;
using Microformats.Definitions;
using Microformats.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Microformats
{
    public class Mf2
    {

        /// <summary>
        /// Create a new parser
        /// </summary>
        public Mf2()
        {
            Load(Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// Supported vocabularies
        /// </summary>
        private List<IVocabulary> Vocabularies { get; set; } = new List<IVocabulary>();

        /// <summary>
        /// Add a new vocabulary
        /// </summary>
        /// <param name="vocabulary"></param>
        /// <exception cref="ArgumentException"></exception>
        public void AddVocabulary(IVocabulary vocabulary)
        {
            if (Vocabularies.Any(v => v.Name == vocabulary.Name))
                throw new ArgumentException($"The vocabulary '{vocabulary.Name}' has already been added");
            Vocabularies.Add(vocabulary);
        }

        /// <summary>
        /// Load all vocabularies from assembly
        /// </summary>
        /// <param name="assembly"></param>
        public void Load(Assembly assembly)
        {
            foreach (var vocabulary in assembly.GetTypes().Where(t => typeof(IVocabulary).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract))
            {
                var newVocab = (IVocabulary)Activator.CreateInstance(vocabulary);
                if (!Vocabularies.Any(v => v.Name == vocabulary.Name))
                    Vocabularies.Add(newVocab);
            }
        }

        /// <summary>
        /// Parse a document according to the Microformats2 specification.
        /// </summary>
        /// <remarks>
        /// <see href="https://microformats.org/wiki/microformats2-parsing#algorithm">https://microformats.org/wiki/microformats2-parsing#algorithm</see>
        /// </remarks>
        /// <param name="html"></param>
        /// <returns></returns>
        public MResult Parse(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var result = new MResult()
            {
                Items = doc.DocumentNode.ChildNodes.SelectMany(m => ParseElementForMicroformat(m)).Where(m => m != null).ToArray()
            };

            return result;
        }

        /// <summary>
        /// Parse element class for root class name(s) "h-*" and if none, backcompat root classes
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        private MValue[] ParseElementForMicroformat(HtmlNode node)
        {
            if (Vocabularies.Any(v => node.GetClasses().Contains(v.Name)))
            {

                var vocab = Vocabularies.Where(v => node.GetClasses().Contains(v.Name));
                if(vocab.Any(c => c.Version == MicroformatsVersion.Two))
                    vocab = vocab.Where(c => c.Version == MicroformatsVersion.Two);

                var resultSet = new MValue
                {
                    Type = MType.Root,
                    Value = vocab.Select(c => c.Name).ToArray(),
                    Id = node.Id
                };

                 var properties = vocab.SelectMany(c => c.Properties).GroupBy(p => p.Name)
                  .Select(g => g.First())
                  .ToList();

                return new[] { resultSet };
            }
            else if (node.HasChildNodes)
            {
                return node.ChildNodes.SelectMany(n => ParseElementForMicroformat(n)).ToArray();
            }
            else
            {
                return Array.Empty<MValue>();
            }

        }
    }
}
